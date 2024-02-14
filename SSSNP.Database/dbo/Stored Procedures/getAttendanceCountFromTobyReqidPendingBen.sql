CREATE procedure [dbo].[getAttendanceCountFromTobyReqidPendingBen]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(10),
@group_type varchar(100),
@group_code varchar(100) ,
@intWagePaymentReqid varchar(8)  
)  
as  
begin  
  
select distinct  td_WageReqBenDet.intWagesReqBenTransid,  td_WageReqBenDet.intWagePaymentReqid, tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,  
case when td_WageReqBenDet.traineealternate is not null then case when tbl_BeneficiaryDet.Name1stAlternatePayee is null then tbl_BeneficiaryDet.NameofHousehold else  tbl_BeneficiaryDet.Name1stAlternatePayee end else     tbl_BeneficiaryDet.NameofHousehold end as NameofHousehold,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.ContactNo,tbl_BeneficiaryDet.Email,tbl_BeneficiaryDet.FamilyIncome,tm_Bomas.BomaName,  
(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where (AttendanceDate between @FromDate and @ToDate) and intWorkActivityId 

like @intWorkActivityId   
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'
and tr_BenAttendanceDet.PaymentFlag is null
and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
and  (AttendanceDate between @FromDate and @ToDate)) as cnt,

(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where (AttendanceDate between @FromDate and @ToDate ) and intWorkActivityId
 
like @intWorkActivityId   
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'
and tr_BenAttendanceDet.PaymentFlag is null
and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
and  (AttendanceDate between @FromDate and @ToDate ))*70 as Amt


from dbo.tbl_BeneficiaryDet  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid  left outer join    
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid inner join td_WageReqBenDet on td_WageReqBenDet.intBenid=tr_BenfWorkDet.intBenid
and td_WageReqBenDet.intWorkid=tr_BenfWorkDet.intWorkid where tr_BenfWorkDet.intWorkid like @intWorkActivityId  



and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b where (AttendanceDate between @FromDate and @ToDate ))
and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
and tbl_BeneficiaryDet.intBenid in (select ab.intBenid from td_WageReqBenDet as ab where ab.intWagePaymentReqid like @intWagePaymentReqid)
and td_WageReqBenDet.intWagePaymentReqid like @intWagePaymentReqid
and tbl_BeneficiaryDet.intBenid not in (select ab.intBenid from td_WageReqBenPADet as ab where ab.intWagePaymentReqid like @intWagePaymentReqid) 
order by tbl_BeneficiaryDet.EnrollmentNo asc
end


