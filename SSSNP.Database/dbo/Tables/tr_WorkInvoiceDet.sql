CREATE TABLE [dbo].[tr_WorkInvoiceDet] (
    [intWorkInvoiceid]  INT          IDENTITY (1, 1) NOT NULL,
    [intWorkid]         INT          NULL,
    [Invoiceid]         INT          NULL,
    [InvoiceStatus]     VARCHAR (30) NULL,
    [InvoiceBy]         INT          NULL,
    [InvoiceDt]         DATETIME     NULL,
    [InvoiceApprovedby] INT          NULL,
    [InvoiceApproveddt] DATETIME     NULL,
    [Created_by]        INT          NULL,
    [Created_dt]        DATETIME     NULL,
    [Modified_by]       INT          NULL,
    [Modified_dt]       DATETIME     NULL,
    CONSTRAINT [PK_tr_WorkInvoiceDet] PRIMARY KEY CLUSTERED ([intWorkInvoiceid] ASC),
    CONSTRAINT [FK_tr_WorkInvoiceDet_tbl_WorkMaster] FOREIGN KEY ([intWorkid]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);

