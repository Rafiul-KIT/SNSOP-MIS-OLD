CREATE Procedure [dbo].[insrtIP]                  
(    
@SPerson varchar(50),
@SEmail varchar(50),              
@intIP int,                  
@IPName varchar(50),                  
@TypeofIP varchar(1),                  
@AuthorisedPerson varchar(50),                  
@ContactNo varchar(15),          
@SecondaryContactNo varchar(15),          
@Email varchar(35),          
@Address varchar(150),          
@Website varchar(50),          
@Created_by int ,           
@User_id varchar(20),          
@Password varchar(50),      
--@Password varchar(20),      
@intTST varchar(20),  
@SecondaryAuthorisedPerson varchar(50)                 
)                  
As                                          
BEGIN                                          
if(@intIP>0)                  
begin                  
if((select COUNT(*) from dbo.td_IPDetails where IPName=@IPName and intIP<> @intIP)>0)                  
begin                  
select 999                  
return 999                  
end                  
else                  
begin                  
update dbo.td_IPDetails set SEmail=@SEmail,SPerson=@SPerson, SecondaryAuthorisedPerson=@SecondaryAuthorisedPerson, IPName=@IPName, TypeofIP=@TypeofIP,AuthorisedPerson=@AuthorisedPerson, ContactNo=@ContactNo,SecondaryContactNo=@SecondaryContactNo,Email=@Email,[Address]=@Address,Website=@Website, Modified_by=@Created_by, Modified_dt=GETDATE(  
)      
        
                     
where intIP like @intIP                  
select 1                  
return 1                  
end                  
end                  
else                  
begin                  
if((select COUNT(*) from td_IPDetails where IPName=@IPName )>0)                  
begin                  
select 999                  
return 999                  
end                  
else                  
begin             
DECLARE @ids  int               
insert into td_IPDetails(SPerson,SEmail,SecondaryAuthorisedPerson,intTST,IPName, TypeofIP, AuthorisedPerson, ContactNo,SecondaryContactNo,Email,[Address] ,Website,Created_by, Created_dt)                  
values(@SPerson,@SEmail,@SecondaryAuthorisedPerson,@intTST,@IPName, @TypeofIP, @AuthorisedPerson, @ContactNo,@SecondaryContactNo,@Email,@Address,@Website,@Created_by,GETDATE())                  
--select @@IDENTITY                  
--return @@IDENTITY            
          
select @ids=@@identity                                         
 insert into tbl_Users                                        
(                                        
 User_id, Password, User_name, User_level, User_type, User_code, Status,  Created_by, Created_dt                                      
)                                        
values                                         
(                                        
 @User_id, @Password, @IPName, '3', 'IP', @ids, 'Y',  @Created_by, GETDATE()                                        
)                      
select @ids             
return @ids          
end                  
end                  
end




