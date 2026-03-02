CREATE procedure [dbo].[sp_deletebenfdetBulkAfterConfirmation]
@Benid varchar(10)
as
begin
--Declare @Benid as int
--set @Benid=(select intBenid from tbl_BeneficiaryDet where EnrollmentNo='MUN3/05/010')

--select * from tr_BenfWorkDet where intBenid=@Benid
--select * from td_WageReqBenDet where intBenid=@Benid
--select * from tr_BenAttendanceDet where intBenid=@Benid

insert into tr_BenfWorkDetTrans
(
intBenfWorkid, intBenid, intWorkid, Created_by, Created_dt, Modified_by, Modifieid_dt, PortYear, PortPhase, PortedSheetName, 
                      Deleted_by, Deleted_dt

)
select

intBenfWorkid, intBenid, intWorkid, Created_by, Created_dt, Modified_by, Modifieid_dt, PortYear, PortPhase, PortedSheetName, 
                      '1000', getdate() from tr_BenfWorkDet  where intBenid=@Benid


					  insert into tr_BenAttendanceDetTrans
					  (
					   intAttendanceid, intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt, Modified_by, Modified_dt, 
                      AttendanceYear, PortYear, PortPhase, PortedSheetName, PaymentFlag, Deletedby, DeletedDt

					  )
					  select
					  
					    intAttendanceid, intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt, Modified_by, Modified_dt, 
                      AttendanceYear, PortYear, PortPhase, PortedSheetName, PaymentFlag, '1000', getdate()
					  
					   from tr_BenAttendanceDet  where intBenid=@Benid 


					   insert into tr_BenWagesPaymentTrans
					   (
					    intWageid, intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt, Modified_by, Modified_dt, Wagerate, WageRateUSD, 
                      PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, ExchangeRate, WageYear, SheetNo, PortYear, PortPhase, PortedSheetName, AttendanceDays, 
                      DeletedBy, DeletedDt

					   )

					   select  intWageid, intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt, Modified_by, Modified_dt, Wagerate, WageRateUSD, 
                      PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, ExchangeRate, WageYear, SheetNo, PortYear, PortPhase, PortedSheetName, AttendanceDays, 
                   '1000', getdate()

from tr_BenWagesPayment where  intBenid=@Benid 

Delete from td_WageReqBenDet where intBenid=@Benid
Delete from tr_BenAttendanceDet where intBenid=@Benid
Delete from td_WageReqBenDet where intBenid=@Benid
Delete from td_WageReqBenPADet where intBenid=@Benid
Delete from tr_BenfWorkDet where intBenid=@Benid

Update tbl_BeneficiaryDet set WorkCode=NULL where  intBenid=@Benid

select 1000
return 1000
end  