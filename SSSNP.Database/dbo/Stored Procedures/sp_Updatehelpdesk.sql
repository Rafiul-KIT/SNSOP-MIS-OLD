CREATE Procedure [dbo].[sp_Updatehelpdesk]              
(        
@int_fbid int,      
@Status varchar(10) ,      
@Remarks varchar(250),      
@Modified_by int         
              
)               
As        
BEGIN TRY      
BEGIN TRANSACTION      
set nocount on        
update tm_Helpdesk  set      
      
Status=@Status,      
Remarks=@Remarks,      
Modified_by  =@Modified_by,         
Modified_dt = getdate() where int_fbid = @int_fbid      
              
COMMIT TRANSACTION       
RETURN 0      
END TRY      
      
BEGIN CATCH      
ROLLBACK TRANSACTION      
RETURN -1      
      
END CATCH




