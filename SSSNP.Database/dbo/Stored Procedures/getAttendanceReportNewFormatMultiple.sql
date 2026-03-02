
CREATE procedure [dbo].[getAttendanceReportNewFormatMultiple] -- '05/23/2016','05/30/2016',1015
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
          
   if(@FromDate<>'' and @ToDate <>'')
   Begin
   select distinct ip.IPName as [IP],  s.StateName as [State Name],c.CountieName as [County Name],paym.PayamName as [Payam Name],bom.BomaName as [Boma Name],w.WorkCode,  b.EnrollmentNo as [Enrollment Number],b.NameofHousehold as [Benficiary Name], 
   b.Age, b.Sex as Gender, b.SelectionCriteria as Component
   ,b.ContactNo as [Mobile Number],Convert(varchar(11),ba.AttendanceDate, 106) as [Date],ba.Status       
 from tr_BenAttendanceDet  as ba  inner join tbl_BeneficiaryDet as b on b.intBenid= ba.intBenid inner join tr_BenfWorkDet as bw on bw.intBenid=b.intBenid and ba.intBenid=bw.intBenid and bw.intWorkid=ba.intWorkActivityId 
 inner join tbl_WorkMaster as w on w.intWorkid=ba.intWorkActivityId and w.intWorkid=bw.intWorkid
 inner join tm_Bomas as bom on bom.intBomasid=w.intBomasid
 inner join tm_Payams as paym on paym.intPayamid=bom.intPayamid
 inner join tm_Counties as c on c.intCountieid=paym.intCountieid
 inner join tm_States as s on s.intStateid=c.intStateid 
 inner join td_IPDetails as ip on ip.intIP=w.IPid
where ba.AttendanceDate between @fromdate and @ToDate+1  and w.WorkCode like @workCode 
and bom.intBomasid like @intBomasid and paym.intPayamid like @intPayamid and c.intCountieid like @intCountieid
and s.intStateid like @intStateid and w.IPid like @IPid
					  order by  b.EnrollmentNo asc
   End
   Else
   begin     
select distinct ip.IPName  as [IP],  s.StateName as [State Name],c.CountieName as [County Name],paym.PayamName as [Payam Name],bom.BomaName as [Boma Name],w.WorkCode,  b.EnrollmentNo as [Enrollment Number],b.NameofHousehold as [Benficiary Name],
b.Age, b.Sex as Gender, b.SelectionCriteria as Component
, b.ContactNo as [Mobile Number],Convert(varchar(11),ba.AttendanceDate, 106) as [Date],ba.Status       
 from tr_BenAttendanceDet  as ba  inner join tbl_BeneficiaryDet as b on b.intBenid= ba.intBenid inner join tr_BenfWorkDet as bw on bw.intBenid=b.intBenid and ba.intBenid=bw.intBenid and bw.intWorkid=ba.intWorkActivityId 
 inner join tbl_WorkMaster as w on w.intWorkid=ba.intWorkActivityId and w.intWorkid=bw.intWorkid
 inner join tm_Bomas as bom on bom.intBomasid=w.intBomasid
 inner join tm_Payams as paym on paym.intPayamid=bom.intPayamid
 inner join tm_Counties as c on c.intCountieid=paym.intCountieid
 inner join tm_States as s on s.intStateid=c.intStateid 
  inner join td_IPDetails as ip on ip.intIP=w.IPid
where w.WorkCode like @workCode 
and bom.intBomasid like @intBomasid and paym.intPayamid like @intPayamid and c.intCountieid like @intCountieid
and s.intStateid like @intStateid and w.IPid like @IPid
					  order by  b.EnrollmentNo asc

					  End
end