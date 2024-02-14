CREATE Procedure [dbo].[Sp_GetProposedId]
(@workId nvarchar(max))
as
begin
 select tr_ProposedWorkAct.intWorkActivityId from tr_ProposedWorkAct

where tr_ProposedWorkAct.intWorkid=@workId
end


