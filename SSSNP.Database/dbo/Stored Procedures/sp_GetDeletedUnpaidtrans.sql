


CREATE  PROCEDURE [dbo].[sp_GetDeletedUnpaidtrans]
@intIP varchar(10),
@intStateid varchar(10),
@intCountieid varchar(10),
@intPayamid varchar(10),
@intBomasid varchar(10),
@Household varchar(80),
@Benname varchar(100),
@Enrollmentno varchar(30),
@workcode varchar(8),
@reqNo varchar(8),
@FromDate datetime,
@ToDate datetime
as
begin
if(@FromDate <>'' and @ToDate <>'')
Begin
select a.* from 
(select  td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName, td_WageReqBenTransDet.intWagesReqBenTransid,td_WageReqBenTransDet.intBenid,tbl_BeneficiaryDetTrans.EnrollmentNo,tbl_BeneficiaryDetTrans.NameofHousehold,tbl_BeneficiaryDetTrans.household_number,  td_WorkWageRequest.intWorkid,  td_WorkWageRequest.ReqNo,td_WorkWageRequest.ReqFromDate,td_WorkWageRequest.ReqToDate,td_WageReqBenTransDet.TotAttendance,td_WageReqBenTransDet.WageRateUSDTotal,
td_WageReqBenTransDet.Deleted_dt,td_WageReqBenTransDet.DeletedRemarks
,u1.User_name as Createdby,td_WageReqBenTransDet.Created_dt as Createddt,u.User_name as Deletedby

  from td_WageReqBenTransDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenTransDet.intWagePaymentReqid
  inner join tbl_BeneficiaryDetTrans on tbl_BeneficiaryDetTrans.intBenid=td_WageReqBenTransDet.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDetTrans.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDetTrans.intIP
		 	 	   inner join tbl_Users as u on u.intUserid=td_WageReqBenTransDet.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=td_WageReqBenTransDet.InsertedStaffid
  where   td_WageReqBenTransDet.DeletedRemarks is not null  
 and tbl_BeneficiaryDetTrans.intIP like @intIP
 and tbl_BeneficiaryDetTrans.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDetTrans.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDetTrans.NameofHousehold like '%'+@Benname+'%'
 and  tbl_BeneficiaryDetTrans.household_number like '%'+@Household+'%'
 and td_WageReqBenTransDet.intWagePaymentReqid like @reqNo
  and td_WageReqBenTransDet.intWorkid like @workcode
  and    td_WageReqBenTransDet.Deleted_dt between @FromDate and @ToDate+1
  union
  select  td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName, td_WageReqBenTransDet.intWagesReqBenTransid,td_WageReqBenTransDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.household_number,  td_WorkWageRequest.intWorkid,  td_WorkWageRequest.ReqNo,td_WorkWageRequest.ReqFromDate,td_WorkWageRequest.ReqToDate,td_WageReqBenTransDet.TotAttendance,td_WageReqBenTransDet.WageRateUSDTotal,
  td_WageReqBenTransDet.Deleted_dt,td_WageReqBenTransDet.DeletedRemarks
  ,u1.User_name as Createdby,td_WageReqBenTransDet.Created_dt as Createddt,u.User_name as Deletedby
  from td_WageReqBenTransDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenTransDet.intWagePaymentReqid
  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenTransDet.intBenid
   inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP
	  	   inner join tbl_Users as u on u.intUserid=td_WageReqBenTransDet.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=td_WageReqBenTransDet.InsertedStaffid
  where   td_WageReqBenTransDet.DeletedRemarks is not null 
 and tbl_BeneficiaryDet.intIP like @intIP
 and tbl_BeneficiaryDet.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDet.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDet.NameofHousehold like '%'+@Benname+'%'
 and  tbl_BeneficiaryDet.household_number like '%'+@Household+'%'
  and td_WageReqBenTransDet.intWagePaymentReqid like @reqNo
  and td_WageReqBenTransDet.intWorkid like @workcode
    and    td_WageReqBenTransDet.Deleted_dt between @FromDate and @ToDate+1
  ) as a 
  order by a.Deleted_dt desc
