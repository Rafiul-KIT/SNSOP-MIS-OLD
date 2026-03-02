
CREATE procedure [dbo].[getBenfWorkWageDetailsPaidService]
As
Begin
select * from Vw_WagepaidData
--select  wr.intWagePaymentReqid, CONVERT(char(10), wr.ReqFromDate,126) as ReqFromDate,CONVERT(char(10), wr.ReqToDate,126) as ReqToDate,wr.Exchangerateattime,wr.WageRateUSD, wr.ReqNo, bw.intWagesReqBenTransid,  b.intBenid, b.EnrollmentNo,b.household_number,   w.intWorkid,w.WorkCode, bw.TotAttendance, bw.WageRateUSDTotal,  CONVERT(char(10), wr.Created_dt,126)  as DateofCreation from td_WageReqBenDet as bw inner join tbl_BeneficiaryDet as b on b.intBenid=bw.intBenid inner join tbl_WorkMaster as w on w.intWorkid=bw.intWorkid

--inner join td_WorkWageRequest as wr on wr.intWorkid=w.intWorkid 
--inner join tr_BenWagesPayment as wp on wp.intBenid=b.intBenid and wp.intWorkid=w.intWorkid and Convert(varchar(11),wp.FromDate,106)=Convert(varchar(11),wr.ReqFromDate,106)
--and Convert(varchar(11),wp.ToDate,106)=Convert(varchar(11),wr.ReqToDate,106)
--where  b.household_number is not null

End
