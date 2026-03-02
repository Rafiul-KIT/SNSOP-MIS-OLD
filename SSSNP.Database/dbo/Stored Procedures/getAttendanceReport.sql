CREATE Procedure [dbo].[getAttendanceReport] -- '05/23/2016','05/30/2016',1015
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(10),
@group_type varchar(100),
@group_code varchar(100)   
)  
as  
begin  
  
 -- Developer : Srikanth
-- Date : 07-08-2017
-- Purpose: Attendance Report Time out expired        
          
CREATE TABLE #Temp          
(          
[empcode] int,          
[leave_date] datetime,          
[type] char(9),          
[Name] varchar(50), 
[IDNo] varchar(50),          
[PreseCnt] varchar(8),          
[AbsentCnt] varchar(8),          
[Totcnt] varchar(8)          
)  
-- Developer : Srikanth
-- Date : 07-08-2017
-- Purpose: Attendance Report Time out expired        
DECLARE @Start datetime,@End datetime,@DList varchar(MAX),@Sql varchar(max)          
    Declare @Tot as int
  set  @Tot=(select top 1 DATEDIFF(dd,@FromDate , @ToDate)+1 )      
     
INSERT INTO #Temp          
select distinct top 90000000000 ba.intBenid ,ba.AttendanceDate ,ba.Status , b.NameofHousehold,b.EnrollmentNo,          
        
--(select top 1 COUNT( p.intBenid)  from tr_BenAttendanceDet as p where p.intBenid=b.intBenid and p.Status='Present' and p.AttendanceDate between @FromDate and @ToDate  )
'1'
 AS PreseCnt,          
--(select top 1 COUNT( p.intBenid)  from tr_BenAttendanceDet as p where p.intBenid=b.intBenid and p.Status='Absent' and p.AttendanceDate between @FromDate and @ToDate  )
'0'
 AS AbsentCnt,          
@Tot as Totcnt          
 from tr_BenAttendanceDet  as ba  inner join tbl_BeneficiaryDet as b on b.intBenid= ba.intBenid   
where ba.AttendanceDate between @fromdate and @ToDate+1  and intWorkActivityId 
like @intWorkActivityId 

--and b.BeneficiaryType=(case when @group_type ='' then b.BeneficiaryType else @group_type end)
--					  and isnull(b.GroupId,'')=(case when @group_code is null then isnull(b.GroupId,'')   else @group_code end)  

--and view_batchattandence.intBatchID = @intBatchID     


  
--group by tblTrainee_Details.intTraineeID,tbl_TraineeAttendence.AttendenceDate,tbl_TraineeAttendence.Attendence,tblTrainee_Details.traineeName,tblTrainee_Details.Acc_No,tblTrainee_Details.traineePENCode          
          
          
--select * from #Temp          
          
--SELECT @Start=MIN(leave_date),@End=MAX(leave_date)          
SELECT @Start=MIN(@fromdate),@End=MAX(@ToDate)          
FROM #Temp           
          
SELECT DATEADD(dd,number,@Start) AS Date INTO #Date          
 FROM master..spt_values          
WHERE type='p' and  DATEADD(dd,number,@Start)<=@End          
          
          
          
SELECT @DList=LEFT(dl.Dates,LEN(dl.Dates)-1)          
FROM          
(SELECT CAST(Date as varchar(11)) + ','          
 FROM #Date          
 FOR XML PATH('')          
)dl(Dates)          
          
SET @Sql='SELECT *          
FROM          
(          


SELECT distinct d.Date,t.IDNo as [Identification No],t.type,t.Name as [Benficiary Name],t.Totcnt  as [Total No of Days],t.PreseCnt   as [No of Days Present],t.AbsentCnt   as [No of Days Absent]          
,0 as [No of Holidays or No Attendance]
          
FROM #Date d          
LEFT OUTER JOIN #Temp t          
ON t.leave_date=d.Date          
)t          
PIVOT (MAX(type) FOR Date IN ([' +  REPLACE(@DList,',','],[')+ ']))p          
'          
          
          
         
             
EXEC(@Sql)  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
--select distinct tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,  
--tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.ContactNo,tbl_BeneficiaryDet.Email,tbl_BeneficiaryDet.FamilyIncome,tm_Bomas.BomaName,  
--(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where (AttendanceDate between @FromDate and @ToDate) and intWorkActivityId 
--like @intWorkActivityId   
--and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'

--and  (AttendanceDate between @FromDate and @ToDate)) as cnt,

--(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where (AttendanceDate between @FromDate and @ToDate ) and intWorkActivityId 
--like @intWorkActivityId   
--and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'

--and  (AttendanceDate between @FromDate and @ToDate ))*70 as Amt


-- from dbo.tbl_BeneficiaryDet  left outer join    
--tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid where tbl_BeneficiaryDet.WorkCode like @intWorkActivityId  

--and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b)
end




