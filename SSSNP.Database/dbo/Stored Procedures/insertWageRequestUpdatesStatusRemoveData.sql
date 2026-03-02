CREATE procedure [dbo].[insertWageRequestUpdatesStatusRemoveData]
(

@intWorkid	int,
@WageRateUSD	decimal(18, 2),
@ReqFromDate	datetime,
@ReqToDate	datetime,
@ReqYear	varchar(5),
@ReqMonth	varchar(10),
@intPayingAgentid	int,
@intCoOrdinatorid	int,
@intSpecialistid	int,
@intManagerid	int,
@Exchangerateattime	decimal(18, 2),

@Created_by	int

	
)
As
Begin
Delete from td_WorkWageRequest where intWagePaymentReqid not in (select a.intWagePaymentReqid from td_WageReqBenDet as a) 
select 1000
return 1000


End

