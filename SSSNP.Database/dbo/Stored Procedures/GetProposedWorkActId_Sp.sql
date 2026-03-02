CREATE Procedure [dbo].[GetProposedWorkActId_Sp]
(@intWorkid int)
as
begin
--select  max(intWorkActivityId) as ProposedWorkActId from tr_ProposedWorkAct
--select  (max(intWorkActivityId)+1) as ProposedWorkActId from tr_ProposedWorkAct
select intWorkActivityId,WorkCode from tr_ProposedWorkAct where intWorkid=@intWorkid
end





