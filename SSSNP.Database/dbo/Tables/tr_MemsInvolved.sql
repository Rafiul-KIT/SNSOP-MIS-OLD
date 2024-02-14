CREATE TABLE [dbo].[tr_MemsInvolved] (
    [intMemberInvolved] INT         IDENTITY (1000, 1) NOT NULL,
    [intWorkid]         INT         NOT NULL,
    [intMemid]          INT         NULL,
    [MemType]           VARCHAR (5) NULL,
    [Created_by]        INT         NULL,
    [Created_dt]        DATETIME    NULL,
    [Modified_by]       INT         NULL,
    [Modified_dt]       DATETIME    NULL,
    CONSTRAINT [PK_tr_MemsInvolved] PRIMARY KEY CLUSTERED ([intMemberInvolved] ASC)
);

