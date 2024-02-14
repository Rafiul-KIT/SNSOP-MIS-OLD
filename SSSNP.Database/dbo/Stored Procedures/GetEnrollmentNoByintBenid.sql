CREATE Procedure [dbo].[GetEnrollmentNoByintBenid]
@intBenid int
as 
begin 

select intBenid,EnrollmentNo from tbl_BeneficiaryDet where intBenid=@intBenid
end



