CREATE Procedure [dbo].[deleteDisability]          
          
(          
@id varchar(50)          
)          
as begin          
    
          
delete from dbo.deleteDisability  where intDisability like @id          
       
   select 100    
   return 100    
          
          
end




