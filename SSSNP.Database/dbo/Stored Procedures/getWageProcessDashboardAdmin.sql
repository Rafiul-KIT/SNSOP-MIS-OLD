
CREATE procedure [dbo].[getWageProcessDashboardAdmin]
(
@intOfficerid varchar(8),
@OfficerLevel varchar(20)
)
As
Begin
if(@OfficerLevel='Cordinator')
Begin

select

(select count(intWagePaymentReqid) from td_WorkWageRequest where intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null ) as TotalReq,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and (Status=1   or  Status=101)) as TotalReqPending,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and Status>=2  and Status<>100   ) as TotalReqAprroved,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and Status=100) as TotalReqRejected,

(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and a.Status=6 ) ) as TotalReqPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and (Status=6 or Status=1001) and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and a.Status=6 )) as TotalReqPendingPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and Status>=7 and (Status<>1000) and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and a.Status=6 ) ) as TotalReqAprrovedPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and Status=1000  and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intCoOrdinatorid in (select u.intUserid from tbl_Users as u where u.User_type='Cordinator') and intWorkid is not null  and a.Status=6 )) as TotalReqRejectedPay

End
else if(@OfficerLevel='Specialist')
Begin

select

(select count(intWagePaymentReqid) from td_WorkWageRequest where  intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and Status>=2 and  Status<>100 ) as TotalReq,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and Status=2 or Status=102) as TotalReqPending,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and Status>=3 and (Status<>100 and Status<>101 )) as TotalReqAprroved,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and Status=101) as TotalReqRejected,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and Status>=7 and  Status<>1000 and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and a.Status=6 )  ) as TotalReqPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and (Status=7 or Status=1002) and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and a.Status=6 )) as TotalReqPendingPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and Status>=8 and (Status<>1000 and Status<>1001 ) and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and a.Status=6 ) ) as TotalReqAprrovedPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and Status=1001 and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intSpecialistid in (select u.intUserid from tbl_Users as u where u.User_type='Specialist')  and a.Status=6 )) as TotalReqRejectedPay
End
else if(@OfficerLevel='Manager')
Begin

select

(select count(intWagePaymentReqid) from td_WorkWageRequest where   intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and Status>=3 and  Status<>101 and  Status<>100  ) as TotalReq,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and Status=3) as TotalReqPending,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and Status>=4 and (Status<>100 and Status<>101 and Status<>102 and Status<>103 and Status<>104)) as TotalReqAprroved,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and Status=102) as TotalReqRejected,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and Status>=8 and  Status<>1000 and  Status<>1001 and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and a.Status=6 ) ) as TotalReqPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and (Status=8 or Status=1003) and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and a.Status=6 )) as TotalReqPendingPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and Status>=9 and (Status<>1000 and Status<>1001 and Status<>1002 and Status<>1003 and Status<>1004) and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and a.Status=6 ) ) as TotalReqAprrovedPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and Status=1002 and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intManagerid in (select u.intUserid from tbl_Users as u where u.User_type='Manager')  and a.Status=6 )) as TotalReqRejectedPay
End
else if(@OfficerLevel='PA')
Begin

select

(select count(intWagePaymentReqid) from td_WorkWageRequest where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and Status>=4  and (Status<>100 and Status<>101 and Status<>102 )) as TotalReq,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and Status=4 ) as TotalReqPending,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and Status=5 ) as TotalReqPendingforAfterGeneration,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and Status>=6  and (Status<>100 and Status<>101 and Status<>102 and Status<>103 and Status<>104)) as TotalReqAprroved,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and Status=103) as TotalReqRejected,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and Status>=9  and (Status<>1000 and Status<>1001 and Status<>1002 ) and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and a.Status=6 ) ) as TotalReqPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and Status=9) as TotalReqPendingPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and Status>=10 and (Status<>1000 and Status<>1001 and Status<>1002 and Status<>1003 and Status<>104) and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and a.Status=6 ) ) as TotalReqAprrovedPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and Status=1003 and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a. intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and a.Status=6 )) as TotalReqRejectedPay,
(select count(intWagePaymentReqid) from td_WorkWageRequest where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and (Status>=6  and (Status<>100 and Status<>101 and Status<>102 and Status<>103 and Status<>104)) and (select count(*) from td_WageReqBenDet as a where a.intWagePaymentReqid=td_WorkWageRequest.intWagePaymentReqid) <>  (select count(*) from td_WageReqBenPADet as b where b.intWagePaymentReqid=td_WorkWageRequest.intWagePaymentReqid) ) as TotalReqPendingAfterSelection,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  intPayingAgentid in (select u.intUserid from tbl_Users as u where u.User_type='PA')  and Status=1000 ) as TotalReqRejectedPayCo
End

else if(@OfficerLevel='WB')
Begin

select

(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  Status>=9  and (Status<>1000 and Status<>1001 and Status<>1002 ) and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where a.Status=6 ) ) as TotalReqPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  Status=9) as TotalReqPendingPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where   Status>=10 and (Status<>1000 and Status<>1001 and Status<>1002 and Status<>1003 and Status<>104) and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where  a.Status=6 ) ) as TotalReqAprrovedPay,
(select count(intWagePaymentReqPAid) from td_WorkWageRequestPA where  Status=1003 and  td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid from td_WorkWageRequest as a where  a.Status=6 )) as TotalReqRejectedPay
End




End

