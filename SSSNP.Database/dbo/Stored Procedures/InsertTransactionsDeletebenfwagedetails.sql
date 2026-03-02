

CREATE procedure [dbo].[InsertTransactionsDeletebenfwagedetails]
(
 @Benid int,
 @intTransid int,
 @DeletedBy int
)
As
Begin



	insert into   td_WageReqBenTransDet
	(
	intWagesReqBenTransid, intWagePaymentReqid, intWorkid, intBenid, TotAttendance, WageRateUSD, WageRateUSDTotal, traineealternate, Status, Created_by, Created_dt, Modified_by, 
                         Modified_dt, Deleted_dt, DeletedRemarks

	)

   select intWagesReqBenTransid, intWagePaymentReqid, intWorkid, intBenid, TotAttendance, WageRateUSD, WageRateUSDTotal, traineealternate, Status, Created_by, Created_dt, @DeletedBy, 
                         Modified_dt, getdate(),'Deleted on' + Convert(varchar, (getdate()))  from td_WageReqBenDet where intBenid = @Benid and intWagesReqBenTransid=@intTransid

 -- insert into   td_WageReqBenTransDet
	--(
	--intWagesReqBenTransid, intWagePaymentReqid, intWorkid, intBenid, TotAttendance, WageRateUSD, WageRateUSDTotal, traineealternate, Status, Created_by, Created_dt, Modified_by, 
 --                        Modified_dt, Deleted_dt, DeletedRemarks

	--)

 --  select intWagesReqBenTransid, intWagePaymentReqid, intWorkid, intBenid, TotAttendance, WageRateUSD, WageRateUSDTotal, traineealternate, Status, Created_by, Created_dt, Modified_by, 
 --                        Modified_dt, getdate(), 'Deleted on' + Convert(varchar, (getdate())) from td_WageReqBenDet  where intBenid = @Benid
 -- and td_WageReqBenDet.intWagesReqBenTransid not in (select isnull(trans_id,0) from tr_BenWagesPayment)


  insert into tr_BenWagesPaymentNotPaidTrans
  (
   intWageid, intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt, Modified_by, Modified_dt, Wagerate, WageRateUSD, PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, 
                         ExchangeRate, WageYear, SheetNo, PortYear, PortPhase, PortedSheetName, AttendanceDays, TempWageRateUSD, TempPaidAmountUSD, PaidGenerateddate, Paidto, trans_id, uuid, Deleted_dt, DeletedRemarks

  )

select  intWageid, intBenid, Year, Month, null, PaidAmount, Created_by, Created_dt, @DeletedBy, Modified_dt, Wagerate, WageRateUSD, PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, 
                         ExchangeRate, WageYear, SheetNo, PortYear, PortPhase, PortedSheetName, AttendanceDays, TempWageRateUSD, TempPaidAmountUSD, PaidGenerateddate, Paidto, trans_id, uuid, getdate(), 'Deleted on' + Convert(varchar, (getdate()))  from tr_BenWagesPaymentNotPaid where trans_id in
  (

   select intWagesReqBenTransid from td_WageReqBenDet where intBenid = @Benid and intWagesReqBenTransid=@intTransid
  )


  
--  insert into tr_BenWagesPaymentNotPaidTrans
--  (
--   intWageid, intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt, Modified_by, Modified_dt, Wagerate, WageRateUSD, PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, 
--                         ExchangeRate, WageYear, SheetNo, PortYear, PortPhase, PortedSheetName, AttendanceDays, TempWageRateUSD, TempPaidAmountUSD, PaidGenerateddate, Paidto, trans_id, uuid, Deleted_dt, DeletedRemarks

--  )

--select  intWageid, intBenid, Year, Month, null, PaidAmount, Created_by, Created_dt, Modified_by, Modified_dt, Wagerate, WageRateUSD, PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, 
--                         ExchangeRate, WageYear, SheetNo, PortYear, PortPhase, PortedSheetName, AttendanceDays, TempWageRateUSD, TempPaidAmountUSD, PaidGenerateddate, Paidto, trans_id, uuid, getdate(),'Deleted on' + Convert(varchar, (getdate()))  from tr_BenWagesPaymentNotPaid where trans_id in
--  (

--  select intWagesReqBenTransid from td_WageReqBenDet  where intBenid = @Benid
--  and td_WageReqBenDet.intWagesReqBenTransid not in (select isnull(trans_id,0) from tr_BenWagesPayment)
--  )

  Delete from tr_BenWagesPaymentNotPaid where trans_id in
  (

   select intWagesReqBenTransid from td_WageReqBenDet where intBenid = @Benid and intWagesReqBenTransid=@intTransid
  )

  Declare @Fromdate datetime
  Declare @ToDate datetime
  set @FromDate=(select ReqFromDate from td_WageReqBenDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenDet.intWagePaymentReqid where td_WageReqBenDet.intBenid=@Benid and  td_WageReqBenDet.intWagesReqBenTransid=@intTransid )
   set @ToDate=(select ReqToDate from td_WageReqBenDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenDet.intWagePaymentReqid where td_WageReqBenDet.intBenid=@Benid and  td_WageReqBenDet.intWagesReqBenTransid=@intTransid )
 
 
  update tr_BenAttendanceDet set WageApplyFlag=NULL,intWageReqid=NULL where intBenid = @Benid and AttendanceDate between @FromDate and @ToDate+1
  
Delete from td_WageReqBenDet  where intBenid = @Benid and intWagesReqBenTransid=@intTransid 


  select 1000
  return 1000
End


