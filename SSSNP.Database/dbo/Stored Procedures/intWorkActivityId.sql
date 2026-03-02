CREATE Procedure [dbo].[intWorkActivityId]
(
@intWorkActivityId varchar(10)
)
as begin

delete from dbo.tr_ProposedWorkAct where intWorkActivityId like @intWorkActivityId


end




