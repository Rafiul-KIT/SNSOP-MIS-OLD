CREATE Procedure [dbo].[CountBeneficiaryTypeInUpdate]
@intBenid int,
@intBomasid int,
@GroupId varchar(50),
@BeneficiaryType  varchar(50)
as 
begin 
if(@BeneficiaryType ='Group')
begin
(select COUNT(*) as CountBeneficiary from tbl_BeneficiaryDet where intBomasid=@intBomasid and GroupId=@GroupId and BeneficiaryType =@BeneficiaryType and intBenid=@intBenid)
   end
   else
   begin
   (select COUNT(*) as CountBeneficiary from tbl_BeneficiaryDet where intBomasid=@intBomasid and BeneficiaryType =@BeneficiaryType and intBenid=@intBenid)
   end              
  
end 



