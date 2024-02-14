CREATE Procedure [dbo].[getCountieSearch]    
(    
@intStateid varchar(50),    
@CountieName varchar(50)   
)    
as begin    
    
SELECT     tm_States.intStateid,tm_States.StateName, tm_Counties.CountieName,  tm_Counties.intCountieid 
FROM         tm_States INNER JOIN    
                      tm_Counties ON tm_States.intStateid = tm_Counties.intStateid 
                       where tm_States.intStateid like @intStateid   
                       and  tm_Counties.CountieName like '%'+@CountieName +'%'    
                          
                          
end




