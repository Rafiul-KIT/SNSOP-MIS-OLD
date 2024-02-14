CREATE procedure [dbo].[GetBomaListWageApplied]

as
Begin
--This old version did not get all DIS related requested payments
--select distinct w.intBomasid from tbl_WorkMaster as w where w.intWorkid in (select a.intWorkid from td_WorkWageRequest as a)  and intBomasid is not null
select distinct intbomasid from td_WageReqBenDet a inner join tbl_BeneficiaryDet b on a.intBenid=b.intBenid

end