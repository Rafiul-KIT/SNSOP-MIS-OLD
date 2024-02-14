CREATE Procedure [dbo].[GetBomawiseBeneficiarywithIP2]     
(     
@intBomasid varchar(10) ,    
@IP varchar(10),  
@workcode varchar(20)      
)     
as begin     


 
Declare @Allocationdays as int
set @Allocationdays = (select top 1 Allocationdays from  td_AllocationDaysDet order by intNoofDays desc)

select tbl_BeneficiaryDet.*
		,tm_Bomas.BomaName
		,case when tbl_BeneficiaryDet.Sex='M' then 'Male' else 'Female' end as gender
		,'0' as Workid 
from dbo.tbl_BeneficiaryDet 
left outer join tm_Bomas 
	on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid     
where   
(  
	tbl_BeneficiaryDet.WorkCode is null 
) 
and SelectionCriteria <> 'Component 2 (Direct Income Support)'
and  tbl_BeneficiaryDet.intBomasid like @intBomasid 
and intIP like @IP   
union 

select tbl_BeneficiaryDet.*
		,tm_Bomas.BomaName
		,case when tbl_BeneficiaryDet.Sex='M' then 'Male' else 'Female' end as gender
		,'1' as Workid from dbo.tbl_BeneficiaryDet
--inner join tr_BenfWorkDet 
--		on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid
--inner join tbl_WorkMaster 
--		on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid
inner join tbl_WorkMaster 
		on tbl_WorkMaster.intWorkid=tbl_BeneficiaryDet.WorkCode		
left outer join tm_Bomas 
		on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid     
where   
(  
	tbl_WorkMaster.Status='Completed'  
)
and SelectionCriteria <> 'Component 2 (Direct Income Support)'
and tbl_BeneficiaryDet.intBomasid like @intBomasid 
and tbl_BeneficiaryDet.intIP like @IP   
union

select tbl_BeneficiaryDet.*
		,tm_Bomas.BomaName	
		,case when tbl_BeneficiaryDet.Sex='M' then 'Male' else 'Female' end as gender
		,'1' as Workid from dbo.tbl_BeneficiaryDet 
left outer join tm_Bomas 
		on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid  
inner join tr_BenfWorkDet 
		on  tr_BenfWorkDet.intBenid= tbl_BeneficiaryDet.intBenid 
where   
(  
	tr_BenfWorkDet.intWorkid like @workcode 
) 
and SelectionCriteria <> 'Component 2 (Direct Income Support)'
and  tbl_BeneficiaryDet.intBomasid like @intBomasid 
and intIP like @IP  
--Drop table #Attedancedet   
end


--3435 yei