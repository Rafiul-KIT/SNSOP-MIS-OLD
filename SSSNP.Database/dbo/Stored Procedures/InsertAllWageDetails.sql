create procedure InsertAllWageDetails
(
@supervisor_id	varchar(8),
@household_number	varchar(250),
@paid_to	varchar(250),
@enrolment_number	varchar(30),
@date_from	datetime,
@date_to	datetime,
@exchange_rate	decimal(18, 2),
@wage	decimal(18, 2),
@amount	decimal(18, 2),
@work_id	varchar(8),
@work_code	varchar(240),
@attendance	varchar(8),
@trans_id	varchar(8),
@uuid	varchar(8),
@status	varchar(8),
@payment_centre	varchar(180),
@matched_fp	varchar(MAX),
@latitude	varchar(30),
@longitude	varchar(30),
@sync	varchar(1),
@created_at	datetime,
@updated_at	datetime
	
)
AS
Begin
insert into tblTotalPaymentofPayConnect
(
supervisor_id, household_number, paid_to, enrolment_number, date_from, date_to, exchange_rate, wage, amount, work_id, work_code, attendance, trans_id, uuid, status, payment_centre, matched_fp, latitude, 
                         longitude, sync, created_at, updated_at

)
values
(
@supervisor_id,
@household_number,
@paid_to,
@enrolment_number,
@date_from,
@date_to,
@exchange_rate,
@wage,
@amount,
@work_id,
@work_code,
@attendance,
@trans_id,
@uuid,
@status,
@payment_centre,
@matched_fp,
@latitude,
@longitude,
@sync,
@created_at,
@updated_at
)
select 1
return 1
End