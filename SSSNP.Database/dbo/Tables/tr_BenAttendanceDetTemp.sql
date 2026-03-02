CREATE TABLE [dbo].[tr_BenAttendanceDetTemp] (
    [intAttendanceid]   INT           IDENTITY (1000, 1) NOT NULL,
    [intWorkActivityId] INT           NULL,
    [intBenid]          INT           NULL,
    [AttendanceDate]    DATETIME      NULL,
    [Status]            VARCHAR (10)  NULL,
    [Created_by]        INT           NULL,
    [Created_dt]        DATETIME      NULL,
    [Modified_by]       INT           NULL,
    [Modified_dt]       DATETIME      NULL,
    [AttendanceYear]    INT           NULL,
    [PortYear]          INT           NULL,
    [PortPhase]         INT           NULL,
    [PortedSheetName]   VARCHAR (180) NULL,
    [PaymentFlag]       INT           NULL,
    CONSTRAINT [PK_tr_BenAttendanceDetTemp] PRIMARY KEY CLUSTERED ([intAttendanceid] ASC)
);

