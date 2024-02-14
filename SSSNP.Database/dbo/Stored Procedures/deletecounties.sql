CREATE Procedure [dbo].[deletecounties]  
          
(          
@id varchar(50)          
)          
as begin          
    
          
delete from  dbo.tm_Counties where intCountieid like @id          
       
   select 100    
   return 100    
          
          
end




