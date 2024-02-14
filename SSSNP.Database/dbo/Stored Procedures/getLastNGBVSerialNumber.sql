


CREATE procedure [dbo].[getLastNGBVSerialNumber]
AS
Begin
select top 1 SerialNumber from NonGBVMain order by SerialNumber Desc
End
