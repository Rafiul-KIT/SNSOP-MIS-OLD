

CREATE procedure [dbo].[InsertTransactionsDeletebenfAllocationDetails]
(
 @Benid int,

 @DeletedBy int
)
As
Begin


  insert into tr_BenfWorkDetTrans
  (
  intBenfWorkid, intBenid, intWorkid, Created_by, Created_dt, Modified_by, Modifieid_dt, PortYear, PortPhase, PortedSheetName, Deleted_by, Deleted_dt, DeletedRemarks

  )
  select intBenfWorkid, intBenid, intWorkid, Created_by, Created_dt, Modified_by, Modifieid_dt, PortYear, PortPhase, PortedSheetName,@DeletedBy, getdate(), 'Deleted on' + Convert(varchar, (getdate())) 
from tr_BenfWorkDet where intBenid = @Benid

  Delete
 from tr_BenfWorkDet where intBenid = @Benid


  select 1000
  return 1000
End


