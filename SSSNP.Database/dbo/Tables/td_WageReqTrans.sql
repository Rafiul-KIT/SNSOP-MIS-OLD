CREATE TABLE [dbo].[td_WageReqTrans] (
    [intWagesReqTransid]  INT           IDENTITY (1000, 1) NOT NULL,
    [intWagePaymentReqid] INT           NULL,
    [intWorkid]           INT           NULL,
    [intFromid]           INT           NULL,
    [intToid]             INT           NULL,
    [Status]              VARCHAR (30)  NULL,
    [TransDate]           DATETIME      NULL,
    [Remarks]             VARCHAR (350) NULL,
    [Created_by]          INT           NULL,
    [Created_dt]          DATETIME      NULL,
    [Modified_by]         INT           NULL,
    [Modified_dt]         DATETIME      NULL,
    CONSTRAINT [PK_td_WageReqTrans] PRIMARY KEY CLUSTERED ([intWagesReqTransid] ASC),
    CONSTRAINT [FK_td_WageReqTrans_td_WorkWageRequest] FOREIGN KEY ([intWagePaymentReqid]) REFERENCES [dbo].[td_WorkWageRequest] ([intWagePaymentReqid])
);

