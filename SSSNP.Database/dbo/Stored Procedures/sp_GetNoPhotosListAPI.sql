

CREATE procedure [dbo].[sp_GetNoPhotosListAPI]
@Stat varchar(8)
as
begin
if(@Stat='1')
Begin

select   b.intBenid,s.StateName,c.CountieName,p.PayamName,bom.BomaName, ip.IPName,  
b.EnrollmentNo,b.NameofHousehold,  b.NameofSpouse,isnull(b.household_number,'Before Biometric') as household_number,

b.PhotoName,
isnull(b.alternate_number,'No Data') as FirstAlternateNumber,isnull(b.Name1stAlternatePayee,'No Data') as FirstAlternateName,
b.FirstAlternatePayeePhotoName,
isnull(b.Secondalternate_number,'No Data') as SecondAlternateNumber,isnull(b.Name2ndAlternatePayee,'No Data') as SecondAlternateName,
b.SecondAlternatepayeePhotoName
from dbo.tbl_BeneficiaryDet as b 
inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
inner join tm_Payams as p on p.intPayamid=bom.intPayamid
inner join tm_Counties as c on c.intCountieid=p.intCountieid
inner join tm_States as s on s.intStateid=c.intStateid
inner join td_IPDetails as ip on ip.intIP=b.intIP
where b.household_number is not null and b.PhotoPath is null

End
if(@Stat='2')
Begin
select  b.intBenid,s.StateName,c.CountieName,p.PayamName,bom.BomaName, ip.IPName,  
b.EnrollmentNo,b.NameofHousehold,  b.NameofSpouse,isnull(b.household_number,'Before Biometric') as household_number,

b.PhotoName,
isnull(b.alternate_number,'No Data') as FirstAlternateNumber,isnull(b.Name1stAlternatePayee,'No Data') as FirstAlternateName,
b.FirstAlternatePayeePhotoName,
isnull(b.Secondalternate_number,'No Data') as SecondAlternateNumber,isnull(b.Name2ndAlternatePayee,'No Data') as SecondAlternateName,
b.SecondAlternatepayeePhotoName
from dbo.tbl_BeneficiaryDet as b 
inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
inner join tm_Payams as p on p.intPayamid=bom.intPayamid
inner join tm_Counties as c on c.intCountieid=p.intCountieid
inner join tm_States as s on s.intStateid=c.intStateid
inner join td_IPDetails as ip on ip.intIP=b.intIP
where b.alternate_number is not null and b.FirstAlternatePayeePhotoPath is null

ENd
if(@Stat='3')
Begin
select  b.intBenid,s.StateName,c.CountieName,p.PayamName,bom.BomaName, ip.IPName,  
b.EnrollmentNo,b.NameofHousehold,  b.NameofSpouse,isnull(b.household_number,'Before Biometric') as household_number,

b.PhotoName,
isnull(b.alternate_number,'No Data') as FirstAlternateNumber,isnull(b.Name1stAlternatePayee,'No Data') as FirstAlternateName,
b.FirstAlternatePayeePhotoName,
isnull(b.Secondalternate_number,'No Data') as SecondAlternateNumber,isnull(b.Name2ndAlternatePayee,'No Data') as SecondAlternateName,

b.SecondAlternatepayeePhotoName
from dbo.tbl_BeneficiaryDet as b 
inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
inner join tm_Payams as p on p.intPayamid=bom.intPayamid
inner join tm_Counties as c on c.intCountieid=p.intCountieid
inner join tm_States as s on s.intStateid=c.intStateid
inner join td_IPDetails as ip on ip.intIP=b.intIP
where b.Secondalternate_number is not null and b.SecondAlternatePayeePhotoPath is null

End
end
