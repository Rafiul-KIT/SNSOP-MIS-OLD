create Procedure InsertFingerPrint
(
@household_number varchar(500),
@fingerprint_number varchar(8),
@fingerprint varchar(Max),
@beneficiary_type varchar(8),
@ReturnVal int output
)
As
Begin

-- By Srikanth on 04-08-2018 for API Update Finger Prints
if((select count(intFingerPrintid) from tr_BenfingerPrintDet where household_number like @household_number and fingerprint_number like @fingerprint_number) =0)
Begin
insert into tr_BenfingerPrintDet ( household_number, beneficiary_type, fingerprint_number, fingerprint) values (@household_number,@beneficiary_type,@fingerprint_number,@fingerprint)
 select 1000
    set @ReturnVal = 1000
End
else
begin
 select 2000
    set @ReturnVal = 2000
end
End