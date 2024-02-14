
CREATE procedure [dbo].[GetBendiciaryalternatecheckingRegistrationUpdate]
(
@intBenid varchar(8),
@household_number1 varchar(300),
@household_number2 varchar(300),
@household_number3 varchar(300),
@household_number varchar(300)
)
AS
Begin
create table #checkaltertnates
(
household_number varchar(300),
Benid varchar(8),
EnrollmentNo varchar(30)
)

insert into #checkaltertnates
select household_number,intBenid,EnrollmentNo from tbl_BeneficiaryDet where household_number is not null

insert into #checkaltertnates
select alternate_number,intBenid,EnrollmentNo from tbl_BeneficiaryDet where alternate_number is not null

insert into #checkaltertnates
select Secondalternate_number,intBenid,EnrollmentNo from tbl_BeneficiaryDet where Secondalternate_number is not null

insert into #checkaltertnates
select thirdalternate_number,intBenid,EnrollmentNo from tbl_BeneficiaryDet where thirdalternate_number is not null

create table #checkaltertnatesfinal
(
EnrollmentNo varchar(30)
)
if(@household_number1<>'')
Begin
insert into #checkaltertnatesfinal
select EnrollmentNo from #checkaltertnates where Benid <> @intBenid and household_number like @household_number1
End
if(@household_number2<>'')
Begin
insert into #checkaltertnatesfinal
select EnrollmentNo from #checkaltertnates where Benid <> @intBenid and household_number like @household_number2
End
if(@household_number3<>'')
Begin
insert into #checkaltertnatesfinal
select EnrollmentNo from #checkaltertnates where Benid <> @intBenid and household_number like @household_number3
End
if(@household_number<>'')
Begin
insert into #checkaltertnatesfinal
select EnrollmentNo from #checkaltertnates where Benid <> @intBenid and household_number like @household_number
End
select * from #checkaltertnatesfinal
Drop table #checkaltertnates
Drop table #checkaltertnatesfinal
End
