CREATE Procedure [dbo].[ChkFundsApprovalByInvoice]        
(          
@invoiceid varchar(10),  
@PaidAmount decimal(18,2)  
)          
as begin          
  
  
select ISNULL( SUM(PaidAmount)+@PaidAmount,0) as TotAmt from tr_FinancialApproval where invoiceid like @invoiceid      
        
end




