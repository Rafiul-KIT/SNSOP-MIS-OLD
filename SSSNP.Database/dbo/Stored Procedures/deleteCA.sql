--created by suresh as on 30-11-2015          
--Purpose : to delete county dvp by id          
CREATE Procedure [dbo].[deleteCA]          
                  
(                  
@id varchar(50)                  
)                  
as begin                  
            
                  
delete from dbo.td_CountyAdmDet  where intCountyAdm like @id                  
               
   select 100            
   return 100            
                  
                  
end




