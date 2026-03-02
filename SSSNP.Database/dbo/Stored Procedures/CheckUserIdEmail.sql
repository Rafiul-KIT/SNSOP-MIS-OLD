--created by suresh as on 15-1-16 for check user id  
CREATE Procedure [dbo].[CheckUserIdEmail]    
(    
@Email varchar(255)    
)    
    
as begin     
select distinct * from tbl_Users where email = @email
end
