CREATE Procedure [dbo].[CountBeneficiaryType]
@intBomasid int,
@GroupId varchar(50),
@BeneficiaryType  varchar(50)
as 
begin 
if(@BeneficiaryType ='Group')
begin
(select COUNT(*) as CountBeneficiary from tbl_BeneficiaryDet where intBomasid=@intBomasid and GroupId=@GroupId and BeneficiaryType =@BeneficiaryType)
   end
   else
   begin
   (select COUNT(*) as CountBeneficiary from tbl_BeneficiaryDet where intBomasid=@intBomasid and BeneficiaryType =@BeneficiaryType)
   end              
  
end 



