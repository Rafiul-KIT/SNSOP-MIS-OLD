CREATE procedure [dbo].[GetWageRequestbyIDPaymentInvoiceFullDetails] 
(
	@intWagePaymentReqid varchar(8)
)

As

Begin

select distinct a
.*
, u.User_name as coordinator
,u1.User_name as Specilist
,u2.User_name as SManager
,isnull(u3.User_name,'') as SPaymentagent
,dbo.GetWageCoordinatorSenton(@intWagePaymentReqid,a.intCoOrdinatorid) as CoordinatorSenton
,COnvert(varchar(11),a.SApproveDate,106) as SpecilaistSenton
,dbo.GetWageManagerSenton(@intWagePaymentReqid,a.intManagerid) as ManagerSenton
,	(select count(distinct wb.intBenid) 
		from td_WageReqBenDet as wb 
		where wb.intWagePaymentReqid=a.intWagePaymentReqid
	) as TotalCount
,	(
	SELECT DISTINCT  count (tbl_BeneficiaryDet.intbenid)
	FROM       td_WageReqBenDet 
	inner join  
		dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid 
	INNER JOIN
		td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
	INNER JOIN
		dbo.tr_BenWagesPayment wb ON wb.uuid=td_WageReqBenDet.intWagePaymentReqid 
		and wb.intBenid=td_WageReqBenDet.intBenid
		and wb.intWorkid=td_WageReqBenDet.intWorkid
		where wb.uuid=a.intWagePaymentReqid
	) as PaidCount
