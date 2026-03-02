CREATE Procedure [dbo].[getWagesReport]  --getWagesReport '1092'
(  

@intWorkActivityId varchar(10)   
)  
as 
----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-- Project : SNSDP

-- Purpose: Show USD Amount also

-- Working Purpose : Show USD Amount also

-- Developer : Srikanth

-- Date: 04-08-2017

-- Modifeid Tables Used: tr_BenWagesPayment
-----------------------------------------------------------------------------
----------------------------------------------------------------------------- 
----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-- Project : SNSDP

-- Purpose: Report of Wages

-- Working Purpose : Show Order and Exchange amount

-- Developer : Srikanth

-- Date: 04-10-2017

-- Modifeid Tables Used: tr_BenWagesPayment
-----------------------------------------------------------------------------
----------------------------------------------------------------------------- 
begin  
  if(@intWorkActivityId='1092')
  Begin
select distinct tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,  
tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.ContactNo,tbl_BeneficiaryDet.Email,tbl_BeneficiaryDet.FamilyIncome,tm_Bomas.BomaName,  


case when w.AttendanceDays is not null then w.AttendanceDays else 

(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where (AttendanceDate between w.FromDate and w.ToDate) and intWorkActivityId 
like @intWorkActivityId   
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'

and  (AttendanceDate between w.FromDate and w.ToDate))
end as cnt,

w.PaidAmount as Amt,
convert(varchar(11),w.PaidDate,106) AS PaidDate,
convert(varchar(11),w.FromDate,106) AS FromDate,
convert(varchar(11),w.ToDate,106) AS ToDate,
w.Wagerate ,
w.PaidAmountUSD,
w.WageRateUSD,
case when w.ExchangeRate is null then convert(decimal(18,2),Wagerate/WageRateUSD)  else w.ExchangeRate end ExchangeRate
,w.intWageid

 from dbo.tbl_BeneficiaryDet  left outer join    
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid

inner join tr_BenWagesPayment as w on w.intBenid=tbl_BeneficiaryDet.intBenid where w.intWorkid like @intWorkActivityId  

and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b)
End
else
Begin
select distinct tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,  
tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.ContactNo,tbl_BeneficiaryDet.Email,tbl_BeneficiaryDet.FamilyIncome,tm_Bomas.BomaName,  


case when w.AttendanceDays is not null then w.AttendanceDays else 

(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where (AttendanceDate between w.FromDate and w.ToDate) and intWorkActivityId 
like @intWorkActivityId   
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'

and  (AttendanceDate between w.FromDate and w.ToDate))
end as cnt,

w.PaidAmount as Amt,
convert(varchar(11),w.PaidDate,106) AS PaidDate,
convert(varchar(11),w.FromDate,106) AS FromDate,
convert(varchar(11),w.ToDate,106) AS ToDate,
w.Wagerate ,
w.PaidAmountUSD,
w.WageRateUSD,
case when w.ExchangeRate is null then convert(decimal(18,2),Wagerate/WageRateUSD)  else w.ExchangeRate end ExchangeRate


 from dbo.tbl_BeneficiaryDet  left outer join    
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid

inner join tr_BenWagesPayment as w on w.intBenid=tbl_BeneficiaryDet.intBenid where w.intWorkid like @intWorkActivityId  

and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b)
End
end


