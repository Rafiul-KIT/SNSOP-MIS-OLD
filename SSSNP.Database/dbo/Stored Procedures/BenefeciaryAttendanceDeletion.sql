
CREATE procedure [dbo].[BenefeciaryAttendanceDeletion]      
(      
      
      
@Benids varchar(8),      
@AttendanceDate NVarchar(MAX),            
@Created_by int      
      
       
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
intBenid int,
AttendanceDate datetime
)

	 insert into #tempAttend
select intAttendanceid,intBenid,AttendanceDate from tr_BenAttendanceDet  WITH(NOLOCK) where intBenid like @Benids and  WageApplyFlag is not null 

 Declare @s nvarchar(MAX) =@AttendanceDate
CREATE  TABLE #temp_tableattend (
    id varchar(50)
);

insert into #temp_tableattend (id)
SELECT  
     Split.a.value('.', 'NVARCHAR(MAX)') AS String  
 FROM  (SELECT   
         CAST ('<M>' + REPLACE(@s, ',', '</M><M>') + '</M>' AS XML) AS String  
     ) AS A CROSS APPLY String.nodes ('/M') AS Split(a);
        
    
	if((select count(*) from #tempAttend  as a   WITH(NOLOCK) where Convert(varchar(11), a.AttendanceDate,106) in (select convert(varchar(11),Convert(datetime, id),106) from #temp_tableattend as a  WITH(NOLOCK)) )<>0)
	Begin
	drop table #temp_tableattend  
	drop table #tempAttend 
	select 999
	return 999
	End
	Else
	Begin
	insert into tr_BenAttendanceDetTrans
  (
  intAttendanceid, intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt, Modified_by, Modified_dt, AttendanceYear, PortYear, PortPhase, PortedSheetName, PaymentFlag, 
                         Deletedby, DeletedDt, DeletedRemarks

  )
  select intAttendanceid, intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt, Modified_by, Modified_dt, AttendanceYear, PortYear, PortPhase, PortedSheetName, PaymentFlag, 
                         @Created_by, getdate(), 'Deleted on ' + Convert(varchar, (getdate())) 


  from tr_BenAttendanceDet where intBenid = @Benids and Convert(varchar(11), AttendanceDate,106)  in (select convert(varchar(11),Convert(datetime, id),106) from #temp_tableattend as a  WITH(NOLOCK))

	Delete from tr_BenAttendanceDet where intBenid like @Benids and Convert(varchar(11), AttendanceDate,106)  in (select convert(varchar(11),Convert(datetime, id),106) from #temp_tableattend as a  WITH(NOLOCK))
--insert into dbo.tr_BenAttendanceDet (intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt)        


--select @WorkCode,left(id, len(id) -1),@AttendanceDate,case when RIGHT( id, 1 )='P' then 'Present' else 'Absent' end ,@Created_by, GETDATE() from #temp_tableattend WITH(NOLOCK)

    drop table #temp_tableattend 
	drop table #tempAttend  
select 1000        
return 1000  
ENd

 -- drop table #temp_tableattend   
--values(@WorkCode, @intBenid, @AttendanceDate, @Status, @Created_by, GETDATE())  


   
          
  --end   
  
 end 




