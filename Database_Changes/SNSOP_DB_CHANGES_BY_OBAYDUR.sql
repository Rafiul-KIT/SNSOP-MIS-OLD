create table tbl_com2_Coaching_Session_Attendance_Audit
(
    Id                  int identity
        primary key,
    FileId              nvarchar(100)              not null,
    UploadedBy          int                        not null,
    UploadDate          datetime default getdate() not null,
    Validity            bit                        not null,
    InvalidReason       nvarchar(500),
    HouseholdNumber     nvarchar(50)               not null,
    CoachingDetailId    int                        not null,
    ClassAttendanceDate datetime
)
go

create table tbl_com2_course
(
    Id             int identity
        primary key,
    CourseCode     varchar(32)
        unique,
    CourseName     varchar(255)
        unique,
    CourseDuration int,
    CreatedBy      int      default 0         not null,
    CreatedDate    datetime default getdate() not null,
    ModifiedBy     int,
    ModifiedDate   datetime
)
go

create table tbl_com2_Course_Details
(
    Id                   int identity
        primary key,
    CourseCode           nvarchar(100)              not null,
    ClassHoldingDate     datetime                   not null,
    ClassDurationMinutes int,
    CreatedBy            int      default 0         not null,
    CreatedDate          datetime default getdate() not null,
    ModifiedBy           int,
    ModifiedDate         datetime
)
go

create table tbl_com2_Household_Enrollment_Data
(
    Id                        int identity,
    StateID                   int
        references tm_States,
    CountyID                  int
        references tm_Counties,
    PayamID                   int
        references tm_Payams,
    BomaID                    int
        references tm_Bomas,
    HouseholdNumber           nvarchar(255) not null
        constraint UK_Household_Enrollment_Data
            unique,
    InterestedInParticipating bit,
    FirstName                 nvarchar(255),
    MiddleName                nvarchar(255),
    LastName                  nvarchar(255),
    NickName                  nvarchar(255),
    Gender                    nvarchar(50),
    Age                       int,
    SurvivesTargeting         bit,
    TreatmentArm              bit,
    HHEnrollmentStatus        bit,
    GroupID                   int,
    EnrolledMemberID          int,
    CreatedBy                 int,
    CreatedDate               datetime,
    ModifiedBy                int,
    ModifiedDate              datetime,
    ReasonForWhyNotEnrolled   nvarchar(255)
)
go


----01/06/2025-----
alter table tbl_com2_Household_Group_View add VSLA_formed bit;

alter table tbl_com2_Household_Enrollment_Data add ReasonForWhyNotEnrolled nvarchar(255);
alter table tbl_com2_Household_Enrollment_Data_Audit add ReasonForWhyNotEnrolled nvarchar(255);

alter table tbl_com2_course add CourseDuration int;

alter table tbl_com2_Training_Attendance add ClassAttendanceDate datetime;
alter table tbl_com2_Training_Attendance_Audit add ClassAttendanceDate datetime;

alter table tbl_com2_Livelihood_Grant_Disbursement add ActualPaymentDate datetime;
alter table tbl_com2_Livelihood_Grant_Disbursement_Audit add ActualPaymentDate datetime;

ALTER TABLE tbl_com2_Financial_Services_Linkage_Tracking
    ADD FinancialServiceConnectionDate datetime;

ALTER TABLE tbl_com2_Financial_Services_Linkage_Tracking
    ADD DepositAccountOpened bit;

ALTER TABLE tbl_com2_Financial_Services_Linkage_Tracking
    ADD DepositAccountOpeningDate datetime;

ALTER TABLE tbl_com2_Financial_Services_Linkage_Tracking
    ADD AccountLastActivityDate datetime;



ALTER TABLE tbl_com2_Financial_Services_Linkage_Tracking_Audit
    ADD FinancialServiceConnectionDate datetime;

ALTER TABLE tbl_com2_Financial_Services_Linkage_Tracking_Audit
    ADD DepositAccountOpened bit;

ALTER TABLE tbl_com2_Financial_Services_Linkage_Tracking_Audit
    ADD DepositAccountOpeningDate datetime;

ALTER TABLE tbl_com2_Financial_Services_Linkage_Tracking_Audit
    ADD AccountLastActivityDate datetime;



ALTER TABLE tbl_com2_course
    ADD CreatedBy int NOT NULL DEFAULT 0;

ALTER TABLE tbl_com2_course
    ADD CreatedDate datetime NOT NULL DEFAULT GETDATE();

ALTER TABLE tbl_com2_course
    ADD ModifiedBy int NULL;

ALTER TABLE tbl_com2_course
    ADD ModifiedDate datetime NULL;


----22/07/2025-----

create table tbl_com2_Coaching_Entity
(
    Id                              int identity
        primary key,
    Name                            varchar(255) not null
        unique,
    NumberOfPlannedCoachingSessions int          not null
)


