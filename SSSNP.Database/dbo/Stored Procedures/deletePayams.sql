CREATE Procedure [dbo].[deletePayams]    
            
(            
@id varchar(50)            
)            
as begin            
      
            
delete from dbo.tm_Payams  where intPayamid like @id            
         
   select 100      
   return 100      
            
            
end




