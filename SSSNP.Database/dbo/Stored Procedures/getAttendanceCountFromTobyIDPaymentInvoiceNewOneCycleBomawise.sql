CREATE procedure [dbo].[getAttendanceCountFromTobyIDPaymentInvoiceNewOneCycleBomawise]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intBomasid varchar(8)
)  
as  
begin  



create table #wageRequestbwdate
(
intWorkid int,
intWagePaymentReqid int

)
insert into #wageRequestbwdate
select distinct tbl_WorkMaster.intWorkid,intWagePaymentReqid from td_WorkWageRequest inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=td_WorkWageRequest.intWorkid
where tbl_WorkMaster.intBomasid = @intBomasid and Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106)=Convert(varchar(11),@FromDate,106) and Convert(varchar(11),td_WorkWageRequest.ReqToDate,106)=Convert(varchar(11),@ToDate,106)

create table #wagerequestdet
(
intWagePaymentReqid int,
intBenid int,

cnt int,
Amt decimal(18,2),
 Amt1 decimal(18,2),
 intWorkid int,
 
 PayStatus varchar(15),
 Paidto varchar(40),
 

)
insert into #wagerequestdet
select 
DISTINCT 
                     dbo.td_WageReqBenDet.intWagePaymentReqid, 
                      dbo.td_WageReqBenDet.intBenid,
                      dbo.tr_BenWagesPayment.AttendanceDays AS cnt,tr_BenWagesPayment.PaidAmountUSD  AS Amt,tr_BenWagesPayment.PaidAmount  AS Amt1,
					  td_WorkWageRequest.intWorkid,
				
						 
					case when 	tr_BenWagesPayment.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPayment.Paidto
					
				
					 from  td_WageReqBenDet  INNER JOIN
td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid 
                   INNER JOIN
            dbo.tr_BenWagesPayment ON tr_BenWagesPayment.uuid=td_WageReqBenDet.intWagePaymentReqid and tr_BenWagesPayment.intBenid=td_WageReqBenDet.intBenid and tr_BenWagesPayment.intWorkid=td_WageReqBenDet.intWorkid
			and tr_BenWagesPayment.trans_id=td_WageReqBenDet.intWagesReqBenTransid
	and  tr_BenWagesPayment.intWorkid=td_WorkWageRequest.intWorkid 
	and   tr_BenWagesPayment.uuid=td_WorkWageRequest.intWagePaymentReqid 
			where 
			  td_WorkWageRequest.intWagePaymentReqid in
			(
			select a.intWagePaymentReqid from #wageRequestbwdate as a
			)


create table #wagerequestdetunpaid
(
intWagePaymentReqid int,
intBenid int,

cnt int,
Amt decimal(18,2),
 Amt1 decimal(18,2),
 intWorkid int,
 
 PayStatus varchar(15),
 Paidto varchar(40),
 

)
insert into #wagerequestdetunpaid
select 
DISTINCT 
                     dbo.td_WageReqBenDet.intWagePaymentReqid, 
                      dbo.td_WageReqBenDet.intBenid,
                      dbo.tr_BenWagesPaymentNotPaid.AttendanceDays AS cnt,tr_BenWagesPaymentNotPaid.PaidAmountUSD  AS Amt,tr_BenWagesPaymentNotPaid.PaidAmount  AS Amt1,
					  td_WorkWageRequest.intWorkid,
				
						 
					'Unpaid' as PayStatus, '' as Paidto
					
				
					 from  td_WageReqBenDet  INNER JOIN
td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid 
                   INNER JOIN
            dbo.tr_BenWagesPaymentNotPaid ON tr_BenWagesPaymentNotPaid.uuid=td_WageReqBenDet.intWagePaymentReqid and tr_BenWagesPaymentNotPaid.intBenid=td_WageReqBenDet.intBenid and tr_BenWagesPaymentNotPaid.intWorkid=td_WageReqBenDet.intWorkid
			and tr_BenWagesPaymentNotPaid.trans_id=td_WageReqBenDet.intWagesReqBenTransid
	and  tr_BenWagesPaymentNotPaid.intWorkid=td_WorkWageRequest.intWorkid 
	and   tr_BenWagesPaymentNotPaid.uuid=td_WorkWageRequest.intWagePaymentReqid 
			where 
			  td_WorkWageRequest.intWagePaymentReqid in
			(
			select a.intWagePaymentReqid from #wageRequestbwdate as a
			)

