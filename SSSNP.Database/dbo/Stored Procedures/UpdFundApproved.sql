CREATE Procedure [dbo].[UpdFundApproved]            
(            
            
@intWorklId int,          
          
@ApprovedDate datetime,          
@PaymentMode varchar(10),          
@TransactionNo varchar(50),          
@PaymentDate datetime,          
@PaidAmt decimal(18, 2),          
@Created_by int  ,
@invoiceid int,
@InvoiceStatus varchar(100),
@intFromid int,
@intToid int,
@InvoiceRemarks varchar(max)   
             
)            
as begin           
if(@InvoiceStatus='Ma-Approve')
begin     
if((select COUNT(*) from tr_FinancialApproval where intWorkId like @intWorklId)=0)      
begin      
insert into dbo.tr_FinancialApproval(intWorkId,invoiceid, ApprovedDate, PaymentMode, TransactionNo, PaymentDate, PaidAmount, Created_by, Created_dt)      
      values(@intWorklId,@invoiceid, @ApprovedDate, @PaymentMode, @TransactionNo, @PaymentDate, @PaidAmt, @Created_by, GETDATE())  
            
      update tbl_WorkMaster set InvoiceRequestStatus='Invoice Appovals',InvoiceApprovedby=@Created_by,InvoiceApproveddt=getdate() where intWorkid like @intWorklId    
      
  update tr_WorkInvoiceDet set  InvoiceStatus='Invoice Appovals',InvoiceApprovedby=@Created_by,InvoiceApproveddt=getdate() where intWorkid like @intWorklId   
  --insert into tr_WorkInvoiceDet ( intWorkid, Invoiceid, InvoiceStatus, InvoiceBy, InvoiceDt, Created_by, Created_dt)
  
  --values (@intWorklId,@ids,'Invoice Request', @Created_by,getdate(),@Created_by,GETDATE())  
 --   if(dbo.InvoiceIsPaid(@invoiceid)='1')
	--begin   
	 update tr_InvoiceStatus
	 set InvoiceStatus=@InvoiceStatus,
	 InvoiceRemarks=@InvoiceRemarks,
	 ApprovedDate =@ApprovedDate,
		Modified_by=@Created_by,
		Modified_dt=getdate()
	 where 
	 invoiceid=@invoiceid

	 insert into td_InvoiceReqTrans([intInvoiceReqid],[intWorkid],[intFromid],[intToid],[Status],[TransDate],[Remarks],[Created_by],[Created_dt])
values(@invoiceid,@intWorklId,@intFromid,@intToid,@InvoiceStatus,@ApprovedDate,@InvoiceRemarks,@Created_by,GETDATE())
	 ----end
	        
      select 100      
      return 100      
            
      
end      
else begin      
       
      insert into dbo.tr_FinancialApproval(intWorkId,invoiceid, ApprovedDate, PaymentMode, TransactionNo, PaymentDate, PaidAmount, Created_by, Created_dt)      
      values(@intWorklId,@invoiceid, @ApprovedDate, @PaymentMode, @TransactionNo, @PaymentDate, @PaidAmt, @Created_by, GETDATE())  
      
        update tr_WorkInvoiceDet set  InvoiceStatus='Invoice Appovals',InvoiceApprovedby=@Created_by,InvoiceApproveddt=getdate() where intWorkid like @intWorklId   
        
	--	     if(dbo.InvoiceIsPaid(@invoiceid)='1')
	--begin   
update tr_InvoiceStatus
	 set InvoiceStatus=@InvoiceStatus,
	 InvoiceRemarks=@InvoiceRemarks,
	 ApprovedDate =@ApprovedDate,
		Modified_by=@Created_by,
		Modified_dt=getdate()
	 where 
	 invoiceid=@invoiceid

	 insert into td_InvoiceReqTrans([intInvoiceReqid],[intWorkid],[intFromid],[intToid],[Status],[TransDate],[Remarks],[Created_by],[Created_dt])
values(@invoiceid,@intWorklId,@intFromid,@intToid,@InvoiceStatus,@ApprovedDate,@InvoiceRemarks,@Created_by,GETDATE())
	 --end
            
      select 100      
      return 100      
 end      
end

else begin
update tr_InvoiceStatus
	 set InvoiceStatus=@InvoiceStatus,
	 InvoiceRemarks=@InvoiceRemarks,
	 ApprovedDate =@ApprovedDate,
		Modified_by=@Created_by,
		Modified_dt=getdate()
	 where 
	 invoiceid=@invoiceid

	 insert into td_InvoiceReqTrans([intInvoiceReqid],[intWorkid],[intFromid],[intToid],[Status],[TransDate],[Remarks],[Created_by],[Created_dt])
values(@invoiceid,@intWorklId,@intFromid,@intToid,@InvoiceStatus,@ApprovedDate,@InvoiceRemarks,@Created_by,GETDATE())
end
end




