CREATE Procedure [dbo].[GetWorkwiseBenDetSearchbyGroup_Sp]
(  
@intBomasid varchar(10),
@group_type varchar(100),
@group_code varchar(100)  
)  
as begin  
  
select *,tm_Bomas.BomaName from dbo.tbl_BeneficiaryDet left outer join  
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid   
  
 where tbl_BeneficiaryDet.WorkCode like @intBomasid 
  --and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
	--and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)

 order by EnrollmentNo --and WorkCode is null  
 
   
end




