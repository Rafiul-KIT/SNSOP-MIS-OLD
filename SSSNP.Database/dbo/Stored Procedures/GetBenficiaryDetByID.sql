--created by siva as 04-1-2016        
--Purpose: to get Work Proposals            
            
--created by siva as on 12-1-2016 for benficiary Details By ID  
  
CREATE Procedure [dbo].[GetBenficiaryDetByID]            
(            
@intBenid varchar(10)            
)            
as begin            
            
select * from dbo.tbl_BeneficiaryDet where intBenid like @intBenid        

--select * from PWImages.dbo.tbl_Attachements where type like 'Appicant Photo' and intTypeId like @intBenid
        
end




