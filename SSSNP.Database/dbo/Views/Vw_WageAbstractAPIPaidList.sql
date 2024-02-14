
CREATE VIEW [dbo].[Vw_WageAbstractAPIPaidList]
AS
SELECT DISTINCT                          
						 ip.IPName
						 --,isnull( s.StateName,'') as StateName
						 , isnull(StateName, (select top 1 s.StateName from td_WorkWageRequest wwr 
								inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
								inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
								inner join tm_States s on s.intStateid=benf.intStateid
								where wwr.ReqNo=wq.ReqNo)) as StateName
						 --,isnull( c.CountieName,'') as CountieName
						 ,isnull(CountieName, (select top 1 c.CountieName from td_WorkWageRequest wwr 
								inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
								inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
								inner join tm_counties c on c.intCountieid=benf.intCountieid
								where wwr.ReqNo=wq.ReqNo)) as CountieName
						 --,isnull( p.PayamName,'') as PayamName
						 ,isnull(PayamName, (select top 1 p.PayamName from td_WorkWageRequest wwr 
								inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
								inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
								inner join tm_payams p on p.intPayamid=benf.intPayamid
								where wwr.ReqNo=wq.ReqNo)) as PayamName
						 --,isnull( b.BomaName,'') as BomaName
						 ,isnull(BomaName, (select top 1 b.BomaName from td_WorkWageRequest wwr 
								inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
								inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
								inner join tm_bomas b on b.intBomasid=benf.intBomasid
								where wwr.ReqNo=wq.ReqNo)) as BomaName
						 , dbo.tbl_WorkMaster.WorkCode
						 , CONVERT(varchar(11), dbo.tr_BenWagesPayment.FromDate, 106) AS FromDate
						 , CONVERT(varchar(11), dbo.tr_BenWagesPayment.ToDate, 106) AS ToDate
						 , ip.intIP
						 --,isnull( s.intStateid,0) as intStateid
						 , isnull(s.intStateid, (select top 1 s.intStateid from td_WorkWageRequest wwr 
								inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
								inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
								inner join tm_States s on s.intStateid=benf.intStateid
								where wwr.ReqNo=wq.ReqNo)) as intStateid
						 --,isnull( c.intCountieid,0) as intCountieid
						  ,isnull(c.intCountieid, (select top 1 c.intCountieid from td_WorkWageRequest wwr 
								inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
								inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
								inner join tm_counties c on c.intCountieid=benf.intCountieid
								where wwr.ReqNo=wq.ReqNo)) as intCountieid
						 --,isnull( p.intPayamid,0) as intPayamid
						 	 ,isnull(p.intPayamid, (select top 1 p.intPayamid from td_WorkWageRequest wwr 
								inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
								inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
								inner join tm_payams p on p.intPayamid=benf.intPayamid
								where wwr.ReqNo=wq.ReqNo)) as intPayamid
						 --,isnull( b.intBomasid,0) as intBomasid
						  ,isnull(b.intBomasid, (select top 1 b.intBomasid from td_WorkWageRequest wwr 
								inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
								inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
								inner join tm_bomas b on b.intBomasid=benf.intBomasid
								where wwr.ReqNo=wq.ReqNo)) as intBomasid
						 ,wq.ReqNo
						 ,wq.intWagePaymentReqid
						 ,tbl_WorkMaster.intWorkid
						 ,tr_BenWagesPayment.FromDate as FDate
						 ,tr_BenWagesPayment.ToDate as Tdate
						 , wpc.WagePaymentCycle
FROM            dbo.tr_BenWagesPayment 
	INNER JOIN dbo.tbl_WorkMaster ON dbo.tbl_WorkMaster.intWorkid = dbo.tr_BenWagesPayment.intWorkid 
	left JOIN dbo.tm_Bomas AS b ON b.intBomasid = dbo.tbl_WorkMaster.intBomasid 
	left JOIN dbo.tm_Payams AS p ON p.intPayamid = b.intPayamid 
	left JOIN dbo.tm_Counties AS c ON c.intCountieid = p.intCountieid 
	left JOIN dbo.tm_States AS s ON s.intStateid = c.intStateid 
	INNER JOIN dbo.td_IPDetails AS ip ON ip.intIP = dbo.tbl_WorkMaster.IPid
	INNER JOIN td_WorkWageRequest as wq on wq.intWagePaymentReqid=tr_BenWagesPayment.uuid
	left join tm_WagePaymentCycle wpc on wpc.intPaymentCycleID=wq.PaymentCycle
WHERE        (dbo.tr_BenWagesPayment.uuid IS NOT NULL)





