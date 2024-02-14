
CREATE procedure [dbo].[getBeneficiaryLookupsearchAll1updatealternate]        
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
        
        
    
        
SELECT   distinct  tbl_BeneficiaryDet.intBenid, tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, tm_States.StateName, tbl_BeneficiaryDet.EnrollmentNo,     
                      tbl_BeneficiaryDet.NameofHousehold, tbl_BeneficiaryDet.FamilySize, tbl_BeneficiaryDet.Sex, tbl_BeneficiaryDet.NameofSpouse, tbl_BeneficiaryDet.NoofAdult,     
                      tbl_BeneficiaryDet.NoofDependents, tbl_BeneficiaryDet.FamilyIncome, tbl_BeneficiaryDet.ContactNo, tbl_BeneficiaryDet.Email, tbl_BeneficiaryDet.BiometricNo,tbl_BeneficiaryDet.household_number,     
                      tbl_BeneficiaryDet.intDisability, tbl_BeneficiaryDet.IsDisability, tbl_BeneficiaryDet.AccountNo, tbl_BeneficiaryDet.NameofBank, tbl_BeneficiaryDet.WorkCode,tbl_BeneficiaryDet.GroupId    
FROM         tbl_BeneficiaryDet INNER JOIN    
                      tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid  
inner join tm_Counties  on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid  
inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid  
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid   
                              
                      where 
                       tbl_BeneficiaryDet.intStateid like @intStateid and tbl_BeneficiaryDet.intCountieid like @intCountieid and tbl_BeneficiaryDet.intPayamid like @intPayamid        
                      and tbl_BeneficiaryDet.intBomasid  like @intBomasid   and tbl_BeneficiaryDet.NameofHousehold like '%'+@NameofHousehold+'%'  and  tbl_BeneficiaryDet.EnrollmentNo like '%'+@EnrollmentNo+'%'  
                      --and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
					   --and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
					   and isnull(tbl_BeneficiaryDet.household_number,'') like '%'+@Householdno+'%'
					   and tbl_BeneficiaryDet.household_number is not null
end

