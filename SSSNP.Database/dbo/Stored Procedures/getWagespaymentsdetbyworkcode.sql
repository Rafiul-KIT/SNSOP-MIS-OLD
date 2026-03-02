CREATE Procedure [dbo].[getWagespaymentsdetbyworkcode]          
(          
        
@intWorkActivityId varchar(10),      
--@EnrollmentNo varchar(50),  
@paymentdate datetime           
)          
as          
begin          
          
select distinct tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,          
tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.ContactNo,tbl_BeneficiaryDet.Email,tbl_BeneficiaryDet.FamilyIncome,tm_Bomas.BomaName,          
  
--(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where (AttendanceDate between w.FromDate and w.ToDate) and intWorkActivityId         
--like @intWorkActivityId           
  
--and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present' and --tbl_BeneficiaryDet.EnrollmentNo like @EnrollmentNo       
        
----and    
--(AttendanceDate between w.FromDate and w.ToDate))
w.AttendanceDays
 as cnt,        
        
w.PaidAmount as Amt,        
convert(varchar(11),w.PaidDate,106) AS PaidDate,        
convert(varchar(11),w.FromDate,106) AS FromDate,        
convert(varchar(11),w.ToDate,106) AS ToDate,        
w.Wagerate            
        
 from dbo.tbl_BeneficiaryDet  left outer join            
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid        
        
inner join tr_BenWagesPayment as w on w.intBenid=tbl_BeneficiaryDet.intBenid
 inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid               where tr_BenfWorkDet.intWorkid like @intWorkActivityId    
-- and tbl_BeneficiaryDet.EnrollmentNo like @EnrollmentNo     
and (Convert(varchar(11), w.PaidDate,106) like Convert(varchar(11), @paymentdate,106))     
        
and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b)        
  
end   
  
  
--select    from tr_BenWagesPayment inner join tbl_BeneficiaryDet on tr



