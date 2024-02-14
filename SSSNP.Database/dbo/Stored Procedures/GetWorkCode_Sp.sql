CREATE Procedure [dbo].[GetWorkCode_Sp]
(
@intWorkid int
)
as
begin
select WorkCode from tr_ProposedWorkAct
where intWorkid=@intWorkid

end
 



