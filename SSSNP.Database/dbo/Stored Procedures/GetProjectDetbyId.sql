CREATE Procedure [dbo].[GetProjectDetbyId]  
(  
@intPrjid varchar(10)  
)  
as begin  
  
select * from td_ProjectDet where intPrjid like @intPrjid 

select distinct s.intStateid,s.StateName from  tr_ProjectTarget inner join tm_States as s on s.intStateid=tr_ProjectTarget.intStateid where intPrjid like @intPrjid order by s.StateName asc
  
end




