CREATE Procedure [dbo].[BenefeciaryAttendanceNewModel]      
(      
      
@WorkCode int,      
@Benids NVarchar(MAX),      
@AttendanceDate datetime,          
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
intWorkActivityId int,
intBenid int,
AttendanceDate datetime
)

	 insert into #tempAttend
select intAttendanceid,intWorkActivityId,intBenid,AttendanceDate 
	from tr_BenAttendanceDet  WITH(NOLOCK) 
		where intWorkActivityId like @WorkCode 
			and Status='Present' 
			and Convert(varchar(11), AttendanceDate,106) like convert(varchar(11),@AttendanceDate,106) 
	--and  WageApplyFlag is not null 

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
	select 999
	return 999
	End
	Else
	Begin
insert into dbo.tr_BenAttendanceDet (intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt)        


select @WorkCode,left(id, len(id) -1),@AttendanceDate,case when RIGHT( id, 1 )='P' then 'Present' else 'Absent' end ,@Created_by, GETDATE() from #temp_tableattend WITH(NOLOCK)

    drop table #temp_tableattend 
	drop table #tempAttend  
select 1000        
return 1000  
ENd

 -- drop table #temp_tableattend   
--values(@WorkCode, @intBenid, @AttendanceDate, @Status, @Created_by, GETDATE())  


   
          
  --end   
  
 end 



