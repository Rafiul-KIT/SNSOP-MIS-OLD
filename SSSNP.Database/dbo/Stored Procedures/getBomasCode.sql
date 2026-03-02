CREATE Procedure [dbo].[getBomasCode]      
(      
@IntBomasid int    
)      
as begin      
      
select  intBomasid ,BomaCode from tm_Bomas where intBomasid =@IntBomasid 
      
end



