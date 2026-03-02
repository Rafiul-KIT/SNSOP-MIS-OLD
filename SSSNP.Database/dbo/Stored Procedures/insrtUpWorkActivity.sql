CREATE Procedure [dbo].[insrtUpWorkActivity]          
(          
@intWorkActivity int,          
@intAreaofWork int,          
@WorkActivity varchar(100),        
@Created_by int          
)          
as begin          
          
          
if(@intWorkActivity>0)          
begin          
if((select COUNT(*) from dbo.tm_WorkActivities where WorkActivity=@WorkActivity and intWorkActivity<> @intWorkActivity)>0)          
begin          
select 999          
return 999          
end          
else          
begin          
update dbo.tm_WorkActivities set intAreaofWork=@intAreaofWork, WorkActivity=@WorkActivity,Modified_by=@Created_by, Modified_dt=GETDATE()             
where intWorkActivity like @intWorkActivity          
select 1          
return 1          
end          
end          
else          
begin          
if((select COUNT(*) from tm_WorkActivities where WorkActivity=@WorkActivity and intAreaofWork=@intAreaofWork)>0)          
begin          
select 999          
return 999          
end          
else          
begin          
insert into dbo.tm_WorkActivities(intAreaofWork, WorkActivity, Created_by, Created_dt)          
values(@intAreaofWork, @WorkActivity, @Created_by, GETDATE())          
select @@IDENTITY          
return @@IDENTITY          
end          
end          
end




