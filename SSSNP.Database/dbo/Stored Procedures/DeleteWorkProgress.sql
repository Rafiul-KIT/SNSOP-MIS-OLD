CREATE Procedure [dbo].[DeleteWorkProgress]
(
@IntWorkDetailsTypeId int
)
as begin

delete from dbo.tbl_Attachements where Type='WorkProgress' and IntWorkDetailsTypeId = @IntWorkDetailsTypeId

end




