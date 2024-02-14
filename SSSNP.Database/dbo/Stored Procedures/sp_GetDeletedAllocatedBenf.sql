


CREATE PROCEDURE [dbo].[sp_GetDeletedAllocatedBenf]
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
select distinct a.IPName,a.StateName,a.CountieName,a.PayamName,a.BomaName,a.intBenid,a.EnrollmentNo,a.NameofHousehold,a.household_number,a.WorkCode,  a.intWorkid,
a.Deleted_dt as DeletedDt,a.DeletedRemarks,
 Createdby,Createddt,Deletedby




 from
(
select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tr_BenfWorkDetTrans.intBenid,tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.household_number,tbl_WorkMaster.WorkCode,  tr_BenfWorkDetTrans.intWorkid,
tr_BenfWorkDetTrans.Deleted_dt,tr_BenfWorkDetTrans.DeletedRemarks,

u1.User_name as Createdby,tr_BenfWorkDetTrans.Created_dt as Createddt,u.User_name as Deletedby

  from   (select b.* from tr_BenfWorkDetTrans as b where b.intBenfWorkTransid in (select MIN(a.intBenfWorkTransid) from  tr_BenfWorkDetTrans as a group by a.intBenid,a.intWorkid)) as    tr_BenfWorkDetTrans 
  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenfWorkDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDetTrans.intWorkid
		  inner join tbl_Users as u on u.intUserid=tr_BenfWorkDetTrans.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=tr_BenfWorkDetTrans.InsertedStaffid

  where   tr_BenfWorkDetTrans.DeletedRemarks is not null  
 and tbl_BeneficiaryDet.intIP like @intIP
 and tbl_BeneficiaryDet.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDet.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDet.NameofHousehold like '%'+@Benname+'%'
 and  isnull(tbl_BeneficiaryDet.household_number,'') like '%'+@Household+'%'
 and  tr_BenfWorkDetTrans.intWorkid like '%'+@Workcode+'%'
   and tr_BenfWorkDetTrans.Deleted_dt between @FromDate and @ToDate+1
 union
 select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tr_BenfWorkDetTrans.intBenid,tbl_BeneficiaryDetTrans.EnrollmentNo,tbl_BeneficiaryDetTrans.NameofHousehold,tbl_BeneficiaryDetTrans.household_number,tbl_WorkMaster.WorkCode,  tr_BenfWorkDetTrans.intWorkid,
tr_BenfWorkDetTrans.Deleted_dt,tr_BenfWorkDetTrans.DeletedRemarks
,
u1.User_name as Createdby,tr_BenfWorkDetTrans.Created_dt as Createddt,u.User_name as Deletedby
  from (select b.* from tr_BenfWorkDetTrans as b where b.intBenfWorkTransid in (select MIN(a.intBenfWorkTransid) from  tr_BenfWorkDetTrans as a group by a.intBenid,a.intWorkid)) as    tr_BenfWorkDetTrans
  inner join tbl_BeneficiaryDetTrans on tbl_BeneficiaryDetTrans.intBenid=tr_BenfWorkDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDetTrans.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDetTrans.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDetTrans.intWorkid
		   inner join tbl_Users as u on u.intUserid=tr_BenfWorkDetTrans.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=tr_BenfWorkDetTrans.InsertedStaffid


  where   tr_BenfWorkDetTrans.DeletedRemarks is not null  
 and tbl_BeneficiaryDetTrans.intIP like @intIP
 and tbl_BeneficiaryDetTrans.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDetTrans.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDetTrans.NameofHousehold like '%'+@Benname+'%'
 and  isnull(tbl_BeneficiaryDetTrans.household_number,'') like '%'+@Household+'%'
  and  tr_BenfWorkDetTrans.intWorkid like '%'+@Workcode+'%'
    and tr_BenfWorkDetTrans.Deleted_dt between @FromDate and @ToDate+1
 ) as a
 order by a.IPName,a.StateName,a.CountieName,a.PayamName,a.BomaName,a.NameofHousehold
End
Else
Begin
select distinct a.IPName,a.StateName,a.CountieName,a.PayamName,a.BomaName,a.intBenid,a.EnrollmentNo,a.NameofHousehold,a.household_number,a.WorkCode,  a.intWorkid,
a.Deleted_dt as DeletedDt,a.DeletedRemarks,
 Createdby,Createddt,Deletedby from
(
select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tr_BenfWorkDetTrans.intBenid,tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.household_number,tbl_WorkMaster.WorkCode,  tr_BenfWorkDetTrans.intWorkid,
tr_BenfWorkDetTrans.Deleted_dt,tr_BenfWorkDetTrans.DeletedRemarks
,

u1.User_name as Createdby,tr_BenfWorkDetTrans.Created_dt as Createddt,u.User_name as Deletedby
  from (select b.* from tr_BenfWorkDetTrans as b where b.intBenfWorkTransid in (select MIN(a.intBenfWorkTransid) from  tr_BenfWorkDetTrans as a group by a.intBenid,a.intWorkid)) as    tr_BenfWorkDetTrans
  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenfWorkDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDetTrans.intWorkid
		   inner join tbl_Users as u on u.intUserid=tr_BenfWorkDetTrans.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=tr_BenfWorkDetTrans.InsertedStaffid
  where   tr_BenfWorkDetTrans.DeletedRemarks is not null  
 and tbl_BeneficiaryDet.intIP like @intIP
 and tbl_BeneficiaryDet.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDet.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDet.NameofHousehold like '%'+@Benname+'%'
 and  isnull(tbl_BeneficiaryDet.household_number,'') like '%'+@Household+'%'
 and  tr_BenfWorkDetTrans.intWorkid like '%'+@Workcode+'%'
 union
 select distinct td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tr_BenfWorkDetTrans.intBenid,tbl_BeneficiaryDetTrans.EnrollmentNo,tbl_BeneficiaryDetTrans.NameofHousehold,tbl_BeneficiaryDetTrans.household_number,tbl_WorkMaster.WorkCode,  tr_BenfWorkDetTrans.intWorkid,
tr_BenfWorkDetTrans.Deleted_dt,tr_BenfWorkDetTrans.DeletedRemarks
,

u1.User_name as Createdby,tr_BenfWorkDetTrans.Created_dt as Createddt,u.User_name as Deletedby
  from (select b.* from tr_BenfWorkDetTrans as b where b.intBenfWorkTransid in (select MIN(a.intBenfWorkTransid) from  tr_BenfWorkDetTrans as a group by a.intBenid,a.intWorkid)) as    tr_BenfWorkDetTrans 
  inner join tbl_BeneficiaryDetTrans on tbl_BeneficiaryDetTrans.intBenid=tr_BenfWorkDetTrans.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDetTrans.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDetTrans.intIP
		 inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDetTrans.intWorkid
		   inner join tbl_Users as u on u.intUserid=tr_BenfWorkDetTrans.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=tr_BenfWorkDetTrans.InsertedStaffid
  where   tr_BenfWorkDetTrans.DeletedRemarks is not null  
 and tbl_BeneficiaryDetTrans.intIP like @intIP
 and tbl_BeneficiaryDetTrans.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDetTrans.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDetTrans.NameofHousehold like '%'+@Benname+'%'
 and  isnull(tbl_BeneficiaryDetTrans.household_number,'') like '%'+@Household+'%'
  and  tr_BenfWorkDetTrans.intWorkid like '%'+@Workcode+'%'
 ) as a
 order by a.IPName,a.StateName,a.CountieName,a.PayamName,a.BomaName,a.NameofHousehold

 End

  end


