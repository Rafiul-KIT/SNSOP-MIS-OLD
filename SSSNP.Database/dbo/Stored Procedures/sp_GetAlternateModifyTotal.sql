
CREATE procedure [dbo].[sp_GetAlternateModifyTotal]
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
select  ip.IPName,s.StateName,c.CountieName,paym.PayamName,bom.BomaName,b.EnrollmentNo,b.NameofHousehold,b.ContactNo,b.household_number,
 'First Alternate' as AlternateType,b.alternate_number,b.Name1stAlternatePayee as NameAlternatePayee,case when b.FirstAlternatePayeeGender =  'M' then 'Male' else 'Female' end as AlternatePayeeGender,
b.FirstAlternatePayeeAge as AlternatePayeeAge,b.FirstAlternatePayeeNationID as AlternatePayeeNationID,b.FirstAlternatePayeeMobile as AlternatePayeeMobile,Convert(varchar(11), b.FirstAlternateUpdDate,106) as ModifiedDate

from dbo.tbl_BeneficiaryDet AS b INNER JOIN
                         dbo.tm_Bomas AS bom ON bom.intBomasid = b.intBomasid INNER JOIN
                         dbo.tm_Payams AS paym ON paym.intPayamid = bom.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = paym.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = b.intIP
						
						 where (FirstAlternateUpdby is not null
						) and b.intBomasid like @intBomasid and b.intPayamid like @intPayamid and b.intCountieid like @intCountieid
and b.intStateid like @intStateid and b.intIP like @intIP and b.EnrollmentNo like '%'+@Enrollmentno+'%' and b.NameofHousehold like '%'+@Benname+'%' 
and b.household_number like '%'+@Household+'%'
union
select  ip.IPName,s.StateName,c.CountieName,paym.PayamName,bom.BomaName,b.EnrollmentNo,b.NameofHousehold,b.ContactNo,b.household_number,
 'Second Alternate' as AlternateType,b.Secondalternate_number as alternate_number,b.Name2ndAlternatePayee as NameAlternatePayee,case when b.SecondAlternatePayeeGender =  'M' then 'Male' else 'Female' end as AlternatePayeeGender,
b.SecondAlternatePayeeAge as AlternatePayeeAge,b.SecondAlternateNationalID as AlternatePayeeNationID,b.SecondAlternatePayeeMobile as AlternatePayeeMobile,Convert(varchar(11), b.SecondAlternateUpdDate,106) as ModifiedDate

from dbo.tbl_BeneficiaryDet AS b INNER JOIN
                         dbo.tm_Bomas AS bom ON bom.intBomasid = b.intBomasid INNER JOIN
                         dbo.tm_Payams AS paym ON paym.intPayamid = bom.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = paym.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = b.intIP
						
						 where (SecondAlternateUpdby is not null
						) and b.intBomasid like @intBomasid and b.intPayamid like @intPayamid and b.intCountieid like @intCountieid
and b.intStateid like @intStateid and b.intIP like @intIP and b.EnrollmentNo like '%'+@Enrollmentno+'%' and b.NameofHousehold like '%'+@Benname+'%' 
and b.household_number like '%'+@Household+'%'
union
select  ip.IPName,s.StateName,c.CountieName,paym.PayamName,bom.BomaName,b.EnrollmentNo,b.NameofHousehold,b.ContactNo,b.household_number,
 'Third Alternate' as AlternateType,b.thirdalternate_number as alternate_number,b.Name3rdAlternatePayee as NameAlternatePayee,case when b.ThirdAlternatePayeeGender =  'M' then 'Male' else 'Female' end as AlternatePayeeGender,
b.ThirdAlternatePayeeAge as AlternatePayeeAge,b.ThirdAlternatePayeeNationID as AlternatePayeeNationID,b.ThirdAlternatePayeeMobile as AlternatePayeeMobile,Convert(varchar(11), b.ThirdAlternateUpdDate,106) as ModifiedDate

from dbo.tbl_BeneficiaryDet AS b INNER JOIN
                         dbo.tm_Bomas AS bom ON bom.intBomasid = b.intBomasid INNER JOIN
                         dbo.tm_Payams AS paym ON paym.intPayamid = bom.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = paym.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = b.intIP
						
						 where (ThirdAlternateUpdby is not null
						) and b.intBomasid like @intBomasid and b.intPayamid like @intPayamid and b.intCountieid like @intCountieid
and b.intStateid like @intStateid and b.intIP like @intIP and b.EnrollmentNo like '%'+@Enrollmentno+'%' and b.NameofHousehold like '%'+@Benname+'%' 
and b.household_number like '%'+@Household+'%'

end
