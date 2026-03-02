CREATE TABLE [dbo].[tr_BenWagesPayment] (
    [intWageid]         INT             IDENTITY (1000, 1) NOT NULL,
    [intBenid]          INT             NULL,
    [Year]              VARCHAR (5)     NULL,
    [Month]             VARCHAR (10)    NULL,
    [PaidDate]          DATETIME        NULL,
    [PaidAmount]        DECIMAL (18, 4) NULL,
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
    [ExchangeRate]      DECIMAL (18, 4) NULL,
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
    [matched_fp]        VARCHAR (MAX)   NULL,
    [upd]               VARCHAR (1)     NULL,
    [latitude]          VARCHAR (30)    NULL,
    [longitude]         VARCHAR (30)    NULL,
    [supervisor_id]     INT             NULL,
    [paidtoname]        VARCHAR (240)   NULL,
    CONSTRAINT [PK_tr_BenWagesPayment] PRIMARY KEY CLUSTERED ([intWageid] ASC),
    CONSTRAINT [FK_tr_BenWagesPayment_tbl_BeneficiaryDet] FOREIGN KEY ([intBenid]) REFERENCES [dbo].[tbl_BeneficiaryDet] ([intBenid]),
    CONSTRAINT [FK_tr_BenWagesPayment_tbl_WorkMaster] FOREIGN KEY ([intWorkid]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPayment]
    ON [dbo].[tr_BenWagesPayment]([intBenid] ASC);


GO
CREATE NONCLUSTERED INDEX [tr_BenWagesPaymentintWorkid]
    ON [dbo].[tr_BenWagesPayment]([intWorkid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPayment_intBenidNew]
    ON [dbo].[tr_BenWagesPayment]([intBenid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPayment_uuidNew]
    ON [dbo].[tr_BenWagesPayment]([uuid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPayment_intWorkidNew]
    ON [dbo].[tr_BenWagesPayment]([intWorkid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPayment_FromDateNew]
    ON [dbo].[tr_BenWagesPayment]([FromDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenWagesPayment_ToDateNew]
    ON [dbo].[tr_BenWagesPayment]([ToDate] ASC);

