create procedure RemoveAlternateTotalDump
As
Begin
truncate table tbl_BenAlternateDump
select 1000
return 1000
End