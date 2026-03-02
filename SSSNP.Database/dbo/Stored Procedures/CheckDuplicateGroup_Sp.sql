CREATE Procedure[dbo].[CheckDuplicateGroup_Sp](
    
	@Boma int ,
	@GroupType varchar(100) ,
	@GroupCode varchar(100)
	
	)
	As 
	select * from GroupMaster
	where Boma=@Boma
	and GroupType=@GroupType
	and GroupCode=@GroupCode




