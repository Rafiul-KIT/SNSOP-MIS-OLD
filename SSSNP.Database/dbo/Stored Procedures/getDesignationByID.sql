CREATE Procedure [dbo].[getDesignationByID]          
(          
@intDesgid varchar(10)          
)          
as begin          
          
SELECT     Type, intDesgid,Designation,Created_by,Created_dt      
FROM      dbo.tm_Designations    where intDesgid like @intDesgid          
          
end




