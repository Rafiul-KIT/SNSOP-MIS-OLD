
CREATE procedure [dbo].[GetRequestIstherewithBenficiaryandWorkCodePA]
(
@ReqestNo as varchar(30),
@EnrollmentNo varchar(30),
@WorkCode varchar(80)
)
As
Begin

select * from td_WorkWageRequest where ReqNo=@ReqestNo
select * from tbl_WorkMaster where WorkCode =@WorkCode
select * from tbl_BeneficiaryDet where EnrollmentNo like @EnrollmentNo

select * from td_WorkWageRequest inner join td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid=td_WorkWageRequest.intWagePaymentReqid inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=td_WageReqBenDet.intWorkid
inner join td_WageReqBenPADet on td_WageReqBenPADet.intWagePaymentReqid=td_WorkWageRequest.intWagePaymentReqid and 
td_WageReqBenPADet.intWorkid=td_WorkWageRequest.intWorkid and td_WageReqBenPADet.intBenid=td_WageReqBenDet.intBenid
and 
td_WageReqBenPADet.intWorkid=td_WageReqBenDet.intWorkid and td_WageReqBenPADet.intWagePaymentReqid=td_WageReqBenDet.intWagePaymentReqid
where td_WorkWageRequest.ReqNo like @ReqestNo and tbl_BeneficiaryDet.EnrollmentNo like @EnrollmentNo and tbl_WorkMaster.WorkCode like @WorkCode

End

