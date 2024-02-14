

CREATE procedure [dbo].[sp_getIPApprovers]        --exec sp_getusersdata 'ip'

as
begin
select * 
from tbl_Users 
where User_type ='IP'
and IP_Approver ='1'
end