CREATE TABLE [dbo].[tr_BenWagesPaymentNotPaid] (
    [intWageid]         INT             IDENTITY (1000, 1) NOT NULL,
    [intBenid]          INT             NULL,
    [Year]              VARCHAR (5)     NULL,
    [Month]             VARCHAR (10)    NULL,
    [PaidDate]          DATETIME        NULL,
    [PaidAmount]        DECIMAL (18, 2) NULL,
    [Created_by]        INT             NULL,
    [Created_dt]        DATETIME        NULL,
    [Modified_by]       INT             NULL,
    [Modified_dt]       DATETIME        NULL,
    [Wagerate]          DECIMAL (18, 2) NULL,
    [WageRateUSD]       DECIMAL (18, 2) NULL,
    [PaidAmountUSD]     DECIMAL (18, 2) NULL,
    [FromDate]          DATETIME        NULL,
    [ToDate]            DATETIME        NULL,
    [intWorkid]         INT             NULL,
    [statusforUSD]      VARCHAR (5)     NULL,
    [ExchangeRate]      DECIMAL (18, 2) NULL,
    [WageYear]          INT             NULL,
    [SheetNo]           INT             NULL,
    [PortYear]          INT             NULL,
    [PortPhase]         INT             NULL,
    [PortedSheetName]   VARCHAR (180)   NULL,
    [AttendanceDays]    INT             NULL,
    [TempWageRateUSD]   DECIMAL (18, 2) NULL,
    [TempPaidAmountUSD] DECIMAL (18, 2) NULL,
    [PaidGenerateddate] DATETIME        NULL,
    [Paidto]            VARCHAR (500)   NULL,
    [trans_id]          VARCHAR (8)     NULL,
    [uuid]              VARCHAR (8)     NULL,
    CONSTRAINT [PK_tr_BenWagesPaymentNotPaid] PRIMARY KEY CLUSTERED ([intWageid] ASC),
    CONSTRAINT [FK_tr_BenWagesPaymentNotPaid_tbl_WorkMaster] FOREIGN KEY ([intWorkid]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPaymentNotPaid]
    ON [dbo].[tr_BenWagesPaymentNotPaid]([intBenid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPaymentNotPaid_1]
    ON [dbo].[tr_BenWagesPaymentNotPaid]([intWorkid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPaymentNotPaid_intBenidNew]
    ON [dbo].[tr_BenWagesPaymentNotPaid]([intBenid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPaymentNotPaid_uuidNew]
    ON [dbo].[tr_BenWagesPaymentNotPaid]([uuid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPaymentNotPaid_intWorkidNew]
    ON [dbo].[tr_BenWagesPaymentNotPaid]([intWorkid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPaymentNotPaid_FromDateNew]
    ON [dbo].[tr_BenWagesPaymentNotPaid]([FromDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPaymentNotPaid_ToDateNew]
    ON [dbo].[tr_BenWagesPaymentNotPaid]([ToDate] ASC);

