CREATE procedure [dbo].[GetPaymentsCountersBoma]

as
Begin

	select  
		benf.intbomasid as BomaId
		,bomas.bomaname as BomaName
		,sum(payments.PaidAmountUSD) as PaidAmount
		,count(payments.intBenid) as BeneficiariesCount
	from 
		tr_BenWagesPayment (nolock) payments
		inner join tbl_BeneficiaryDet (nolock) benf on benf.intBenid=payments.intBenid
		inner join tm_bomas (nolock) bomas on bomas.intbomasid=benf.intbomasid
	group by benf.intbomasid,bomas.bomaname

end