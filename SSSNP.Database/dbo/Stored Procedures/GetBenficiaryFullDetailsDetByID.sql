
 
CREATE Procedure [dbo].[GetBenficiaryFullDetailsDetByID]   -- exec GetBenficiaryFullDetailsDetByID '28252'            
(            
@intBenid varchar(8)            
)            
as begin    


Declare @FilePaths varchar(380)
set @FilePaths=(select FileServerPath from tm_FilePathMap)

Declare @BiometricFilePaths varchar(380)
set @BiometricFilePaths=(select BiomatricPath from tm_FilePathMap)


create table #tblAlternatemorethan
(
intBenid int,
household_number varchar(240),
alternate_number varchar(240),
alternate_name varchar(180),
photo_url varchar(180),
Photopath varchar(380)
)
insert into #tblAlternatemorethan
select intBenid,household_number,alternate_number,alternate_name,photo_url,@BiometricFilePaths + photo_url as Photopath from tbl_BenfMorethanAlternateDet where intBenid=@intBenid
       
           
select   b.intBenid,s.StateName,c.CountieName,p.PayamName,bom.BomaName, ip.IPName,  
b.EnrollmentNo,b.NameofHousehold,  b.NameofSpouse,b.household_number,
case when b.Sex='M' then 'Male' else 'Female' end as Gender
,b.PhotoName,b.PhotoPath,   b.Name1stAlternatePayee,
b.alternate_number,b.Name2ndAlternatePayee,b.Secondalternate_number

,convert(varchar(11),  b.Created_dt,106) as ConfirmedorCreatedDate,
case when b.PhotoPath is null then @BiometricFilePaths + b.PhotoName else  REPLACE(b.PhotoPath,   '~/',@FilePaths) end as PhotoURL,
 case when b.FirstAlternatePayeePhotoPath is null then @BiometricFilePaths + b.FirstAlternatePayeePhotoName else REPLACE(b.FirstAlternatePayeePhotoPath,   '[path]',@FilePaths) end
  as FirstAlternatePayeePhotoURL,
case when b.SecondAlternatePayeePhotoPath is null then @BiometricFilePaths + b.SecondAlternatepayeePhotoName else REPLACE(  b.SecondAlternatePayeePhotoPath,'[path]',      @FilePaths) end as SecondAlternatePayeePhotoURL
,b.Age,b.FirstAlternatePayeeAge,
case when b.FirstAlternatePayeeGender='M' then 'Male' else case when b.FirstAlternatePayeeGender is null  then '' else 'Female' end end FirstAlternatePayeeGender ,
b.SecondAlternatePayeeAge,
case when b.SecondAlternatePayeeGender='M' then 'Male' else case when b.SecondAlternatePayeeGender is null  then '' else 'Female' end end SecondAlternatePayeeGender
from dbo.tbl_BeneficiaryDet as b
inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
inner join tm_Payams as p on p.intPayamid=bom.intPayamid
inner join tm_Counties as c on c.intCountieid=p.intCountieid
inner join tm_States as s on s.intStateid=c.intStateid
inner join td_IPDetails as ip on ip.intIP=b.intIP
 where
 
 intBenid = @intBenid  

 --Work Code
 select distinct w.WorkCode from tr_BenfWorkDet as bw inner join tbl_WorkMaster as w on w.intWorkid=bw.intWorkid
  where bw.intBenid =@intBenid
 
  -- and
  --w.intworkid in (select a.intworkid from td_WageReqBenDet as a where a.intBenid=@intBenid )
 
 --Attendance
 select count(distinct AttendanceDate) as Attendance from tr_BenAttendanceDet as bw  where bw.intBenid =@intBenid and Status='Present'
 -- Wage requests
 
