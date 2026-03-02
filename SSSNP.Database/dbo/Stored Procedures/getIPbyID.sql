--created by suresh as on 06-1-2016                  
--Purpose : to get States by id                  
CREATE Procedure [dbo].[getIPbyID]                  
(                  
@intIP varchar(10)                  
)                  
as begin                  
                  
          
          
SELECT    td_IPDetails.SPerson,td_IPDetails.SEmail, td_IPDetails.intIP,SecondaryAuthorisedPerson, td_IPDetails.IPName, td_IPDetails.AuthorisedPerson, td_IPDetails.TypeofIP, td_IPDetails.ContactNo, td_IPDetails.SecondaryContactNo, td_IPDetails.Email, td_IPDetails.Address,           
                      td_IPDetails.Website, td_IPDetails.Created_by, tbl_Users.Password, tbl_Users.User_id, tbl_Users.User_type          
FROM         td_IPDetails left outer JOIN          
                      tbl_Users ON td_IPDetails.intIP = tbl_Users.User_code where intIP like @intIP and tbl_Users.User_type like 'IP'          
          
     select * from tbl_Users where User_code like @intIP and User_type like 'IP'         
                  
end




