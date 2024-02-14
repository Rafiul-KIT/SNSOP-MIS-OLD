

--created by Nagasree
--Purpose: to get Work Proposals            
            
--created by Nagasree 
  
CREATE procedure [dbo].[UpdateBenficiaryDetByAPIID]            
(            
@intBenid varchar(10),
@statsu varchar(5)
            
)            
as begin            
            
update tbl_BeneficiaryServiceDet set BenStatus = @statsu where intBenTempid like @intBenid

select 1
return 1        

--select * from PWImages.dbo.tbl_Attachements where type like 'Appicant Photo' and intTypeId like @intBenid
        
end





