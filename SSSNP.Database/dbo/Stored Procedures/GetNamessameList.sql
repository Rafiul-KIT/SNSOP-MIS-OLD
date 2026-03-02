
CREATE procedure [dbo].[GetNamessameList]
@Stat varchar(8)
as
begin
if(@Stat='1')
Begin
-- Household and Alternate one and Second Alternate two  are same - 12

select ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid,b.intBenid,b.EnrollmentNo,  b.household_number,b.NameofHousehold,b.NameofSpouse,
b.PhotoName,b.alternate_number,b.Name1stAlternatePayee,b.FirstAlternatePayeePhotoName,
b.Secondalternate_number,b.Name2ndAlternatePayee,b.SecondAlternatepayeePhotoName
 from tbl_BeneficiaryDet  as b
 inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
 inner join td_IPDetails as ip on ip.intIP=b.intIP
 where b.Name1stAlternatePayee= b.Name2ndAlternatePayee and  b.Name1stAlternatePayee= b.NameofHousehold and 
  alternate_number is not null and Secondalternate_number is not null and household_number is not null
End
if(@Stat='2')
Begin
  -- Household and Alternate one are same - 442
  select ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid,b.intBenid,b.EnrollmentNo,  b.household_number,b.NameofHousehold,b.NameofSpouse,
b.PhotoName,b.alternate_number,b.Name1stAlternatePayee,b.FirstAlternatePayeePhotoName,
b.Secondalternate_number,b.Name2ndAlternatePayee,b.SecondAlternatepayeePhotoName
 from tbl_BeneficiaryDet  as b
 inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
 inner join td_IPDetails as ip on ip.intIP=b.intIP
 where b.Name1stAlternatePayee= b.NameofHousehold and 
  alternate_number is not null and household_number is not null
  End
if(@Stat='3')
Begin
   -- Household and Alternate two are same - 63
  select ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid,b.intBenid,b.EnrollmentNo,  b.household_number,b.NameofHousehold,b.NameofSpouse,
b.PhotoName,b.alternate_number,b.Name1stAlternatePayee,b.FirstAlternatePayeePhotoName,
b.Secondalternate_number,b.Name2ndAlternatePayee,b.SecondAlternatepayeePhotoName
 from tbl_BeneficiaryDet  as b
 inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
 inner join td_IPDetails as ip on ip.intIP=b.intIP
 where b.Name2ndAlternatePayee= b.NameofHousehold and 
  Secondalternate_number is not null and household_number is not null
  End
if(@Stat='4')
Begin
  -- Alternate one and Alternate two are same - 224
   select ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid,b.intBenid,b.EnrollmentNo,  b.household_number,b.NameofHousehold,b.NameofSpouse,
b.PhotoName,b.alternate_number,b.Name1stAlternatePayee,b.FirstAlternatePayeePhotoName,
b.Secondalternate_number,b.Name2ndAlternatePayee,b.SecondAlternatepayeePhotoName
 from tbl_BeneficiaryDet  as b
 inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
 inner join td_IPDetails as ip on ip.intIP=b.intIP
 where b.Name2ndAlternatePayee= b.Name1stAlternatePayee and 
  Secondalternate_number is not null and alternate_number is not null
  End

  End
