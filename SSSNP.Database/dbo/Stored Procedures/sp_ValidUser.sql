CREATE Procedure [dbo].[sp_ValidUser]    
(    
@userid varchar(50),  
@password varchar(50)    
)    
As    
Begin    
 set nocount on     
    
select * from tbl_users   
  where USER_ID = @userid and password = @password  and status='Y'   

  select intDesgid from td_TstTeamDet where intTST in
  (
  select User_code from  tbl_users   
  where USER_ID = @userid and password = @password  and status='Y' 
  )

end
GO
