
CREATE Procedure [dbo].[CheckIsIPApprover]    
(    
@User_id varchar(50)    
)    
    
as begin     
select * from tbl_Users where intuserid like @User_id
and IP_Approver=1
end