CREATE Procedure [dbo].[GetGroupMasterById](@GroupId int)
As
select * from GroupMaster where GroupId=@GroupId



