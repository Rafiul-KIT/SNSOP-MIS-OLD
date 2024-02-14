
CREATE procedure [dbo].[GetAlternateDet]
(
@intBenid varchar(8)
)
As
Begin

select Name1stAlternatePayee as AlternateName from tbl_BeneficiaryDet where intBenid like @intBenid
union
select Name2ndAlternatePayee as AlternateName from tbl_BeneficiaryDet where intBenid like @intBenid
End

