CREATE procedure [dbo].[GetBomawiseBeneficiarywithIP2ALL]   -- GetBomawiseBeneficiarywithIP2ALL '%','%','%' 
(     
@intBomasid varchar(10) ,    
@IP varchar(10),  
@workcode varchar(20)      
)     
as begin     
 
 Declare @Allocationdays as int
 
 
 --set @Allocationdays=60
 
 set @Allocationdays = (select top 1 Allocationdays from  td_AllocationDaysDet order by intNoofDays desc)
 
select tbl_BeneficiaryDet.*,tm_Bomas.BomaName,case when tbl_BeneficiaryDet.Sex='M' then 'Male' else 'Female' end as gender from dbo.tbl_BeneficiaryDet left outer join     
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid     
    
   
where   
--(  
--(tbl_BeneficiaryDet.intBenid not in (select tr_BenfWorkDet.intBenid from tr_BenfWorkDet) ) 


--) and  
tbl_BeneficiaryDet.intBomasid like @intBomasid and intIP like @IP    and tbl_BeneficiaryDet.intBenid not in (select tr_BenDropdoutList.intBenid from tr_BenDropdoutList)
union 
select tbl_BeneficiaryDet.*,tm_Bomas.BomaName,case when tbl_BeneficiaryDet.Sex='M' then 'Male' else 'Female' end as gender from dbo.tbl_BeneficiaryDet
inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid
inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid
 left outer join     
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid     
    
   
where   
(  

tbl_WorkMaster.Status='Completed' 

-- and
--(select COUNT(ta.intAttendanceid) from tr_BenAttendanceDet as ta where ta.intBenid=tbl_BeneficiaryDet.intBenid and ta.intWorkActivityId = tbl_WorkMaster.intWorkid and ta.Status='Present' )<=@Allocationdays

)

 and  
tbl_BeneficiaryDet.intBomasid like @intBomasid and tbl_BeneficiaryDet.intIP like @IP    and tbl_BeneficiaryDet.intBenid not in (select tr_BenDropdoutList.intBenid from tr_BenDropdoutList)
    
end

