CREATE view Noofdayslat

as
select

isnull( (select (  
  
select  SUM(x.cnt)  
  
from  
(  
select   
   
(select  ( ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0)) as Cnt   
from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a   
on a.intBenid=tr_BenAttendanceDet.intBenid   
where (AttendanceDate between w.FromDate and w.ToDate)   
--and intWorkActivityId like '1033'  
and a.intIP=td_IPDetails.intIP
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'    
    
and  (AttendanceDate between w.FromDate and w.ToDate))  as cnt  
  
--as cnt   
   
 from dbo.tbl_BeneficiaryDet  left outer join        
td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP    
    
inner join tr_BenWagesPayment as w on w.intBenid=tbl_BeneficiaryDet.intBenid 
where tbl_BeneficiaryDet.intIP=dbo.td_IPDetails.intIP
    
and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b)    
) as x   
)),0)

 as [NoofWorkdaysCreated]
