


CREATE  PROCEDURE [dbo].[sp_GetBenfDettrans]
@intIP varchar(10),
@intStateid varchar(10),
@intCountieid varchar(10),
@intPayamid varchar(10),
@intBomasid varchar(10),
@Household varchar(80),
@Benname varchar(100),
@Enrollmentno varchar(30),
@FromDate datetime,
@ToDate datetime
as
begin
if(@FromDate <>'' and @ToDate <>'')
Begin
select  ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid,  bd.intBenid,bd.EnrollmentNo,
bd.NameofHousehold,bd.NameofSpouse,bd.DeletedDt,bd.household_number,bd.DeletedRemarks,
u1.User_name as Createdby,bd.Created_dt as Createddt,u.User_name as Deletedby
 
 
  from tbl_BeneficiaryDetTrans as bd 
 inner join tm_Bomas as bom on bom.intBomasid=bd.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
 inner join td_IPDetails as ip on ip.intIP=bd.intIP
 inner join tbl_Users as u on u.intUserid=bd.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=bd.InsertedStaffid
  where bd.DeletedRemarks is not null

  and ip.intIP like @intIP and states.intStateid like @intStateid and countie.intCountieid like @intCountieid and  payam.intPayamid like @intPayamid and bom.intBomasid like @intBomasid
  
  and bd.household_number like @Household and bd.NameofHousehold like @Benname and bd.EnrollmentNo like @Enrollmentno
  and bd.DeletedDt between @FromDate and @ToDate+1
ENd
Else
Begin
select  ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid,  bd.intBenid,bd.EnrollmentNo,
bd.NameofHousehold,bd.NameofSpouse,bd.DeletedDt,bd.household_number,bd.DeletedRemarks,
 u1.User_name as Createdby,bd.Created_dt as Createddt,u.User_name as Deletedby
 
  from tbl_BeneficiaryDetTrans as bd 
 inner join tm_Bomas as bom on bom.intBomasid=bd.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
 inner join td_IPDetails as ip on ip.intIP=bd.intIP
  inner join tbl_Users as u on u.intUserid=bd.DeletedStaffid
inner join tbl_Users as u1 on u1.intUserid=bd.InsertedStaffid

  where bd.DeletedRemarks is not null

  and ip.intIP like @intIP and states.intStateid like @intStateid and countie.intCountieid like @intCountieid and  payam.intPayamid like @intPayamid and bom.intBomasid like @intBomasid
  
  and bd.household_number like @Household and bd.NameofHousehold like @Benname and bd.EnrollmentNo like @Enrollmentno
  End
  end


