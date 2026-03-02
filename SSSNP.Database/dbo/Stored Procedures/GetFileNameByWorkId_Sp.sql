CREATE Procedure [dbo].[GetFileNameByWorkId_Sp]
(@intTypeId int)
as
begin 
select PhotoFileName,IntWorkDetailsTypeId from tbl_Attachements
where intTypeId=@intTypeId
and Type='Proposal'
end




