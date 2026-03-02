
CREATE procedure [dbo].[GetWorkCodeIsThere](@WorkCode varchar(100))
as
select * from tbl_WorkMaster where WorkCode=@WorkCode 


