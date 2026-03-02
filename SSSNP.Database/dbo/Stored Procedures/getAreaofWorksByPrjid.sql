CREATE Procedure [dbo].[getAreaofWorksByPrjid]  
(  
@intPrjid varchar(10)  
)  
as begin  
select distinct tr_ProjectTarget.intAreaofWork,tm_AreaofWork.AreaofWork from dbo.tr_ProjectTarget   
inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProjectTarget.intAreaofWork  
 where intPrjid like @intPrjid  
 end




