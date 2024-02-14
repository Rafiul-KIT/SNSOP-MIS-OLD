

CREATE procedure [dbo].[GetEnrollmentNoByintBenid2]
@intBenid int
as 
begin 

select intBenTempid,household_number as EnrollmentNo from tbl_BeneficiaryServiceDet where intBenTempid=@intBenid
end




