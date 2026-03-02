 CREATE Procedure [dbo].[GetUSDWagesPaidByIP](@id varchar(8))
as
 select  tr_BenWagesPayment.intBenid ,tr_BenWagesPayment.Year,tr_BenWagesPayment.Month,tr_BenWagesPayment.PaidDate,tr_BenWagesPayment.PaidAmountUSD   from tr_BenWagesPayment
inner join  tbl_BeneficiaryDet on tr_BenWagesPayment.intBenid =tbl_BeneficiaryDet.intBenid 
 inner join  td_IPDetails on  tbl_BeneficiaryDet.intIP =td_IPDetails.intIP 
 where td_IPDetails.intIP=@id 



