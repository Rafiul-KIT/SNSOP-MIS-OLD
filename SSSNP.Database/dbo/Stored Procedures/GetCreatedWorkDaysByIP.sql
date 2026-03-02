CREATE Procedure [dbo].[GetCreatedWorkDaysByIP](@id varchar(8))
as
 select  DISTINCT ba.intAttendanceid,ba.AttendanceDate,a.NameofHousehold,  wm.WorkCode ,td_IPDetails.IPName
FROM         dbo.tr_BenAttendanceDet AS ba INNER JOIN
                      dbo.tbl_BeneficiaryDet AS a ON a.intBenid = ba.intBenid
					  
					  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=a.intBenid  INNER JOIN
                      dbo.tr_BenWagesPayment AS w ON w.intBenid = a.intBenid and w.intWorkid=tr_BenfWorkDet.intWorkid
					  inner join  td_IPDetails on  a.intIP =td_IPDetails.intIP 
					  inner join tbl_WorkMaster wm on ba.intWorkActivityId =wm.intWorkid   
WHERE     (ba.AttendanceDate BETWEEN w.FromDate AND w.ToDate)  AND (ba.Status = 'Present') and   a.intIP=@id 



