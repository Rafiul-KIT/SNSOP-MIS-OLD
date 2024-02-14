
CREATE procedure [dbo].[getCheckingWagesApplyrestrictionbyIDNewModel]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(8),
@Benids nvarchar(MaX)
)  
as  
begin 

 Declare @s nvarchar(MAX) =@Benids
CREATE  TABLE #temp_table (
    id varchar(8)
);

insert into #temp_table (id)
SELECT  
     Split.a.value('.', 'VARCHAR(100)') AS String  
 FROM  (SELECT   
         CAST ('<M>' + REPLACE(@s, ',', '</M><M>') + '</M>' AS XML) AS String  
     ) AS A CROSS APPLY String.nodes ('/M') AS Split(a);
     
--select * from temp_table

create table #attendance
(
EnrollmentNo varchar(30),
NameofHousehold varchar(180),
intBenid varchar(8),
intWorkActivityId varchar(8)
)
insert into #attendance
select EnrollmentNo,NameofHousehold,b.intBenid,intWorkActivityId from  tr_BenAttendanceDet  inner join tbl_BeneficiaryDet as b on b.intBenid=tr_BenAttendanceDet.intBenid and b.intBenid=tr_BenAttendanceDet.intBenid where tr_BenAttendanceDet.intWorkActivityId like @intWorkActivityId and tr_BenAttendanceDet.WageApplyFlag is not null and tr_BenAttendanceDet.AttendanceDate between @FromDate and @ToDate

select distinct b.EnrollmentNo,b.NameofHousehold  from #attendance as b inner join #temp_table on #temp_table.id=b.intBenid 
  --select * from td_WorkWageRequest where intWorkid like @intWorkActivityId and Convert(varchar(11),ReqFromDate,103)= Convert(varchar(11),@FromDate,103) and  Convert(varchar(11),ReqToDate,103)= Convert(varchar(11),@ToDate,103) and intWagePaymentReqid in
  --(
  --select a.intWagePaymentReqid from td_WageReqBenDet as a where a.intBenid like @intBenid and a.intWorkid like @intWorkActivityId
  --)

  -- select intAttendanceid from tr_BenAttendanceDet where  intBenid like @intBenid and WageApplyFlag is not null and AttendanceDate between @FromDate and @ToDate+1

  Drop table #temp_table
  Drop table #attendance

end

