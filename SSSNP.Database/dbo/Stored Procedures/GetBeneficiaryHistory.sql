
  
				CREATE  Procedure [dbo].[GetBeneficiaryHistory]            
				(            
				@intBenid varchar(10)            
				)            
				as begin            
            
				select * from tbl_BeneficiaryDetNewComponent where intBenid like @intBenid 
				ORDER BY intNewCompoId desc
				end