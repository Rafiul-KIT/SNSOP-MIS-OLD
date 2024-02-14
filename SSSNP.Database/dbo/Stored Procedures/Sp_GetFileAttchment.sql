 
CREATE Procedure [dbo].[Sp_GetFileAttchment]
(@WorkActivityID nvarchar(max))
as
begin

  SELECT   tbl_Attachements.PhotoFileName as WorkPhoto,File_Path 
FROM  tbl_Attachements
left join tr_ProposedWorkAct on tbl_Attachements.IntWorkDetailsTypeId=tr_ProposedWorkAct.intWorkActivityId
where tbl_Attachements.IntWorkDetailsTypeId like @WorkActivityID
and PhotoFileName!=''
and  SUBSTRING(tbl_Attachements.PhotoFileName,0,CHARINDEX('_',tbl_Attachements.PhotoFileName,0))='WorkDocument'

SELECT   tbl_Attachements.PhotoFileName as EquipmentPhoto,File_Path
FROM tbl_Attachements    
left join tr_ProposedWorkAct on tbl_Attachements.IntWorkDetailsTypeId=tr_ProposedWorkAct.intWorkActivityId
where tbl_Attachements.IntWorkDetailsTypeId like @WorkActivityID
and PhotoFileName!=''
and  SUBSTRING(tbl_Attachements.PhotoFileName,0,CHARINDEX('_',tbl_Attachements.PhotoFileName,0))='EquipmentDocument'

SELECT   tbl_Attachements.PhotoFileName as MaterialsPhoto,File_Path
FROM tbl_Attachements    
left join tr_ProposedWorkAct on tbl_Attachements.IntWorkDetailsTypeId=tr_ProposedWorkAct.intWorkActivityId
where tbl_Attachements.IntWorkDetailsTypeId like @WorkActivityID
and PhotoFileName!=''
 and  SUBSTRING(tbl_Attachements.PhotoFileName,0,CHARINDEX('_',tbl_Attachements.PhotoFileName,0))='MaterialDocument'

	  
end






