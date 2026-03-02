CREATE TABLE [dbo].[td_WageReqBenDet] (
    [intWagesReqBenTransid] INT             IDENTITY (1000, 1) NOT NULL,
    [intWagePaymentReqid]   INT             NULL,
    [intWorkid]             INT             NULL,
    [intBenid]              INT             NULL,
    [TotAttendance]         INT             NULL,
    [WageRateUSD]           DECIMAL (18, 2) NULL,
    [WageRateUSDTotal]      DECIMAL (18, 2) NULL,
    [traineealternate]      VARCHAR (8)     NULL,
    [Status]                VARCHAR (15)    NULL,
    [Created_by]            INT             NULL,
    [Created_dt]            DATETIME        NULL,
    [Modified_by]           INT             NULL,
    [Modified_dt]           DATETIME        NULL,
    [ExchangeRateOLD]       DECIMAL (18, 4) NULL,
    [ExchangeRateNew]       DECIMAL (18, 4) NULL,
    [CheckA]                INT             NULL,
    [ReInitiate]            VARCHAR (1)     NULL,
    [AmountInSSP]           AS              ([WageRateUSDTotal]*isnull([ExchangeRateNew],(0))),
    [cashP_FLI]             BIT             NULL,
    [cashP_ECD]             BIT             NULL,
    [cashP_WASh]            BIT             NULL,
    CONSTRAINT [PK_td_WageReqBenDet] PRIMARY KEY CLUSTERED ([intWagesReqBenTransid] ASC),
    CONSTRAINT [FK_td_WageReqBenDet_tbl_BeneficiaryDet] FOREIGN KEY ([intBenid]) REFERENCES [dbo].[tbl_BeneficiaryDet] ([intBenid]),
    CONSTRAINT [FK_td_WageReqBenDet_tbl_WorkMaster] FOREIGN KEY ([intWorkid]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid]),
    CONSTRAINT [FK_td_WageReqBenDet_td_WorkWageRequest] FOREIGN KEY ([intWagePaymentReqid]) REFERENCES [dbo].[td_WorkWageRequest] ([intWagePaymentReqid])
);




GO
CREATE NONCLUSTERED INDEX [IX_td_WageReqBenDet_intWagePaymentReqidNew]
    ON [dbo].[td_WageReqBenDet]([intWagePaymentReqid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_td_WageReqBenDet_intWorkidNew]
    ON [dbo].[td_WageReqBenDet]([intWorkid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_td_WageReqBenDet_intBenidNew]
    ON [dbo].[td_WageReqBenDet]([intBenid] ASC);

