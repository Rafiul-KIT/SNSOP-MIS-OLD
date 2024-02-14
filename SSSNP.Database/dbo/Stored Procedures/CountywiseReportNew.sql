CREATE Procedure [dbo].[CountywiseReportNew]                            
                            
(                            
@intTST varchar(8),                            
@intIP  varchar(8)                           
)                            
as begin 

Create table #TblCounty
(
CountieName varchar(60),
intCountieid varchar(8),
intIP varchar(8),
IPName varchar(60),
NoofPayams varchar(8),
NoofBomas varchar(8),
TotalWorkProposed varchar(8)
)    
Insert into   #TblCounty
select DISTINCT c.CountieName, c.intCountieid, ips.intIP, ips.IPName,
                          COUNT(DISTINCT w.intPayamid)  AS NoofPayams,
                           COUNT(DISTINCT w.intBomasid) AS NoofBomas,
                           COUNT(distinct w.intWorkid) AS TotalWorkProposed FROM         dbo.tm_Counties AS c INNER JOIN
                      dbo.tbl_WorkMaster AS w ON w.intCountieid = c.intCountieid INNER JOIN
                      dbo.td_IPDetails AS ips ON ips.intIP = w.IPid group by c.CountieName, c.intCountieid, ips.intIP, ips.IPName


create table #tblWorks
(
intIP varchar(8),
intCountieid varchar(8),
intWorkid varchar(8),
Status varchar(30)
)
   insert into #tblWorks
   select IPid,intCountieid,intWorkid,Status
                            FROM          dbo.tbl_WorkMaster


create table  #tblBenficiary
(
intBenid varchar(8),
intWorkid varchar(8),
intCountieid varchar(8),
Sex varchar(1),
intIP varchar(8)
)
INSERT INTO #tblBenficiary
SELECT    tbl_BeneficiaryDet.intBenid,tr_BenfWorkDet.intWorkid,tbl_BeneficiaryDet.intCountieid,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.intIP
                            FROM          dbo.tbl_BeneficiaryDet INNER JOIN
                                                   dbo.tr_BenfWorkDet ON dbo.tr_BenfWorkDet.intBenid = dbo.tbl_BeneficiaryDet.intBenid






create table #WorkDays
(
cnt int,
intCountieid int,
intIP int
)
Insert into #WorkDays


SELECT     ISNULL(COUNT(DISTINCT ba.intAttendanceid), 0) AS Cnt, a.intCountieid,a.intIP
FROM         dbo.tr_BenAttendanceDet AS ba INNER JOIN
                      dbo.tbl_BeneficiaryDet AS a ON a.intBenid = ba.intBenid INNER JOIN
                      dbo.tr_BenWagesPayment AS w ON w.intBenid = a.intBenid
WHERE     (ba.AttendanceDate BETWEEN w.FromDate AND w.ToDate) AND (ba.Status = 'Present')
GROUP BY a.intCountieid,a.intIP


create table #TotWorkDays
(
cnt int,
intCountieid int
)
Insert into #TotWorkDays

   SELECT     SUM(a.cnt) AS Cnt, a.intCountieid
FROM        #WorkDays as a group by  a.intCountieid

   

create table #WagePaid
(
cnt decimal(18,2),
intCountieid int,
intIP int
)
Insert into #WagePaid


SELECT     SUM(dbo.tr_BenWagesPayment.PaidAmount) AS cnt, dbo.tbl_BeneficiaryDet.intCountieid,tbl_BeneficiaryDet.intIP
FROM         dbo.tr_BenWagesPayment INNER JOIN
                      dbo.tbl_BeneficiaryDet ON dbo.tr_BenWagesPayment.intBenid = dbo.tbl_BeneficiaryDet.intBenid