SELECT DISTINCT
                  w.WorkCode,     td_WorkWageRequest.ReqNo as RequestNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate, CONVERT(varchar(11),
                         dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate,td_WageReqBenDet.TotAttendance AS TotalWorkDays,
                        dbo.td_WageReqBenDet.WageRateUSDTotal,
case when td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<>100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate


FROM            dbo.td_WorkWageRequest
INNER JOIN td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid= td_WorkWageRequest.intWagePaymentReqid
inner join tbl_WorkMaster as w on w.intWorkid=td_WorkWageRequest.intWorkid
where td_WageReqBenDet.intBenid =@intBenid
  and td_WageReqBenDet.TotAttendance<>0
  order by ReqNo asc


 --Paid Details
 SELECT DISTINCT
                  w.WorkCode,     td_WorkWageRequest.ReqNo as RequestNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate, CONVERT(varchar(11),
                         dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate,td_WageReqBenDet.TotAttendance AS TotalWorkDays,
                        dbo.td_WageReqBenDet.WageRateUSDTotal,
case when td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<>100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate,
tr_BenWagesPayment.PaidAmountUSD  AS Amt1,
case when tr_BenWagesPayment.Paidto is null then '' else   case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   tbl_BeneficiaryDet.NameofHousehold else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name1stAlternatePayee else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name2ndAlternatePayee else case when (select count(*)  from tbl_BenfMorethanAlternateDet where alternate_number=tr_BenWagesPayment.Paidto) <>0 then (select alternate_name from tbl_BenfMorethanAlternateDet where alternate_number=tr_BenWagesPayment.Paidto) else  'Paid Person details is not available in MIS due to alternative not registered.' end end end end end as PaidToName,

case when tr_BenWagesPayment.Paidto is null then 'Unpaid' else 'Paid' end as PayStatus, tr_BenWagesPayment.Paidto,
case when tr_BenWagesPayment.Paidto is null then '' else   case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   'Household' else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else case when (select count(*)  from tbl_BenfMorethanAlternateDet where alternate_number=tr_BenWagesPayment.Paidto) <>0 then 'Alternate' else  '' end end end end end as PaidToType


FROM            dbo.td_WorkWageRequest
INNER JOIN td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid= td_WorkWageRequest.intWagePaymentReqid
inner join tbl_WorkMaster as w on w.intWorkid=td_WorkWageRequest.intWorkid
inner join tr_BenWagesPayment on tr_BenWagesPayment.trans_id=td_WageReqBenDet.intWagesReqBenTransid
and tr_BenWagesPayment.intBenid=td_WageReqBenDet.intBenid and w.intWorkid=td_WorkWageRequest.intWorkid
inner join  dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid
where td_WageReqBenDet.intBenid =@intBenid

 and td_WageReqBenDet.TotAttendance<>0  


 order by ReqNo asc

 --AttendnaceDetailsall
 select distinct AttendanceDate, Convert(varchar(11), AttendanceDate,103) as attedance ,Status from tr_BenAttendanceDet as bw  where bw.intBenid =@intBenid order by AttendanceDate asc

 -- Wage requests Pending
 
SELECT DISTINCT
                  w.WorkCode,     td_WorkWageRequest.ReqNo as RequestNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate, CONVERT(varchar(11),
                         dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate,td_WageReqBenDet.TotAttendance AS TotalWorkDays,
                        dbo.td_WageReqBenDet.WageRateUSDTotal,
case when td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<>100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate
,td_WageReqBenDet.intWagesReqBenTransid

FROM            dbo.td_WorkWageRequest
INNER JOIN td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid= td_WorkWageRequest.intWagePaymentReqid
inner join tbl_WorkMaster as w on w.intWorkid=td_WorkWageRequest.intWorkid
where td_WageReqBenDet.intBenid =@intBenid
  and td_WageReqBenDet.TotAttendance<>0
  and td_WageReqBenDet.intWagesReqBenTransid not in (select isnull(p.trans_id,0) from tr_BenWagesPayment as p where p.intBenid like @intBenid)
  order by ReqNo asc


  select * from #tblAlternatemorethan where intBenid =@intBenid and alternate_number in (select a.Paidto from tr_BenWagesPayment as a)

  Drop table #tblAlternatemorethan
       
end


