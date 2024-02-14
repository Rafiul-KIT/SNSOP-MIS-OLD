CREATE Procedure [dbo].[Deletetbl_Attachment_Sp]
(@intTypeId int)
as
begin
delete from tbl_Attachements
where intTypeId=@intTypeId
and Type='Proposal'
end



