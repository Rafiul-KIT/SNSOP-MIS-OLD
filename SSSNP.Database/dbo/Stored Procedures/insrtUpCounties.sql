CREATE Procedure [dbo].[insrtUpCounties]              
(   
@NoofBlocks int,           
@intCountieid int,              
@intStateid int,              
@CountieCode varchar(5),              
@CountieName varchar(50),              
@Latitude varchar(15),              
@Longitude varchar(15),              
@Created_by int,  
@Population int              
)              
as begin              
              
              
if(@intCountieid>0)              
begin              
if((select COUNT(*) from dbo.tm_Counties where CountieName=@CountieName and intCountieid<> @intCountieid)>0)              
begin              
select 999              
return 999              
end              
else              
begin              
update dbo.tm_Counties set NoofBlocks=@NoofBlocks,Population=@Population,intStateid=@intStateid, CountieCode=@CountieCode, CountieName=@CountieName, Latitude=@Latitude, Longitude=@Longitude, Modified_by=@Created_by, Modified_dt=GETDATE()                 
where intCountieid like @intCountieid              
select 1              
return 1              
end              
end              
else              
begin              
if((select COUNT(*) from tm_Counties where CountieName=@CountieName)>0)              
begin              
select 999              
return 999              
end              
else              
begin              
insert into dbo.tm_Counties(NoofBlocks,Population,intStateid, CountieCode, CountieName, Latitude, Longitude, Created_by, Created_dt)              
values(@NoofBlocks,@Population,@intStateid, @CountieCode, @CountieName, @Latitude, @Longitude, @Created_by, GETDATE())              
select @@IDENTITY              
return @@IDENTITY              
end              
end              
end




