CREATE procedure [dbo].[sp_GetBenfsearchdetForAllDetails]
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
isnull(b.alternate_number,'No Data') as FirstAlternateNumber,isnull(b.Name1stAlternatePayee,'No Data') as FirstAlternateName,
isnull(b.Secondalternate_number,'No Data') as SecondAlternateNumber,isnull(b.Name2ndAlternatePayee,'No Data') as SecondAlternateName,
isnull(b.thirdalternate_number,'No Data') as thirdalternate_number,isnull(b.Name3rdAlternatePayee,'No Data') as ThirdAlternateName,

isnull(b.Age,'0') as Age,


isnull(case when b.Sex='F' then 'Female' else case when b.Sex='M' then 'Male' else null end end,'No Data') as Householdgender,

Convert(varchar(11),b.Created_dt,106) as Createddate,

b.Address,
isnull(case when b.FirstAlternatePayeeGender='F' then 'Female' else case when b.FirstAlternatePayeeGender='M' then 'Male' else null end end,'No Data') as FirstAlternateGender,
isnull(case when b.SecondAlternatePayeeGender='F' then 'Female' else case when b.SecondAlternatePayeeGender='M' then 'Male' else null end end,'No Data') as SecondAlternateGender,
isnull(b.FirstAlternatePayeeAge,'0') as FirstAlternatePayeeAge,
isnull(b.FirstAlternatePayeeNationID,'No Data') as FirstAlternatePayeeNationID,
isnull(b.FirstAlternatePayeeMobile ,'No Data') as FirstAlternatePayeeMobile,
isnull(b.SecondAlternatePayeeAge ,'0') as SecondAlternatePayeeAge,
isnull(b.SecondAlternateNationalID ,'No Data') as SecondAlternateNationalID,
isnull(b.SecondAlternatePayeeMobile,'No Data') as SecondAlternatePayeeMobile,
isnull(b.FamilySize,'0') as FamilySize,
isnull(b.NoofAdult,'0') as NoofAdult,
isnull(b.NoofDependents,'0') as NoofDependents,
isnull(b.FamilyIncome,'0') as FamilyIncome,
isnull(case when b.ContactNo='' then 'No Data' else b.ContactNo end,'No Data') as ContactNo,
isnull(case when b.Email='' then 'No Data' else b.Email end,'No Data') as Email,
isnull(case when  b.SecondLineHome='' then 'No Data' else b.SecondLineHome end,'No Data') as SecondLineHome,
isnull(case when b.SOurceofIncome='' then 'No Data' else b.SOurceofIncome end ,'No Data') as SOurceofIncome,
isnull(case when b.LegalStatus='' then 'No Data' else b.LegalStatus end ,'No Data') as LegalStatus,
case when b.EnrollmentNo like '%CST%' then 'CST' else 'Group' end as BeneficiaryType,
isnull(b.NoofAdult,'0') as [No.of other adult household members],
isnull(b.NumberMaleAdults,'0') as [Number of male adults],
isnull(b.NumberFemaleAdults,'0') as [Number of Female adults],
isnull(b.NumberAdultsAgeAbove65,'0') as [Number of adults Age above 65],
isnull(b.NumberAdultsAgeBetween45_64,'0') as [Number of adults age between 45 - 64],
isnull(b.NumberAdultsAgeBetween18_30,'0') as [Number of adults age between 18 - 30],
isnull(b.NumberAdultsAgeBetween30_45,'0') as [Number of adults age between 30 - 45 years],
isnull(b.NoofDependents,'0') as [Number of dependents],
isnull(b.NumberFemaleChildren,'0') as [Number of Female Children],
isnull(b.NumberMaleChildren,'0') as [Number of Male Children],
isnull(b.NumberChildrenAgeAbove5years,'0') as [Number of children age <5 years],
isnull(b.NumberChildrenAgeAbove12years,'0') as [Number of chilren age <12 years],
isnull(b.NumberChildrenAgeAbove18years,'0') as [Number of children age <18 years],
isnull(b.EthnicityTribeRgin,'No Data') as [Ethnicity or Tribe of orgin],
isnull(b.NationalID,'No Data') as [National ID Number],
isnull( case when   b.BiometricNo='' then 'No Data' else b.BiometricNo end ,'No Data') as [BiometricNo],
isnull( case when    b.NameofBank='' then 'No Data' else  b.NameofBank end,'No Data') as [NameofBank],
isnull( case when  b.AccountNo='' then 'No Data' else b.AccountNo end ,'No Data') as [AccountNo],
u1.User_name as Createdby,
--Convert(varchar(11),b.Created_dt,106) + ' ' +  CONVERT(varchar(15),CAST(b.Created_dt AS TIME),100) as Createddt
Convert(varchar(11),b.Created_dt,106)  as Createddt
from dbo.tbl_BeneficiaryDet as b 
inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
inner join tm_Payams as p on p.intPayamid=bom.intPayamid
inner join tm_Counties as c on c.intCountieid=p.intCountieid
inner join tm_States as s on s.intStateid=c.intStateid
inner join td_IPDetails as ip on ip.intIP=b.intIP
inner join tbl_Users as u1 on u1.intUserid=b.InsertedStaffid
 where
 
 intBenid like '%'   
 and b.intIP like @intIP
 and b.intBomasid like @intBomasid
 and p.intPayamid like @intPayamid
 and c.intCountieid like @intCountieid
 and s.intStateid like @intStateid
 and b.EnrollmentNo like '%'+@Enrollmentno+'%'
 and b.NameofHousehold like '%'+@Benname+'%'
 and  isnull(b.household_number,'') like '%'+@Household+'%'
 and b.household_number is not null
 order by s.StateName,c.CountieName,p.PayamName,bom.BomaName, ip.IPName,b.NameofHousehold
 end