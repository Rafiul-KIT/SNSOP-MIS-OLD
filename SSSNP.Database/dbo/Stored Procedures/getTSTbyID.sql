--created by suresh as on 30-11-2015            
--Purpose : to get tst by id            
CREATE Procedure [dbo].[getTSTbyID]            
(            
@intTST varchar(10)            
)            
as begin            
            
SELECT     td_TstTeamDet.*, tm_Payams.intPayamid AS Expr1, tm_Counties.intCountieid, tm_States.intStateid            
FROM         td_TstTeamDet INNER JOIN            
                      tm_Payams ON td_TstTeamDet.intPayamid = tm_Payams.intPayamid INNER JOIN            
                      tm_Counties ON tm_Payams.intCountieid = tm_Counties.intCountieid INNER JOIN            
                      tm_States ON tm_Counties.intStateid = tm_States.intStateid where td_TstTeamDet.intTST like @intTST    
                          
                          
                          
                       select * from tbl_Users where User_code like @intTST 
					   --and User_type like 'TST'               
                       
                       select * from dbo.tbl_Attachements where type like 'TST Photo' and intTypeId like @intTST  
            
end




