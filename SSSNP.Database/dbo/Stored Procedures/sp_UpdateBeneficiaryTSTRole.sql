


Create procedure [dbo].[sp_UpdateBeneficiaryTSTRole]
  
@intBenid int ,	 
@SelectionCriteria	varchar(255),
@SelectionReason varchar(255),
@FamilySize	varchar(50),
@Modified_by int

as begin  
   
		Update tbl_BeneficiaryDet
					set SelectionCriteria = @SelectionCriteria,
						SelectionReason = @SelectionReason,
						FamilySize = @FamilySize,
						Modified_by = @Modified_by,
						Modified_dt = getdate()
							Where intBenid = @intBenid
				
				 
end

