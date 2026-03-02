CREATE TABLE [dbo].[tr_InvoiceStatus] (
    [ID]             INT           IDENTITY (1000, 1) NOT NULL,
    [invoiceid]      INT           NULL,
    [intWorkid]      INT           NULL,
    [InvoiceStatus]  VARCHAR (100) NULL,
    [InvoiceRemarks] VARCHAR (MAX) NULL,
    [Created_by]     INT           NULL,
    [Created_dt]     DATE          NULL,
    [Modified_by]    INT           NULL,
    [Modified_dt]    DATE          NULL,
    [ApprovedDate]   DATE          NULL
);

