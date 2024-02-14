CREATE TABLE [dbo].[tbl_WorkMaster] (
    [intWorkid]            INT           IDENTITY (1000, 1) NOT NULL,
    [intTSTid]             INT           NULL,
    [WorkCode]             VARCHAR (120) NULL,
    [IPid]                 INT           NOT NULL,
    [intPrjid]             INT           NOT NULL,
    [TypeofMeeting]        VARCHAR (3)   NULL,
    [MeetingDate]          DATETIME      NULL,
    [intStateid]           INT           NULL,
    [intCountieid]         INT           NULL,
    [intPayamid]           INT           NULL,
    [intBomasid]           INT           NULL,
    [PDC_ApprvDate]        DATETIME      NULL,
    [Status]               VARCHAR (20)  NULL,
    [StatusDate]           DATETIME      NULL,
    [Remarks]              VARCHAR (150) NULL,
    [Created_by]           INT           NULL,
    [Created_dt]           DATETIME      NULL,
    [Modified_by]          INT           NULL,
    [Modified_dt]          DATETIME      NULL,
    [Inprogressby]         INT           NULL,
    [Inprogressdt]         DATETIME      NULL,
    [Completedby]          INT           NULL,
    [Completeddt]          DATETIME      NULL,
    [InvoiceRequestStatus] VARCHAR (30)  NULL,
    [InvoiceRequestby]     INT           NULL,
    [InvoiceRequestdt]     DATETIME      NULL,
    [InvoiceApprovedby]    INT           NULL,
    [InvoiceApproveddt]    DATETIME      NULL,
    [YearofRegistration]   INT           NULL,
    [PortYear]             INT           NULL,
    [PortPhase]            INT           NULL,
    [PortedSheetName]      VARCHAR (180) NULL,
    CONSTRAINT [PK_tbl_WorkProposals] PRIMARY KEY CLUSTERED ([intWorkid] ASC),
    CONSTRAINT [FK_tbl_WorkMaster_tm_Bomas] FOREIGN KEY ([intBomasid]) REFERENCES [dbo].[tm_Bomas] ([intBomasid]),
    CONSTRAINT [FK_tbl_WorkMaster_tm_Counties] FOREIGN KEY ([intCountieid]) REFERENCES [dbo].[tm_Counties] ([intCountieid]),
    CONSTRAINT [FK_tbl_WorkMaster_tm_Payams] FOREIGN KEY ([intPayamid]) REFERENCES [dbo].[tm_Payams] ([intPayamid]),
    CONSTRAINT [FK_tbl_WorkMaster_tm_States] FOREIGN KEY ([intStateid]) REFERENCES [dbo].[tm_States] ([intStateid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster]
    ON [dbo].[tbl_WorkMaster]([IPid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster_1]
    ON [dbo].[tbl_WorkMaster]([intWorkid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster_2]
    ON [dbo].[tbl_WorkMaster]([intCountieid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster_3]
    ON [dbo].[tbl_WorkMaster]([intPayamid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster_4]
    ON [dbo].[tbl_WorkMaster]([intBomasid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster_IPidNew]
    ON [dbo].[tbl_WorkMaster]([IPid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster_WorkCodeNew]
    ON [dbo].[tbl_WorkMaster]([WorkCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster_intStateidNew]
    ON [dbo].[tbl_WorkMaster]([intStateid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster_intCountieidNew]
    ON [dbo].[tbl_WorkMaster]([intCountieid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster_intPayamidNew]
    ON [dbo].[tbl_WorkMaster]([intPayamid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkMaster_intBomasidNew]
    ON [dbo].[tbl_WorkMaster]([intBomasid] ASC);

