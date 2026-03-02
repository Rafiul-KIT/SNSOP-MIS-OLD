

CREATE procedure [dbo].[getAPIDashboard]
(
@intIP varchar(8)
)
As
Begin


select

(select count(intBenTempid) from tbl_BeneficiaryServiceDet where intIP like @intIP ) as Total,
(select count(intBenTempid) from tbl_BeneficiaryServiceDet where intIP like @intIP and BenStatus = '1') as Pending,
(select count(intBenTempid) from tbl_BeneficiaryServiceDet where intIP like @intIP and BenStatus = '2') as confirmed,
(select count(intBenTempid) from tbl_BeneficiaryServiceDet where intIP like @intIP and BenStatus = '3') as Dropped




End


