
CREATE procedure [dbo].[HouseHoldNumberVerificationWages]
(
@household_number varchar(120),
@reqno varchar(8),
@work_id varchar(8)
)
As
Begin
--Created by: Srikanth
--Created on; 24-03-2018
select * from tbl_BeneficiaryDet where household_number like @household_number

select * from tbl_WorkMaster where intWorkid like @work_id

select * from td_WorkWageRequest where intWorkid like @work_id and intWagePaymentReqid like @reqno

select * from  td_WorkWageRequest where intWorkid like @work_id and intWagePaymentReqid like @reqno and intWagePaymentReqid in (select a.intWagePaymentReqid from td_WageReqBenDet as a where a.intWorkid like @work_id and a.intBenid in (select intBenid from  tbl_BeneficiaryDet where household_number like @household_number))

select a.intWagesReqBenTransid from td_WageReqBenDet as a where a.intWorkid like @work_id and a.intBenid in (select intBenid from  tbl_BeneficiaryDet where household_number like @household_number) and  intWagePaymentReqid like @reqno
End

