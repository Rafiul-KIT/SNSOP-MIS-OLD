CREATE Procedure[dbo].[GetPW1]
(
@intMobilization varchar(10)
)
as begin


select * from dbo.tr_PW where intMobilization like @intMobilization
end




