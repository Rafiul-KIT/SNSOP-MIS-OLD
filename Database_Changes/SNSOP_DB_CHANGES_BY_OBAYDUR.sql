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
