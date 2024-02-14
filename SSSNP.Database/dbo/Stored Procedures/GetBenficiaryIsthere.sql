
CREATE procedure [dbo].[GetBenficiaryIsthere](@WorkCode varchar(100))
as
select * from tbl_BeneficiaryDet where EnrollmentNo=@WorkCode 


