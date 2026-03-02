--created by suresh as on 06-1-2015        
--Purpose : to delete IP by id        
CREATE Procedure [dbo].[deleteIP]        
                
(                
@id varchar(50)                
)                
as begin                
          
                
delete from dbo.td_IPDetails  where intIP like @id                
             
   select 100          
   return 100          
                
                
end




