CREATE TABLE [dbo].[tr_FinancialApproval] (
    [intFundApproval] INT             IDENTITY (1000, 1) NOT NULL,
    [intWorkId]       INT             NULL,
    [ApprovedDate]    DATETIME        NULL,
    [PaymentMode]     VARCHAR (10)    NULL,
    [TransactionNo]   VARCHAR (50)    NULL,
    [PaymentDate]     DATETIME        NULL,
    [PaidAmount]      DECIMAL (18, 2) NULL,
    [Created_by]      INT             NULL,
    [Created_dt]      DATETIME        NULL,
    [Modified_by]     INT             NULL,
    [Modified_dt]     DATETIME        NULL,
    [invoiceid]       INT             NULL,
    CONSTRAINT [PK_tr_FinancialApproval] PRIMARY KEY CLUSTERED ([intFundApproval] ASC),
    CONSTRAINT [FK_tr_FinancialApproval_tbl_WorkMaster] FOREIGN KEY ([intWorkId]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);

