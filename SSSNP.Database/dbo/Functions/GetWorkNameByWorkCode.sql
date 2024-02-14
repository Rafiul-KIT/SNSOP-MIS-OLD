
CREATE function [dbo].[GetWorkNameByWorkCode] 
( @workcode int
)
RETURNS varchar(100)
AS
BEGIN
	
	DECLARE @WorkName as varchar(100)
	set @WorkName=(select top 1 isnull(WorkName,'') from tr_ProposedWorkAct where intWorkid=@workcode and WorkName is not null)
	return @WorkName

END




