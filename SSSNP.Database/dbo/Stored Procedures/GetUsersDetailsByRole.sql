--created by siva as 04-1-2016            
--Purpose: to get Work Proposals                
                
create procedure [dbo].[GetUsersDetailsByRole]
(                
@role varchar(10)                
)                
as begin                
                
	select *
	from tbl_users 
	where User_type =@role

end