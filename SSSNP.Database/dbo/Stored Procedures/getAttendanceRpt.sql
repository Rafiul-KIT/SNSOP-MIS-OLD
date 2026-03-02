CREATE Procedure [dbo].[getAttendanceRpt]  
(  
@Year varchar(10),  
@Month varchar(10),  
@intWorkActivityId varchar(10)   
)  
as  
begin  
  
  
  
  
  
  
  
  
  
  
  
  
  
           
          
CREATE TABLE #Temp          
(          
[empcode] int,          
[leave_date] datetime,          
[type] char(9),          
[Name] varchar(50),          
[AccountNo] varchar(50),          
[IDNo] varchar(50),          
[PreseCnt] varchar(8),          
[AbsentCnt] varchar(8),          
[Totcnt] varchar(8)          
)          
DECLARE @Start datetime,@End datetime,@DList varchar(2000),@Sql varchar(max)          
          
    Declare @FromDate as Datetime
    Declare @ToDate as datetime    
INSERT INTO #Temp          
select distinct top 10000 view_batchattandence.intTraineeID ,view_batchattandence.AttendenceDate ,view_batchattandence.Attendence , traineeName,Acc_No,view_batchattandence.traineeTempCode,          
        
(select top 1 COUNT( p.intTraineeID)  from tbl_TraineeAttendence as p where p.intTraineeID=view_batchattandence.intTraineeID and p.Attendence='Present' and p.AttendenceDate between @FromDate and @ToDate  ) AS PreseCnt,          
(select top 1 COUNT( p.intTraineeID)  from tbl_TraineeAttendence as p where p.intTraineeID=view_batchattandence.intTraineeID and p.Attendence='Absent' and p.AttendenceDate between @FromDate and @ToDate  ) AS AbsentCnt,          
(select top 1 DATEDIFF(dd,@FromDate , @ToDate)+1 ) as Totcnt          
 from view_batchattandence      
where tr_BenAttendanceDet.AttendenceDate between @fromdate and @ToDate 
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


SELECT distinct d.Date,t.IDNo as [Identification No],t.type,t.Name as [Trainee Name],t.AccountNo as [Account No],(convert(int,t.PreseCnt)+ convert(int,t.AbsentCnt))  as [Total No of Days],t.PreseCnt   as [No of Days Present],t.AbsentCnt   as [No of Days Absent]          

          
FROM #Date d          
LEFT OUTER JOIN #Temp t          
ON t.leave_date=d.Date          
)t          
PIVOT (MAX(type) FOR Date IN ([' +  REPLACE(@DList,',','],[')+ ']))p          
'          
          
          
         
             
EXEC(@Sql)  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
select distinct tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,  
tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.ContactNo,tbl_BeneficiaryDet.Email,tbl_BeneficiaryDet.FamilyIncome,tm_Bomas.BomaName,  
(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where YEAR(AttendanceDate)=@Year and MONTH(AttendanceDate) = @Month and intWorkActivityId 
like @intWorkActivityId   
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'

and  MONTH(tr_BenAttendanceDet.AttendanceDate) =@Month and YEAR(tr_BenAttendanceDet.AttendanceDate) =@Year) as cnt,

(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where YEAR(AttendanceDate)=@Year and MONTH(AttendanceDate) = @Month and intWorkActivityId 
like @intWorkActivityId   
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'

and  MONTH(tr_BenAttendanceDet.AttendanceDate) =@Month and YEAR(tr_BenAttendanceDet.AttendanceDate) =@Year)*70 as Amt


 from dbo.tbl_BeneficiaryDet  left outer join    
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid where tbl_BeneficiaryDet.WorkCode like @intWorkActivityId  

and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b)
end




