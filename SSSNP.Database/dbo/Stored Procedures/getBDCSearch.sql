--created by suresh as on 14-1-16 for Boma Development Comitte      
      
CREATE Procedure [dbo].[getBDCSearch]            
(            
@intStateid varchar(50),            
@intCountieid varchar(50),            
@intPayamid varchar(50),              
@BDCName varchar(50),
@Created_by varchar(50)            
)            
as begin            
            
SELECT     tm_States.StateName, tm_Counties.CountieName, tm_Payams.PayamName,td_BDCDet.BDCName,td_BDCDet.intBDC,td_BDCDet.Email            
FROM         tm_States INNER JOIN            
                      tm_Counties ON tm_States.intStateid = tm_Counties.intStateid INNER JOIN            
                      tm_Payams ON tm_Counties.intCountieid = tm_Payams.intCountieid INNER JOIN            
                      td_BDCDet ON tm_Payams.intPayamid = td_BDCDet.intPayamid            
                       where tm_States.intStateid like @intStateid and tm_Counties.intCountieid like @intCountieid            
                       and tm_Payams.intPayamid like @intPayamid and td_BDCDet.BDCName like '%'+@BDCName +'%'            
                                  and td_BDCDet.Created_by like @Created_by
                                  
end




