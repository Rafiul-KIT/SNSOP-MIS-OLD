
CREATE procedure [dbo].[sp_GetDeletedAttenBenAPI]

as
begin
select a.* from 
(select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tr_BenAttendanceDetTrans.intBenid,tbl_BeneficiaryDetTrans.EnrollmentNo,tbl_BeneficiaryDetTrans.NameofHousehold,tbl_BeneficiaryDetTrans.household_number,tbl_WorkMaster.WorkCode,  tr_BenAttendanceDetTrans.intWorkActivityId,
CONVERT(char(10), tr_BenAttendanceDetTrans.AttendanceDate,126) as AttendanceDate,
CONVERT(char(10), tr_BenAttendanceDetTrans.DeletedDt,126) as DeletedDt,


tr_BenAttendanceDetTrans.DeletedRemarks

  from tr_BenAttendanceDetTrans 
  inner join tbl_BeneficiaryDetTrans on tbl_BeneficiaryDetTrans.intBenid=tr_BenAttendanceDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDetTrans.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDetTrans.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenAttendanceDetTrans.intWorkActivityId
  where   tr_BenAttendanceDetTrans.DeletedRemarks is not null  


  union

  select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.household_number, tbl_WorkMaster.WorkCode, tr_BenAttendanceDetTrans.intWorkActivityId
  ,
CONVERT(char(10), tr_BenAttendanceDetTrans.AttendanceDate,126) as AttendanceDate,
CONVERT(char(10), tr_BenAttendanceDetTrans.DeletedDt,126) as DeletedDt,
tr_BenAttendanceDetTrans.DeletedRemarks

  from tr_BenAttendanceDetTrans 
  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenAttendanceDetTrans.intWorkActivityId
  where   tr_BenAttendanceDetTrans.DeletedRemarks is not null  

  ) as a 
    order by a.DeletedDt desc
  end
