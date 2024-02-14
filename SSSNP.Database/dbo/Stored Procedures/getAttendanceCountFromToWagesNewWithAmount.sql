CREATE procedure [dbo].[getAttendanceCountFromToWagesNewWithAmount]  --getAttendanceCountFromToWages  Convert(datetime, '07/27/2018'),Convert(datetime, '07/27/2018')
(  
	@FromDate datetime,  
	@ToDate datetime,  
	@intWorkActivityId varchar(10),
	@group_type varchar(100),
	@group_code varchar(100),
	@WageRate decimal(18,2),
	@PaymentCycle int
)  
as  
begin  

create table #tempAttend
(
	intAttendanceid int,
	intWorkActivityId int,
	intBenid int,
	AttendanceDate datetime,
	WageApplyFlag varchar(8)
)
insert into 
	#tempAttend
	select 
		intAttendanceid
		,intWorkActivityId
		,intBenid
		,AttendanceDate
		,WageApplyFlag 
	from 
		tr_BenAttendanceDet 
	where 
	intWorkActivityId like @intWorkActivityId 
	and Status='Present' 
	and AttendanceDate between @FromDate and @ToDate

create table #WorkAlloc
(
	intBenid int,
	intWorkid int
) 

insert into 
	#WorkAlloc
	select distinct 
		intBenid
		,intWorkid  
	from   
		tr_BenfWorkDet 
	where intWorkid like @intWorkActivityId 
	

select distinct 
	tbl_BeneficiaryDet.intBenid
	,tbl_BeneficiaryDet.EnrollmentNo
	,tbl_BeneficiaryDet.NameofHousehold
	,tbl_BeneficiaryDet.Sex
	,tbl_BeneficiaryDet.Age
	,tbl_BeneficiaryDet.SelectionCriteria
	,tbl_BeneficiaryDet.ContactNo
	,tbl_BeneficiaryDet.Email
	,tbl_BeneficiaryDet.FamilyIncome
	,tm_Bomas.BomaName
	,DATEDIFF(dd,@Fromdate,@Todate+1) as Totaldays
	,isnull(
			(select count(distinct AttendanceDate) 
				from #tempAttend 
				where  intBenid=tbl_BeneficiaryDet.intBenid and WageApplyFlag is null ),0
			)as cnt
	,isnull(
			isnull(
			(
			select count(distinct AttendanceDate) 
				from #tempAttend 
				where  intBenid=tbl_BeneficiaryDet.intBenid and WageApplyFlag is null ),0)   * isnull(@WageRate,0),0) as Amt
	,isnull(
			(select count(distinct AttendanceDate) 
				from #tempAttend 
				where  intBenid=tbl_BeneficiaryDet.intBenid and WageApplyFlag is not null),0) as cntdone
	,isnull(
			(select count(distinct AttendanceDate) 
				from #tempAttend 
					where  intBenid=tbl_BeneficiaryDet.intBenid ),0) as TotAttend
					
from 
	dbo.tbl_BeneficiaryDet  
	inner join #WorkAlloc 
		on #WorkAlloc.intBenid=tbl_BeneficiaryDet.intBenid              
	inner join tbl_WorkMaster 
		on tbl_WorkMaster.intWorkid=#WorkAlloc.intWorkid  
	inner join tm_Bomas 
		on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid 
	and tbl_BeneficiaryDet.intbenid not in (
				select a.intbenid from td_WageReqBenDet a
				inner join td_WorkWageRequest c on c.intWagePaymentReqid= a.intWagePaymentReqid
				where a.intworkid!=999 and c.PaymentCycle = @PaymentCycle
				)
where  
	tbl_BeneficiaryDet.SelectionCriteria = 'Component 1 (Public Works)' and  tbl_WorkMaster.intWorkid like @intWorkActivityId 

order by 
	tbl_BeneficiaryDet.EnrollmentNo asc

Drop table #tempAttend
Drop table #WorkAlloc
end
