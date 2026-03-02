---created by suresh as on 14-1-16 purpose of getbomacomitte by id          
          
CREATE Procedure [dbo].[getBDCbyID]                        
(                        
@intBDC varchar(50)                        
)                        
as begin             
    
    
                        
SELECT     td_BDCDet.*, tm_Payams.intPayamid AS Expr1, tm_Counties.intCountieid,tm_Counties.CountieName, tm_States.intStateid, tm_States.StateName               
FROM         td_BDCDet INNER JOIN                
                            tm_Payams ON td_BDCDet.intPayamid = tm_Payams.intPayamid INNER JOIN          
                      tm_Counties ON td_BDCDet.intCountieid = tm_Counties.intCountieid INNER JOIN   
                        
                      tm_States ON tm_Counties.intStateid = tm_States.intStateid where td_BDCDet.intBDC like @intBDC        
                            
                            
   select * from dbo.tr_CouncilMemDet where MemType like 'BDC' and intCPBid like @intBDC        
   --select * from dbo.tr_CouncilMemDet where MemType like 'TST' and intCPBid like @intBDC        
           
   --select * from dbo.tr_CouncilMemDet where MemType like 'CA' and intCPBid like @intBDC        
           
   --select * from dbo.tr_CouncilMemDet where MemType like 'PDC' and intCPBid like @intBDC        
      
          select * from tbl_Users where User_code like @intBDC and User_type like 'BDC'     
                         
end




