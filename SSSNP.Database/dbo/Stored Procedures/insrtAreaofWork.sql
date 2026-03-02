CREATE Procedure [dbo].[insrtAreaofWork]          
(          
@intAreaofWork int,          
@AreaofWork varchar(50),          
@Created_by int           
)          
as begin          
          
          
if(@intAreaofWork>0)          
begin          
if((select COUNT(*) from dbo.tm_AreaofWork where AreaofWork=@AreaofWork and intAreaofWork<> @intAreaofWork)>0)          
begin          
select 999          
return 999          
end          
else          
begin          
update dbo.tm_AreaofWork set  AreaofWork=@AreaofWork,Modified_by=@Created_by, Modified_dt=GETDATE()             
where intAreaofWork like @intAreaofWork          
select 1          
return 1          
end          
end          
else          
begin          
if((select COUNT(*) from tm_AreaofWork where AreaofWork=@AreaofWork )>0)          
begin          
select 999          
return 999          
end          
else          
begin          
insert into tm_AreaofWork(AreaofWork,Created_by, Created_dt)          
values(@AreaofWork,@Created_by,GETDATE())          
select @@IDENTITY          
return @@IDENTITY          
end          
end          
end




