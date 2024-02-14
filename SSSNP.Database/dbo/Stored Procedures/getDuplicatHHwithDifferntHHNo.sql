
CREATE procedure [dbo].[getDuplicatHHwithDifferntHHNo]

as
begin

select ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid,b.intBenid,b.EnrollmentNo,  b.household_number,b.NameofHousehold,b.NameofSpouse,
b.PhotoName,b.alternate_number,b.Name1stAlternatePayee,b.FirstAlternatePayeePhotoName,
b.Secondalternate_number,b.Name2ndAlternatePayee,b.SecondAlternatepayeePhotoName
 from tbl_BeneficiaryDet  as b
 inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
 inner join td_IPDetails as ip on ip.intIP=b.intIP
 inner join (select ss.intBomasid,ss.NameofHousehold from tbl_BeneficiaryDet as ss group by ss.intBomasid,ss.NameofHousehold having count(ss.household_number)>1) as dd
 on dd.intBomasid=b.intBomasid and dd.NameofHousehold=b.NameofHousehold 

 

End
