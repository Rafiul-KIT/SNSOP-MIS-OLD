
CREATE procedure [dbo].[GetMainBenfDetforDeletion]
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
b.EnrollmentNo,b.NameofHousehold,  b.NameofSpouse,b.household_number

from dbo.tbl_BeneficiaryDet as b 
inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
inner join tm_Payams as p on p.intPayamid=bom.intPayamid
inner join tm_Counties as c on c.intCountieid=p.intCountieid
inner join tm_States as s on s.intStateid=c.intStateid
inner join td_IPDetails as ip on ip.intIP=b.intIP
 where
 
 intBenid like '%'   
 and b.intIP like @intIP
 and b.intBomasid like @intBomasid
 and p.intPayamid like @intPayamid
 and c.intCountieid like @intCountieid
 and s.intStateid like @intStateid
 and b.EnrollmentNo like @Enrollmentno
 and b.NameofHousehold like '%' + @Benname +'%'
 and  b.household_number like @Household
 order by s.StateName,c.CountieName,p.PayamName,bom.BomaName, ip.IPName,b.NameofHousehold
 end



