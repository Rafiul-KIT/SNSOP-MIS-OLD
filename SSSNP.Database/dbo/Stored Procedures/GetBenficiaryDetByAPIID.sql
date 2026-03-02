

--created by Nagasree
--Purpose: to get Work Proposals            
            
--created by Nagasree 
  
CREATE procedure [dbo].[GetBenficiaryDetByAPIID]            
(            
@intBenid varchar(10)            
)            
as begin            
            
select * from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid        

--select * from PWImages.dbo.tbl_Attachements where type like 'Appicant Photo' and intTypeId like @intBenid
        
end





