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

