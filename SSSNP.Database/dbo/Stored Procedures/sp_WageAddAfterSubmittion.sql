create procedure sp_WageAddAfterSubmittion
(
@EnrollmentNo varchar(30)
)
As
Begin


Declare @Workid int
Declare @Benid int
set @Benid=(select intBenid from tbl_BeneficiaryDet where EnrollmentNo=@Enrollmentno)
set @Workid=(select MAx(tr_BenfWorkDet.intWorkid) from tr_BenfWorkDet where intBenid like @Benid) 

--select ReqNo,  @Enrollmentno as Nos,convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as FromDate,convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ToDate,      dbo.GetAttendacneCount(@Workid,@Benid,convert(datetime,td_WorkWageRequest.ReqFromDate),convert(datetime,td_WorkWageRequest.ReqToDate)) as Attendance,'3.00' as WageRate, dbo.GetAttendacneCount(@Workid,@Benid,convert(datetime,td_WorkWageRequest.ReqFromDate),convert(datetime,td_WorkWageRequest.ReqToDate))*3,Exchangerateattime from td_WorkWageRequest where  intWorkid=@Workid


insert into td_WageReqBenDet
(
  intWagePaymentReqid, intWorkid, intBenid, TotAttendance, WageRateUSD, WageRateUSDTotal, traineealternate, Status, Created_by, Created_dt, Modified_by, Modified_dt, 
                         ExchangeRateOLD, ExchangeRateNew


)
select  intWagePaymentReqid,intWorkid,@Benid,dbo.GetAttendacneCount(@Workid,@Benid,convert(datetime,td_WorkWageRequest.ReqFromDate),convert(datetime,td_WorkWageRequest.ReqToDate)),'3.00', dbo.GetAttendacneCount(@Workid,@Benid,convert(datetime,td_WorkWageRequest.ReqFromDate),convert(datetime,td_WorkWageRequest.ReqToDate))*3,null,'Request',Created_by,Created_dt,null,null,'51.45',Exchangerateattime from td_WorkWageRequest where  intWorkid=@Workid
and intWagePaymentReqid not in (select td_WageReqBenDet.intWagePaymentReqid from td_WageReqBenDet where intBenid like @Benid) and dbo.GetAttendacneCount(@Workid,@Benid,convert(datetime,td_WorkWageRequest.ReqFromDate),convert(datetime,td_WorkWageRequest.ReqToDate))<>0


select 1
return 1


End