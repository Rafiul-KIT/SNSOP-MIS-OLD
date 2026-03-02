CREATE Procedure [dbo].[getWageAttachmentStoredCount]

@reqNo varchar(50)

as 
begin 

select count(*) as AttachCount from tbl_WageAttachements
where intWagePaymentReqid = (select intWagePaymentReqid from td_WorkWageRequest where ReqNo=@reqNo )
end