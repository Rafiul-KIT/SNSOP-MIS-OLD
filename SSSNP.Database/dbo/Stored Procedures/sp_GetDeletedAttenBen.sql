


CREATE PROCEDURE [dbo].[sp_GetDeletedAttenBen]
@intIP varchar(10),
@intStateid varchar(10),
@intCountieid varchar(10),
@intPayamid varchar(10),
@intBomasid varchar(10),
@Household varchar(80),
@Benname varchar(100),
@Enrollmentno varchar(30),
@Workcode varchar(100),
@FromDate datetime,
@ToDate datetime
as
begin
if(@FromDate <>'' and @ToDate <>'')
Begin
select a.* from 
(select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tr_BenAttendanceDetTrans.intBenid,tbl_BeneficiaryDetTrans.EnrollmentNo,tbl_BeneficiaryDetTrans.NameofHousehold,tbl_BeneficiaryDetTrans.household_number,tbl_WorkMaster.WorkCode,  tr_BenAttendanceDetTrans.intWorkActivityId,tr_BenAttendanceDetTrans.AttendanceDate,
tr_BenAttendanceDetTrans.DeletedDt,tr_BenAttendanceDetTrans.DeletedRemarks
,u1.User_name as Createdby,tr_BenAttendanceDetTrans.Created_dt as Createddt,u.User_name as Deletedby

  from (select b.* from tr_BenAttendanceDetTrans as b where b.intAttendanceTransid in (select MIN(intAttendanceTransid) from  tr_BenAttendanceDetTrans as a group by a.intWorkActivityId,a.intBenid,a.AttendanceDate))  as tr_BenAttendanceDetTrans
  inner join tbl_BeneficiaryDetTrans on tbl_BeneficiaryDetTrans.intBenid=tr_BenAttendanceDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDetTrans.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDetTrans.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenAttendanceDetTrans.intWorkActivityId
		 	   inner join tbl_Users as u on u.intUserid=tr_BenAttendanceDetTrans.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=tr_BenAttendanceDetTrans.InsertedStaffid
  where   tr_BenAttendanceDetTrans.DeletedRemarks is not null  
 and tbl_BeneficiaryDetTrans.intIP like @intIP
 and tbl_BeneficiaryDetTrans.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDetTrans.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDetTrans.NameofHousehold like '%'+@Benname+'%'
 and  isnull(tbl_BeneficiaryDetTrans.household_number,'') like '%'+@Household+'%'
 and  isnull(tr_BenAttendanceDetTrans.intWorkActivityId,'') like '%'+@Workcode+'%'
 and tr_BenAttendanceDetTrans.DeletedDt between @FromDate and @ToDate+1

  union

  select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.household_number, tbl_WorkMaster.WorkCode, tr_BenAttendanceDetTrans.intWorkActivityId,tr_BenAttendanceDetTrans.AttendanceDate,
tr_BenAttendanceDetTrans.DeletedDt,tr_BenAttendanceDetTrans.DeletedRemarks
,u1.User_name as Createdby,tr_BenAttendanceDetTrans.Created_dt as Createddt,u.User_name as Deletedby
  from (select b.* from tr_BenAttendanceDetTrans as b where b.intAttendanceTransid in (select MIN(intAttendanceTransid) from  tr_BenAttendanceDetTrans as a group by a.intWorkActivityId,a.intBenid,a.AttendanceDate))  as tr_BenAttendanceDetTrans 
  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenAttendanceDetTrans.intWorkActivityId
		  	   inner join tbl_Users as u on u.intUserid=tr_BenAttendanceDetTrans.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=tr_BenAttendanceDetTrans.InsertedStaffid
  where   tr_BenAttendanceDetTrans.DeletedRemarks is not null  
 and tbl_BeneficiaryDet.intIP like @intIP
 and tbl_BeneficiaryDet.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDet.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDet.NameofHousehold like '%'+@Benname+'%'
 and  isnull(tbl_BeneficiaryDet.household_number,'') like '%'+@Household+'%'
 and   isnull(tr_BenAttendanceDetTrans.intWorkActivityId,'') like '%'+@Workcode+'%'
  and tr_BenAttendanceDetTrans.DeletedDt between @FromDate and @ToDate+1
  ) as a 
    order by a.DeletedDt desc
End
Else
Begin
select a.* from 
(select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tr_BenAttendanceDetTrans.intBenid,tbl_BeneficiaryDetTrans.EnrollmentNo,tbl_BeneficiaryDetTrans.NameofHousehold,tbl_BeneficiaryDetTrans.household_number,tbl_WorkMaster.WorkCode,  tr_BenAttendanceDetTrans.intWorkActivityId,tr_BenAttendanceDetTrans.AttendanceDate,
tr_BenAttendanceDetTrans.DeletedDt,tr_BenAttendanceDetTrans.DeletedRemarks
,u1.User_name as Createdby,tr_BenAttendanceDetTrans.Created_dt as Createddt,u.User_name as Deletedby
  from (select b.* from tr_BenAttendanceDetTrans as b where b.intAttendanceTransid in (select MIN(intAttendanceTransid) from  tr_BenAttendanceDetTrans as a group by a.intWorkActivityId,a.intBenid,a.AttendanceDate))  as tr_BenAttendanceDetTrans 
  inner join tbl_BeneficiaryDetTrans on tbl_BeneficiaryDetTrans.intBenid=tr_BenAttendanceDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDetTrans.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDetTrans.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenAttendanceDetTrans.intWorkActivityId
		  	   inner join tbl_Users as u on u.intUserid=tr_BenAttendanceDetTrans.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=tr_BenAttendanceDetTrans.InsertedStaffid
  where   tr_BenAttendanceDetTrans.DeletedRemarks is not null  
 and tbl_BeneficiaryDetTrans.intIP like @intIP
 and tbl_BeneficiaryDetTrans.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDetTrans.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDetTrans.NameofHousehold like '%'+@Benname+'%'
 and  isnull(tbl_BeneficiaryDetTrans.household_number,'') like '%'+@Household+'%'
 and  isnull(tr_BenAttendanceDetTrans.intWorkActivityId,'') like '%'+@Workcode+'%'

  union

  select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.household_number, tbl_WorkMaster.WorkCode, tr_BenAttendanceDetTrans.intWorkActivityId,tr_BenAttendanceDetTrans.AttendanceDate,
tr_BenAttendanceDetTrans.DeletedDt,tr_BenAttendanceDetTrans.DeletedRemarks
,u1.User_name as Createdby,tr_BenAttendanceDetTrans.Created_dt as Createddt,u.User_name as Deletedby
  from (select b.* from tr_BenAttendanceDetTrans as b where b.intAttendanceTransid in (select MIN(intAttendanceTransid) from  tr_BenAttendanceDetTrans as a group by a.intWorkActivityId,a.intBenid,a.AttendanceDate))  as tr_BenAttendanceDetTrans 
  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenAttendanceDetTrans.intWorkActivityId
		  	   inner join tbl_Users as u on u.intUserid=tr_BenAttendanceDetTrans.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=tr_BenAttendanceDetTrans.InsertedStaffid
  where   tr_BenAttendanceDetTrans.DeletedRemarks is not null  
 and tbl_BeneficiaryDet.intIP like @intIP
 and tbl_BeneficiaryDet.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDet.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDet.NameofHousehold like '%'+@Benname+'%'
 and  isnull(tbl_BeneficiaryDet.household_number,'') like '%'+@Household+'%'
 and   isnull(tr_BenAttendanceDetTrans.intWorkActivityId,'') like '%'+@Workcode+'%'
  ) as a 
    order by a.DeletedDt desc
End

  end