,	(select count(distinct wb.intBenid) 
		from td_WageReqBenDet as wb 
		where wb.intWagePaymentReqid=a.intWagePaymentReqid)- 
		(
		SELECT DISTINCT  count (tbl_BeneficiaryDet.intbenid)
	FROM       td_WageReqBenDet 
	inner join  
		dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid 
	INNER JOIN
		td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
	INNER JOIN
		dbo.tr_BenWagesPayment wb ON wb.uuid=td_WageReqBenDet.intWagePaymentReqid 
		and wb.intBenid=td_WageReqBenDet.intBenid
		and wb.intWorkid=td_WageReqBenDet.intWorkid
		where wb.uuid=a.intWagePaymentReqid
		) as UnpaidCount,
	(select sum(wb.WageRateUSDTotal) 
		from td_WageReqBenDet as wb 
		where wb.intWagePaymentReqid=a.intWagePaymentReqid
		) as ToalAmount
		,
	(
	select sum(wb.PaidAmountUSD)
		FROM       td_WageReqBenDet 
		inner join  
		dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid 
		INNER JOIN
		td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
		INNER JOIN
		dbo.tr_BenWagesPayment wb ON wb.uuid=td_WageReqBenDet.intWagePaymentReqid 
		and wb.intBenid=td_WageReqBenDet.intBenid
		and wb.intWorkid=td_WageReqBenDet.intWorkid
		where wb.uuid=a.intWagePaymentReqid
	) as PaidAmount
	,(select sum(wb.PaidAmountUSD) 
		FROM       td_WageReqBenDet 
		inner join  
		dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid 
		INNER JOIN
		td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
		INNER JOIN
		dbo.tr_BenWagesPayment wb ON wb.uuid=td_WageReqBenDet.intWagePaymentReqid 
		and wb.intBenid=td_WageReqBenDet.intBenid
		and wb.intWorkid=td_WageReqBenDet.intWorkid
		inner join tbl_BeneficiaryDet ben on ben.intbenid=wb.intbenid
		where wb.uuid=a.intWagePaymentReqid
		and ben.SelectionCriteria='Component 2 (Direct Income Support)'
	) as PaidAmountDIS
	,(select sum(wb.PaidAmountUSD) 
		FROM       td_WageReqBenDet 
		inner join  
		dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid 
		INNER JOIN
		td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
		INNER JOIN
		dbo.tr_BenWagesPayment wb ON wb.uuid=td_WageReqBenDet.intWagePaymentReqid 
		and wb.intBenid=td_WageReqBenDet.intBenid
		and wb.intWorkid=td_WageReqBenDet.intWorkid
		inner join tbl_BeneficiaryDet ben on ben.intbenid=wb.intbenid
		where wb.uuid=a.intWagePaymentReqid
		and ben.SelectionCriteria='Component 1 (Public Works)'
	) as PaidAmountPublicWorks
	,(select sum(wb.WageRateUSDTotal) 
		from td_WageReqBenDet as wb 
		where wb.intWagePaymentReqid=a.intWagePaymentReqid) 
		- (select sum(wb.PaidAmountUSD)
		FROM       td_WageReqBenDet 
		inner join  
		dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid 
		INNER JOIN
		td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
		INNER JOIN
		dbo.tr_BenWagesPayment wb ON wb.uuid=td_WageReqBenDet.intWagePaymentReqid 
		and wb.intBenid=td_WageReqBenDet.intBenid
		and wb.intWorkid=td_WageReqBenDet.intWorkid
		where wb.uuid=a.intWagePaymentReqid)
	 as UnPaidAmount
	,(select Convert(varchar(11), Min(wb.PaidDate),106) 
		from tr_BenWagesPayment as wb 
			where wb.uuid=a.intWagePaymentReqid
	) as paidDate
	,(select count(distinct wb.intBenid) 
	from tr_BenWagesPayment as wb 
		inner join tbl_BeneficiaryDet as ben 
			on ben.intBenid=wb.intBenid 
	where wb.uuid=a.intWagePaymentReqid 
		and ben.household_number = wb.Paidto
	) as PaidActualHouseholds
	,(select count(distinct wb.intBenid) 
		from tr_BenWagesPayment as wb 
			inner join tbl_BeneficiaryDet as ben 
				on ben.intBenid=wb.intBenid 
		where wb.uuid=a.intWagePaymentReqid 
			and ben.household_number <> wb.Paidto
	) as PaidAlternateHouseholds
	,(select sum(wb.PaidAmount) 
		FROM       td_WageReqBenDet 
		inner join  
		dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid 
		INNER JOIN
		td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
		INNER JOIN
		dbo.tr_BenWagesPayment wb ON wb.uuid=td_WageReqBenDet.intWagePaymentReqid 
		and wb.intBenid=td_WageReqBenDet.intBenid
		and wb.intWorkid=td_WageReqBenDet.intWorkid
		where wb.uuid=a.intWagePaymentReqid
		
	) as PaidAmountSSP
	,(select sum(wb.PaidAmount) 
		FROM       td_WageReqBenDet 
		inner join  
		dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid 
		INNER JOIN
		td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
		INNER JOIN
		dbo.tr_BenWagesPayment wb ON wb.uuid=td_WageReqBenDet.intWagePaymentReqid 
		and wb.intBenid=td_WageReqBenDet.intBenid
		and wb.intWorkid=td_WageReqBenDet.intWorkid
		inner join tbl_BeneficiaryDet ben on ben.intbenid=wb.intbenid
		where wb.uuid=a.intWagePaymentReqid
		and ben.SelectionCriteria='Component 2 (Direct Income Support)'
	) as PaidAmountDISSSP
	,(select sum(wb.PaidAmount) 
		FROM       td_WageReqBenDet 
		inner join  
		dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid 
		INNER JOIN
		td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
		INNER JOIN
		dbo.tr_BenWagesPayment wb ON wb.uuid=td_WageReqBenDet.intWagePaymentReqid 
		and wb.intBenid=td_WageReqBenDet.intBenid
		and wb.intWorkid=td_WageReqBenDet.intWorkid
		inner join tbl_BeneficiaryDet ben on ben.intbenid=wb.intbenid
		where wb.uuid=a.intWagePaymentReqid
		and ben.SelectionCriteria='Component 1 (Public Works)'
	) as PaidAmountPublicWorksSSP
	,wpc.WagePaymentCycle
from 
	td_WorkWageRequest as a 
		inner join tbl_Users as u 
			on u.intUserid=a.intCoOrdinatorid
		inner join tbl_Users as u1 
			on u1.intUserid=a.intSpecialistid
		inner join tbl_Users as u2 
			on u2.intUserid=a.intManagerid
		left join tbl_Users as u3 
			on u3.intUserid=a.intPayingAgentid
		left join tm_WagePaymentCycle wpc 
			on a.PaymentCycle=wpc.intPaymentCycleID
 where 
	a.intWagePaymentReqid like @intWagePaymentReqid
End
GO