create table tbl_com2_Coaching_Details
(
    Id                             int identity
        primary key,
    CoachingId                     int not null
        references tbl_com2_Coaching_Entity
            on delete cascade,
    CoachingSessionHoldingDate     date,
    CoachingSessionDurationMinutes int
)


create table tbl_com2_Coaching_Session_Attendance
(
    Id                     int identity
        primary key,
    HouseholdNumber        varchar(50) not null,
    CoachingDetailId       int         not null
        references tbl_com2_Coaching_Details
            on delete cascade,
    CoachingAttendanceDate date,
    CreatedBy              int,
    CreatedDate            datetime,
    ModifiedBy             int,
    ModifiedDate           datetime
)


create table tbl_com2_Coaching_Session_Attendance_Audit
(
    Id                  int identity
        primary key,
    FileId              nvarchar(100)              not null,
    UploadedBy          int                        not null,
    UploadDate          datetime default getdate() not null,
    Validity            bit                        not null,
    InvalidReason       nvarchar(500),
    HouseholdNumber     nvarchar(50)               not null,
    CoachingDetailId    int                        not null,
    ClassAttendanceDate datetime
)


alter table tbl_audit_payroll_download add PaymentCycle nvarchar(500);



----------06/08/2025-----------
ALTER procedure [dbo].[Retrieve_getWageReportAbstractAPIInvoicebyReqNoPaid] -- '05/23/2016','05/30/2016',1015
(
    @workCode varchar(120),
    @intStateid varchar(8),
    @intCountieid varchar(8),
    @intPayamid varchar(8),
    @intBomasid varchar(10) ,
    @IPid varchar(8)  ,
    @ReqNo varchar(30),
    @paymentCycle varchar(30),
    @reportType varchar(10), -- "All", "DIS", "LIPW" (non-DIS)
    @fromDate as datetime,
    @toDate as datetime 
)
as
begin
    select *
         , ExchangeRate * AmountPaidUSD as AmountPaidSSP
    from WagePaidResultDetails_tblTemp
    where
        workcode like @workcode
      and intip  like @IPid
      and intstateid  like @intstateid
      and intCountyId  like @intCountieid
      and intPayamid like  @intPayamid
      and intbomaid like  @intbomasid
      and RequestNo like @reqno
      and PaymentCycle like @paymentCycle
      and
        (
            --(@reportType = 'DIS' and workcode='DIS - Direct Income Support')
            --or (@reportType = 'LIPW' and workcode<>'DIS - Direct Income Support')
            (@reportType = 'DIS' and (workcode='DIS - Direct Income Support' or workremarks like '%DIS%' ))
                or (@reportType = 'LIPW' and workcode<>'DIS - Direct Income Support' and workremarks not like '%DIS%')
                or (@reportType <> 'DIS' and @reportType <> 'LIPW')
            )
      and (@fromdate is null or (@fromDate <= fromDate and @fromDate <todate))
      and (@fromdate is null or (@toDate >= fromDate and @toDate >= todate))

    select
        SUM(TotalMembers) AS TotalMembers
         ,SUM(TotalWorkDays) AS TotalWorkDays
         --,SUM(TotalAmount) AS AmountPaidUSD --Total
         ,ISNULL(null,
                 (select SUM(b.wagerateusdtotal) as TotalAmount from td_WorkWageRequest a
                                                                         inner join td_WageReqBenDet b on a.intWagePaymentReqid=b.intWagePaymentReqid
                                                                         inner join tbl_BeneficiaryDet c on b.intBenid=c.intBenid
                  where a.Status in (4,5,6)
                    and intStateId like @intStateid
                    and intCountieid like @intCountieid
                    and intPayamId like @intPayamid
                    and intBomasId like @intBomasid
                    and PaymentCycle like @paymentCycle)
          ) as AmountPaidUSD

         ,SUM(TotalMembersPaid) AS TotalMembersPaid

         ,SUM(AmountPaidUSD) AS TotalMembersPaidMain --Paid
         ,SUM(TotalMembers)-SUM(TotalMembersPaid) as TotalMembersUnPaid
         --,SUM(TotalAmount)-SUM(AmountPaidUSD) as TotalAmountUnPaid --unpaid
         ,ISNULL(null,
                 (select SUM(b.wagerateusdtotal) as TotalAmount from td_WorkWageRequest a
                                                                         inner join td_WageReqBenDet b on a.intWagePaymentReqid=b.intWagePaymentReqid
                                                                         inner join tbl_BeneficiaryDet c on b.intBenid=c.intBenid
                  where a.Status in (4,5,6)
                    and intStateId like @intStateid
                    and intCountieid like @intCountieid
                    and intPayamId like @intPayamid
                    and intBomasId like @intBomasid
                    and PaymentCycle like @paymentCycle)
                     -SUM(AmountPaidUSD)) as TotalAmountUnPaid --unpaid
         ,SUM(AmountPaidUSD*exchangerate) AS TotalMembersPaidMainSSP --PaidSSP
    from WagePaidResultDetails_tblTemp
    where intStateId like @intStateid
      and intCountyId like @intCountieid
      and intPayamId like @intPayamid
      and intBomaId like @intBomasid
      and PaymentCycle like @paymentCycle
      and WorkCode like @workCode --changed here
      and RequestNo like @ReqNo --changed here
      and
        (
            (@reportType = 'DIS' and workcode='DIS - Direct Income Support')
                or (@reportType = 'LIPW' and workcode<>'DIS - Direct Income Support')
                or (@reportType <> 'DIS' and @reportType <> 'LIPW')
            )
      and (@fromdate is null or (@fromDate <= fromDate and @fromDate <todate))
      and (@fromdate is null or (@toDate >= fromDate and @toDate >= todate))


    declare @TotalBeneficiariesPaid as int
    declare @TotalBeneficiariesUnpaid as int


    /*set @TotalBeneficiariesPaid=( select count (distinct a.intbenid) from tr_BenWagesPayment a
                                                                              inner join tbl_BeneficiaryDet b on a.intbenid=b.intBenid
                                                                              inner join td_WorkWageRequest wwr
                                                                                         on a.uuid=wwr.intWagePaymentReqid
                                  where intStateId like @intStateid
                                    and intCountieid like @intCountieid
                                    and intPayamId like @intPayamid
                                    and intBomasId like @intBomasid
                                    and PaymentCycle like @paymentCycle
                                    and (@fromdate is null or (@fromDate <= fromDate and @fromDate <todate))
                                    and (@fromdate is null or (@toDate >= fromDate and @toDate >= todate))
    )
    set @TotalBeneficiariesUnPaid= (SELECT COUNT(DISTINCT b.intBenid)
                                    FROM td_WageReqBenDet wrbd
                                             INNER JOIN td_WorkWageRequest wwr ON wrbd.intWagePaymentReqid = wwr.intWagePaymentReqid
                                             INNER JOIN tbl_BeneficiaryDet b ON wrbd.intBenid = b.intBenid
                                    WHERE b.intStateId LIKE @intStateid
                                      AND b.intCountieid LIKE @intCountieid
                                      AND b.intPayamId LIKE @intPayamid
                                      AND b.intBomasId LIKE @intBomasid
                                      AND wwr.PaymentCycle LIKE @paymentCycle
                                      AND (@ReqNo IS NULL OR wwr.ReqNo LIKE @ReqNo)
                                      AND (@workCode IS NULL OR b.WorkCode LIKE @workCode)
                                      AND (@fromDate IS NULL OR (@fromDate <= wwr.ReqFromDate AND @fromDate < wwr.ReqToDate))
                                      AND (@toDate IS NULL OR (@toDate >= wwr.ReqFromDate AND @toDate >= wwr.ReqToDate))
                                      AND NOT EXISTS (
                                        SELECT 1
                                        FROM tr_BenWagesPayment p
                                        WHERE p.intBenid = b.intBenid
                                          AND p.uuid = wwr.intWagePaymentReqid
                                          AND (@fromDate IS NULL OR (@fromDate <= p.fromDate AND @fromDate < p.toDate))
                                          AND (@toDate IS NULL OR (@toDate >= p.fromDate AND @toDate >= p.toDate))
                                    ))*/

    set @TotalBeneficiariesPaid = (
        select sum(TotalMembersPaid)
        from WagePaidResultDetails_tblTemp
        where intStateId like @intStateid
          and intCountyId like @intCountieid
          and intPayamId like @intPayamid
          and intBomaId like @intBomasid
          and PaymentCycle like @paymentCycle
          and workCode like @workCode  -- Filter by work code
          and RequestNo like @ReqNo   -- Filter by request number
          and TotalMembersPaid > 0    -- Only include paid beneficiaries
          and (@fromDate is null or (@fromDate <= FromDate and @fromDate < ToDate))
          and (@toDate is null or (@toDate >= FromDate and @toDate >= ToDate))
    )

-- Modify TotalBeneficiariesUnPaid to calculate from the temp table
    set @TotalBeneficiariesUnPaid = (
        select sum(TotalMembers) - sum(TotalMembersPaid)  -- Unpaid = Total - Paid
        from WagePaidResultDetails_tblTemp
        where intStateId like @intStateid
          and intCountyId like @intCountieid
          and intPayamId like @intPayamid
          and intBomaId like @intBomasid
          and PaymentCycle like @paymentCycle
          and workCode like @workCode  -- Filter by work code
          and RequestNo like @ReqNo   -- Filter by request number
          and (@fromDate is null or (@fromDate <= FromDate and @fromDate < ToDate))
          and (@toDate is null or (@toDate >= FromDate and @toDate >= ToDate))
    )

    select @TotalBeneficiariesPaid,@TotalBeneficiariesUnpaid

end
go

