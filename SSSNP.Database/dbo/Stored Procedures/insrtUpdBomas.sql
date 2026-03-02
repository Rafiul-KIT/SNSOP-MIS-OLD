CREATE Procedure [dbo].[insrtUpdBomas]          
(          
@intBomasid int,          
@intPayamid int,          
@BomaCode varchar(5),          
@BomaName varchar(50),          
@Latitude varchar(15),          
@Longitude varchar(15),          
@Created_by int,
@Population int           
)          
as begin          
   
   declare @Exist_Boma_Code varchar(5)
     
          
if(@intBomasid>0)          
begin          
if((select COUNT(*) from tm_Bomas where BomaName=@BomaName and intBomasid<> @intBomasid)>0)          
begin          
select 999          
return 999          
end          
else          
begin       

set @Exist_Boma_Code = (select BomaCode from tm_Bomas where BomaCode=@BomaCode and intBomasid!=@intBomasid)
if(@Exist_Boma_Code!='')
BEGIN
	select 3
	return 3 -- duplicacy 
END
ELSE
BEGIN
update dbo.tm_Bomas set Population=@Population, intPayamid=@intPayamid, BomaCode=@BomaCode, BomaName=@BomaName, Latitude=@Latitude, Longitude=@Longitude, Modified_by=@Created_by, Modified_dt=GETDATE()             
where intBomasid like @intBomasid 
select 1          
return 1 
END
         
end          
end          
else          
begin          
if((select COUNT(*) from tm_Bomas where BomaName=@BomaName and intPayamid=@intPayamid)>0)          
begin          
select 999          
return 999          
end          
else          
begin  

set @Exist_Boma_Code = (select BomaCode from tm_Bomas where BomaCode=@BomaCode)
if(@Exist_Boma_Code!='')
BEGIN
	select 3
	return 3 -- duplicacy 
END
ELSE
BEGIN

	insert into tm_Bomas(Population,intPayamid, BomaCode, BomaName, Latitude, Longitude, Created_by, Created_dt)          
values(@Population,@intPayamid, @BomaCode, @BomaName, @Latitude, @Longitude, @Created_by, GETDATE())   
Declare @ids as int
set @ids=  @@IDENTITY 

--Declare @cnt as int
--set @cnt=1
--while(@cnt<50)
-- Begin
-- insert into GroupMaster
-- (

-- )
 
 --End

 
 insert into GroupMaster
 (
 State, County, Payam, Boma, GroupType, GroupCode, GroupStrength, Created_by, Created_dt
 )

select tm_States.intStateid,tm_Counties.intCountieid,tm_Payams.intPayamid,tm_Bomas.intBomasid,'Group',  tdGroupNumbers.GroupNumbers,25,'1000',getdate() from tm_Bomas  inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
inner join tm_States on tm_States.intStateid=tm_Counties.intStateid cross join tdGroupNumbers where tm_Bomas.intBomasid=@ids


 insert into GroupMaster
 (
 State, County, Payam, Boma, GroupType, GroupCode, GroupStrength, Created_by, Created_dt
 )

select tm_States.intStateid,tm_Counties.intCountieid,tm_Payams.intPayamid,tm_Bomas.intBomasid,'CST',  NULL,12,'1000',getdate() from tm_Bomas  inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
inner join tm_States on tm_States.intStateid=tm_Counties.intStateid where tm_Bomas.intBomasid=@ids




insert into GroupMaster
(
State, County, Payam, Boma, GroupType, GroupCode, GroupStrength, Created_by, Created_dt
)

select tm_States.intStateid, tm_Counties.intCountieid, tm_Payams.intPayamid, tm_Bomas.intBomasid,'Group',tdGroupNumbers.GroupNumbers,'25','1000',getdate() from tm_Bomas inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
cross join tdGroupNumbers
where tm_Bomas.intBomasid  not in (select g.Boma from GroupMaster as g)

insert into GroupMaster
(
State, County, Payam, Boma, GroupType, GroupCode, GroupStrength, Created_by, Created_dt
)
select tm_States.intStateid, tm_Counties.intCountieid, tm_Payams.intPayamid, tm_Bomas.intBomasid,'CST',null,'12','1000',getdate() from tm_Bomas inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
inner join tm_States on tm_States.intStateid=tm_Counties.intStateid

where tm_Bomas.intBomasid not in (select g.Boma from GroupMaster as g where g.GroupType='CST')







    
select @ids          
return @ids 
END   
end          
end          
end




