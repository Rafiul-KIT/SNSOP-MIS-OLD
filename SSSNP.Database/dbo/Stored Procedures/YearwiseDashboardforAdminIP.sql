--exec YearwiseDashboardforAdminIP '2018'  ,'1005'
CREATE Procedure [dbo].[YearwiseDashboardforAdminIP]          
(            
@Year varchar(10) ,
@IPid varchar(8)           
)            
as begin            

if(@Year='ALL')
begin


select 'No. of Implementing Partners' as [Profession],td_IPDetails.IPName as cnt  from td_IPDetails  where td_IPDetails.intIp like @IPid             
        
            
create table #tblWorks1
(
intWorkid int,
intStateid int,
intCountieid int,
intPayamid int,
intBomasid int,
IPid int,
Status varchar(30)
)   
insert into #tblWorks1
select intWorkid, intStateid,intCountieid,intPayamid,intBomasid,IPid,Status from    tbl_WorkMaster  
where IPid like @IPid
          
            
select 'No. of Implementing Counties' as [Profession],isnull(COUNT(distinct intCountieid),0) as cnt  from #tblWorks1      
        
            
select 'No. of Implementing Payams' as [Profession],isnull(COUNT(distinct intPayamid),0) as cnt  from #tblWorks1
            
        
            
select 'Number of Implementing Bomas' as [Profession],isnull(COUNT(distinct intBomasid),0) as cnt  from #tblWorks1         
            
        
            
--select 'Total no. of works proposed' as [Profession],isnull(COUNT(distinct intWorkid),0) as cnt  from tbl_WorkMaster            
--where (YEAR(Created_dt) = @Year)             
   
   select 'Total no. of works proposed' as [Profession], isnull(COUNT(distinct intWorkid),0) as cnt   from #tblWorks1
          
        
            
         
    select 'Total no.of works to be approved' as [Profession],isnull(COUNT(distinct intWorkid),0) as cnt  from #tblWorks1 where (Status like 'New Proposal')            
          
          
        select 'No. of works in progress' as [Profession],isnull(COUNT(distinct intWorkid),0) as cnt  from #tblWorks1 where  (Status like 'Inprogress' or Status like 'Invoice Appovals' or Status like 'Invoice Request'  or Status like 'Approved')            
            
--select 'No. of works in progress' as [Profession],        
--(        
--(        
--select COUNT(distinct intWorkid)   from tbl_WorkMaster            
--where (YEAR(Created_dt) = @Year)   ) -            
--(select COUNT(distinct intWorkid) as cnt from tbl_WorkMaster            
--where (YEAR(Created_dt) = @Year) and Status like 'Inprogress' )) as cnt        
             
            
            
        
            
select 'No. of works completed' as [Profession],isnull(COUNT(distinct intWorkid),0) as cnt  from #tblWorks1 where Status like 'Completed'             
            
           
            
select 'Funds disbursed' as [Profession],isnull(sum(tr_FinancialApproval.PaidAmount),0) as cnt  from tr_FinancialApproval            
           
            


create table #tblBenf1
(
intBenid int,
Sex varchar(8),
BeneficiaryType varchar(8)
)  
insert into #tblBenf1
select intBenid,Sex,BeneficiaryType from  tbl_BeneficiaryDet where intBenid in (select a.intBenid from   tr_BenfWorkDet as a)        
    and intIP like @IPid  
	  
create table #tblBenfAll1
(
intBenid int,
Sex varchar(8),
BeneficiaryType varchar(8)
)
insert into #tblBenfAll1  
select intBenid,Sex,BeneficiaryType from  tbl_BeneficiaryDet  where  intIP like @IPid    
       
            
select 'Direct Beneficiaries' as [Profession],isnull(COUNT(distinct intBenid),0) as cnt  from #tblBenf1           
            
             
            
            
        
            
select 'Pecentage of Female Beneficiaries' as [Profession],isnull(COUNT(distinct intBenid),0) as cnt  from #tblBenf1 where Sex like 'F'          
            
         
            
select 'Pecentage of Male Beneficiaries' as [Profession],isnull(COUNT(distinct intBenid),0) as cnt  from #tblBenf1 where Sex like 'M'          
            
            
        
            
--select 'No. of work days created' as [Profession],COUNT(distinct tr_BenAttendanceDet.AttendanceDate) as cnt  from tr_BenAttendanceDet            
--where (YEAR(Created_dt) = @Year) and Status like 'Present'     
    
