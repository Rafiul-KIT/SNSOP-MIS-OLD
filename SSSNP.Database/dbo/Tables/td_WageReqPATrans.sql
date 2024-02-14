CREATE TABLE [dbo].[td_WageReqPATrans] (
    [intWagesReqTransPAid]  INT           IDENTITY (1000, 1) NOT NULL,
    [intWagePaymentReqPAid] INT           NULL,
    [intWagePaymentReqid]   INT           NULL,
    [intWorkid]             INT           NULL,
    [intFromid]             INT           NULL,
    [intToid]               INT           NULL,
    [Status]                VARCHAR (30)  NULL,
    [TransDate]             DATETIME      NULL,
    [Remarks]               VARCHAR (350) NULL,
    [Created_by]            INT           NULL,
    [Created_dt]            DATETIME      NULL,
    [Modified_by]           INT           NULL,
    [Modified_dt]           DATETIME      NULL,
    CONSTRAINT [PK_td_WageReqPATrans] PRIMARY KEY CLUSTERED ([intWagesReqTransPAid] ASC)
);

