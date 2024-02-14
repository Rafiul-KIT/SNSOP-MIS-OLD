CREATE Procedure [dbo].[insrtUpdpayams]              
(  
@NoofBomas int,            
@intPayamid int,              
@intCountieid int,              
@PayamCode varchar(5),              
@PayamName varchar(50),              
@Latitude varchar(15),              
@Longitude varchar(15),              
@Created_by int,  
@Population int               
)              
as begin              
              
              
if(@intPayamid>0)              
begin              
if((select COUNT(*) from tm_Payams where PayamName=@PayamName and intPayamid<> @intPayamid)>0)              
begin              
select 999              
return 999              
end              
else              
begin              
update dbo.tm_Payams set NoofBomas=@NoofBomas,Population=@Population,intCountieid=@intCountieid, PayamCode=@PayamCode, PayamName=@PayamName, Latitude=@Latitude, Longitude=@Longitude, Modified_by=@Created_by, Modified_dt=GETDATE()                 
where intPayamid like @intPayamid            
select 1              
return 1              
end              
end              
else              
begin              
if((select COUNT(*) from tm_Payams where PayamName=@PayamName and intCountieid=@intCountieid)>0)              
begin              
select 999              
return 999              
end              
else              
begin              
insert into tm_Payams(NoofBomas,Population,intCountieid, PayamCode, PayamName, Latitude, Longitude, Created_by, Created_dt)              
values(@NoofBomas,@Population,@intCountieid, @PayamCode, @PayamName, @Latitude, @Longitude, @Created_by, GETDATE())              
select @@IDENTITY              
return @@IDENTITY              
end              
end              
end




