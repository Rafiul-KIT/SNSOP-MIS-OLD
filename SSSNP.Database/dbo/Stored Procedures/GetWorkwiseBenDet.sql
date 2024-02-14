CREATE Procedure [dbo].[GetWorkwiseBenDet]  
(  
@intBomasid varchar(10)  
)  
as begin  
  
select distinct tbl_BeneficiaryDet.*,tm_Bomas.BomaName,@intBomasid as intWorkid from dbo.tbl_BeneficiaryDet
inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid left outer join  
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid   
  
 where tr_BenfWorkDet.intWorkid like @intBomasid order by EnrollmentNo --and WorkCode is null  
   
end




