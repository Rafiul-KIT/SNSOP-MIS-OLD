--created by suresh for Payam Development Name Insertion,updation          
          
CREATE Procedure [dbo].[insrtPDC]                    
(                    
@intPDC int,                 
@PDCName varchar(105),              
@intStateid int,              
@intCountieid int,          
@intPayamid int,                 
@Address varchar(50),                    
@Email varchar(20),                    
@Contact_No varchar(15),         
@Status varchar(5),             
@Created_by int   ,              
@User_id varchar(20),              
@Password varchar(20)                     
)                    
as begin                    
                    
                    
if(@intPDC>0)                    
begin                    
if((select COUNT(*) from td_PDCDet where PDCName=@PDCName and intPDC<> @intPDC)>0)                    
begin                    
select 999                    
return 999                    
end                    
else                    
begin                    
update dbo.td_PDCDet set PDCName=@PDCName,intStateid=@intStateid,intPayamid=@intPayamid,intCountieid=@intCountieid,Address=@Address, Email=@Email,Contact_No=@Contact_No,Status=@Status,Modified_by=@Created_by, Modified_dt=GETDATE()                       
where intPDC like @intPDC                    
select 1                    
return 1                    
end                    
end                    
else                    
begin                    
if((select COUNT(*) from td_PDCDet where PDCName=@PDCName and intPayamid=@intPayamid)>0)                    
begin                    
select 999                    
return 999                    
end                    
else                    
begin                
declare @ids int                  
insert into td_PDCDet(PDCName,intStateid,intCountieid,intPayamid,[Address],Email,Contact_No,Status,Created_by, Created_dt)                    
values(@PDCName,@intStateid,@intCountieid,@intPayamid,@Address, @Email, @Contact_No,@Status,@Created_by, GETDATE())                    
--select @@IDENTITY                    
--return @@IDENTITY              
select @ids=@@identity                                             
-- insert into tbl_Users                                            
--(                                            
-- User_id, Password, User_name, User_level, User_type, User_code, Status,Created_by, Created_dt                                          
--)                                            
--values                                             
--(                                            
-- @User_id, @Password, @PDCName, '5', 'PDC', @ids, 'Y',  @Created_by, GETDATE()                                            
--)                          
select @ids                           
return @ids              
              
                    
end                    
end                    
end




