CREATE Procedure [dbo].[TotalSubmittedInvoiceCooridinator](@UserId int)

as
  Select tIS.invoiceid,wm.intWorkid,
  [dbo].[GetStatusNameByStatusCode](tIS.InvoiceStatus)   as Status,
  [dbo].[GetLastUpdatedDateByInvoice](tIS.invoiceid) as InvoiceDate,fr.FundRaiseAmount as InvoiceAmount,
  wm.WorkCode,IP.IPName ,[dbo].[GetRemarksByInvoice](invoiceid) as Remarks,(case when tIS.InvoiceStatus ='Sp-Reject' then [dbo].[GetUserNameById](fr.Specialist_Id) else '' end) as UserId   from tr_InvoiceStatus  tIS
  inner join tr_FundsRaising fr
  on tis.invoiceid=fr.intFundRaise
  inner join tbl_WorkMaster wm
  on tis.intWorkid=wm.intWorkid 
  inner join td_IPDetails IP
  on wm.IPid=IP.intIP
  where 
  fr.CoOrdinator_Id = @UserId 
  and  isnull(tIS.InvoiceStatus,'') in ('Sp-Reject','')




