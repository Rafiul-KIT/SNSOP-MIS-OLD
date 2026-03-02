  CREATE Procedure [dbo].[GetFundsUtilizationByIP](@id varchar(8))
as
 select w.WorkCode,f.FundsUtilizationDate ,f.FundsUtilizationFor ,f.Amount  from tbl_FundsUtilization f 
 join tbl_WorkMaster w
 on f.intWorkid = w.intWorkid
 where f.FundsUtilizationFor in ('Skilled labour','Tools','Materials')
 and w.IPid = @id



