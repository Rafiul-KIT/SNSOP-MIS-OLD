CREATE Procedure [dbo].[insrtDesignations]              
(              
@intDesgid int,
@Type varchar(1),              
@Designation varchar(50),              
@Created_by int               
)              
as begin              
              
              
if(@intDesgid>0)              
begin              
if((select COUNT(*) from dbo.tm_Designations where Designation=@Designation and intDesgid<> @intDesgid)>0)              
begin              
select 999              
return 999              
end              
else              
begin              
update dbo.tm_Designations set Type=@Type,  Designation=@Designation,Modified_by=@Created_by, Modified_dt=GETDATE()                 
where intDesgid like @intDesgid            
select 1              
return 1              
end              
end              
else              
begin              
if((select COUNT(*) from tm_Designations where Designation=@Designation )>0)              
begin              
select 999              
return 999              
end              
else              
begin              
insert into tm_Designations(Type,Designation,Created_by, Created_dt)              
values(@Type,@Designation,@Created_by,GETDATE())              
select @@IDENTITY              
return @@IDENTITY              
end              
end              
end




