CREATE Procedure  [dbo].[GetUserPermissionByUser](@invoiceid int)
as
select * from tr_FundsRaising where intFundRaise=@invoiceid



