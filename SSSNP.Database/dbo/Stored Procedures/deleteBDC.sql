--created by suresh as on 13-1-2016              
--Purpose : to delete BDC by id              
CREATE Procedure [dbo].[deleteBDC]              
                      
(                      
@id varchar(50)                      
)                      
as begin                      
                
                      
delete from dbo.td_BDCDet  where intBDC like @id                      
                   
   select 100                
   return 100                
                      
                      
end




