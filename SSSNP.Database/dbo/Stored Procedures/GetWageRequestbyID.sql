
CREATE procedure [dbo].[GetWageRequestbyID]
(
@intWagePaymentReqid varchar(8)
)
As
Begin
select a.*, u.User_name as coordinator,
u1.User_name as Specilist,
u2.User_name as SManager
,isnull(u3.User_name, '') as SPaymentagent
,ISNULL(pc.WagePaymentCycle,'') as PaymentCycleName


 from td_WorkWageRequest as a 
 left join tbl_Users as u on u.intUserid=a.intCoOrdinatorid
 
 left join tbl_Users as u1 on u1.intUserid=a.intSpecialistid

 left join tbl_Users as u2 on u2.intUserid=a.intManagerid

 left join tbl_Users as u3 on u3.intUserid=a.intPayingAgentid
 left join tm_WagePaymentCycle pc on a.PaymentCycle=pc.intPaymentCycleID
 
  where a.intWagePaymentReqid like @intWagePaymentReqid
  
End



