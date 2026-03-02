CREATE Procedure [dbo].[getWorkComments]          
(          
@IntWorkId varchar(10),
@Status varchar(20),
@Created_by varchar(10) 
)          
as begin          
          
SELECT     intWorkCommentId, IntWorkId, Comment, SetStatus, tbl_WorkComments.Created_by, tbl_Users.[User_name] as Creator, tbl_WorkComments.Created_dt as CreatedDate, tbl_users.User_type as UserRole
FROM         tbl_WorkComments  
		INNER JOIN    
			tbl_Users 
		on tbl_WorkComments.Created_by = tbl_users.intUserid

where intWorkid=@IntWorkId
	and (@Status is null OR tbl_WorkComments.SetStatus=@Status)
	and (@Created_by is null OR tbl_WorkComments.Created_by=@Created_by)

order by tbl_WorkComments.Created_dt desc
end





select * from tbl_WorkComments