CREATE Procedure [dbo].[PayamwiseReportLatestbyid]  --  '%','%','1080'                      
                          
(                          
@intTST varchar(15),                          
@intIP  varchar(15),  
@intCountieid varchar(8)                         
)                          
as begin      


Create table #TblPayam
(
PayamName varchar(60),
intPayamid varchar(8),
intIP varchar(8),
IPName varchar(60),
NoofBomas varchar(8)
)    
Insert into   #TblPayam
select DISTINCT tm_Payams.PayamName,tm_Payams.intPayamid,td_IPDetails.intIP,td_IPDetails.IPName,    (SELECT     COUNT(distinct intBomasid) AS Expr1                          
                            FROM          dbo.tbl_WorkMaster                          
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)                          
                                                FROM                                   
                      dbo.tm_Payams  inner join tbl_WorkMaster on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid         
                              
                      inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid        
                              
                       where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP  and tm_Payams.intCountieid like @intCountieid 


create table #tblWorks
(
intIP varchar(8),
intPayamid varchar(8),
intWorkid varchar(8),
Status varchar(30)
)
   insert into #tblWorks
   select IPid,intPayamid,intWorkid,Status
                            FROM          dbo.tbl_WorkMaster


create table  #tblBenficiary
(
intBenid varchar(8),
intWorkid varchar(8),
intPayamid varchar(8),
Sex varchar(1),
intIP varchar(8)
)
INSERT INTO #tblBenficiary
SELECT    tbl_BeneficiaryDet.intBenid,tr_BenfWorkDet.intWorkid,tbl_BeneficiaryDet.intPayamid,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.intIP
                            FROM          dbo.tbl_BeneficiaryDet INNER JOIN
                                                   dbo.tr_BenfWorkDet ON dbo.tr_BenfWorkDet.intBenid = dbo.tbl_BeneficiaryDet.intBenid






create table #WorkDaysCreated
(
cnt int,
intPayamid int,
intIP int
)
Insert into #WorkDaysCreated


select
ISNULL( COUNT(DISTINCT ba.intAttendanceid), 0) AS Cnt,intPayamid,intIP
FROM         dbo.tr_BenAttendanceDet AS ba INNER JOIN
                      dbo.tbl_BeneficiaryDet AS a ON a.intBenid = ba.intBenid INNER JOIN
                      dbo.tr_BenWagesPayment AS w ON w.intBenid = a.intBenid
WHERE     (ba.AttendanceDate BETWEEN w.FromDate AND w.ToDate) AND (ba.Status = 'Present')
GROUP BY a.intPayamid,a.intIP


      
        
