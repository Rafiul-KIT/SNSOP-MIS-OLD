CREATE Procedure [dbo].[getWorkActivitiesByProjects]
(
@intPrjid varchar(10),
@intAreaofWork varchar(20)
)
as begin
select distinct tr_ProjectTarget.intWorkActivity,tm_WorkActivities.WorkActivity from dbo.tr_ProjectTarget 
inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProjectTarget.intAreaofWork
inner join tm_WorkActivities on tm_WorkActivities.intWorkActivity=tr_ProjectTarget.intWorkActivity
 where intPrjid like  @intPrjid and tm_AreaofWork.intAreaofWork like @intAreaofWork
 end




