--creatd by suresh as on 01-17-2016 for county adv search    
CREATE Procedure [dbo].[getCASearch]            
(            
@intStateid varchar(50),            
@intCountieid varchar(50),              
@CountyAdmName varchar(50),
@Created_by varchar(50)
)            
as begin            
            
SELECT     tm_States.StateName, tm_Counties.CountieName, td_CountyAdmDet.CountyAdmName, td_CountyAdmDet.intCountyAdm ,td_CountyAdmDet.Email           
FROM         tm_States INNER JOIN            
                      tm_Counties ON tm_States.intStateid = tm_Counties.intStateid INNER JOIN            
                                  
                      td_CountyAdmDet ON td_CountyAdmDet.intCountieid = tm_Counties.intCountieid            
                       where tm_States.intStateid like @intStateid and tm_Counties.intCountieid like @intCountieid            
                       and td_CountyAdmDet.CountyAdmName like '%'+@CountyAdmName +'%' and td_CountyAdmDet.Created_by like @Created_by           
                                  
                                  
end




