CREATE TABLE [dbo].[tr_BenAttendanceDetDIS] (
    [intAttendanceid]   INT           IDENTITY (1000, 1) NOT NULL,
    [intWorkActivityId] INT           NULL,
    [intBenid]          INT           not NULL,
    [AttendanceRifYear]    nvarchar(5)   not NULL,
    [AttendanceRifMonth]   int           not NULL,
    [AttendanceRifTotalDay] decimal(8,2)          not NULL,
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
    [WageApplyFlag]     INT           NULL,
    [intWageReqid]      INT           NULL,
    CONSTRAINT [PK_tr_BenAttendanceDetDIS] PRIMARY KEY CLUSTERED ([intAttendanceid] ASC),
    CONSTRAINT [FK_tr_BenAttendanceDet_tbl_BeneficiaryDetDIS] FOREIGN KEY ([intBenid]) REFERENCES [dbo].[tbl_BeneficiaryDet] ([intBenid]),
    CONSTRAINT [FK_tr_BenAttendanceDet_tbl_WorkMasterDIS] FOREIGN KEY ([intWorkActivityId]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDetDIS]
    ON [dbo].[tr_BenAttendanceDet]([intBenid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDet_1DIS]
    ON [dbo].[tr_BenAttendanceDet]([intWorkActivityId] ASC);


GO

GO
CREATE NONCLUSTERED INDEX [tr_BenAttendanceDetintWorkidDIS]
    ON [dbo].[tr_BenAttendanceDet]([intWorkActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [tr_BenAttendanceDetintBenidDIS]
    ON [dbo].[tr_BenAttendanceDet]([intBenid] ASC);



GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDet_intWorkActivityIdNewDIS]
    ON [dbo].[tr_BenAttendanceDet]([intWorkActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDet_intBenidNewDIS]
    ON [dbo].[tr_BenAttendanceDet]([intBenid] ASC);

