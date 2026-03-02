--created by siva as 22-1-2016
--Purpose: to Get Ips by TST in Dropdown
      
CREATE Procedure [dbo].[GetIpsByTST]  
(      
@Created_by varchar(10)      
)      
as begin      
      
select * from dbo.td_IPDetails 
--where Created_by like @Created_by
      
end




