CREATE Procedure [dbo].[InsUpdProjectDet]        
(        
@intPrjid int,  
@intIP int,  
@PrjName varchar(50),  
@StartDate datetime,  
@EndDate datetime,  
@PrjSanctionDate datetime,  
@TargetBeneficiary varchar(50),  
@ProjectCost decimal(18, 2),  
@PrjInchargeName varchar(50),  
@PrjInchargeMobile varchar(15),  
@PrjInchargeEmail varchar(50),  
@Status varchar(50),  
@Created_by int         
)        
as begin        
        
        
if(@intPrjid>0)        
begin        
if((select COUNT(*) from td_ProjectDet where PrjName=@PrjName and intPrjid<> @intPrjid)>0)        
begin        
select 999        
return 999        
end        
else        
begin        
update dbo.td_ProjectDet set intIP=@intIP, PrjName=@PrjName, StartDate=@StartDate, EndDate=@EndDate, PrjSanctionDate=@PrjSanctionDate, TargetBeneficiary=@TargetBeneficiary, ProjectCost=@ProjectCost, PrjInchargeName=@PrjInchargeName, PrjInchargeMobile=@PrjInchargeMobile, PrjInchargeEmail=@PrjInchargeEmail,Status=@Status, Modified_by=@Created_by, Modified_dt=GETDATE()           
where intPrjid like @intPrjid        
select 1        
return 1        
end        
end        
else        
Begin        
if((select COUNT(*) from td_ProjectDet where PrjName=@PrjName )>0)        
begin        
select 999        
return 999        
end        
else        
begin        
insert into dbo.td_ProjectDet (intIP, PrjName, StartDate, EndDate, PrjSanctionDate, TargetBeneficiary, ProjectCost, PrjInchargeName, PrjInchargeMobile, PrjInchargeEmail,Status, Created_by, Created_dt)  
values(@intIP,@PrjName,@StartDate,@EndDate,@PrjSanctionDate,@TargetBeneficiary,@ProjectCost,@PrjInchargeName,@PrjInchargeMobile,@PrjInchargeEmail,@Status,@Created_by,GETDATE())                        
select @@IDENTITY        
return @@IDENTITY        
end        
end        
end




