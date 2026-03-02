
CREATE procedure [dbo].[sp_ResetUseridPassword]        --exec sp_ResetUseridPassword 1073
@intUserid varchar(50)
as
begin

update tbl_Users set Password ='fba777e32d1f3aca810286e670f927a4' where intUserid like @intUserid
--update tbl_Users set Password ='SNSDP123' where intUserid like @intUserid

return 1
end

