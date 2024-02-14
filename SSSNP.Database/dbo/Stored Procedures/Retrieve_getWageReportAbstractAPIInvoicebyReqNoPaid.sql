

CREATE procedure [dbo].[Retrieve_getWageReportAbstractAPIInvoicebyReqNoPaid] -- '05/23/2016','05/30/2016',1015
(  
@workCode varchar(120),
@intStateid varchar(8),
@intCountieid varchar(8),
@intPayamid varchar(8),
@intBomasid varchar(8) ,
@IPid varchar(8)  ,
@ReqNo varchar(30),
@paymentCycle varchar(30),
@reportType varchar(10), -- "All", "DIS", "LIPW" (non-DIS)
@fromDate as datetime,
@toDate as datetime
)  
as  
begin  
select *
, ExchangeRate * AmountPaidUSD as AmountPaidSSP
from WagePaidResultDetails_tblTemp
where 
	workcode like @workcode
	and intip  like @IPid
	and intstateid  like @intstateid
	and intCountyId  like @intCountieid
	and intPayamid like  @intPayamid
	and intbomaid like  @intbomasid
	and RequestNo like @reqno
	and PaymentCycle like @paymentCycle
	and
	(
	--(@reportType = 'DIS' and workcode='DIS - Direct Income Support')
	--or (@reportType = 'LIPW' and workcode<>'DIS - Direct Income Support')
	(@reportType = 'DIS' and (workcode='DIS - Direct Income Support' or workremarks like '%DIS%' ))
	or (@reportType = 'LIPW' and workcode<>'DIS - Direct Income Support' and workremarks not like '%DIS%')
	or (@reportType <> 'DIS' and @reportType <> 'LIPW')
	)
	and (@fromdate is null or (@fromDate <= fromDate and @fromDate <todate))
	and (@fromdate is null or (@toDate >= fromDate and @toDate >= todate))

select
	SUM(TotalMembers) AS TotalMembers
	,SUM(TotalWorkDays) AS TotalWorkDays
	--,SUM(TotalAmount) AS AmountPaidUSD --Total
	,ISNULL(null, 
			(select SUM(b.wagerateusdtotal) as TotalAmount from td_WorkWageRequest a
			inner join td_WageReqBenDet b on a.intWagePaymentReqid=b.intWagePaymentReqid
			inner join tbl_BeneficiaryDet c on b.intBenid=c.intBenid
			where a.Status in (4,5,6)
			and intStateId like @intStateid
			and intCountieid like @intCountieid
			and intPayamId like @intPayamid
			and intBomasId like @intBomasid
			and PaymentCycle like @paymentCycle)
		) as AmountPaidUSD
	
	,SUM(TotalMembersPaid) AS TotalMembersPaid

	,SUM(AmountPaidUSD) AS TotalMembersPaidMain --Paid
		,SUM(TotalMembers)-SUM(TotalMembersPaid) as TotalMembersUnPaid
	--,SUM(TotalAmount)-SUM(AmountPaidUSD) as TotalAmountUnPaid --unpaid
	,ISNULL(null, 
			(select SUM(b.wagerateusdtotal) as TotalAmount from td_WorkWageRequest a
			inner join td_WageReqBenDet b on a.intWagePaymentReqid=b.intWagePaymentReqid
			inner join tbl_BeneficiaryDet c on b.intBenid=c.intBenid
			where a.Status in (4,5,6)
			and intStateId like @intStateid
			and intCountieid like @intCountieid
			and intPayamId like @intPayamid
			and intBomasId like @intBomasid
			and PaymentCycle like @paymentCycle)
			-SUM(AmountPaidUSD)) as TotalAmountUnPaid --unpaid
			,SUM(AmountPaidUSD*exchangerate) AS TotalMembersPaidMainSSP --PaidSSP
from WagePaidResultDetails_tblTemp
where intStateId like @intStateid
and intCountyId like @intCountieid
and intPayamId like @intPayamid
and intBomaId like @intBomasid
and PaymentCycle like @paymentCycle
and
	(
	(@reportType = 'DIS' and workcode='DIS - Direct Income Support')
	or (@reportType = 'LIPW' and workcode<>'DIS - Direct Income Support')
	or (@reportType <> 'DIS' and @reportType <> 'LIPW')
	)
and (@fromdate is null or (@fromDate <= fromDate and @fromDate <todate))
and (@fromdate is null or (@toDate >= fromDate and @toDate >= todate))


declare @TotalBeneficiariesPaid as int
declare @TotalBeneficiariesUnpaid as int


set @TotalBeneficiariesPaid=( select count (distinct a.intbenid) from tr_BenWagesPayment a
inner join tbl_BeneficiaryDet b on a.intbenid=b.intBenid
inner join td_WorkWageRequest wwr
on a.uuid=wwr.intWagePaymentReqid
where intStateId like @intStateid
and intCountieid like @intCountieid
and intPayamId like @intPayamid
and intBomasId like @intBomasid
and PaymentCycle like @paymentCycle
	and (@fromdate is null or (@fromDate <= fromDate and @fromDate <todate))
	and (@fromdate is null or (@toDate >= fromDate and @toDate >= todate))
)
set @TotalBeneficiariesUnPaid= (select count (distinct intbenid) from tbl_BeneficiaryDet 
where intStateId like @intStateid
and intCountieid like @intCountieid
and intPayamId like @intPayamid
and intBomasId like @intBomasid
and intbenid not in (select distinct intbenid from tr_BenWagesPayment))

select @TotalBeneficiariesPaid,@TotalBeneficiariesUnpaid 

end