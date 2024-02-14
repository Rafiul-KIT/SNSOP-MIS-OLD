--created by siva as 04-1-2016            
--Purpose: to get Work Proposals                
                
create procedure [dbo].[GetWorkProposalCreatorByIntWorkId]                
(                
@intWorkId varchar(10)                
)                
as begin                
                
	select users.*
	from 
		dbo.tbl_WorkMaster wm
			inner join 
				tbl_users users 
					on wm.created_by = users.intUserid
	where intWorkid like @intWorkid            

end