CREATE procedure [dbo].[GetCheckalternatenumberinAPI]
(
@household_number varchar(300)
)
AS
Begin
create table #checkaltertnates
(
household_number varchar(300)
)

insert into #checkaltertnates
select household_number from tbl_BeneficiaryDet where household_number is not null

insert into #checkaltertnates
select alternate_number from tbl_BeneficiaryDet where alternate_number is not null

insert into #checkaltertnates
select Secondalternate_number from tbl_BeneficiaryDet where Secondalternate_number is not null

insert into #checkaltertnates
select thirdalternate_number from tbl_BeneficiaryDet where thirdalternate_number is not null

create table #checkaltertnatesfinal
(
household_number varchar(300)
)
if(@household_number<>'')
Begin
insert into #checkaltertnatesfinal
select household_number from #checkaltertnates where household_number like @household_number
End

select * from #checkaltertnatesfinal
Drop table #checkaltertnates
Drop table #checkaltertnatesfinal
End