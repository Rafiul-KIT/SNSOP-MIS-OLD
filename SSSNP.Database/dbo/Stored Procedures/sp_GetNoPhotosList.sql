
create procedure dbo.sp_GetNoPhotosList
@intIP varchar(10),
@intStateid varchar(10),
@intCountieid varchar(10),
@intPayamid varchar(10),
@intBomasid varchar(10),
@Household varchar(80),
@Benname varchar(100),
@Enrollmentno varchar(30)
as
begin

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
and b.intBomasid like @intBomasid and b.intPayamid like @intPayamid and b.intCountieid like @intCountieid
and b.intStateid like @intStateid and b.intIP like @intIP and b.EnrollmentNo like '%'+@Enrollmentno+'%' and b.NameofHousehold like '%'+@Benname+'%' 
and b.household_number like '%'+@Household+'%'


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
and b.intBomasid like @intBomasid and b.intPayamid like @intPayamid and b.intCountieid like @intCountieid
and b.intStateid like @intStateid and b.intIP like @intIP and b.EnrollmentNo like '%'+@Enrollmentno+'%' and b.NameofHousehold like '%'+@Benname+'%' 
and b.household_number like '%'+@Household+'%'


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
and b.intBomasid like @intBomasid and b.intPayamid like @intPayamid and b.intCountieid like @intCountieid
and b.intStateid like @intStateid and b.intIP like @intIP and b.EnrollmentNo like '%'+@Enrollmentno+'%' and b.NameofHousehold like '%'+@Benname+'%' 
and b.household_number like '%'+@Household+'%'

end