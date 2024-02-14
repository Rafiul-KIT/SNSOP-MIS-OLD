CREATE Procedure [dbo].[getAreaofWorkbyID]      
(      
@intAreaofWork varchar(10)      
)      
as begin      
      
SELECT      intAreaofWork,AreaofWork,Created_by,Created_dt  
FROM      dbo.tm_AreaofWork    where intAreaofWork like @intAreaofWork      
      
end




