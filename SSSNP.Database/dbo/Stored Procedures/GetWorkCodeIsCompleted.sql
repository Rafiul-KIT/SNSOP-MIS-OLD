CREATE Procedure [dbo].[GetWorkCodeIsCompleted](@WorkCode varchar(100))
as
select * from tbl_WorkMaster where WorkCode=@WorkCode and Status !='Completed'


