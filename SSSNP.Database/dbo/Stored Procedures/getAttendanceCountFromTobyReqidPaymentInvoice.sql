CREATE procedure [dbo].[getAttendanceCountFromTobyReqidPaymentInvoice]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(10),
@group_type varchar(100),
@group_code varchar(100) ,
@intWagePaymentReqid varchar(8) ,
@intWagePaymentReqPAid varchar(8) 
)  
as  
begin  
  select * from Vw_WageInvoice
--inner join tr_BenWagesPayment on tr_BenWagesPayment.trans_id=td_WageReqBenDet.intWagesReqBenTransid








 where intWorkid like @intWorkActivityId  
 and intWagePaymentReqPAid like @intWagePaymentReqPAid


--and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b where (AttendanceDate between @FromDate and @ToDate ))
--and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
--and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
--and tbl_BeneficiaryDet.intBenid in (select ab.intBenid from td_WageReqBenDet as ab where ab.intWagePaymentReqid like @intWagePaymentReqid)
--and tbl_BeneficiaryDet.intBenid in (select ab.intBenid from td_WageReqBenPADet as ab where ab.intWagePaymentReqPAid like intWagePaymentReqPAid)
--and td_WorkWageRequestPA.intWagePaymentReqPAid like @intWagePaymentReqPAid
--and td_WageReqBenPADet.intWagePaymentReqPAid like @intWagePaymentReqPAid
order by EnrollmentNo asc
end


