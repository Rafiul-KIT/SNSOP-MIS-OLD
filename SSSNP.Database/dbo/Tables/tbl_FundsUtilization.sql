CREATE TABLE [dbo].[tbl_FundsUtilization] (
    [intFundsUtilization]  INT           IDENTITY (1000, 1) NOT NULL,
    [intWorkid]            INT           NULL,
    [FundsUtilizationDate] DATE          NULL,
    [FundsUtilizationFor]  VARCHAR (100) NULL,
    [Amount]               FLOAT (53)    NULL,
    [Remarks]              VARCHAR (MAX) NULL,
    [Created_by]           INT           NULL,
    [Created_dt]           DATETIME      NULL,
    [Modified_by]          INT           NULL,
    [Modied_dt]            DATETIME      NULL,
    CONSTRAINT [PK_tbl_FundsUtilization] PRIMARY KEY CLUSTERED ([intFundsUtilization] ASC)
);

