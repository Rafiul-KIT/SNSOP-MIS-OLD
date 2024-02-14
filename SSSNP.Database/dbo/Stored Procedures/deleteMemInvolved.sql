CREATE Procedure [dbo].[deleteMemInvolved]
(
@intWorkid varchar(10),
@MemType varchar(10)
)
as begin

delete from tr_MemsInvolved where MemType like @MemType and intWorkid like @intWorkid

select 1
return 1
end




