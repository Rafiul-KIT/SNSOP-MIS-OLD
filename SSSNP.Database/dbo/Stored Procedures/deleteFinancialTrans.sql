CREATE Procedure [dbo].[deleteFinancialTrans]       
        
(        
@id varchar(10)        
)        
as begin        
        
delete from tr_FinancialApproval where intFundApproval like @id        
        
end




