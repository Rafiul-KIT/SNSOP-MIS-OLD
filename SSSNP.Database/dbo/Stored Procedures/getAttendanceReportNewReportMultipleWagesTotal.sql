
CREATE procedure [dbo].[getAttendanceReportNewReportMultipleWagesTotal] -- '05/23/2016','05/30/2016',1015
(  
@FromDate datetime,  
@ToDate datetime,  
@workCode varchar(120),
@intStateid varchar(8),
@intCountieid varchar(8),
@intPayamid varchar(8),
@intBomasid varchar(8) ,
@IPid varchar(8)  
)  
as  
begin  
  
 -- Developer : Srikanth
-- Date : 14-03-2018
-- Purpose: Attendance Report New Format       
      
					         
select  
 isnull(  sum(isnull( ba.PaidAmountUSD,0)),0) as PaidAmountInUSD  ,
--    SUM(
--	isnull(
--case when ba.AttendanceDays is not null then ba.AttendanceDays else 

--(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where (AttendanceDate between ba.FromDate and ba.ToDate) and intWorkActivityId 
--like ba.intWorkid   
--and a.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'

--and  (AttendanceDate between ba.FromDate and ba.ToDate))
--end
--,0)
--)
-- as AttendanceDays,

isnull(  sum(isnull(ba.PaidAmount,0)),0) as PaidAmountInSSP


 from tr_BenWagesPayment  as ba  inner join tbl_BeneficiaryDet as b on b.intBenid= ba.intBenid inner join tr_BenfWorkDet as bw on bw.intBenid=b.intBenid and ba.intBenid=bw.intBenid and bw.intWorkid=ba.intWorkid 
 inner join tbl_WorkMaster as w on w.intWorkid=ba.intWorkid and w.intWorkid=bw.intWorkid
 inner join tm_Bomas as bom on bom.intBomasid=w.intBomasid
 inner join tm_Payams as paym on paym.intPayamid=bom.intPayamid
 inner join tm_Counties as c on c.intCountieid=paym.intCountieid
 inner join tm_States as s on s.intStateid=c.intStateid 
  inner join td_IPDetails as ip on ip.intIP=w.IPid
where w.WorkCode like @workCode 
and bom.intBomasid like @intBomasid and paym.intPayamid like @intPayamid and c.intCountieid like @intCountieid
and s.intStateid like @intStateid and w.IPid like @IPid




	select sum(a.AttendanceDays) as TotalAttendance from (				         
select  
 --isnull(ba.AttendanceDays,0)
   isnull(
case when ba.AttendanceDays is not null then ba.AttendanceDays else 

(select ISNULL(MAX(td_WageReqBenDet.TotAttendance) ,0) as Cnt from td_WageReqBenDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenDet.intWagePaymentReqid where Convert(varchar(11),  td_WorkWageRequest.ReqFromDate,106)=Convert(varchar(11),  ba.FromDate,106) and Convert(varchar(11),  td_WorkWageRequest.ReqToDate,106)=Convert(varchar(11),  ba.ToDate,106)  and td_WorkWageRequest.intWorkid 
= ba.intWorkid     and td_WageReqBenDet.intWorkid 
= ba.intWorkid  
and td_WageReqBenDet.intBenid=ba.intBenid 
)
end
,0)

 as AttendanceDays


 from tr_BenWagesPayment  as ba  inner join tbl_BeneficiaryDet as b on b.intBenid= ba.intBenid inner join tr_BenfWorkDet as bw on bw.intBenid=b.intBenid and ba.intBenid=bw.intBenid and bw.intWorkid=ba.intWorkid 
 inner join tbl_WorkMaster as w on w.intWorkid=ba.intWorkid and w.intWorkid=bw.intWorkid
 inner join tm_Bomas as bom on bom.intBomasid=w.intBomasid
 inner join tm_Payams as paym on paym.intPayamid=bom.intPayamid
 inner join tm_Counties as c on c.intCountieid=paym.intCountieid
 inner join tm_States as s on s.intStateid=c.intStateid 
  inner join td_IPDetails as ip on ip.intIP=w.IPid
where w.WorkCode like @workCode 
and bom.intBomasid like @intBomasid and paym.intPayamid like @intPayamid and c.intCountieid like @intCountieid
and s.intStateid like @intStateid and w.IPid like @IPid
) as a

					

end




