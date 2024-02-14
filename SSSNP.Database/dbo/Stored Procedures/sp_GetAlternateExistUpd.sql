
CREATE procedure [dbo].[sp_GetAlternateExistUpd]
@intIP varchar(10),
@intStateid varchar(10),
@intCountieid varchar(10),
@intPayamid varchar(10),
@intBomasid varchar(10),
@Household varchar(80),
@Benname varchar(100),
@Enrollmentno varchar(30)
--@Workcode varchar(100)
as
begin
select  ip.IPName,s.StateName,c.CountieName,paym.PayamName,bom.BomaName,b.EnrollmentNo,b.NameofHousehold,b.ContactNo,b.household_number,
b1.AlternateType,b1.alternate_number,b1.NameAlternatePayee,case when b1.AlternatePayeeGender='M' then 'Male' else 'Female' end as AlternatePayeeGender,
b1.AlternatePayeeAge,b1.AlternatePayeeNationID,b1.AlternatePayeeMobile,Convert(varchar(11), b1.Created_dt,106) as ModifiedDate

from dbo.tbl_BeneficiaryDet AS b INNER JOIN
                         dbo.tm_Bomas AS bom ON bom.intBomasid = b.intBomasid INNER JOIN
                         dbo.tm_Payams AS paym ON paym.intPayamid = bom.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = paym.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = b.intIP
						 inner join tbl_BenAlternateTransDet as b1 on b1.intBenid=b.intBenid
						 where  b.intBomasid like @intBomasid and b.intPayamid like @intPayamid and b.intCountieid like @intCountieid
and b.intStateid like @intStateid and b.intIP like @intIP and b.EnrollmentNo like '%'+@Enrollmentno+'%' and b.NameofHousehold like '%'+@Benname+'%' 
and b.household_number like '%'+@Household+'%'
end
