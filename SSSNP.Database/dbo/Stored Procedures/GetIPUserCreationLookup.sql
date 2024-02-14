
CREATE Procedure [dbo].[GetIPUserCreationLookup]      --GetIPUserCreationLookup '%'            
(                  
@intIP varchar(10)  ,@intUserid varchar(10))                  
as begin                  
                  
        
     select * from tbl_Users where User_code like @intIP and Role is not Null  and intUserid like @intUserid                  
end