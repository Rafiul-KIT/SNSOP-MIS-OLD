


CREATE  PROCEDURE [dbo].[sp_BenTransferDet]
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
select   b.intBenid,s.StateName,c.CountieName,p.PayamName,bom.BomaName,

(select bm.BomaName  from  tm_Bomas as bm where bm.intBomasid=b.OLDBomaid) as OLDBomaName,


 ip.IPName,  
b.EnrollmentNo,b.NameofHousehold,  b.NameofSpouse,b.household_number,
case when b.Sex='M' then 'Male' else 'Female' end as Gender
,b.PhotoName,b.PhotoPath,   b.Name1stAlternatePayee,
b.alternate_number,b.Name2ndAlternatePayee,b.Secondalternate_number,
u1.User_name as Createdby,
Convert(varchar(11),b.Created_dt,106) as Createddt,
u.User_name as Transferby,
Convert(varchar(11),b.BomaTransferDate,106) as TrasferDate


from dbo.tbl_BeneficiaryDet as b 
inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
inner join tm_Payams as p on p.intPayamid=bom.intPayamid
inner join tm_Counties as c on c.intCountieid=p.intCountieid
inner join tm_States as s on s.intStateid=c.intStateid
inner join td_IPDetails as ip on ip.intIP=b.intIP
inner join tbl_Users as u on u.intUserid=b.TransferStaffid
inner join tbl_Users as u1 on u1.intUserid=b.InsertedStaffid

 where
 
b.TransferFlag is not null 
 --and intBenid like '%'   
 and b.intIP like @intIP
 and b.intBomasid like @intBomasid
 and p.intPayamid like @intPayamid
 and c.intCountieid like @intCountieid
 and s.intStateid like @intStateid
 and b.EnrollmentNo like '%'+@Enrollmentno+'%'
 and b.NameofHousehold like '%'+@Benname+'%'
 and  b.household_number like '%'+@Household+'%'
 and b.BomaTransferDate between @FromDate and @Todate+1
 order by b.BomaTransferDate desc
End
Else
Begin
select   b.intBenid,s.StateName,c.CountieName,p.PayamName,bom.BomaName,

(select bm.BomaName  from  tm_Bomas as bm where bm.intBomasid=b.OLDBomaid) as OLDBomaName,


 ip.IPName,  
b.EnrollmentNo,b.NameofHousehold,  b.NameofSpouse,b.household_number,
case when b.Sex='M' then 'Male' else 'Female' end as Gender
,b.PhotoName,b.PhotoPath,   b.Name1stAlternatePayee,
b.alternate_number,b.Name2ndAlternatePayee,b.Secondalternate_number,
u1.User_name as Createdby,
Convert(varchar(11),b.Created_dt,106) as Createddt,
u.User_name as Transferby,
Convert(varchar(11),b.BomaTransferDate,106) as TrasferDate


from dbo.tbl_BeneficiaryDet as b 
inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
inner join tm_Payams as p on p.intPayamid=bom.intPayamid
inner join tm_Counties as c on c.intCountieid=p.intCountieid
inner join tm_States as s on s.intStateid=c.intStateid
inner join td_IPDetails as ip on ip.intIP=b.intIP
inner join tbl_Users as u on u.intUserid=b.TransferStaffid
inner join tbl_Users as u1 on u1.intUserid=b.InsertedStaffid

 where
 
b.TransferFlag is not null 
 --and intBenid like '%'   
 and b.intIP like @intIP
 and b.intBomasid like @intBomasid
 and p.intPayamid like @intPayamid
 and c.intCountieid like @intCountieid
 and s.intStateid like @intStateid
 and b.EnrollmentNo like '%'+@Enrollmentno+'%'
 and b.NameofHousehold like '%'+@Benname+'%'
 and  b.household_number like '%'+@Household+'%'
 order by b.BomaTransferDate desc
 End

 end


