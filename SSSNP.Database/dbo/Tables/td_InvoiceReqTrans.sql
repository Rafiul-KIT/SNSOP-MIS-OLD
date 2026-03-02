CREATE TABLE [dbo].[td_InvoiceReqTrans] (
    [intInvoiceReqTransid] INT           IDENTITY (1, 1) NOT NULL,
    [intInvoiceReqid]      INT           NULL,
    [intWorkid]            INT           NULL,
    [intFromid]            INT           NULL,
    [intToid]              INT           NULL,
    [Status]               VARCHAR (30)  NULL,
    [TransDate]            DATETIME      NULL,
    [Remarks]              VARCHAR (350) NULL,
    [Created_by]           INT           NULL,
    [Created_dt]           DATETIME      NULL,
    [Modified_by]          INT           NULL,
    [Modified_dt]          DATETIME      NULL
);

