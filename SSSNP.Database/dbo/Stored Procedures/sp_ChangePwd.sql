CREATE Procedure [dbo].[sp_ChangePwd]      
(    
@Userid varchar(50),    
@Password varchar(50),    
@modifiedby varchar(20)    
)    
As      
Begin      
    
set nocount on    
Update tbl_Users set Password=@Password,Modified_by=@modifiedby,Modified_dt=getdate() where User_id =@Userid      
set nocount off    
      
end     
    
set ANSI_NULLS ON    
set QUOTED_IDENTIFIER ON




