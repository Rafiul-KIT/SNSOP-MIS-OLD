CREATE Procedure [dbo].[deleteWorkActivities]    
            
(            
@id varchar(50)            
)            
as begin            
      
            
delete from  dbo.tm_WorkActivities where intWorkActivity like @id            
         
   select 100      
   return 100      
            
            
end




