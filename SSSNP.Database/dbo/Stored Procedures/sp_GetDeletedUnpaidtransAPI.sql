
CREATE procedure [dbo].[sp_GetDeletedUnpaidtransAPI]

as
begin
select a.* from 
(select  td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName, td_WageReqBenTransDet.intWagesReqBenTransid,td_WageReqBenTransDet.intBenid,tbl_BeneficiaryDetTrans.EnrollmentNo,tbl_BeneficiaryDetTrans.NameofHousehold,tbl_BeneficiaryDetTrans.household_number,  td_WorkWageRequest.intWorkid,  td_WorkWageRequest.ReqNo,
CONVERT(char(10), td_WorkWageRequest.ReqFromDate,126) as ReqFromDate
,
CONVERT(char(10), td_WorkWageRequest.ReqToDate,126) as ReqToDate
,
td_WageReqBenTransDet.TotAttendance,td_WageReqBenTransDet.WageRateUSDTotal,
CONVERT(char(10), td_WageReqBenTransDet.Deleted_dt,126) as Deleted_dt

,

td_WageReqBenTransDet.DeletedRemarks

  from td_WageReqBenTransDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenTransDet.intWagePaymentReqid
  inner join tbl_BeneficiaryDetTrans on tbl_BeneficiaryDetTrans.intBenid=td_WageReqBenTransDet.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDetTrans.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDetTrans.intIP
  where   td_WageReqBenTransDet.DeletedRemarks is not null  

  union
  select  td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName, td_WageReqBenTransDet.intWagesReqBenTransid,td_WageReqBenTransDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.household_number,  td_WorkWageRequest.intWorkid,  td_WorkWageRequest.ReqNo,
  
CONVERT(char(10), td_WorkWageRequest.ReqFromDate,126) as ReqFromDate
,
CONVERT(char(10), td_WorkWageRequest.ReqToDate,126) as ReqToDate
,
  
  td_WageReqBenTransDet.TotAttendance,td_WageReqBenTransDet.WageRateUSDTotal,
CONVERT(char(10), td_WageReqBenTransDet.Deleted_dt,126) as Deleted_dt
,
  
  td_WageReqBenTransDet.DeletedRemarks
  from td_WageReqBenTransDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenTransDet.intWagePaymentReqid
  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenTransDet.intBenid
   inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP

  where   td_WageReqBenTransDet.DeletedRemarks is not null 

  ) as a 
  order by a.Deleted_dt desc
  end
