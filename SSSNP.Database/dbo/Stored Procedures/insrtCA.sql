--created by suresh as on 1-17-2016    
CREATE Procedure [dbo].[insrtCA]              
(              
@intCountyAdm int,           
@CountyAdmName varchar(105),        
@intStateid int,        
@intCountieid int,              
@Address varchar(50),              
@Email varchar(20),              
@ContactNo varchar(15),        
@Created_by int   ,        
@User_id varchar(20),        
@Password varchar(20)               
)              
as begin              
              
              
if(@intCountyAdm>0)              
begin              
if((select COUNT(*) from td_CountyAdmDet where CountyAdmName=@CountyAdmName and intCountyAdm<> @intCountyAdm)>0)              
begin              
select 999              
return 999              
end              
else              
begin              
update dbo.td_CountyAdmDet set CountyAdmName=@CountyAdmName,intStateid=@intStateid,intCountieid=@intCountieid,Address=@Address, Email=@Email, ContactNo=@ContactNo,Modified_by=@Created_by, Modified_dt=GETDATE()                 
where intCountyAdm like @intCountyAdm              
select 1              
return 1              
end              
end              
else              
begin              
if((select COUNT(*) from td_CountyAdmDet where CountyAdmName=@CountyAdmName and intCountieid=@intCountieid)>0)              
begin              
select 999              
return 999              
end              
else              
begin          
declare @ids int            
insert into td_CountyAdmDet(CountyAdmName,intStateid,intCountieid,[Address],Email,ContactNo,Created_by, Created_dt)              
values(@CountyAdmName,@intStateid,@intCountieid,@Address, @Email, @ContactNo,@Created_by, GETDATE())              
--select @@IDENTITY              
--return @@IDENTITY        
select @ids=@@identity                                       
-- insert into tbl_Users                                      
--(                                      
-- User_id, Password, User_name, User_level, User_type, User_code, Status,  Created_by, Created_dt                                    
--)                                      
--values                                       
--(                                      
-- @User_id, @Password, @CountyAdmName, '6', 'CA', @ids, 'Y',  @Created_by, GETDATE()                                      
--)                    
    
select @ids          
return @ids        
        
              
end              
end              
end




