CREATE Procedure [dbo].[UpdFundRaising]      
(      
      
@intWorklId int,      
@FundRaiseDate datetime,      
@FundRaiseAmount decimal(18, 2),      
@Remarks varchar(150),      
@Created_by int ,
@CoOrdinator_Id int,
@Specialist_Id int,     
@Manager_Id int       
)  

    
as begin 

-- Declare 
Declare @ids as int
-- Declare     
      
if((select COUNT(*) from tr_FundsRaising where intWorklId=@intWorklId)>0)      
begin      
      
--update tr_FundsRaising set FundRaiseDate=@FundRaiseDate, FundRaiseAmount=@FundRaiseAmount, Remarks=@Remarks,Modified_by=@Created_by,Modified_dt=GETDATE() where intWorklId=@intWorklId      

insert into tr_FundsRaising (intWorklId, FundRaiseDate, FundRaiseAmount, Remarks,Created_by,Created_dt,CoOrdinator_Id,Specialist_Id,Manager_Id)        
      
values(@intWorklId, @FundRaiseDate, @FundRaiseAmount, @Remarks,@Created_by,GETDATE(),@CoOrdinator_Id,@Specialist_Id,@Manager_Id)

   
     Set @ids=@@IDENTITY
	   insert into tr_InvoiceStatus(invoiceid,intWorkid,Created_by,Created_dt)
  values(@ids,@intWorklId,@Created_by,getdate())

  insert into td_InvoiceReqTrans([intInvoiceReqid],[intWorkid],[intFromid],[intToid],[Created_by],[Created_dt])
values(@ids,@intWorklId,@Created_by,@CoOrdinator_Id,@Created_by,GETDATE())

  insert into tr_WorkInvoiceDet ( intWorkid, Invoiceid, InvoiceStatus, InvoiceBy, InvoiceDt, Created_by, Created_dt)
  
  values (@intWorklId,@ids,'Invoice Request', @Created_by,getdate(),@Created_by,GETDATE())    
     
select 1000      
return 1000      
      
end      
else begin      
      
insert into tr_FundsRaising (intWorklId, FundRaiseDate, FundRaiseAmount, Remarks,Created_by,Created_dt,CoOrdinator_Id,Specialist_Id,Manager_Id)      
      
values(@intWorklId, @FundRaiseDate, @FundRaiseAmount, @Remarks,@Created_by,GETDATE(),@CoOrdinator_Id,@Specialist_Id,@Manager_Id)      
     
     Set @ids=@@IDENTITY
     
      
  update tbl_WorkMaster set InvoiceRequestby=@Created_by,InvoiceRequestdt=getdate(),InvoiceRequestStatus='Invoice Request' where intWorkid like @intWorklId    
   
   insert into tr_InvoiceStatus(invoiceid,intWorkid,Created_by,Created_dt)
  values(@ids,@intWorklId,@Created_by,getdate())
  
insert into td_InvoiceReqTrans([intInvoiceReqid],[intWorkid],[intFromid],[intToid],[Created_by],[Created_dt])
values(@ids,@intWorklId,@Created_by,@CoOrdinator_Id,@Created_by,GETDATE())
  
  insert into tr_WorkInvoiceDet ( intWorkid, Invoiceid, InvoiceStatus, InvoiceBy, InvoiceDt, Created_by, Created_dt)
  
  values (@intWorklId,@ids,'Invoice Request', @Created_by,getdate(),@Created_by,GETDATE())

 
  

      
select @@IDENTITY      
return @@IDENTITY      
      
end      
      
end