create table #bendet
(
intBenid int,
EnrollmentNo varchar(30),
ContactNo varchar(30),
NameofHousehold varchar(80),
household_number varchar(40),
alternate_number varchar(40),
Name1stAlternatePayee varchar(80),
Secondalternate_number varchar(40),
Name2ndAlternatePayee varchar(80)
)
insert into #bendet
select intBenid,EnrollmentNo,ContactNo,NameofHousehold,household_number,alternate_number,Name1stAlternatePayee,Secondalternate_number,Name2ndAlternatePayee from
tbl_BeneficiaryDet where tbl_BeneficiaryDet.intBomasid =@intBomasid



select  distinct a.intWagePaymentReqid, 
                      b.intBenid, b.EnrollmentNo,b.ContactNo,  b.NameofHousehold,
                      a.cnt,a.Amt,a.Amt1,a.intWorkid,
				case when 	a.Paidto is null then '' else   	case when b.household_number=a.Paidto then   b.NameofHousehold else case when b.alternate_number=a.Paidto then b.Name1stAlternatePayee else case when b.Secondalternate_number=a.Paidto then b.Name2ndAlternatePayee else 'Paid Person details is not available in MIS due to more than two alternative beneficiaries or alternative not registered.' end end end end as PaidToName,
						 
					a.PayStatus,a.Paidto,
					case when 	a.Paidto is null then '' else   	case when b.household_number=a.Paidto then   'Household' else case when b.alternate_number=a.Paidto then 'Alternate' else case when b.Secondalternate_number=a.Paidto then 'Alternate' else '' end end end end as PaidToType
					,b.household_number  from #wagerequestdet as a inner join #bendet as b on a.intBenid=b.intBenid
UNION




select  distinct a.intWagePaymentReqid, 
                      b.intBenid, b.EnrollmentNo,b.ContactNo,  b.NameofHousehold,
                      a.cnt,a.Amt,a.Amt1,a.intWorkid,
				'' as PaidToName,
						 
					a.PayStatus,a.Paidto,
					'' as PaidToType
					,b.household_number  from #wagerequestdetunpaid as a inner join #bendet as b on a.intBenid=b.intBenid



					Drop table #wagerequestdetunpaid
					Drop table #bendet
					Drop table #wagerequestdet
					Drop table #wageRequestbwdate



--create table #wageRequestbwdate
--(
--intWorkid int,
--intWagePaymentReqid int

--)
--insert into #wageRequestbwdate
--select distinct tbl_WorkMaster.intWorkid,intWagePaymentReqid from td_WorkWageRequest inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=td_WorkWageRequest.intWorkid
--where tbl_WorkMaster.intBomasid = @intBomasid and Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106)=Convert(varchar(11),@FromDate,106) and Convert(varchar(11),td_WorkWageRequest.ReqToDate,106)=Convert(varchar(11),@ToDate,106)

--create table #wagerequestdet
--(
--intWagePaymentReqid int,
--intBenid int,

--cnt int,
--Amt decimal(18,2),
-- Amt1 decimal(18,2),
-- intWorkid int,
 
-- PayStatus varchar(15),
-- Paidto varchar(40),
 

--)
--insert into #wagerequestdet
--select 
--DISTINCT 
--                     dbo.td_WageReqBenDet.intWagePaymentReqid, 
--                      dbo.td_WageReqBenDet.intBenid,
--                      dbo.tr_BenWagesPayment.AttendanceDays AS cnt,tr_BenWagesPayment.PaidAmountUSD  AS Amt,tr_BenWagesPayment.PaidAmount  AS Amt1,
--					  td_WorkWageRequest.intWorkid,
				
						 
--					case when 	tr_BenWagesPayment.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPayment.Paidto
					
				
--					 from  td_WageReqBenDet  INNER JOIN
--td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid 
--                   INNER JOIN
--            dbo.tr_BenWagesPayment ON tr_BenWagesPayment.uuid=td_WageReqBenDet.intWagePaymentReqid and tr_BenWagesPayment.intBenid=td_WageReqBenDet.intBenid and tr_BenWagesPayment.intWorkid=td_WageReqBenDet.intWorkid
--			and tr_BenWagesPayment.trans_id=td_WageReqBenDet.intWagesReqBenTransid
	
--			where 
--			  td_WorkWageRequest.intWagePaymentReqid in
--			(
--			select a.intWagePaymentReqid from #wageRequestbwdate as a
--			)


