CREATE TABLE [dbo].[tr_BenAttendanceDet] (
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
    [WageApplyFlag]     INT           NULL,
    [intWageReqid]      INT           NULL,
    [cashP_FLI]         BIT           NULL,
    [cashP_ECD]         BIT           NULL,
    [cashP_WASH]        BIT           NULL,
    CONSTRAINT [PK_tr_BenAttendanceDet] PRIMARY KEY CLUSTERED ([intAttendanceid] ASC),
    CONSTRAINT [FK_tr_BenAttendanceDet_tbl_BeneficiaryDet] FOREIGN KEY ([intBenid]) REFERENCES [dbo].[tbl_BeneficiaryDet] ([intBenid]),
    CONSTRAINT [FK_tr_BenAttendanceDet_tbl_WorkMaster] FOREIGN KEY ([intWorkActivityId]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);




GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDet]
    ON [dbo].[tr_BenAttendanceDet]([intBenid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDet_1]
    ON [dbo].[tr_BenAttendanceDet]([intWorkActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDet_2]
    ON [dbo].[tr_BenAttendanceDet]([AttendanceDate] ASC);


GO
CREATE NONCLUSTERED INDEX [tr_BenAttendanceDetintWorkid]
    ON [dbo].[tr_BenAttendanceDet]([intWorkActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [tr_BenAttendanceDetintBenid]
    ON [dbo].[tr_BenAttendanceDet]([intBenid] ASC);


GO
CREATE NONCLUSTERED INDEX [tr_BenAttendanceDetAttendanceDate]
    ON [dbo].[tr_BenAttendanceDet]([AttendanceDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDet_intWorkActivityIdNew]
    ON [dbo].[tr_BenAttendanceDet]([intWorkActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDet_intBenidNew]
    ON [dbo].[tr_BenAttendanceDet]([intBenid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDet_AttendanceDateNew]
    ON [dbo].[tr_BenAttendanceDet]([AttendanceDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenAttendanceDet_StatusNew]
    ON [dbo].[tr_BenAttendanceDet]([Status] ASC);