GROUP BY dbo.tbl_BeneficiaryDet.intCountieid,tbl_BeneficiaryDet.intIP
                          
   
create table #WageAmount
(
cnt decimal(18,2),
intCountieid int

)
Insert into #WageAmount
SELECT     SUM(a.cnt) AS cnt, a.intCountieid
FROM         #WagePaid as a 
GROUP BY a.intCountieid




   SELECT DISTINCT tc.CountieName, tc.intCountieid, tc.intIP, tc.IPName,
                          tc.NoofPayams,
                          tc.NoofBomas,
                           tc.TotalWorkProposed,
                          (SELECT     COUNT(a.intWorkid) AS Expr1
                            FROM        #tblWorks as a
                            WHERE      (a.intIP = tc.intIP)
							 AND (a.intCountieid = tc.intCountieid) AND (a.Status LIKE 'New Proposal')) AS TotaltobeApproved,
                        (SELECT     COUNT(a.intWorkid) AS Expr1
                            FROM        #tblWorks as a
                            WHERE      (a.intIP = tc.intIP)
							 AND (a.intCountieid = tc.intCountieid) AND (a.Status in ( 'Inprogress',
                                                  'Invoice Appovals' ,
                                                  'Invoice Request','Approved'))) AS TotalInprogress,
                            (SELECT     COUNT(a.intWorkid) AS Expr1
                            FROM        #tblWorks as a
                            WHERE      (a.intIP = tc.intIP)
							 AND (a.intCountieid = tc.intCountieid)  AND (a.Status LIKE 'Completed')) AS TotalClosed,
                           (SELECT     COUNT(a.intWorkid) AS Expr1
                            FROM        #tblWorks as a
                            WHERE      (a.intIP = tc.intIP)
							 AND (a.intCountieid = tc.intCountieid) AND (a.Status LIKE 'Rejected')) AS TotalRejected,
                          (SELECT     COUNT(DISTINCT dbo.tr_FinancialApproval.intWorkId) AS Expr1
                            FROM          dbo.tr_FinancialApproval INNER JOIN
                                                  #tblWorks AS b ON b.intWorkid = dbo.tr_FinancialApproval.intWorkId
                            WHERE      (b.intIP = tc.intIP) AND (b.intCountieid = tc.intCountieid)) AS TotalFundsDisbursed,
                          (SELECT     COUNT(DISTINCT ben.intBenid) AS Expr1
                            FROM        #tblBenficiary as ben
                            WHERE      (ben.intIP = tc.intIP) AND (ben.intCountieid = tc.intCountieid)) AS TotalBeneficiaries,
                           (SELECT     COUNT(DISTINCT ben.intBenid) AS Expr1
                            FROM        #tblBenficiary as ben
                            WHERE      (ben.intIP = tc.intIP) AND (ben.intCountieid = tc.intCountieid and ben.Sex='F'))  AS TotalFemaleBeneficiaries,
                          (SELECT     COUNT(DISTINCT ben.intBenid) AS Expr1
                            FROM        #tblBenficiary as ben
                            WHERE      (ben.intIP = tc.intIP) AND (ben.intCountieid = tc.intCountieid and ben.Sex='M'))  AS TotalmaleBeneficiaries,
							 ISNULL
                          ((SELECT     wg.Cnt
                              FROM         #WorkDays AS wg
                              WHERE     (wg.intCountieid = tc.intCountieid) AND (wg.intIP = tc.intIP)), 0) 
							  
							 AS TotalWorkDays, 
							  ISNULL
                          ((SELECT     wp.cnt
                              FROM        #WagePaid AS wp
                              WHERE     (wp.intCountieid = tc.intCountieid) AND (wp.intIP = tc.intIP)), 0) 
						  AS TotalWageAmt, 
						  ISNULL
                          ((SELECT     wg.Cnt
                              FROM         #TotWorkDays AS wg
                              WHERE     (wg.intCountieid = tc.intCountieid)), 0)
							  
							  AS NoofWorkdaysCreated, 
							  
							  --ISNULL
         --                 ((SELECT     wp1.cnt
         --                     FROM         dbo.Vw_WagesPaid AS wp1
         --                     WHERE     (wp1.intCountieid = tc.intCountieid)), 0) 
							  
						 ISNULL
                          ((SELECT     wp1.cnt
                              FROM        #WageAmount AS wp1
                              WHERE     (wp1.intCountieid = tc.intCountieid)), 0)	  AS TotalWageAmount
FROM        #TblCounty as tc
 where  tc.intIP like @intIP 


   
   Drop table #tblBenficiary
   
   Drop table #TblCounty
   
   Drop table #tblWorks

   Drop table #TotWorkDays
   
    Drop table #WageAmount
   
   Drop table #WagePaid
   Drop table #WorkDays
   
   
   
   
          
          
--SELECT *
                                              
--FROM                                     
--                    Vw_CountrywiseReportLat  as c    
                                
--                       where  c.intIP like @intIP                
  end




