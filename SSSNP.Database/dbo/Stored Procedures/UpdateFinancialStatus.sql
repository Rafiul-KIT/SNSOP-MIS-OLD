CREATE Procedure [dbo].[UpdateFinancialStatus](
@invoiceid int,
@InvoiceStatus varchar(100),
@InvoiceRemarks varchar(max),
@ApprovedDate date,
@Modified_by int,
@intFromid int,
@intToid int

)
as
Declare @intWorkid int
set @intWorkid=(select [intWorkid] from tr_InvoiceStatus where invoiceid = @invoiceid)
update tr_InvoiceStatus
set InvoiceStatus=@InvoiceStatus,
InvoiceRemarks=@InvoiceRemarks,
ApprovedDate=@ApprovedDate,
Modified_by =@Modified_by,
Modified_dt =GETDATE()
where 
invoiceid =@invoiceid


insert into td_InvoiceReqTrans([intInvoiceReqid],[intWorkid],[intFromid],[intToid],[Status],[TransDate],[Remarks],[Created_by],[Created_dt])
values(@invoiceid,@intWorkid,@intFromid,@intToid,@InvoiceStatus,@ApprovedDate,@InvoiceRemarks,@Modified_by,GETDATE())




