CREATE Procedure [dbo].[sp_insertpayingagent]                     
@intpaid int,              
@PAname varchar(150),      
@intStateid int,      
@intCountieid int,      
@intPayamid int,            
@Address varchar(150),            
@Email varchar(20),            
@ContactNo varchar(15),       
@Status varchar(20),       
@Created_by int   ,      
@User_id varchar(20),  
@User_type varchar(10),     
@Password varchar(20)            
as begin 

if(@intpaid>0)            
begin            
if((select COUNT(*) from td_PayingAgent where PAname=@PAname and @intpaid<> @intpaid)>0)            
begin            
select 999            
return 999            
end   
else            
begin            
update td_PayingAgent set PAname=@PAname,intStateid=@intStateid,intCountieid=@intCountieid,intPayamid=@intPayamid, Address=@Address, Email=@Email, ContactNo=@ContactNo,Status=@Status, Modified_by=@Created_by, Modified_dt=GETDATE()               
where intpaid like @intpaid            
select 1            
return 1            
end 
end
else
begin
declare @ids int          
insert into td_PayingAgent (PAname,intStateid,intCountieid,intPayamid,Address ,Email,ContactNo,Status, Created_by, Created_dt)            
values(@PAname,@intStateid,@intCountieid,@intPayamid,@Address, @Email, @ContactNo,@Status, @Created_by, GETDATE())            
--select @@IDENTITY            
--return @@IDENTITY      
select @ids=@@identity                                     
 insert into tbl_Users                                    
(                                    
 User_id, Password, User_name, User_level, User_type, User_code, Status,  Created_by, Created_dt                                  
)                                    
values                                     
(                                    
 @User_id, @Password, @PAname, '3', @User_type, @ids, 'Y',  @Created_by, GETDATE()                                    
)                  
select @ids     
return @ids           
end 
end