--create table #wagerequestdetunpaid
--(
--intWagePaymentReqid int,
--intBenid int,

--cnt int,
--Amt decimal(18,2),
-- Amt1 decimal(18,2),
-- intWorkid int,
 
-- PayStatus varchar(15),
-- Paidto varchar(40),
 

--)
--insert into #wagerequestdetunpaid
--select 
--DISTINCT 
--                     dbo.td_WageReqBenDet.intWagePaymentReqid, 
--                      dbo.td_WageReqBenDet.intBenid,
--                      dbo.tr_BenWagesPaymentNotPaid.AttendanceDays AS cnt,tr_BenWagesPaymentNotPaid.PaidAmountUSD  AS Amt,tr_BenWagesPaymentNotPaid.PaidAmount  AS Amt1,
--					  td_WorkWageRequest.intWorkid,
				
						 
--					'Unpaid' as PayStatus, '' as Paidto
					
				
--					 from  td_WageReqBenDet  INNER JOIN
--td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid 
--                   INNER JOIN
--            dbo.tr_BenWagesPaymentNotPaid ON tr_BenWagesPaymentNotPaid.uuid=td_WageReqBenDet.intWagePaymentReqid and tr_BenWagesPaymentNotPaid.intBenid=td_WageReqBenDet.intBenid and tr_BenWagesPaymentNotPaid.intWorkid=td_WageReqBenDet.intWorkid
--			and tr_BenWagesPaymentNotPaid.trans_id=td_WageReqBenDet.intWagesReqBenTransid
	
--			where 
--			  td_WorkWageRequest.intWagePaymentReqid in
--			(
--			select a.intWagePaymentReqid from #wageRequestbwdate as a
--			)

--create table #bendet
--(
--intBenid int,
--EnrollmentNo varchar(30),
--ContactNo varchar(30),
--NameofHousehold varchar(80),
--household_number varchar(40),
--alternate_number varchar(40),
--Name1stAlternatePayee varchar(80),
--Secondalternate_number varchar(40),
--Name2ndAlternatePayee varchar(80)
--)
--insert into #bendet
--select intBenid,EnrollmentNo,ContactNo,NameofHousehold,household_number,alternate_number,Name1stAlternatePayee,Secondalternate_number,Name2ndAlternatePayee from
--tbl_BeneficiaryDet where tbl_BeneficiaryDet.intBomasid =@intBomasid



--select  distinct a.intWagePaymentReqid, 
--                      b.intBenid, b.EnrollmentNo,b.ContactNo,  b.NameofHousehold,
--                      a.cnt,a.Amt,a.Amt1,a.intWorkid,
--				case when 	a.Paidto is null then '' else   	case when b.household_number=a.Paidto then   b.NameofHousehold else case when b.alternate_number=a.Paidto then b.Name1stAlternatePayee else case when b.Secondalternate_number=a.Paidto then b.Name2ndAlternatePayee else 'Paid Person details is not available in MIS due to more than two alternative beneficiaries or alternative not registered.' end end end end as PaidToName,
						 
--					a.PayStatus,a.Paidto,
--					case when 	a.Paidto is null then '' else   	case when b.household_number=a.Paidto then   'Household' else case when b.alternate_number=a.Paidto then 'Alternate' else case when b.Secondalternate_number=a.Paidto then 'Alternate' else '' end end end end as PaidToType
--					,b.household_number  from #wagerequestdet as a inner join #bendet as b on a.intBenid=a.intBenid
--UNION




--select  distinct a.intWagePaymentReqid, 
--                      b.intBenid, b.EnrollmentNo,b.ContactNo,  b.NameofHousehold,
--                      a.cnt,a.Amt,a.Amt1,a.intWorkid,
--				'' as PaidToName,
						 
--					a.PayStatus,a.Paidto,
--					'' as PaidToType
--					,b.household_number  from #wagerequestdetunpaid as a inner join #bendet as b on a.intBenid=a.intBenid



--					Drop table #wagerequestdetunpaid
--					Drop table #bendet
--					Drop table #wagerequestdet
--					Drop table #wageRequestbwdate

----  SELECT DISTINCT 
----                     dbo.td_WageReqBenDet.intWagePaymentReqid, 
----                      dbo.tbl_BeneficiaryDet.intBenid, dbo.tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.ContactNo,  tbl_BeneficiaryDet.NameofHousehold,
----                      dbo.tr_BenWagesPayment.AttendanceDays AS cnt,tr_BenWagesPayment.PaidAmountUSD  AS Amt,tr_BenWagesPayment.PaidAmount  AS Amt1,td_WorkWageRequest.intWorkid,
----				case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   tbl_BeneficiaryDet.NameofHousehold else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name1stAlternatePayee else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name2ndAlternatePayee else 'Paid Person details is not available in MIS due to more than two alternative beneficiaries or alternative not registered.' end end end end as PaidToName,
						 
