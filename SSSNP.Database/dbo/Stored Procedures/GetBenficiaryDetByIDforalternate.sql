
--created by siva as 04-1-2016        
--Purpose: to get Work Proposals            
            
--created by siva as on 12-1-2016 for benficiary Details By ID  
  
CREATE procedure [dbo].[GetBenficiaryDetByIDforalternate]            
(            
@intBenid varchar(10)            
)            
as begin            
            
select * from dbo.tbl_BeneficiaryDet where intBenid like @intBenid  

select intBenid from  tbl_BeneficiaryDet  where intBenid like @intBenid and  alternate_number is not null and alternate_number in (select tr.Paidto from tr_BenWagesPayment as tr where tr.intBenid like @intBenid)
select intBenid from  tbl_BeneficiaryDet  where intBenid like @intBenid and  Secondalternate_number is not null and Secondalternate_number in (select tr.Paidto from tr_BenWagesPayment as tr where tr.intBenid like @intBenid)
select intBenid from  tbl_BeneficiaryDet  where intBenid like @intBenid and  thirdalternate_number is not null and thirdalternate_number in (select tr.Paidto from tr_BenWagesPayment as tr where tr.intBenid like @intBenid)
SELECT DISTINCT 
                  w.WorkCode,     td_WorkWageRequest.ReqNo as RequestNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate,td_WageReqBenDet.TotAttendance AS TotalWorkDays, 
                        dbo.td_WageReqBenDet.WageRateUSDTotal,
					case when 	td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<>100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate,
					tr_BenWagesPayment.PaidAmountUSD  AS Amt1,
				case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   tbl_BeneficiaryDet.NameofHousehold else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name1stAlternatePayee else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name2ndAlternatePayee else 'Paid Person details is not available in MIS due to more than two alternative beneficiaries or alternative not registered.' end end end end as PaidToName,
						 
					case when 	tr_BenWagesPayment.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPayment.Paidto,
					case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   'Household' else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else '' end end end end as PaidToType
					,CONVERT(varchar(11), tr_BenWagesPayment.PaidDate,103) PaidDates
				
FROM            dbo.td_WorkWageRequest 
INNER JOIN td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid= td_WorkWageRequest.intWagePaymentReqid 
inner join tbl_WorkMaster as w on w.intWorkid=td_WorkWageRequest.intWorkid
inner join tr_BenWagesPayment on tr_BenWagesPayment.trans_id=td_WageReqBenDet.intWagesReqBenTransid
and tr_BenWagesPayment.intBenid=td_WageReqBenDet.intBenid and w.intWorkid=td_WorkWageRequest.intWorkid
inner join  dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid
where td_WageReqBenDet.intBenid =@intBenid

 --and td_WageReqBenDet.TotAttendance<>0  
--select * from PWImages.dbo.tbl_Attachements where type like 'Appicant Photo' and intTypeId like @intBenid
        
end





