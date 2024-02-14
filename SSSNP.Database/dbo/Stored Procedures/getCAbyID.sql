CREATE Procedure [dbo].[getCAbyID]              
(              
@intCountyAdm varchar(10)              
)              
as begin              
              
SELECT     td_CountyAdmDet.*, tm_Counties.intCountieid AS Expr1, tm_Counties.intCountieid,tm_Counties.CountieName, tm_States.intStateid, tm_States.StateName       
FROM         td_CountyAdmDet INNER JOIN        
                             
                      tm_Counties ON td_CountyAdmDet.intCountieid = tm_Counties.intCountieid INNER JOIN        
                      tm_States ON tm_Counties.intStateid = tm_States.intStateid where td_CountyAdmDet.intCountyAdm like @intCountyAdm      
              
select * from dbo.tr_CouncilMemDet where MemType like 'CA' and intCPBid like @intCountyAdm      
select * from tbl_Users where User_code like @intCountyAdm and User_type like 'CA'     
  
  
end




