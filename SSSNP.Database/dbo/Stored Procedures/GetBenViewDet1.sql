  
CREATE Procedure [dbo].[GetBenViewDet1]   -- exec GetBenficiaryDetByIDForInvoice '28252'            
(            
@intBenid varchar(8)            
)            
as begin  

Declare @FilePaths varchar(380)
set @FilePaths=(select FileServerPath from tm_FilePathMap)

Declare @BiometricFilePaths varchar(380)
set @BiometricFilePaths=(select BiomatricPath from tm_FilePathMap)

create table #tblAlternatemorethan
(
intBenid int,
household_number varchar(240),
alternate_number varchar(240),
alternate_name varchar(180),
photo_url varchar(180),
Photopath varchar(380)
)
insert into #tblAlternatemorethan
select intBenid,household_number,alternate_number,alternate_name,photo_url,@BiometricFilePaths + photo_url as Photopath from tbl_BenfMorethanAlternateDet where intBenid=@intBenid
	              
            
select  top 100 b.intBenid,s.StateName,c.CountieName,p.PayamName,bom.BomaName, ip.IPName,  
b.EnrollmentNo,b.NameofHousehold,b.NameofSpouse,b.household_number,b.FamilySize,b.FamilyIncome ,
case when b.Sex='M' then 'Male' else 'Female' end as Gender, b.SelectionCriteria as SelectionCriteria,
b.Age,b.Address,b.ContactNo,b.Email,b.NoofAdult,b.NoofDependents,
b.NameofBank,b.AccountNo,b.LegalStatus,b.SOurceofIncome,b.BeneficiaryType,b.GroupId,b.PhotoName,b.PhotoPath,   b.Name1stAlternatePayee,
b.alternate_number,b.FirstAlternatePayeeAge,
case when b.FirstAlternatePayeeGender='M' then 'Male' else case when b.FirstAlternatePayeeGender is null  then '' else 'Female' end end FirstAlternatePayeeGender ,
b.FirstAlternatePayeeNationID,b.FirstAlternatePayeeMobile,
b.FirstAlternatePayeePhotoName,b.FirstAlternatePayeePhotoPath,b.Name2ndAlternatePayee,b.Secondalternate_number,
b.SecondAlternatePayeeAge,
case when b.SecondAlternatePayeeGender='M' then 'Male' else case when b.SecondAlternatePayeeGender is null  then '' else 'Female' end end SecondAlternatePayeeGender,

b.SecondAlternatePayeeMobile,b.SecondAlternateNationalID,
b.SecondAlternatepayeePhotoName,b.SecondAlternatePayeePhotoPath,
b.female_dependants,b.male_dependants,b.latitude,b.longitude,convert(varchar(11),  b.Created_dt,106) as ConfirmedorCreatedDate,
case when b.PhotoPath is null then @BiometricFilePaths + b.PhotoName else  REPLACE(b.PhotoPath,   '~/',@FilePaths) end as PhotoURL,
 case when b.FirstAlternatePayeePhotoPath is null then @BiometricFilePaths + b.FirstAlternatePayeePhotoName else REPLACE(b.FirstAlternatePayeePhotoPath,   '\',@FilePaths) end
  as FirstAlternatePayeePhotoURL,
case when b.SecondAlternatePayeePhotoPath is null then @BiometricFilePaths + b.SecondAlternatepayeePhotoName else REPLACE(  b.SecondAlternatePayeePhotoPath,'\',      @FilePaths) end as SecondAlternatePayeePhotoURL,
case when b.ThirdAlternatePayeePhotoPath is null then @BiometricFilePaths + b.ThirdAlternatepayeePhotoName else REPLACE(  b.ThirdAlternatePayeePhotoPath,'\',      @FilePaths) end as ThirdAlternatePayeePhotoURL,
 b.Name3rdAlternatePayee,
b.thirdalternate_number,
case when b.ThirdAlternatePayeeGender='M' then 'Male' else case when b.ThirdAlternatePayeeGender is null  then '' else 'Female' end end ThirdAlternatePayeeGender 
from dbo.tbl_BeneficiaryDet as b 
inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
inner join tm_Payams as p on p.intPayamid=bom.intPayamid
inner join tm_Counties as c on c.intCountieid=p.intCountieid
inner join tm_States as s on s.intStateid=c.intStateid
inner join td_IPDetails as ip on ip.intIP=b.intIP
 where
 -- PhotoPath is not null and 
 intBenid like @intBenid   

   select * from #tblAlternatemorethan where intBenid =@intBenid and alternate_number in (select a.Paidto from tr_BenWagesPayment as a)

  Drop table #tblAlternatemorethan
        
end