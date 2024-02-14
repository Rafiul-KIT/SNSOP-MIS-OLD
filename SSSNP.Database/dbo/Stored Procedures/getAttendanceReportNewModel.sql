
CREATE Procedure [dbo].[getAttendanceReportNewModel] 
(  
	@FromDate datetime,  
	@ToDate datetime,  
	@intWorkActivityId varchar(10),
	@group_type varchar(100),
	@group_code varchar(100)   
)  
as  
begin  

CREATE TABLE #Temp          
(          
	[empcode] int,          
	[leave_date] datetime,          
	[type] char(9),          
	[Name] varchar(50), 
	[IDNo] varchar(50),          
	[PreseCnt] varchar(8),          
	--[AbsentCnt] varchar(8),          
	[Totcnt] varchar(8),
	[Age] int,
	[Gender] varchar(1),
	[Component] varchar(200)
)  
     
DECLARE 
		@Start datetime
		,@End datetime
		,@DList varchar(MAX)
		,@Sql varchar(max)          
Declare 
		@Tot as int

set  @Tot=DATEDIFF(dd,@FromDate , @ToDate+1)     
     
INSERT INTO #Temp          
	select 
		intBenid 
		,AttendanceDate 
		,Status 
		,NameofHousehold
		,EnrollmentNo
		,CASE
			when (Status='Present') then '1'
			else  '0' 
		END
		as PreseCnt

		--,'1' AS PreseCnt
		--,'0' AS AbsentCnt
		,@Tot as Totcnt 
		,Age
		,Gender
		,component
	from 
		Vw_BenDetforAttendance 
		
	where 
		AttendanceDate between @fromdate 
		and @ToDate+1  
		and intWorkActivityId like @intWorkActivityId
		

SELECT 
	DATEADD(dd,number,@fromdate) AS Date INTO #Date          
FROM master..spt_values          
WHERE type='p' and  DATEADD(dd,number,@fromdate)<=@ToDate          
                  
SELECT 
	@DList=LEFT(dl.Dates,LEN(dl.Dates)-1)          
FROM          
	(SELECT CAST(Date as varchar(11)) + ','          
	 FROM #Date          
	 FOR XML PATH('')          
	)dl(Dates)          
          
SET @Sql='SELECT *          
		FROM          
		(          
		SELECT distinct d.Date,t.IDNo as [Identification No],t.type,t.Name as [Beneficiary Name]
		,Age, Gender
		, Component
		,t.Totcnt  as [Total No of Days]
		,sum(cast (t.PreseCnt as int)) over (partition by t.idno)  as [No of Days Present]
		--t.PreseCnt   as [No of Days Present]
		,cast(t.Totcnt as int) -sum(cast (t.PreseCnt as int)) over (partition by t.idno)    as [No of Holidays or No Attendance]
          
		FROM #Date d          
		LEFT OUTER JOIN #Temp t          
		ON t.leave_date=d.Date 
		where t.Name is not null
		)t          
		PIVOT (MAX(type) FOR Date IN ([' +  REPLACE(@DList,',','],[')+ ']))p'          
      
EXEC(@Sql)  
end