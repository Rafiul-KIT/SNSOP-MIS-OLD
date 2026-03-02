

CREATE procedure [dbo].[DeletewageDocument]
(
@intWageImageid int,
@DeletedBy	int
)
AS
Begin
insert into tbl_WageAttachementsDeleteTrans
(
intImageId, intWagePaymentReqid, Description, PhotoFileName, PhotoFilePath, Created_by, Created_dt,  
                      Deleted_by, DeleteDate

)
select
intImageId, intWagePaymentReqid, Description, PhotoFileName, PhotoFilePath, Created_by, Created_dt,
@DeletedBy,getdate()

 from tbl_WageAttachements where intImageId = @intWageImageid

 Delete from tbl_WageAttachements where  intImageId = @intWageImageid

 select @intWageImageid
 return @intWageImageid
End

