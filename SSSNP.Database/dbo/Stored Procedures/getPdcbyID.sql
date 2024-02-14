--alter Procedure for getpdcbyid for data retrive    
CREATE Procedure [dbo].[getPdcbyID]                    
(                    
@intPDC varchar(10)                    
)                    
as begin                    
                    
SELECT     td_PDCDet.*, tm_Payams.intPayamid AS Expr1, tm_Counties.intCountieid,tm_Counties.CountieName, tm_States.intStateid, tm_States.StateName           
FROM         td_PDCDet INNER JOIN            
                           tm_Payams ON td_PDCDet.intPayamid = tm_Payams.intPayamid INNER JOIN      
                      tm_Counties ON td_PDCDet.intCountieid = tm_Counties.intCountieid INNER JOIN            
                      tm_States ON tm_Counties.intStateid = tm_States.intStateid where td_PDCDet.intPDC like @intPDC    
                        
                      select * from dbo.tr_CouncilMemDet where MemType like 'PDC' and intCPBid like @intPDC  
                       select * from tbl_Users where User_code like @intPDC and User_type like 'PDC'                 
                     
end




