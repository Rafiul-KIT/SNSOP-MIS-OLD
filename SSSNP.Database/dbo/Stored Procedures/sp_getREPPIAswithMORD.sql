CREATE Procedure [dbo].[sp_getREPPIAswithMORD]        
        
As        
Begin        
 set nocount on         
    
SELECT     distinct tbl_Users.User_name  as piaName, case when tbl_Users.User_Type='Adm' then tbl_Users.intUserid else tbl_Users.intUserid end as intPIAid    
FROM         td_TstTeamDet RIGHT OUTER JOIN    
                      tbl_Users ON td_TstTeamDet.intTST = tbl_Users.User_Code  where  (tbl_Users.User_Type='Adm' or tbl_Users.User_Type='TST' or tbl_Users.User_Type='IP')      
        
end




