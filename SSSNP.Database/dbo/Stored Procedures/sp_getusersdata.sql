
CREATE procedure [dbo].[sp_getusersdata]        --exec sp_getusersdata 'ip'
@Type varchar(50)
as
begin
select * from tbl_Users where User_type like '%'+@Type+'%'
end


