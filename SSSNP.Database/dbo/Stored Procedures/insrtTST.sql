--created by suresh as on 1-17-2016 for insert tst    
CREATE Procedure [dbo].[insrtTST]            
(            
@intTST int,         
@Ministry varchar(15),      
@TstName varchar(50),      
@intStateid int,      
@intCountieid int,      
@intPayamid int,            
@Address varchar(50),            
@Email varchar(20),            
@ContactNo varchar(15),            
@Type varchar(5),       
@intDesgid int,      
@Status varchar(20),       
@Created_by int   ,      
@User_id varchar(20),  
@User_type varchar(10),     
@Password varchar(50)  
-- @Password varchar(20)           
)            
as begin            
            
            
if(@intTST>0)            
begin            
if((select COUNT(*) from td_TstTeamDet where TstName=@TstName and @intTST<> @intTST)>0)            
begin            
select 999            
return 999            
end            
else            
begin            
update dbo.td_TstTeamDet set Ministry=@Ministry,TstName=@TstName,intStateid=@intStateid,@User_type=@User_type, intCountieid=@intCountieid,intPayamid=@intPayamid, Address=@Address, Email=@Email, ContactNo=@ContactNo, Type=@Type,intDesgid=@intDesgid,Status=@Status, Modified_by=@Created_by, Modified_dt=GETDATE()               
where intTST like @intTST            
select 1            
return 1            
end            
end            
else            
begin            
if((select COUNT(*) from td_TstTeamDet where TstName=@TstName and Email=@Email and intPayamid=@intPayamid)>0)            
begin            
select 999            
return 999            
end            
else            
begin        
declare @ids int          
insert into td_TstTeamDet(Ministry,TstName,intStateid,intCountieid,intPayamid,[Address] ,Email,ContactNo,[Type],intDesgid,[Status], Created_by, Created_dt)            
values(@Ministry,@TstName,@intStateid,@intCountieid,@intPayamid,@Address, @Email, @ContactNo, @Type,@intDesgid,@Status, @Created_by, GETDATE())            
--select @@IDENTITY            
--return @@IDENTITY      
select @ids=@@identity                                     
 insert into tbl_Users                                    
(                                    
 User_id, Password, User_name, User_level, User_type, User_code, Status,  Created_by, Created_dt                                  
)                                    
values                                     
(                                    
 @User_id, @Password, @TstName, '3', @User_type, @ids, 'Y',  @Created_by, GETDATE()                                    
)                  
select @ids     
return @ids      
      
            
end            
end            
end




