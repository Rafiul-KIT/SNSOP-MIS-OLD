
CREATE procedure [dbo].[sp_GetBenfDettransAPI]

as
begin
select  ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid,  bd.intBenid,bd.EnrollmentNo,
bd.NameofHousehold,bd.NameofSpouse,

CONVERT(char(10), bd.DeletedDt,126) as DeletedDt,


bd.household_number,bd.DeletedRemarks
 
 
  from tbl_BeneficiaryDetTrans as bd 
 inner join tm_Bomas as bom on bom.intBomasid=bd.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
 inner join td_IPDetails as ip on ip.intIP=bd.intIP

  where bd.DeletedRemarks is not null

 
  end
