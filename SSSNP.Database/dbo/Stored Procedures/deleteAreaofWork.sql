CREATE Procedure [dbo].[deleteAreaofWork]        
       
(        
@id varchar(50)        
)        
as begin        
 
       
delete from dbo.tm_AreaofWork  where intAreaofWork like @id        
    
  select 100  
  return 100  
       
       
end




