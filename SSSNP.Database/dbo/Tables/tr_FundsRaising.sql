CREATE TABLE [dbo].[tr_FundsRaising] (
    [intFundRaise]    INT             IDENTITY (1000, 1) NOT NULL,
    [intWorklId]      INT             NOT NULL,
    [FundRaiseDate]   DATETIME        NULL,
    [FundRaiseAmount] DECIMAL (18, 2) NULL,
    [Remarks]         VARCHAR (150)   NULL,
    [ApprovedDate]    DATETIME        NULL,
    [PaymentMode]     VARCHAR (10)    NULL,
    [TransactionNo]   VARCHAR (50)    NULL,
    [PaymentDate]     DATETIME        NULL,
    [PaidAmt]         DECIMAL (18, 2) NULL,
    [Created_by]      INT             NULL,
    [Created_dt]      DATETIME        NULL,
    [Modified_dt]     DATETIME        NULL,
    [Modified_by]     INT             NULL,
    [CoOrdinator_Id]  INT             NULL,
    [Specialist_Id]   INT             NULL,
    [Manager_Id]      INT             NULL,
    CONSTRAINT [PK_tr_FundsRaising] PRIMARY KEY CLUSTERED ([intFundRaise] ASC),
    CONSTRAINT [FK_tr_FundsRaising_tbl_WorkMaster] FOREIGN KEY ([intWorklId]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);

