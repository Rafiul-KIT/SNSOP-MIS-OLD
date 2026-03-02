CREATE Procedure [dbo].[getCountiesbyID]      
(      
@intCountieid varchar(10)      
)      
as begin      
      
SELECT tm_Counties.*,dbo.tm_States .intStateid as Expr1,CountieCode,CountieName,tm_Counties.Latitude,tm_Counties.Longitude,tm_Counties.Created_by,tm_Counties.Created_dt  
FROM   dbo.tm_Counties   inner join dbo.tm_States on dbo.tm_Counties.intStateid=dbo.tm_States.intStateid where dbo.tm_Counties.intCountieid like @intCountieid      
      
end




