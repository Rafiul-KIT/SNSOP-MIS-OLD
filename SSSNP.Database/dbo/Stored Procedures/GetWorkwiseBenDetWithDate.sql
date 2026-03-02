CREATE Procedure [dbo].[GetWorkwiseBenDetWithDate]  
(  
@intBomasid varchar(10),
@AttendDate datetime  
)  
as begin  

--create table #attendance
--(
--intBenid int,
--intWorkActivityId int,
--AttendanceDate datetime
--)
--insert into #attendance
--select aten.intBenid,aten.intWorkActivityId,aten.AttendanceDate from tr_BenAttendanceDet as aten where aten.intWorkActivityId like @intBomasid and Convert(varchar(11),aten.AttendanceDate,106)=Convert(varchar(11),@AttendDate,106) and WageApplyFlag is not null 
  
select distinct tbl_BeneficiaryDet.*,tm_Bomas.BomaName,@intBomasid as intWorkid from dbo.tbl_BeneficiaryDet
inner join
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid  
  
 where  tbl_BeneficiaryDet.intBenid in (select distinct a.intBenid from  tr_BenfWorkDet as a where a.intWorkid like @intBomasid)
 order by tbl_BeneficiaryDet.EnrollmentNo asc
 --and tr_BenfWorkDet.intBenid not in (select at.intBenid from #attendance as at) order by EnrollmentNo --and WorkCode is null  
   
end
