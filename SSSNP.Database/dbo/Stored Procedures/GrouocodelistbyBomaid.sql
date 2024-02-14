create procedure [dbo].[GrouocodelistbyBomaid]
(
@Bomaid varchar(8)
)
As
Begin

select * from GroupMaster where Boma like @Bomaid and GroupType='Group' order  by GroupCode asc
End


