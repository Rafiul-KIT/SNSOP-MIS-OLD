CREATE Procedure [dbo].[DeleteProjectTargets]  
(  
@intPrjtarget varchar(10)  
)  
as begin  
  
delete from dbo.tr_ProjectTarget where intPrjtarget like @intPrjtarget
  
  
end




