CREATE Procedure [dbo].[chkpwd]
(
@user_id varchar(50)
)
as begin

if((select COUNT(*) from tbl_users where USER_ID like @user_id and modified_dt is null)>0)
 begin

select 1000
return 1000
end
else if((select count(*) from tbl_users where USER_ID like @user_id and  modified_dt + 30 < GETDATE())>0)
begin 
select 99
return 99

end
else 
begin
select 123
return 123
end
end




