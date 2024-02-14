CREATE procedure [dbo].[getOfficers]
(
@officer varchar(8)
)

AS
Begin
select intUserid,User_name from tbl_Users where User_type='PA' order by intuserid--User_name asc
select intUserid,User_name from tbl_Users where User_type='Cordinator'  order by intuserid--User_name asc
select intUserid,User_name from tbl_Users where User_type='Specialist'  order by intuserid--User_name asc
select intUserid,User_name from tbl_Users where User_type='Manager'  order by intuserid--User_name asc
End