End
Else
Begin
select a.* from 
(select  td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName, td_WageReqBenTransDet.intWagesReqBenTransid,td_WageReqBenTransDet.intBenid,tbl_BeneficiaryDetTrans.EnrollmentNo,tbl_BeneficiaryDetTrans.NameofHousehold,tbl_BeneficiaryDetTrans.household_number,  td_WorkWageRequest.intWorkid,  td_WorkWageRequest.ReqNo,td_WorkWageRequest.ReqFromDate,td_WorkWageRequest.ReqToDate,td_WageReqBenTransDet.TotAttendance,td_WageReqBenTransDet.WageRateUSDTotal,
td_WageReqBenTransDet.Deleted_dt,td_WageReqBenTransDet.DeletedRemarks
,u1.User_name as Createdby,td_WageReqBenTransDet.Created_dt as Createddt,u.User_name as Deletedby

  from td_WageReqBenTransDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenTransDet.intWagePaymentReqid
  inner join tbl_BeneficiaryDetTrans on tbl_BeneficiaryDetTrans.intBenid=td_WageReqBenTransDet.intBenid
  inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDetTrans.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDetTrans.intIP
		  	   inner join tbl_Users as u on u.intUserid=td_WageReqBenTransDet.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=td_WageReqBenTransDet.InsertedStaffid
  where   td_WageReqBenTransDet.DeletedRemarks is not null  
 and tbl_BeneficiaryDetTrans.intIP like @intIP
 and tbl_BeneficiaryDetTrans.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDetTrans.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDetTrans.NameofHousehold like '%'+@Benname+'%'
 and  tbl_BeneficiaryDetTrans.household_number like '%'+@Household+'%'
 and td_WageReqBenTransDet.intWagePaymentReqid like @reqNo
  and td_WageReqBenTransDet.intWorkid like @workcode
  union
  select  td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName, td_WageReqBenTransDet.intWagesReqBenTransid,td_WageReqBenTransDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.household_number,  td_WorkWageRequest.intWorkid,  td_WorkWageRequest.ReqNo,td_WorkWageRequest.ReqFromDate,td_WorkWageRequest.ReqToDate,td_WageReqBenTransDet.TotAttendance,td_WageReqBenTransDet.WageRateUSDTotal,
  td_WageReqBenTransDet.Deleted_dt,td_WageReqBenTransDet.DeletedRemarks
  ,u1.User_name as Createdby,td_WageReqBenTransDet.Created_dt as Createddt,u.User_name as Deletedby
  from td_WageReqBenTransDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenTransDet.intWagePaymentReqid
  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenTransDet.intBenid
   inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid
   inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
    inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
	 inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
	 inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP
	  	   inner join tbl_Users as u on u.intUserid=td_WageReqBenTransDet.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=td_WageReqBenTransDet.InsertedStaffid

  where   td_WageReqBenTransDet.DeletedRemarks is not null 
 and tbl_BeneficiaryDet.intIP like @intIP
 and tbl_BeneficiaryDet.intBomasid like @intBomasid
 and tm_Payams.intPayamid like @intPayamid
 and tm_Counties.intCountieid like @intCountieid
 and tm_States.intStateid like @intStateid
 and tbl_BeneficiaryDet.EnrollmentNo like '%'+@Enrollmentno+'%'
 and tbl_BeneficiaryDet.NameofHousehold like '%'+@Benname+'%'
 and  tbl_BeneficiaryDet.household_number like '%'+@Household+'%'
  and td_WageReqBenTransDet.intWagePaymentReqid like @reqNo
  and td_WageReqBenTransDet.intWorkid like @workcode
  ) as a 
  order by a.Deleted_dt desc
  End
  end


