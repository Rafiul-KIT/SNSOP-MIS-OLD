CREATE Procedure [dbo].[getProjectDetLookupSearch]    
(    
@intIP varchar(10),    
@PrjName varchar(50)  ,

@intStateid varchar(10),                    
@intCountieid varchar(10),                    
@intPayamid varchar(10),                    
@intBomasid varchar(10),
@Status varchar(50) ,
@year varchar(50)
)    
as begin    
  if(@year!='')    
   begin  
   select  distinct p.intPrjid as Projid,p.* from td_ProjectDet p
    left join tr_ProjectTarget w
   on p.intPrjid=w.intPrjid
   
   where  
   
    intIP like @intIP and  p.PrjName like '%'+@PrjName+'%'  and p.Status like @Status
	 and ( (YEAR (p.EndDate) >= @year) and (YEAR (p.StartDate) <= @year )) 

	 and w.intStateid like @intStateid and w.intCountieid like @intCountieid and w.intPayamid like @intPayamid                    
        and w.intBomasid  like @intBomasid 
    end
	else
	begin
	select distinct p.intPrjid as Projid,p.* from td_ProjectDet p 
	left join tr_ProjectTarget w
   on p.intPrjid=w.intPrjid
   
   where  
    --distinct p.intPrjid as Projid,
    intIP like @intIP and  p.PrjName like '%'+@PrjName+'%'  and p.Status like @Status

	 and w.intStateid like @intStateid and w.intCountieid like @intCountieid and w.intPayamid like @intPayamid                    
        and w.intBomasid  like @intBomasid 
	end
end