SELECT DISTINCT PayamName,intPayamid,intIP,IPName,              
                                                    
                                        
                                            
                                 NoofBomas        
                              AS NoofBomas,          
                                        
                                        
                              (SELECT     COUNT(*) AS Expr1                          
                            FROM         #tblWorks                        
                            WHERE      (#tblWorks.intIP = paym.intIP) and #tblWorks.intPayamid=paym.intPayamid)                          
                                                      
                              AS TotalWorkProposed ,          
                                            
                                            
                               (SELECT     COUNT(*) AS Expr1                          
                            FROM         #tblWorks                        
                            WHERE      (#tblWorks.intIP = paym.intIP) and #tblWorks.intPayamid=paym.intPayamid and (#tblWorks.Status like 'New Proposal'))                          
                                                      
                              AS TotaltobeApproved ,          
                               (SELECT     COUNT(*) AS Expr1                          
                            FROM         #tblWorks  as a                       
                            WHERE      (a.intIP = paym.intIP) and a.intPayamid=paym.intPayamid and (a.Status like 'Inprogress' or a.Status like 'Invoice Appovals' or  a.Status like 'Invoice Request' or a.Status like 'Approved' ))                            
                          AS TotalInprogress ,          
                                        
                       --(SELECT     COUNT(*) AS Expr1                          
                       --     FROM          dbo.tbl_WorkMaster                          
                       --     WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)                          
                       --          -        (SELECT     COUNT(*) AS Expr1                          
                       --     FROM          dbo.tbl_WorkMaster                          
                       --     WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid and (tbl_WorkMaster.Status like 'Inprogress'))                          
                                                      
                       --       AS TotalInprogress ,          
                                        
                                         
                              (SELECT     COUNT(*) AS Expr1                          
                            FROM         #tblWorks  as a                       
                            WHERE      (a.intIP = paym.intIP) and a.intPayamid=paym.intPayamid  and (a.Status like 'Completed'))                          
                                                      
                              AS TotalClosed ,          
                                
                                (SELECT     COUNT(*) AS Expr1                          
                            FROM         #tblWorks  as a                       
                            WHERE      (a.intIP = paym.intIP) and a.intPayamid=paym.intPayamid and (a.Status like 'Rejected'))                          
                                                      
                              AS TotalRejected ,          
                                        
                                         
                              (SELECT     COUNT(distinct tr_FinancialApproval.intWorkId) AS Expr1                          
                            FROM tr_FinancialApproval          
                            inner join             
                                          
                               #tblWorks as a  on a.intWorkid=tr_FinancialApproval.intWorkId                         
                            WHERE      (a.intIP = paym.intIP) and a.intPayamid=paym.intPayamid)                          
                                                      
                              AS TotalFundsDisbursed ,          
                                        
                              (SELECT     COUNT(distinct b.intBenid) AS Expr1                          
                            FROM  #tblBenficiary  as b                     
                            WHERE      (b.intIP = paym.intIP) and b.intPayamid=paym.intPayamid)                          
                                                      
                              AS TotalBeneficiaries ,          
                          
                               (SELECT     COUNT(distinct b.intBenid) AS Expr1                          
                            FROM  #tblBenficiary  as b                     
                            WHERE      (b.intIP = paym.intIP) and b.intPayamid=paym.intPayamid and b.Sex like 'F'    
                             --and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Closed')    
                             )                          
                                                      
                              AS TotalFemaleBeneficiaries,     
                                  
                                  
                                  
                               (SELECT     COUNT(distinct b.intBenid) AS Expr1                          
                            FROM  #tblBenficiary  as b                     
                            WHERE      (b.intIP = paym.intIP) and b.intPayamid=paym.intPayamid and b.Sex like 'M'    
                             --and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Closed')    
                             )                          
                                                      
                              AS TotalmaleBeneficiaries,          
                                        
                            --  (SELECT     COUNT(distinct tbl_BeneficiaryDet.intBenid) AS Expr1                          
                            --FROM tbl_BeneficiaryDet          
               
                            --WHERE      (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP) )                          
                                                      
                            --  AS TotalWorkDays          
                                  
                            isnull(  ( select MAX(a.c) from ( (SELECT    (COUNT( distinct tr_BenAttendanceDet.intAttendanceid))  AS c                          
                            FROM tr_BenAttendanceDet          
                              inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid       
                                    
                            WHERE      (tbl_BeneficiaryDet.intIP = paym.intIP       
                            and tbl_BeneficiaryDet.intPayamid=paym.intPayamid)       
                            and tr_BenAttendanceDet.status like 'Present'      
                            group by tr_BenAttendanceDet.intBenid        
                            )  ) as a                         
                                                      
                              ),0)AS TotalWorkDays     , 
                              
                              
--                              isnull( (select (  
  
--select  SUM(x.cnt)  
  
--from  
--(  
--select   
   
--(select  ( ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0)) as Cnt   
--from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a   
--on a.intBenid=tr_BenAttendanceDet.intBenid   
--where (AttendanceDate between w.FromDate and w.ToDate)   
----and intWorkActivityId like '1033'  
----and a.intPayamid=tm_Bomas.intBomasid       
--and a.intPayamid=tm_Payams.intPayamid
--and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'    
    
--and  (AttendanceDate between w.FromDate and w.ToDate))  as cnt  
  
----as cnt   
   
-- from dbo.tbl_BeneficiaryDet  left outer join        
--tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid    
    
--inner join tr_BenWagesPayment as w on w.intBenid=tbl_BeneficiaryDet.intBenid where tbl_BeneficiaryDet.intPayamid=dbo.tm_Payams.intPayamid  
    
--and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b)    
--) as x   
--)),0) 

(
ISNULL
(
(
select
ISNULL( ba.Cnt,0)
FROM        #WorkDaysCreated as ba
WHERE     ba.intPayamid=paym.intPayamid and ba.intIP=paym.intIP
)
 
,0)
)


as [NoofWorkdaysCreated],  

                               
                                
--                               (select COUNT(tr_BenAttendanceDet.intAttendanceid) from tr_BenAttendanceDet where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet where tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid )   
--and Status like 'Present' ) as [NoofWorkdaysCreated],    
                            isnull(  (select  sum(isnull(tr_BenWagesPayment.PaidAmount,0))   from tr_BenWagesPayment where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet where tbl_BeneficiaryDet.intPayamid=paym.intPayamid  and tbl_BeneficiaryDet.intIP=paym.intIP )  )        ,0)
      
as [TotalWageAmount]              
                                           
                                            
FROM                                   
                    #TblPayam     as  paym     
  
  
  
  Drop table #TblPayam
  Drop table #tblBenficiary
  Drop table #tblWorks
  Drop Table #WorkDaysCreated
  
end




