CREATE Procedure [dbo].[UpdFundRaisingUsingInvoiceId]      
(      
      
@intWorklId int,      
@FundRaiseDate datetime,      
@FundRaiseAmount decimal(18, 2),      
@Remarks varchar(150),      
@Created_by int ,
@CoOrdinator_Id int,
@Specialist_Id int,     
@Manager_Id int ,
@Invoiceid int    
)  

    
as begin 

-- Declare 
Declare @ids as int
-- Declare     
      
     
      
--update tr_FundsRaising set FundRaiseDate=@FundRaiseDate, FundRaiseAmount=@FundRaiseAmount, Remarks=@Remarks,Modified_by=@Created_by,Modified_dt=GETDATE() where intWorklId=@intWorklId      

Update  tr_FundsRaising 
set  FundRaiseDate=@FundRaiseDate,
 FundRaiseAmount=@FundRaiseAmount,
  Remarks=@Remarks,
Modified_by=@Created_by,
Modified_dt=GETDATE()
  where intWorklId=@intWorklId and intFundRaise=@Invoiceid
    
	   Update tr_InvoiceStatus
set InvoiceStatus = null,
InvoiceRemarks=null,
Modified_by=Created_by,
Modified_dt=getdate()
where intWorkid=@intWorklId
and invoiceid=@Invoiceid


  insert into td_InvoiceReqTrans([intInvoiceReqid],[intWorkid],[intFromid],[intToid],[Created_by],[Created_dt])
values(@Invoiceid,@intWorklId,@Created_by,@CoOrdinator_Id,@Created_by,GETDATE())


     
select 1000      
return 1000      
      
end




