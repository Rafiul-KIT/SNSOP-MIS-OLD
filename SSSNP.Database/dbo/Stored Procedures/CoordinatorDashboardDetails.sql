CREATE Procedure [dbo].[CoordinatorDashboardDetails]( @UserId int)

as
declare @InvoiceSubmittedForReview int
declare @PendingInvoiceForReviewRejection int
declare @InvoiceReviewed int
declare @InvoiceRejected int
set  @InvoiceSubmittedForReview=(select isnull(Count(tr_InvoiceStatus.ID),0) from tr_InvoiceStatus inner join tr_FundsRaising on tr_InvoiceStatus.invoiceid=tr_FundsRaising.intFundRaise  where  tr_FundsRaising.CoOrdinator_Id=@UserId )
set  @PendingInvoiceForReviewRejection=(select isnull(Count(tr_InvoiceStatus.ID),0) from tr_InvoiceStatus inner join tr_FundsRaising on tr_InvoiceStatus.invoiceid=tr_FundsRaising.intFundRaise   where isnull(tr_InvoiceStatus.InvoiceStatus,'') in ('Sp-Reject','') and tr_FundsRaising.CoOrdinator_Id=@UserId)
set  @InvoiceReviewed=(select isnull(Count(tr_InvoiceStatus.ID),0) from tr_InvoiceStatus inner join tr_FundsRaising on tr_InvoiceStatus.invoiceid=tr_FundsRaising.intFundRaise where tr_InvoiceStatus.InvoiceStatus in('Co-Approve','Sp-Approve','Ma-Approve','Sp-Reject','Ma-Reject') and tr_FundsRaising.CoOrdinator_Id=@UserId)
set  @InvoiceRejected=(select isnull(Count(tr_InvoiceStatus.ID),0) from tr_InvoiceStatus inner join tr_FundsRaising on tr_InvoiceStatus.invoiceid=tr_FundsRaising.intFundRaise where InvoiceStatus='Co-Reject' and tr_FundsRaising.CoOrdinator_Id=@UserId)
select @InvoiceSubmittedForReview as InvoiceSubmittedForReview ,
@PendingInvoiceForReviewRejection as PendingInvoiceForReviewRejection,
@InvoiceReviewed as InvoiceReviewed,@InvoiceRejected as InvoiceRejected





