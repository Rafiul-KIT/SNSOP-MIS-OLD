

CREATE procedure [dbo].[CountBeneficiaryTypeInUpdate1]
@intBenid int,
@intBomasid int,
@GroupId varchar(50),
@BeneficiaryType  varchar(50)
as 
begin 
if(@BeneficiaryType ='Group')
begin
(select COUNT(*) as CountBeneficiary from tbl_BeneficiaryServiceDet where intBomasid=@intBomasid and GroupId=@GroupId and BeneficiaryType =@BeneficiaryType and intBenTempid=@intBenid)
   end
   else
   begin
   (select COUNT(*) as CountBeneficiary from tbl_BeneficiaryServiceDet where intBomasid=@intBomasid and BeneficiaryType =@BeneficiaryType and intBenTempid=@intBenid)
   end              
  
end 




