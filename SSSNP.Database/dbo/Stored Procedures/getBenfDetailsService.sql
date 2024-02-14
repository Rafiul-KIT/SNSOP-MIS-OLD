
CREATE procedure [dbo].[getBenfDetailsService]

AS
Begin
select top 1000 b.intBenid,
ips.IPName,
ips.intIP as IPCode,

 b.EnrollmentNo,
 b.GroupId,
 b.BeneficiaryType,
 b.NameofHousehold,
 b.FamilySize,
 case when b.Sex='M' then 'Male' else 'Female' end as Gender,
 b.Age,
 b.NameofSpouse,
 
 b.NumberMaleAdults,
 b.NumberFemaleAdults,
 b.NumberAdultsAgeAbove65,
 b.NumberAdultsAgeBetween45_64,
 b.NumberAdultsAgeBetween18_30,
 b.NumberAdultsAgeBetween30_45,
 b.NoofDependents,
 b.NumberFemaleChildren,
 b.NumberMaleChildren,
 b.NumberChildrenAgeAbove5years,
 b.NumberChildrenAgeAbove12years,
 b.NumberChildrenAgeAbove18years,
 b.FamilyIncome,
 b.SOurceofIncome,
 b.NationalID,
 s.intStateid,
 s.StateName,
 c.intCountieid,
 c.CountieName,
 p.intPayamid,
 p.PayamName,
 bom.intBomasid,
 bom.BomaName,
 b.EthnicityTribeRgin,
 b.Address,
 b.ContactNo,
 b.Email,
 b.SecondLineHome,
 b.LegalStatus,
 b.BiometricNo,
 b.NameofBank,
 b.AccountNo,
 b.PhotoPath+'/' + b.PhotoName  as Photo,
 CONVERT(char(10), b.Created_dt,126)  as DateofCreation,
 --Convert(varchar(11),b.Created_dt,106)
 household_number,


 supervisor_id, member_number, female_dependants, male_dependants, b.latitude, b.longitude, id_url, TransferDate, TransferUpdateat
 
  from tbl_BeneficiaryDet as b inner join td_IPDetails as ips on ips.intIP=b.intIP
  inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
  inner join tm_Payams as p on p.intPayamid=bom.intPayamid
  inner join tm_Counties as c on c.intCountieid=p.intCountieid
  inner join tm_States as s on s.intStateid=c.intStateid

  where  b.household_number is not null
ENd

