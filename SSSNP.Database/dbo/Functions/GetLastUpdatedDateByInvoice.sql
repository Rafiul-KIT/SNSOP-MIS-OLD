CREATE function [dbo].[GetLastUpdatedDateByInvoice] 
( @invoiceid int
)
RETURNS datetime
AS
BEGIN
	declare @Stat datetime
	set @Stat=(select top 1 cast((case when TransDate is null then Created_dt else TransDate end) as date) from td_InvoiceReqTrans where intInvoiceReqid=@invoiceid order by [intInvoiceReqTransid] desc)
	
	return @Stat

END

