
CREATE procedure [dbo].[sp_GetDeletedAllocatedBenfAPI]

as
begin
select distinct a.IPName,a.StateName,a.CountieName,a.PayamName,a.BomaName,a.intBenid,a.EnrollmentNo,a.NameofHousehold,a.household_number,a.WorkCode,  a.intWorkid,
CONVERT(char(10), a.Deleted_dt,126) as Deleted_dt,
a.DeletedRemarks from
(
select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tr_BenfWorkDetTrans.intBenid,tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.household_number,tbl_WorkMaster.WorkCode,  tr_BenfWorkDetTrans.intWorkid,
tr_BenfWorkDetTrans.Deleted_dt,tr_BenfWorkDetTrans.DeletedRemarks

  from tr_BenfWorkDetTrans 
  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenfWorkDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDetTrans.intWorkid

  where   tr_BenfWorkDetTrans.DeletedRemarks is not null  

 union
 select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tr_BenfWorkDetTrans.intBenid,tbl_BeneficiaryDetTrans.EnrollmentNo,tbl_BeneficiaryDetTrans.NameofHousehold,tbl_BeneficiaryDetTrans.household_number,tbl_WorkMaster.WorkCode,  tr_BenfWorkDetTrans.intWorkid,


CONVERT(char(10), tr_BenfWorkDetTrans.Deleted_dt,126) as Deleted_dt,

tr_BenfWorkDetTrans.DeletedRemarks

  from tr_BenfWorkDetTrans 
  inner join tbl_BeneficiaryDetTrans on tbl_BeneficiaryDetTrans.intBenid=tr_BenfWorkDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDetTrans.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDetTrans.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDetTrans.intWorkid

  where   tr_BenfWorkDetTrans.DeletedRemarks is not null  

 ) as a

  end
