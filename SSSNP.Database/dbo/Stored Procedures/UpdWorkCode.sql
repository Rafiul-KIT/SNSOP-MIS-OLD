CREATE Procedure [dbo].[UpdWorkCode]            
            
(            
@intWorkid varchar(50),            
@WorkCode varchar(120)            
)            
as begin            
            
update dbo.tbl_WorkMaster set WorkCode=@WorkCode where intWorkid like @intWorkid
            
            
end




