CREATE function [dbo].[GetUserNameById] 
( @intUserid int
)
RETURNS varchar(100)
AS
BEGIN
	declare @Stat varchar(100)
	set @Stat=(select top 1 isnull(User_id,'') from tbl_Users where intUserid=@intUserid )
	
	return @Stat

END


