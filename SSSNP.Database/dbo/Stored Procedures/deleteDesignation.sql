CREATE Procedure [dbo].[deleteDesignation]            
            
(            
@id varchar(50)            
)            
as begin            
      
            
delete from dbo.tm_Designations  where intDesgid like @id            
         
   select 100      
   return 100      
            
            
end




