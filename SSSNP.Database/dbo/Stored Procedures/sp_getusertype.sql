
CREATE procedure [dbo].[sp_getusertype]
as
begin
select distinct  User_type from tbl_Users
end