--select 'No. of work days created' as [Profession],COUNT(distinct tr_BenAttendanceDet.intAttendanceid) as cnt      
-- from tr_BenAttendanceDet where tr_BenAttendanceDet.intBenid in (select intBenid from tbl_BeneficiaryDet where WorkCode is not null)  and  (YEAR(Created_dt) = @Year)     
-- and Status like 'Present'     
create table #tblPayment1
(
AttendanceDays int,
PaidAmount decimal(18,2),
PaidAmountUSD decimal(18,2),
WageRateUSD decimal(18,2),
ExchangeRate decimal(18,2),
BenCount int
)
insert into #tblPayment1
select sum(isnull(p.Attendancedays,0)), sum(isnull(p.PaidAmount,0)),sum(isnull(p.PaidAmountUSD,0)),sum(isnull(p.WageRateUSD,0)),sum(isnull(p.ExchangeRate,0)), count(p.intWageid)  from tr_BenWagesPayment as p where   p.intBenid in (select aa.intbenid from #tblBenf1 as aa)
and p.intWorkid in (select intworkid from #tblWorks1)

select 'No. of work days created' as [Profession],   ISNULL(SUM(AttendanceDays), 0) AS Cnt
FROM     #tblPayment1
    
        
        
select 'No. of Wages Paid SSP' as [Profession],isnull(sum(PaidAmount),0) as cnt  from #tblPayment1        
        
     select 'No. of works Rejected' as [Profession],COUNT(distinct intWorkid) as cnt  from #tblWorks1 where Status like 'Rejected'    


select 'No. of Wages Paid USD' as [Profession],isnull(sum(PaidAmountUSD),0) as cnt  from #tblPayment1
  
  --table 16
  select  COUNT(distinct intBenid) as cnt	 
 from #tblBenf1 where BeneficiaryType = 'Group'
 --table 17
select  COUNT(distinct intBenid) as cnt	 
 from #tblBenf1 where BeneficiaryType = 'CST'
 --table 18
select param_value from td_param
 --table 19
select isnull(sum(ProjectCost),0) AS ProjectCost from td_ProjectDet 

 --table 20
select isnull((WageRateUSD/case when BenCount=0 then 1 else BenCount end),0) AS Wage_Rate  from #tblPayment1

--table 21
select isnull( (ExchangeRate/case when BenCount=0 then 1 else BenCount end),0)  AS Exchange_Rate  from #tblPayment1            

--table 22
select isnull(Sum(Amount),0) As cnt from tbl_FundsUtilization where FundsUtilizationFor in ('Skilled labour','Tools','Materials')




--table 23
		        
select 'Enrolled Beneficiaries' as [Profession],COUNT(distinct intBenid) as cnt 
 from #tblBenfAll1

   --table 24         
select 'Enrolled of Female Beneficiaries' as [Profession],COUNT(distinct intBenid) as cnt  from #tblBenfAll1 
where 
Sex like 'F'   
    
	--table 25        
select 'Enrolled of Male Beneficiaries' as [Profession],COUNT(distinct intBenid) as cnt  from #tblBenfAll1  
where 
Sex like 'M'  


drop table #tblWorks1
drop table #tblBenf1
drop table #tblBenfAll1
drop table #tblPayment1

end
else

begin
select 'No. of Implementing Partners' as [Profession], td_IPDetails.IpName as cnt  from td_IPDetails       where td_IPDetails.intIp like @IPid           
           
            
create table #tblWorks
(
intWorkid int,
intStateid int,
intCountieid int,
intPayamid int,
intBomasid int,
IPid int,
Status varchar(30)

)   
insert into #tblWorks 
select intWorkid, intStateid,intCountieid,intPayamid,intBomasid,IPid,Status from    tbl_WorkMaster where    (YEAR(Created_dt) = @Year)  
    and  IPid like @IPid    
            
select 'No. of Implementing Counties' as [Profession],isnull(COUNT(distinct intCountieid),0) as cnt  from #tblWorks      
        
            
select 'No. of Implementing Payams' as [Profession],isnull(COUNT(distinct intPayamid),0) as cnt  from #tblWorks
            
        
            
select 'Number of Implementing Bomas' as [Profession],isnull(COUNT(distinct intBomasid),0) as cnt  from #tblWorks         
            
        
            
--select 'Total no. of works proposed' as [Profession],isnull(COUNT(distinct intWorkid),0) as cnt  from tbl_WorkMaster            
--where (YEAR(Created_dt) = @Year)             
   
   select 'Total no. of works proposed' as [Profession], isnull(COUNT(distinct intWorkid),0) as cnt   from #tblWorks
          
        
            
         
    select 'Total no.of works to be approved' as [Profession],isnull(COUNT(distinct intWorkid),0) as cnt  from #tblWorks where (Status like 'New Proposal')            
          
          
        select 'No. of works in progress' as [Profession],isnull(COUNT(distinct intWorkid),0) as cnt  from #tblWorks where  (Status like 'Inprogress' or Status like 'Invoice Appovals' or Status like 'Invoice Request'  or Status like 'Approved')            
            
--select 'No. of works in progress' as [Profession],        
--(        
--(        
--select COUNT(distinct intWorkid)   from tbl_WorkMaster            
--where (YEAR(Created_dt) = @Year)   ) -            
--(select COUNT(distinct intWorkid) as cnt from tbl_WorkMaster            
--where (YEAR(Created_dt) = @Year) and Status like 'Inprogress' )) as cnt        
             
            
            
        
            
select 'No. of works completed' as [Profession],isnull(COUNT(distinct intWorkid),0) as cnt  from #tblWorks where Status like 'Completed'             
            
           
            
select 'Funds disbursed' as [Profession],isnull(sum(tr_FinancialApproval.PaidAmount),0) as cnt  from tr_FinancialApproval            
where (YEAR(Created_dt) = @Year)            
            


create table #tblBenf
(
intBenid int,
Sex varchar(8),
BeneficiaryType varchar(8)
)  
insert into #tblBenf
select intBenid,Sex,BeneficiaryType from  tbl_BeneficiaryDet where intBenid in (select a.intBenid from   tr_BenfWorkDet as a)    and (YEAR(Created_dt) = @Year)      
and intIp like @IPid      
	  
create table #tblBenfAll
(
intBenid int,
Sex varchar(8),
BeneficiaryType varchar(8)
)
insert into #tblBenfAll  
select intBenid,Sex,BeneficiaryType from  tbl_BeneficiaryDet where  (YEAR(Created_dt) = @Year)      
    and intIp like @IPid     
            
select 'Direct Beneficiaries' as [Profession],isnull(COUNT(distinct intBenid),0) as cnt  from #tblBenf           
            
             
            
            
        
            
select 'Pecentage of Female Beneficiaries' as [Profession],isnull(COUNT(distinct intBenid),0) as cnt  from #tblBenf where Sex like 'F'          
            
         
            
select 'Pecentage of Male Beneficiaries' as [Profession],isnull(COUNT(distinct intBenid),0) as cnt  from #tblBenf where Sex like 'M'          
            
            
        
            
--select 'No. of work days created' as [Profession],COUNT(distinct tr_BenAttendanceDet.AttendanceDate) as cnt  from tr_BenAttendanceDet            
--where (YEAR(Created_dt) = @Year) and Status like 'Present'     
    
--select 'No. of work days created' as [Profession],COUNT(distinct tr_BenAttendanceDet.intAttendanceid) as cnt      
-- from tr_BenAttendanceDet where tr_BenAttendanceDet.intBenid in (select intBenid from tbl_BeneficiaryDet where WorkCode is not null)  and  (YEAR(Created_dt) = @Year)     
-- and Status like 'Present'     
create table #tblPayment
(
AttendanceDays int,
PaidAmount decimal(18,2),
PaidAmountUSD decimal(18,2),
WageRateUSD decimal(18,2),
ExchangeRate decimal(18,2),
BenCount int
)
insert into #tblPayment
select sum(isnull(p.AttendanceDays,0)), sum(isnull(p.PaidAmount,0)),sum(isnull(p.PaidAmountUSD,0)),sum(isnull(p.WageRateUSD,0)),sum(isnull(p.ExchangeRate,0)), count(p.intWageid)  from tr_BenWagesPayment as p where  YEAR(p.Created_dt)= @YEAR and p.intBenid in (select aa.intbenid from #tblBenf as aa)
and p.intWorkid in (select intworkid from #tblWorks)
select 'No. of work days created' as [Profession],   ISNULL(SUM(AttendanceDays), 0) AS Cnt
FROM     #tblPayment
    
        
        
select 'No. of Wages Paid SSP' as [Profession],isnull(sum(PaidAmount),0) as cnt  from #tblPayment        
        
     select 'No. of works Rejected' as [Profession],COUNT(distinct intWorkid) as cnt  from #tblWorks where Status like 'Rejected'    


select 'No. of Wages Paid USD' as [Profession],isnull(sum(PaidAmountUSD),0) as cnt  from #tblPayment
  
  --table 16
  select  COUNT(distinct intBenid) as cnt	 
 from #tblBenf where BeneficiaryType = 'Group'
 --table 17
select  COUNT(distinct intBenid) as cnt	 
 from #tblBenf where BeneficiaryType = 'CST'
 --table 18
select param_value from td_param
 --table 19
select isnull(sum(ProjectCost),0) AS ProjectCost from td_ProjectDet where (YEAR(Created_dt) = @Year)

 --table 20
select isnull((WageRateUSD/case when BenCount=0 then 1 else BenCount end),0) AS Wage_Rate  from #tblPayment

--table 21
select isnull( (ExchangeRate/case when BenCount=0 then 1 else BenCount end),0)  AS Exchange_Rate  from #tblPayment            

--table 22
select isnull(Sum(Amount),0) As cnt from tbl_FundsUtilization where FundsUtilizationFor in ('Skilled labour','Tools','Materials')
and (YEAR(Created_dt) = @Year) 



--table 23
		        
select 'Enrolled Beneficiaries' as [Profession],COUNT(distinct intBenid) as cnt 
 from #tblBenfAll

   --table 24         
select 'Enrolled of Female Beneficiaries' as [Profession],COUNT(distinct intBenid) as cnt  from #tblBenfAll 
where 
Sex like 'F'   
    
	--table 25        
select 'Enrolled of Male Beneficiaries' as [Profession],COUNT(distinct intBenid) as cnt  from #tblBenfAll  
where 
Sex like 'M'  


drop table #tblWorks
drop table #tblBenf
drop table #tblBenfAll
drop table #tblPayment
     end    
end    




