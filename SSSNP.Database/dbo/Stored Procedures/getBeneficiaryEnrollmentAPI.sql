        

CREATE Procedure [dbo].[getBeneficiaryEnrollmentAPI]        
(        

@BomaCode varchar(500)
)        
as begin
        
SELECT distinct tm_Bomas.BomaCode , tbl_BeneficiaryDet.intBenid, tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, tm_States.StateName, tbl_BeneficiaryDet.EnrollmentNo,     
                      tbl_BeneficiaryDet.NameofHousehold, tbl_BeneficiaryDet.FamilySize, tbl_BeneficiaryDet.Sex, tbl_BeneficiaryDet.NameofSpouse, tbl_BeneficiaryDet.NoofAdult,     
                      tbl_BeneficiaryDet.NoofDependents, tbl_BeneficiaryDet.FamilyIncome, tbl_BeneficiaryDet.ContactNo, tbl_BeneficiaryDet.Email, tbl_BeneficiaryDet.BiometricNo,tbl_BeneficiaryDet.household_number,     
                      tbl_BeneficiaryDet.intDisability, tbl_BeneficiaryDet.IsDisability, tbl_BeneficiaryDet.AccountNo, tbl_BeneficiaryDet.NameofBank, tbl_BeneficiaryDet.WorkCode,tbl_BeneficiaryDet.GroupId    
FROM         tbl_BeneficiaryDet   
	inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid  
	inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid   
	inner join tm_Counties  on tm_Counties.intCountieid=tm_Payams.intCountieid  
	INNER JOIN tm_States on tm_States.intStateid=tm_Counties.intStateid  
					    where tm_Bomas.BomaCode = @BomaCode 

end



                       


