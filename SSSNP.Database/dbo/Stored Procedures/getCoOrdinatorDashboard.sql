CREATE procedure [dbo].[getCoOrdinatorDashboard]
(
@intOfficerid varchar(8),
@OfficerLevel varchar(20)
)
As
Begin
if(@OfficerLevel='Cordinator')
Begin

select

(select count(intWagePaymentReqid) from td_WorkWageRequest where intCoOrdinatorid like @intOfficerid ) as TotalReq,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intCoOrdinatorid like @intOfficerid and Status=1) as TotalReqPending,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intCoOrdinatorid like @intOfficerid and Status>=2) as TotalReqAprroved,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intCoOrdinatorid like @intOfficerid and Status=100) as TotalReqRejected
End
else if(@OfficerLevel='Specialist')
Begin

select

(select count(intWagePaymentReqid) from td_WorkWageRequest where intSpecialistid like @intOfficerid and Status>=2 and Status<>100) as TotalReq,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intSpecialistid like @intOfficerid and Status=2) as TotalReqPending,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intSpecialistid like @intOfficerid and Status>=3) as TotalReqAprroved,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intSpecialistid like @intOfficerid and Status=101) as TotalReqRejected
End
else if(@OfficerLevel='Manager')
Begin

select

(select count(intWagePaymentReqid) from td_WorkWageRequest where intSpecialistid like @intOfficerid and Status>=3 and Status<>100) as TotalReq,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intSpecialistid like @intOfficerid and Status=3) as TotalReqPending,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intSpecialistid like @intOfficerid and Status>=4) as TotalReqAprroved,
(select count(intWagePaymentReqid) from td_WorkWageRequest where intSpecialistid like @intOfficerid and Status=102) as TotalReqRejected
End






End

