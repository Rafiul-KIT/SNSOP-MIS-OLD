CREATE procedure [dbo].[InsUpdBeneficiaryWagesRequestAlternateNewModel]    
(   
    
@Benids nvarchar(MAX)  ,    
   
@Created_by int,
@Wagerate decimal(18, 2),  
 @intWorkid varchar(8),
 @intWagePaymentReqid varchar(8),
@FromDate datetime,
@ToDate datetime
    
     
)    
----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-- Project : SNSDP

-- Purpose: Insert Wage Payment

-- Working Purpose : Insert Wage payment with USD adding Exchange Rate and USD Entry Field

-- Developer : Srikanth

-- Date: 04-10-2017

-- Modifeid Tables Used: tr_BenWagesPayment
-----------------------------------------------------------------------------
----------------------------------------------------------------------------- 
as begin    
    
    
 Declare @s nvarchar(MAX) =@Benids
CREATE  TABLE #temp_tableawage (
    id varchar(8)
);

insert into #temp_tableawage (id)
SELECT  
     Split.a.value('.', 'VARCHAR(100)') AS String  
 FROM  (SELECT   
         CAST ('<M>' + REPLACE(@s, ',', '</M><M>') + '</M>' AS XML) AS String  
     ) AS A CROSS APPLY String.nodes ('/M') AS Split(a);
        
  
	 create table #tempAttend
(
intAttendanceid int,
intWorkActivityId int,
intBenid int,
AttendanceDate datetime,
WageApplyFlag varchar(8)
)

	 insert into #tempAttend
select intAttendanceid,intWorkActivityId,intBenid,AttendanceDate,WageApplyFlag from tr_BenAttendanceDet where intWorkActivityId like @intWorkid and Status='Present' and AttendanceDate between @FromDate and @ToDate and WageApplyFlag is null 

create table #WorkAlloc
(
intBenid int,
intWorkid int
) 
insert into #WorkAlloc
select distinct intBenid,intWorkid  from   tr_BenfWorkDet where intWorkid like @intWorkid 
      
        
insert into td_WageReqBenDet(intBenid,  WageRateUSDTotal, Created_by, Created_dt,WageRateUSD,intWorkid,intWagePaymentReqid,TotAttendance,Status,traineealternate)        

--values(@intBenid,@PaidAmount, @Created_by,  GETDATE(),@Wagerate,@intWorkid,@intWagePaymentReqid,@TotAttendance,'Request',@trineealternate)        
   select  id,
   
  isnull( (isnull((select count(distinct AttendanceDate) from #tempAttend where  intBenid=aa.id ),0)  
   * isnull(@WageRate,0)),0),
   @Created_by,
    GETDATE(),isnull(@WageRate,0),@intWorkid,@intWagePaymentReqid,

	 (isnull((select count(distinct AttendanceDate) from #tempAttend where  intBenid=aa.id  ),0)),'Request',null
     from    #temp_tableawage  as aa
   
        
        
 Drop table #temp_tableawage
 Drop table #tempAttend
 Drop table #WorkAlloc
 select 1000        
return 1000    
end
