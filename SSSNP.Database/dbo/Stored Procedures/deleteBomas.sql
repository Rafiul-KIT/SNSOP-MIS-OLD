--created by siva as on 30-11-2015  
--Purpose : to delete Bomas by id  
CREATE Procedure [dbo].[deleteBomas]  
          
(          
@id varchar(50)          
)          
as begin          
    
          
delete from dbo.tm_Bomas  where intBomasid like @id          
       
   select 100    
   return 100    
          
          
end




