CREATE Procedure [dbo].[getFundsDet]        
(        
@intWorklId varchar(10)        
)        
as begin        
        
select * from tr_FinancialApproval where intWorkId like @intWorklId    

select * from tr_FundsRaising where intWorklId like @intWorklId    
    
    
        
end




