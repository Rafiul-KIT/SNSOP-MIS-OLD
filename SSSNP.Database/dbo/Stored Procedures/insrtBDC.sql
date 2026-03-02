--alter Procedure as on 14-1-2016 for insert,update Boma development comitte        
CREATE Procedure [dbo].[insrtBDC]                    
(                    
@intBDC int,                 
@BDCName varchar(105),              
@intStateid int,              
@intCountieid int,          
@intPayamid int,                 
@Address varchar(150),                    
@Email varchar(50),                    
@Contact_No varchar(15),          
@Status varchar(5),            
@Created_by int   ,              
@User_id varchar(50),              
@Password varchar(50)                     
)                    
as begin                    
                    
                    
if(@intBDC>0)                    
begin                    
if((select COUNT(*) from td_BDCDet where BDCName=@BDCName and intBDC<> @intBDC)>0)                    
begin                    
select 999                    
return 999                    
end                    
else                    
begin                    
update dbo.td_BDCDet set BDCName=@BDCName,intStateid=@intStateid,intPayamid=@intPayamid,intCountieid=@intCountieid,Address=@Address, Email=@Email,Contact_No=@Contact_No,Status=@Status,Modified_by=@Created_by, Modified_dt=GETDATE()                       
where intBDC like @intBDC        
select 1                    
return 1                    
end                    
end                    
else                    
begin                    
if((select COUNT(*) from td_BDCDet where BDCName=@BDCName and intPayamid=@intPayamid)>0)                    
begin                    
select 999                    
return 999                    
end                    
else                    
begin                
declare @ids int                  
insert into td_BDCDet(BDCName,intStateid,intCountieid,intPayamid,[Address],Email,Contact_No,Status,Created_by, Created_dt)                    
values(@BDCName,@intStateid,@intCountieid,@intPayamid,@Address, @Email, @Contact_No,@Status,@Created_by, GETDATE())                    
--select @@IDENTITY                    
--return @@IDENTITY              
select @ids=@@identity                                             
-- insert into tbl_Users                                            
--(                                            
-- User_id, Password, User_name, User_level, User_type, User_code, Status,  Created_by, Created_dt                                          
--)                                            
--values                                             
--(                                            
-- @User_id, @Password, @BDCName, '4', 'BDC', @ids, 'Y',  @Created_by, GETDATE()                                            
--)                          
select @ids                   
return @ids              
              
                    
end                    
end                    
end




