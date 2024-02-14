


CREATE VIEW [dbo].[Vw_WagesPaidIP]
AS
SELECT     SUM(dbo.tr_BenWagesPayment.PaidAmount) AS cnt, dbo.tbl_BeneficiaryDet.intCountieid,tbl_BeneficiaryDet.intIP
FROM         dbo.tr_BenWagesPayment INNER JOIN
                      dbo.tbl_BeneficiaryDet ON dbo.tr_BenWagesPayment.intBenid = dbo.tbl_BeneficiaryDet.intBenid
GROUP BY dbo.tbl_BeneficiaryDet.intCountieid,tbl_BeneficiaryDet.intIP



