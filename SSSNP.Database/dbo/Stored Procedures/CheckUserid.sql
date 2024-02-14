--created by suresh as on 15-1-16 for check user id  
CREATE Procedure [dbo].[CheckUserid]    
(    
@User_id varchar(50)    
)    
    
as begin     
select * from tbl_Users where User_id like @User_id     
end




