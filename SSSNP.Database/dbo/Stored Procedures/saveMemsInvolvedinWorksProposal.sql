CREATE Procedure [dbo].[saveMemsInvolvedinWorksProposal]
(

@intWorkid	int,
@intMemid	int,
@MemType	varchar(5),
@Created_by	int
)
as begin

insert into dbo.tr_MemsInvolved (intWorkid,intMemid,MemType,Created_by,Created_dt)
values(@intWorkid,@intMemid,@MemType,@Created_by,GETDATE())

select @@IDENTITY
return @@IDENTITY

end




