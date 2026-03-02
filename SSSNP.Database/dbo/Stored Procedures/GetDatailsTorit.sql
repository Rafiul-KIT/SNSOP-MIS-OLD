CREATE procedure [dbo].[GetDatailsTorit]

As
Begin

select intBenid, EnrollmentNo, intBomasid,(select b.BomaCode from tm_Bomas as b where b.intBomasid=tbl_BeneficiaryDet.intBomasid) as Bcode from tbl_BeneficiaryDet where intCountieid='1080' order by intPayamid,intBomasid,intBenid desc
End

