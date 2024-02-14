CREATE function [dbo].[GetStatusByInvoice] 
( @invoiceid int
)
RETURNS varchar(100)
AS
BEGIN
	declare @Stat varchar(100)
	set @Stat=(select top 1 Status from td_InvoiceReqTrans where intInvoiceReqid=@invoiceid order by [intInvoiceReqTransid] desc)
	
	return @Stat

END


