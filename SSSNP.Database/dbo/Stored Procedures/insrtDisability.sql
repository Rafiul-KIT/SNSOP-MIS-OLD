CREATE Procedure [dbo].[insrtDisability]          
(          
@intDisability int,          
@Disability varchar(50),          
@Created_by int           
)          
as begin          
          
          
if(@intDisability>0)          
begin          
if((select COUNT(*) from dbo.tm_Disability where Disability=@Disability and intDisability<> @intDisability)>0)          
begin          
select 999          
return 999          
end          
else          
begin          
update dbo.tm_Disability set  Disability=@Disability,Modified_by=@Created_by, Modified_dt=GETDATE()             
where intDisability like @intDisability          
select 1          
return 1          
end          
end          
else          
begin          
if((select COUNT(*) from tm_Disability where Disability=@Disability )>0)          
begin          
select 999          
return 999          
end          
else          
begin          
insert into tm_Disability(Disability,Created_by, Created_dt)          
values(@Disability,@Created_by,GETDATE())          
select @@IDENTITY          
return @@IDENTITY          
end          
end          
end




