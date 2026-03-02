
CREATE procedure [dbo].[GetWageRequestbyIDWages]
(
@intWagePaymentReqid varchar(8)
)
As
Begin

select *,ISNULL( usr.User_name, 'N/A' )as UploadedBy from tbl_WageAttachements a
inner join td_WorkWageRequest  b on a.intWagePaymentReqid = b.intWagePaymentReqid 
left join tbl_Users usr on usr.intUserid=a.Created_by
  where b.intWagePaymentReqid like @intWagePaymentReqid

  
  

  
End