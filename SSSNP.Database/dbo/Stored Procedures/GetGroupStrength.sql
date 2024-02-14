CREATE Procedure [dbo].[GetGroupStrength]
@intBomasid int,
@GroupCode varchar(100),
@BeneficiaryType  varchar(50)
as 
begin 
if(@BeneficiaryType ='Group')
begin
(select GroupStrength from GroupMaster where Boma=@intBomasid and GroupCode=@GroupCode and GroupType =@BeneficiaryType)
   end
   else
   begin
   (select GroupStrength from GroupMaster where Boma=@intBomasid and GroupType =@BeneficiaryType)
   end              
  
end 



