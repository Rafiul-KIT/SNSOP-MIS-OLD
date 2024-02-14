
CREATE procedure [dbo].[getBeneficiaryLookupsearchAllFullDetails1]    --getBeneficiaryLookupsearchAllFullDetails '%','%','%','%','%','%','1007','','',''     
(        
@intStateid varchar(10),        
@intCountieid varchar(10),        
@intPayamid varchar(10),        
@intBomasid varchar(10),      
@NameofHousehold varchar(50),    
@EnrollmentNo varchar(50) ,
@intIP    varchar(50),
@group_type varchar(100),
@group_code varchar(100),
@Householdno varchar(500)
)        
as begin        
        
        
    
        
--select distinct
--ips.IPName,
-- b.EnrollmentNo,
-- b.GroupId as GroupCode,
-- b.BeneficiaryType,
-- b.NameofHousehold as [Household Name],
-- b.FamilySize,
-- case when b.Sex='M' then 'Male' else 'Female' end as Gender,
-- b.Age,
-- b.NameofSpouse as [Name of Spouse],
-- b.Name1stAlternatePayee as [Name of First Alternate Payee],
-- b.Name2ndAlternatePayee as [Name of Second Alternate Payee] ,
-- b.NumberMaleAdults,
-- b.NumberFemaleAdults,
-- b.NumberAdultsAgeAbove65,
-- b.NumberAdultsAgeBetween45_64,
-- b.NumberAdultsAgeBetween18_30,
-- b.NumberAdultsAgeBetween30_45,
-- b.NoofDependents,
-- b.NumberFemaleChildren,
-- b.NumberMaleChildren,
-- b.NumberChildrenAgeAbove5years,
-- b.NumberChildrenAgeAbove12years,
-- b.NumberChildrenAgeAbove18years,
-- b.FamilyIncome,
-- b.SOurceofIncome,
-- b.NationalID,
 
-- s.StateName,

-- c.CountieName,

-- p.PayamName,

-- bom.BomaName,
-- b.EthnicityTribeRgin,
-- b.Address,
-- b.ContactNo,
-- b.Email,
-- b.SecondLineHome,
-- b.LegalStatus,
-- b.BiometricNo,
-- b.NameofBank,
-- b.AccountNo,
-- [FirstAlternatePayeeGender],
--	[FirstAlternatePayeeAge],
--	[FirstAlternatePayeeMobile],
	
--	[SecondAlternatePayeeGender],
--	[SecondAlternatePayeeAge],
--	[SecondAlternatePayeeMobile],
--	 FirstAlternatePayeeNationID,
-- SecondAlternateNationalID,
-- NoofAdult as [No.of other adult household members],

-- Convert(varchar(11),b.Created_dt,106) as DateofCreation

 
--  from tbl_BeneficiaryDet as b inner join td_IPDetails as ips on ips.intIP=b.intIP
--  inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
--  inner join tm_Payams as p on p.intPayamid=bom.intPayamid
--  inner join tm_Counties as c on c.intCountieid=p.intCountieid
--  inner join tm_States as s on s.intStateid=c.intStateid






        
select distinct
ips.IPName,
 b.EnrollmentNo,
 b.GroupId as GroupCode,
 b.BeneficiaryType,
 b.NameofHousehold as [Household Name],
 b.FamilySize,
 case when b.Sex='M' then 'Male' else 'Female' end as Gender,
 b.Age,
 b.NameofSpouse as [Name of Spouse],
 b.Name1stAlternatePayee as [Name of First Alternate Payee],
 b.Name2ndAlternatePayee as [Name of Second Alternate Payee] ,
 --b.NumberMaleAdults,
 --b.NumberFemaleAdults,
 --b.NumberAdultsAgeAbove65,
 --b.NumberAdultsAgeBetween45_64,
 --b.NumberAdultsAgeBetween18_30,
 --b.NumberAdultsAgeBetween30_45,
 --b.NoofDependents,
 --b.NumberFemaleChildren,
 --b.NumberMaleChildren,
 --b.NumberChildrenAgeAbove5years,
 --b.NumberChildrenAgeAbove12years,
 --b.NumberChildrenAgeAbove18years,
 b.FamilyIncome,
 b.SOurceofIncome,
 b.NationalID,
 
 s.StateName,

 c.CountieName,

 p.PayamName,

 bom.BomaName,
 --b.EthnicityTribeRgin,
 b.Address,
 b.ContactNo,
 b.Email,
 --b.SecondLineHome,
 b.LegalStatus,
 --b.BiometricNo,
 b.NameofBank,
 b.AccountNo,b.household_number,
 --[FirstAlternatePayeeGender],
	--[FirstAlternatePayeeAge],
	--[FirstAlternatePayeeMobile],
	
	--[SecondAlternatePayeeGender],
	--[SecondAlternatePayeeAge],
	--[SecondAlternatePayeeMobile],
	-- FirstAlternatePayeeNationID,
 --SecondAlternateNationalID,
 --NoofAdult as [No.of other adult household members],

 Convert(varchar(11),b.Created_dt,106) as DateofCreation
 ,b.SelectionCriteria
 ,b.SelectionReason
 
  from tbl_BeneficiaryDet as b inner join td_IPDetails as ips on ips.intIP=b.intIP
  inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
  inner join tm_Payams as p on p.intPayamid=bom.intPayamid
  inner join tm_Counties as c on c.intCountieid=p.intCountieid
  inner join tm_States as s on s.intStateid=c.intStateid
  where
                    b.intIP like @intIP and 
                       b.intStateid like @intStateid and b.intCountieid like @intCountieid and b.intPayamid like @intPayamid        
                      and b.intBomasid  like @intBomasid   and b.NameofHousehold like '%'+@NameofHousehold+'%'  and  b.EnrollmentNo like '%'+@EnrollmentNo+'%' 
				  --and b.BeneficiaryType like (case when @group_type ='' or @group_type is null then '%' else @group_type end)
				  --and isnull(b.GroupId,'') like (case when @group_code is null or @group_code ='' then '%'  else @group_code end)
					and isnull(b.household_number,'') like '%'+@Householdno+'%'  

end
