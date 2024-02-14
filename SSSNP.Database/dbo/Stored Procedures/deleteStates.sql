--created by suresh as on 06-1-2015    
--Purpose : to delete States by id    
CREATE Procedure [dbo].[deleteStates]    
            
(            
@id varchar(50)            
)            
as begin            
      
            
delete from dbo.tm_States  where intStateid like @id            
         
   select 100      
   return 100      
            
            
end




