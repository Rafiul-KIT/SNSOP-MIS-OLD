CREATE TABLE [dbo].[tm_Course] (
    [intCourse]   INT          IDENTITY (1000, 1) NOT NULL,
    [CourseCode]  VARCHAR (5)  NULL,
    [CourseName]  VARCHAR (50) NULL,
    [Duration]    INT          NULL,
    [Created_by]  INT          NULL,
    [Created_dt]  DATETIME     NULL,
    [Modified_by] INT          NULL,
    [Modified_dt] DATETIME     NULL,
    CONSTRAINT [PK_tm_Course] PRIMARY KEY CLUSTERED ([intCourse] ASC)
);

