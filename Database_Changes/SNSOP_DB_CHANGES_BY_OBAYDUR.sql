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

