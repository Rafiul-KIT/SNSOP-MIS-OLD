CREATE TABLE [dbo].[tbl_WorkProgress] (
    [intWorkProgres]  INT           IDENTITY (1000, 1) NOT NULL,
    [intWorkid]       INT           NULL,
    [WorkStartDate]   DATETIME      NULL,
    [Status]          VARCHAR (10)  NULL,
    [Incharge]        VARCHAR (50)  NULL,
    [Remarks]         VARCHAR (150) NULL,
    [Created_by]      INT           NULL,
    [Created_dt]      DATETIME      NULL,
    [Modified_by]     INT           NULL,
    [Modied_dt]       DATETIME      NULL,
    [PortYear]        INT           NULL,
    [PortPhase]       INT           NULL,
    [PortedSheetName] VARCHAR (180) NULL,
    CONSTRAINT [PK_tbl_WorkProgress] PRIMARY KEY CLUSTERED ([intWorkProgres] ASC),
    CONSTRAINT [FK_tbl_WorkProgress_tbl_WorkMaster] FOREIGN KEY ([intWorkid]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WorkProgress]
    ON [dbo].[tbl_WorkProgress]([intWorkid] ASC);

