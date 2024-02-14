Create Procedure [dbo].[BeneficiaryAttendanceNewModelDIS]      
(      
      
@WorkCode int,      
@Benids NVarchar(MAX),      
@AttendanceRifYear nvarchar(10),          
@AttendanceRifMonth int,          
@AttendanceRifTotalDay decimal(10,2),          
@Created_by int   ,
@returnValue int OUTPUT
      
       
)      
as begin      
 If OBJECT_ID (N'#temp_tableattend') IS NOT NULL 
Begin
Drop table #temp_tableattend;
End
 If OBJECT_ID (N'#tempAttend') IS NOT NULL 
Begin
Drop table #tempAttend;
End

create table #tempAttend
(
intAttendanceid int,
intWorkActivityId int,
intBenid int,
attendanceRifYear nvarchar(10),          
attendanceRifMonth int,          
attendanceRifTotalDay decimal(10,2)
)

	 insert into #tempAttend
select intAttendanceid,intWorkActivityId,intBenid,AttendanceRifYear, AttendanceRifMonth, AttendanceRifTotalDay 
	from tr_BenAttendanceDetDIS  WITH(NOLOCK) 
		where intWorkActivityId like @WorkCode 
			and Status='Present' 
			-- and Convert(varchar(11), AttendanceDate,106) like convert(varchar(11),@AttendanceDate,106) 
			and  AttendanceRifYear = @AttendanceRifYear and AttendanceRifMonth = @AttendanceRifMonth
	

 Declare @s nvarchar(MAX) =@Benids
CREATE  TABLE #temp_tableattend (
    id varchar(50)
);

insert into #temp_tableattend (id)
SELECT  
     Split.a.value('.', 'NVARCHAR(MAX)') AS String  
 FROM  (SELECT   
         CAST ('<M>' + REPLACE(@s, ',', '</M><M>') + '</M>' AS XML) AS String  
     ) AS A CROSS APPLY String.nodes ('/M') AS Split(a);
        
    
	if((select count(*) from #tempAttend  as a   WITH(NOLOCK) where a.intBenid in (select left(a.id, len(id) -1) from #temp_tableattend as a  WITH(NOLOCK)) )<>0)
	Begin
	drop table #temp_tableattend  
	drop table #tempAttend 
	      set @ReturnValue = 999  ;
		  return @ReturnValue
	
	End
	Else
	Begin
insert into dbo.tr_BenAttendanceDetDIS(intWorkActivityId, intBenid, AttendanceRifYear, AttendanceRifMonth, AttendanceRifTotalDay , Status, Created_by, Created_dt)        

select @WorkCode,left(id, len(id) -1),@AttendanceRifYear,@AttendanceRifMonth,@AttendanceRifTotalDay,case when RIGHT( id, 1 )='P' then 'Present' else 'Absent' end ,@Created_by, GETDATE() from #temp_tableattend WITH(NOLOCK)

    drop table #temp_tableattend 
	drop table #tempAttend  

    set @ReturnValue = 1000  ;
	return @ReturnValue
  
ENd

  
  
 end