--created by suresh as on 13-1-2016          
--Purpose : to delete PDC by id            
CREATE Procedure [dbo].[deletePDC]            
                    
(                    
@id varchar(50)                    
)                    
as begin                    
              
                    
delete from dbo.td_PDCDet  where intPDC like @id                    
                 
   select 100              
   return 100              
                    
                    
end




