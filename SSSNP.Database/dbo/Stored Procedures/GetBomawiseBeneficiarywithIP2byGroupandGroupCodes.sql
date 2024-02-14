CREATE Procedure [dbo].[GetBomawiseBeneficiarywithIP2byGroupandGroupCodes]     
(     
@intBomasid varchar(10) ,    
@IP varchar(10),  
@workcode varchar(20) ,
@grouptype varchar(20),
@groupcode varchar(20)     
)     
as begin 

-- Modified by: Srikanth
-- Modified dt : 08-03-2018    
 
Declare @Allocationdays as int
 
set @Allocationdays = (select top 1 Allocationdays from  td_AllocationDaysDet order by intNoofDays desc)
 
select tbl_BeneficiaryDet.*
		,tm_Bomas.BomaName
		,case when tbl_BeneficiaryDet.Sex='M' then 'Male' else 'Female' end as gender 
from dbo.tbl_BeneficiaryDet 
left outer join tm_Bomas 
	on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid     
where
tbl_BeneficiaryDet.BeneficiaryType like @grouptype 
and tbl_BeneficiaryDet.GroupId like @groupcode 
and tbl_BeneficiaryDet.WorkCode is null  
and  tbl_BeneficiaryDet.intBomasid like @intBomasid 
and intIP like @IP   

union 

select tbl_BeneficiaryDet.*
		,tm_Bomas.BomaName
		,case when tbl_BeneficiaryDet.Sex='M' then 'Male' else 'Female' end as gender 
from dbo.tbl_BeneficiaryDet
inner join tr_BenfWorkDet 
	on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid
inner join tbl_WorkMaster 
	on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid
left outer join tm_Bomas 
	on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid     
where 
tbl_BeneficiaryDet.BeneficiaryType like @grouptype 
and tbl_BeneficiaryDet.GroupId like @groupcode 
and SelectionCriteria <> 'Component 2 (Direct Income Support)'
and
(  
	tbl_WorkMaster.Status='Completed'  
		and
		(select COUNT(ta.intAttendanceid) 
		from tr_BenAttendanceDet as ta 
		where ta.intBenid=tbl_BeneficiaryDet.intBenid 
		and ta.intWorkActivityId = tbl_WorkMaster.intWorkid 
		and ta.Status='Present' )<@Allocationdays)
and tbl_BeneficiaryDet.intBomasid like @intBomasid 
and tbl_BeneficiaryDet.intIP like @IP   

union

select tbl_BeneficiaryDet.*
		,tm_Bomas.BomaName
		,case when tbl_BeneficiaryDet.Sex='M' then 'Male' else 'Female' end as gender 
from dbo.tbl_BeneficiaryDet 
left outer join tm_Bomas 
	on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid  
inner join tr_BenfWorkDet 
	on  tr_BenfWorkDet.intBenid= tbl_BeneficiaryDet.intBenid 
where   
tbl_BeneficiaryDet.BeneficiaryType like @grouptype 
and tbl_BeneficiaryDet.GroupId like @groupcode 
and tr_BenfWorkDet.intWorkid like @workcode 
and tbl_BeneficiaryDet.intBomasid like @intBomasid 
and intIP like @IP       
and SelectionCriteria <> 'Component 2 (Direct Income Support)'
end



