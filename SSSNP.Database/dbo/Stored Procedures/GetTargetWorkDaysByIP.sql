CREATE Procedure [dbo].[GetTargetWorkDaysByIP](@id varchar(8))
as
SELECT   distinct bd.EnrollmentNo,bd.NameofHousehold,td_IPDetails.IPName,td_IPDetails.intIP                    
                            FROM tbl_BeneficiaryDet bd 
							  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=bd.intBenid       
                            left outer join tbl_WorkMaster on tbl_WorkMaster.intWorkid= tr_BenfWorkDet.intWorkid 
							inner join  td_IPDetails on  bd.intIP =td_IPDetails.intIP    
                            WHERE   bd.intIP=@id 



