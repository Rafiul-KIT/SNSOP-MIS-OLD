CREATE Procedure [dbo].[getFundsDetByInvoiceID]        
(        
@Invoiceid  varchar(10)        
)        
as begin        
        
select tr_FinancialApproval.* from tr_FinancialApproval
inner join tr_WorkInvoiceDet on  tr_FinancialApproval.invoiceid = tr_WorkInvoiceDet.Invoiceid
 where tr_WorkInvoiceDet.Invoiceid like @Invoiceid 

select * from tr_FundsRaising 
where intFundRaise= @Invoiceid
   
    
    
        
end




