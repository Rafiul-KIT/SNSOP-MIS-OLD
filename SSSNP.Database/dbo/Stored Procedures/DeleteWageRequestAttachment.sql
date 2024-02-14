
create procedure [dbo].[DeleteWageRequestAttachment]
(
@intImageId integer
)
As
Begin

delete from tbl_WageAttachements
  where intImageId=@intImageId

  
  

  
End