----					case when 	tr_BenWagesPayment.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPayment.Paidto,
----					case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   'Household' else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else '' end end end end as PaidToType
----					,tbl_BeneficiaryDet.household_number,'' as matched_fp
----FROM       td_WageReqBenDet inner join  dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid INNER JOIN
----td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid

                    
----                   INNER JOIN
----            dbo.tr_BenWagesPayment ON tr_BenWagesPayment.uuid=td_WageReqBenDet.intWagePaymentReqid and tr_BenWagesPayment.intBenid=td_WageReqBenDet.intBenid and tr_BenWagesPayment.intWorkid=td_WageReqBenDet.intWorkid
----		inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenWagesPayment.intWorkid and tbl_WorkMaster.intWorkid=td_WorkWageRequest.intWorkid and tbl_WorkMaster.intWorkid=td_WageReqBenDet.intWorkid
----			where tbl_WorkMaster.intBomasid like @intBomasid and Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106)=Convert(varchar(11),@FromDate,106) and Convert(varchar(11),td_WorkWageRequest.ReqToDate,106)=Convert(varchar(11),@ToDate,106)
----UNION




----SELECT DISTINCT 
----                     dbo.td_WageReqBenDet.intWagePaymentReqid, 
----                      dbo.tbl_BeneficiaryDet.intBenid, dbo.tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.ContactNo,  tbl_BeneficiaryDet.NameofHousehold,
----                      dbo.tr_BenWagesPaymentNotPaid.AttendanceDays AS cnt,tr_BenWagesPaymentNotPaid.PaidAmountUSD  AS Amt,tr_BenWagesPaymentNotPaid.PaidAmount  AS Amt1,td_WorkWageRequest.intWorkid,
----				case when 	tr_BenWagesPaymentNotPaid.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPaymentNotPaid.Paidto then   tbl_BeneficiaryDet.NameofHousehold else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPaymentNotPaid.Paidto then tbl_BeneficiaryDet.Name1stAlternatePayee else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPaymentNotPaid.Paidto then tbl_BeneficiaryDet.Name2ndAlternatePayee else '' end end end end as PaidToName,
						 
----					case when 	tr_BenWagesPaymentNotPaid.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPaymentNotPaid.Paidto,
----					case when 	tr_BenWagesPaymentNotPaid.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPaymentNotPaid.Paidto then   'Household' else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPaymentNotPaid.Paidto then 'Alternate' else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPaymentNotPaid.Paidto then 'Alternate' else '' end end end end as PaidToType
----					,tbl_BeneficiaryDet.household_number,'' as matched_fp
----FROM       td_WageReqBenDet inner join  dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid INNER JOIN
----td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
                    
----                    INNER JOIN
----            dbo.tr_BenWagesPaymentNotPaid ON tr_BenWagesPaymentNotPaid.uuid=td_WageReqBenDet.intWagePaymentReqid and tr_BenWagesPaymentNotPaid.intBenid=td_WageReqBenDet.intBenid and tr_BenWagesPaymentNotPaid.intWorkid=td_WageReqBenDet.intWorkid
----			inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenWagesPaymentNotPaid.intWorkid and tbl_WorkMaster.intWorkid=td_WorkWageRequest.intWorkid and tbl_WorkMaster.intWorkid=td_WageReqBenDet.intWorkid
----			where tbl_WorkMaster.intBomasid like @intBomasid and Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106)=Convert(varchar(11),@FromDate,106) and Convert(varchar(11),td_WorkWageRequest.ReqToDate,106)=Convert(varchar(11),@ToDate,106)










--and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b where (AttendanceDate between @FromDate and @ToDate ))
--and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
--and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
--and tbl_BeneficiaryDet.intBenid in (select ab.intBenid from td_WageReqBenDet as ab where ab.intWagePaymentReqid like @intWagePaymentReqid)
--and tbl_BeneficiaryDet.intBenid in (select ab.intBenid from td_WageReqBenPADet as ab where ab.intWagePaymentReqPAid like intWagePaymentReqPAid)
--and td_WorkWageRequestPA.intWagePaymentReqPAid like @intWagePaymentReqPAid
--and td_WageReqBenPADet.intWagePaymentReqPAid like @intWagePaymentReqPAid
----order by EnrollmentNo asc
end


