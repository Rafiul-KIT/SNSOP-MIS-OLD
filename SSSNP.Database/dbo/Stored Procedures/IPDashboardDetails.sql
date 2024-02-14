CREATE Procedure [dbo].[IPDashboardDetails](@UserId int)

as
declare @InvoiceSubmitted int

declare @InvoiceReviewed int
declare @InvoiceVerified int
declare @InvoiceApproved int
declare @InvoiceRejected int
set  @InvoiceSubmitted=(select isnull(Count(tr_InvoiceStatus.ID),0) from tr_InvoiceStatus inner join tr_FundsRaising on tr_InvoiceStatus.invoiceid=tr_FundsRaising.intFundRaise  where  tr_FundsRaising.Created_by =@UserId and isnull(InvoiceStatus,'') not in ('Invoice Submitted'))
set  @InvoiceVerified=(select isnull(Count(tr_InvoiceStatus.ID),0) from tr_InvoiceStatus inner join tr_FundsRaising on tr_InvoiceStatus.invoiceid=tr_FundsRaising.intFundRaise  where  tr_FundsRaising.Created_by=@UserId and InvoiceStatus ='Sp-Approve')
set  @InvoiceReviewed=(select isnull(Count(tr_InvoiceStatus.ID),0) from tr_InvoiceStatus inner join tr_FundsRaising on tr_InvoiceStatus.invoiceid=tr_FundsRaising.intFundRaise  where  tr_FundsRaising.Created_by=@UserId and InvoiceStatus ='Co-Approve')
set  @InvoiceApproved=(select isnull(Count(tr_InvoiceStatus.ID),0) from tr_InvoiceStatus inner join tr_FundsRaising on tr_InvoiceStatus.invoiceid=tr_FundsRaising.intFundRaise  where  tr_FundsRaising.Created_by=@UserId and InvoiceStatus ='Ma-Approve')
set  @InvoiceRejected=(select isnull(Count(tr_InvoiceStatus.ID),0) from tr_InvoiceStatus inner join tr_FundsRaising on tr_InvoiceStatus.invoiceid=tr_FundsRaising.intFundRaise  where  tr_FundsRaising.Created_by=@UserId and InvoiceStatus ='Co-Reject')
select @InvoiceSubmitted as InvoiceSubmitted ,
@InvoiceVerified as InvoiceVerified,
@InvoiceReviewed as InvoiceReviewed,
@InvoiceApproved as InvoiceApproved,
@InvoiceRejected as InvoiceRejected





