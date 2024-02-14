
CREATE procedure [dbo].[GetWorkProposalAttachments]
(
@intWorkId varchar(8)
)
As
Begin

		select 
			intImageId
			, [Type]
			, PhotoFilename
			, tbl_Attachements.Created_by
			, tbl_Users.[User_name] as Created_byName
			, tbl_users.User_type as UserRole
			, tbl_Attachements.Created_dt
			, file_path
		from tbl_Attachements
		inner join tbl_users on tbl_Attachements.created_by=tbl_users.intuserid
		where type = 'WorkProposalAttachment'
		and intworkdetailsTypeid =@intWorkId 
  

  
End