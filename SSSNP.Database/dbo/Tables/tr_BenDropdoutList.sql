CREATE TABLE [dbo].[tr_BenDropdoutList] (
    [intDropoutid]  INT           IDENTITY (1000, 1) NOT NULL,
    [intBenid]      INT           NULL,
    [DropoutReason] VARCHAR (250) NULL,
    [DropdoutDate]  DATETIME      NULL,
    [Created_by]    INT           NULL,
    [Created_dt]    DATETIME      NULL,
    CONSTRAINT [PK_DropdoutList] PRIMARY KEY CLUSTERED ([intDropoutid] ASC)
);

