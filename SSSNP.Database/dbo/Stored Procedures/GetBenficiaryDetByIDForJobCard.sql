
--created by siva as 19-2-2016              
--Purpose: to get Job card Details             
                  
--created by siva as on 12-1-2016 for benficiary Details By ID        
        
CREATE Procedure [dbo].[GetBenficiaryDetByIDForJobCard]                  
(                  
@intBenid varchar(10)                  
)                  
as begin                  
                  
    
        
SELECT tAPI.intBenTempid , tbl_WorkMaster.WorkCode,tbl_BeneficiaryDet.Age,   tbl_BeneficiaryDet.intBenid, tbl_BeneficiaryDet.EnrollmentNo, tbl_BeneficiaryDet.NameofHousehold, tbl_BeneficiaryDet.intIP, tbl_BeneficiaryDet.FamilySize,     
                      tbl_BeneficiaryDet.Sex, tbl_BeneficiaryDet.NameofSpouse, tbl_BeneficiaryDet.NoofAdult, tbl_BeneficiaryDet.NoofDependents, tbl_BeneficiaryDet.FamilyIncome,     
                      tbl_BeneficiaryDet.Address, tbl_BeneficiaryDet.ContactNo, tbl_BeneficiaryDet.Email, tbl_BeneficiaryDet.BiometricNo,      
                      tbl_BeneficiaryDet.Created_dt, tm_Payams.PayamName, tm_States.StateName, tm_Counties.CountieName, tm_Bomas.BomaName,
					   tbl_BeneficiaryDet.PhotoPath,tbl_BeneficiaryDet.PhotoName   ,
					      tbl_BeneficiaryDet.FirstAlternatePayeePhotoName, tbl_BeneficiaryDet.FirstAlternatePayeePhotoPath, tbl_BeneficiaryDet.Name1stAlternatePayee ,
					    tbl_BeneficiaryDet.SecondAlternatepayeePhotoName , tbl_BeneficiaryDet.SecondAlternatePayeePhotoPath, tbl_BeneficiaryDet.Name2ndAlternatePayee 
FROM         tbl_BeneficiaryDet LEFT OUTER JOIN   
 tm_Bomas ON tbl_BeneficiaryDet.intBomasid = tm_Bomas.intBomasid left outer JOIN 
 tm_Payams ON tm_Payams.intPayamid = tm_Bomas.intPayamid left outer JOIN  
 tm_Counties ON tm_Counties.intCountieid = tm_Payams.intCountieid INNER JOIN   
                      tm_States ON tm_States.intStateid = tm_Counties.intStateid left outer JOIN    
                       
                                               
                      --tm_WorkActivities ON tbl_BeneficiaryDet.WorkCode = tm_WorkActivities.intWorkActivity    
                      tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid 
					  
					                 left outer join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid   
									 left join tbl_BeneficiaryServiceDet as tAPI on tAPI.household_number = tbl_BeneficiaryDet.household_number 
									 
                     where tbl_BeneficiaryDet.intBenid like @intBenid              
    
      
--select * from PWImages.dbo.tbl_Attachements where type like 'Appicant Photo' and intTypeId like @intBenid      
              
end




