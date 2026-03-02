CREATE Procedure [dbo].[RejectedInvoiceSpecialist](@UserId int)

as
 Select tIS.invoiceid,wm.intWorkid,
 [dbo].[GetStatusNameByStatusCode](tIS.InvoiceStatus)   as Status,
  [dbo].[GetLastUpdatedDateByInvoice](tIS.invoiceid) as InvoiceDate,fr.FundRaiseAmount as InvoiceAmount,
  wm.WorkCode,IP.IPName ,'' as Remarks,'' as UserId    from tr_InvoiceStatus  tIS
  inner join tr_FundsRaising fr
  on tis.invoiceid=fr.intFundRaise
  inner join tbl_WorkMaster wm
  on tis.intWorkid=wm.intWorkid 
  inner join td_IPDetails IP
  on wm.IPid=IP.intIP
  where 
   fr.Specialist_Id = @UserId  and
   tis.InvoiceStatus='Sp-Reject'



