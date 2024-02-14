CREATE Procedure [dbo].[getVerifyAttendancedet]
@id varchar(8),
@Attendancedate datetime
as begin

--select distinct intWorkActivityId from tr_BenAttendanceDet where intWorkActivityId lik 

select * from tr_BenAttendanceDet where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid  
                           where tr_BenfWorkDet.intBenid  like @id) and convert(varchar,cast(AttendanceDate as datetime),103) LIKE convert(varchar,cast(@Attendancedate as datetime),103)      order by intAttendanceid asc
--  AttendanceDate like  


end




