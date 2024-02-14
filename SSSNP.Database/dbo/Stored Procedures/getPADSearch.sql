--created by suresh for pad search as on 17-1-2016    
CREATE Procedure [dbo].[getPADSearch]          
(          
@intStateid varchar(50),          
@intCountieid varchar(50),          
@intPayamid varchar(50),            
@PDCName varchar(50) ,
@Created_by varchar(50)         
)          
as begin          
          
SELECT     tm_States.StateName, tm_Counties.CountieName, tm_Payams.PayamName, td_PDCDet.PDCName, td_PDCDet.intPDC          
FROM         tm_States INNER JOIN          
                      tm_Counties ON tm_States.intStateid = tm_Counties.intStateid INNER JOIN          
                      tm_Payams ON tm_Counties.intCountieid = tm_Payams.intCountieid INNER JOIN          
                      td_PDCDet ON tm_Payams.intPayamid = td_PDCDet.intPayamid          
                       where tm_States.intStateid like @intStateid and tm_Counties.intCountieid like @intCountieid          
                       and tm_Payams.intPayamid like @intPayamid and td_PDCDet.PDCName like '%'+@PDCName +'%' 
                       and td_PDCDet.Created_by like @Created_by         
                                
                                
end




