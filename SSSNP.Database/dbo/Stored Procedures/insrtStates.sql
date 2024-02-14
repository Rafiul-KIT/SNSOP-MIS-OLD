CREATE Procedure [dbo].[insrtStates]            
(    
@NoofCounties int,        
@intStateid int,            
@StateName varchar(50),            
@StateCode varchar(5),            
@Latitude varchar(15),            
@Longitude varchar(15),      
@Created_by int ,  
@Population int            
)            
as begin            
            
            
if(@intStateid>0)            
begin            
if((select COUNT(*) from dbo.tm_States where StateName=@StateName and intStateid<> @intStateid)>0)            
begin            
select 999            
return 999            
end            
else            
begin            
update dbo.tm_States set NoofCounties=@NoofCounties, Population=@Population, StateCode=@StateCode, StateName=@StateName,Latitude=@Latitude, Longitude=@Longitude, Modified_by=@Created_by, Modified_dt=GETDATE()               
where intStateid like @intStateid            
select 1            
return 1            
end            
end            
else            
begin            
if((select COUNT(*) from tm_States where StateName=@StateName )>0)            
begin            
select 999            
return 999            
end            
else            
begin            
insert into tm_States(NoofCounties,Population,StateName, StateCode, Latitude, Longitude, Created_by, Created_dt)            
values(@NoofCounties,@Population,@StateName, @StateCode, @Latitude, @Longitude, @Created_by,GETDATE())            
select @@IDENTITY            
return @@IDENTITY            
end            
end            
end




