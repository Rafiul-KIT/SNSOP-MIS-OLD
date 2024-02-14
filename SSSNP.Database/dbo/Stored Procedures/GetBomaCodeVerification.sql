create procedure GetBomaCodeVerification
(
@BomaCode varchar(30)
)
as
Begin
select * from tm_Bomas where BomaCode like @BomaCode
End