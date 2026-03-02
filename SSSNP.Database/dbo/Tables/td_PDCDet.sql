CREATE TABLE [dbo].[td_PDCDet] (
    [intPDC]       INT           IDENTITY (1000, 1) NOT NULL,
    [PDCName]      VARCHAR (100) NULL,
    [intStateid]   INT           NULL,
    [intCountieid] INT           NULL,
    [intPayamid]   INT           NULL,
    [Address]      VARCHAR (150) NULL,
    [Email]        VARCHAR (50)  NULL,
    [Contact_No]   VARCHAR (15)  NULL,
    [Status]       VARCHAR (5)   NULL,
    [Created_by]   INT           NULL,
    [Created_dt]   DATETIME      NULL,
    [Modified_by]  INT           NULL,
    [Modified_dt]  DATETIME      NULL,
    CONSTRAINT [PK_td_PDCDet] PRIMARY KEY CLUSTERED ([intPDC] ASC)
);

