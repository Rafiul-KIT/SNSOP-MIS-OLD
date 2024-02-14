CREATE Procedure [dbo].[sp_ValidUserReset]    
(    
@userid varchar(50),  
@password varchar(50)    
)    
As    
Begin    
 set nocount on     
    
select * from tbl_users   
  where USER_ID = @userid and password = @password  and status='Y'   
end




