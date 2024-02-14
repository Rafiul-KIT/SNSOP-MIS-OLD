CREATE Procedure [dbo].[WorkAllocation]
(

@WorkCode	varchar(150),
@intBenid	varchar(20),

@Created_by	int

	
)
as begin

update tbl_BeneficiaryDet set WorkCode=@WorkCode,Modified_by=@Created_by,Modified_dt=GETDATE() where intBenid like @intBenid

insert into tr_BenfWorkDet ( intBenid,intWorkid,Created_by,Created_dt) values (@intBenid,@WorkCode,@Created_by,GETDATE())
select 1
return 1

end




