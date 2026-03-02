CREATE procedure [dbo].[getIPWageProcessDashboard]
(
@intOfficerid varchar(8),
@OfficerLevel varchar(20)
)
As
Begin


select

(select count(intWagePaymentReqid) from td_WorkWageRequest where Created_by like @intOfficerid ) as TotalReq,
(select count(intWagePaymentReqid) from td_WorkWageRequest where Created_by like @intOfficerid and Status=1) as TotalReqCoodinator,
(select count(intWagePaymentReqid) from td_WorkWageRequest where Created_by like @intOfficerid and Status=2 ) as TotalReqSepcialist,
(select count(intWagePaymentReqid) from td_WorkWageRequest where Created_by like @intOfficerid and Status=3 )  as TotalReqManager,
(select count(intWagePaymentReqid) from td_WorkWageRequest where Created_by like @intOfficerid and (Status=100 or Status=101 or Status=102 or Status=103) ) as TotalReqRejected,
(select count(intWagePaymentReqid) from td_WorkWageRequest where Created_by like @intOfficerid and Status=4 ) as TotalReqpayingagent,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where intWagePaymentReqid in  (select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid )  and Status=6 ) as TotalReqPayCoOrdinator,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where intWagePaymentReqid in  (select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid ) and Status=7) as TotalReqPaySpecialist,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where intWagePaymentReqid in  (select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid ) and Status=8  ) as TotalReqPayManager,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where intWagePaymentReqid in  (select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid ) and Status=9  ) as TotalReqPayPayingAgentConfirmation,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where intWagePaymentReqid in  (select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid ) and Status=10  ) as TotalReqPayPayingAgentDone,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where intWagePaymentReqid in  (select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid ) and (Status=1000 or Status=1001 or Status=1002 or Status=1003)) as TotalReqRejectedPay,
(select count(intWagePaymentReqid) from td_WorkWageRequest where Created_by like @intOfficerid and (Status=100)) as CoOrdinatorRejectWage,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where intWagePaymentReqid in  (select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid ) and (Status=1000 ))  as CoOrdinatorRejectInvoice




End

