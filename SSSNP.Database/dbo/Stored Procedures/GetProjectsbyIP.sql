CREATE Procedure [dbo].[GetProjectsbyIP]
(
@intIP varchar(10)
)
as begin

select td_ProjectDet.* from td_ProjectDet 

	inner join tr_ProjectTarget w
   on td_ProjectDet.intPrjid=w.intPrjid 
   
   where intIP like @intIP  


   --select * from tr_ProjectTarget where intPrjid not in (select a.intPrjid from td_ProjectDet as a)

end




