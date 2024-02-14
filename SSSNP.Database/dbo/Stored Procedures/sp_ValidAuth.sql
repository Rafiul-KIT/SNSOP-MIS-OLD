
CREATE Procedure [dbo].[sp_ValidAuth]    
(    
  
@preferred_user varchar(255)
   
)    
As    
Begin    
 set nocount on     
    
select * from tbl_users   
  where Email = @preferred_user and status='Y'   

  select intDesgid from td_TstTeamDet where intTST in
  (
  select User_code from  tbl_users   
  where Email = @preferred_user and status='Y' 
  )

end
