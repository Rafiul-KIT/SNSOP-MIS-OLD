create procedure [dbo].[UpdateSubProjectDraft]
(
@WorkId varchar(8),
@Status varchar(20),
@UserID varchar(8)
)
AS
Begin


Update tbl_WorkMaster 
set [status]=@Status, Modified_dt=getdate(), Modified_by=@UserID
where intWorkId=@WorkId 

End