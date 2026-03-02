CREATE Procedure [dbo].[FundDisbursedReportsByTransDrildown]                      
                      
(                      
@intTST varchar(15),                      
@intIP  varchar(15)                     
)                      
as begin                      
             
                      
                      
SELECT *                                       
FROM        dbo.tr_FinancialApproval inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId
                       where tbl_WorkMaster.intTSTid like @intTST  and  tbl_WorkMaster.IPid like @intIP          
                                            
                                            
end




