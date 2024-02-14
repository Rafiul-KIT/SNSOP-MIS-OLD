
CREATE procedure [dbo].[GetWageRequestbyIDPA]
(
@intWagePaymentReqid varchar(8),
@intWagePaymentReqPAid varchar(8)
)
As
Begin
select a.*, u.User_name as coordinator,
u1.User_name as Specilist,
u2.User_name as SManager,
u3.User_name as SPaymentagent



 from td_WorkWageRequest as a inner join tbl_Users as u on u.intUserid=a.intCoOrdinatorid
 
 inner join tbl_Users as u1 on u1.intUserid=a.intSpecialistid

 inner join tbl_Users as u2 on u2.intUserid=a.intManagerid

 inner join tbl_Users as u3 on u3.intUserid=a.intPayingAgentid
 
  where a.intWagePaymentReqid like @intWagePaymentReqid

  select a.*, u.User_name as coordinator,
u1.User_name as Specilist,
u2.User_name as SManager,
u3.User_name as SPaymentagent



 from td_WorkWageRequestPA as a inner join tbl_Users as u on u.intUserid=a.intCoOrdinatorid
 
 inner join tbl_Users as u1 on u1.intUserid=a.intSpecialistid

 inner join tbl_Users as u2 on u2.intUserid=a.intManagerid

 inner join tbl_Users as u3 on u3.intUserid=a.intPayingAgentid
 
  where a.intWagePaymentReqPAid like @intWagePaymentReqPAid
  

End

