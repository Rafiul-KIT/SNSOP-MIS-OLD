CREATE TABLE [dbo].[td_WageReqBenPADetTrans] (
    [intWagesReqBenPATransid] INT             IDENTITY (1000, 1) NOT NULL,
    [intWagePaymentReqPAid]   INT             NULL,
    [intWagesReqBenTransid]   INT             NULL,
    [intWagePaymentReqid]     INT             NULL,
    [intWorkid]               INT             NULL,
    [intBenid]                INT             NULL,
    [TotAttendance]           INT             NULL,
    [WageRateUSD]             DECIMAL (18, 2) NULL,
    [WageRateUSDTotal]        DECIMAL (18, 2) NULL,
    [WageRateSSP]             DECIMAL (18, 2) NULL,
    [WageRateSSPTotal]        DECIMAL (18, 2) NULL,
    [Status]                  VARCHAR (15)    NULL,
    [Created_by]              INT             NULL,
    [Created_dt]              DATETIME        NULL,
    [Modified_by]             INT             NULL,
    [Modified_dt]             DATETIME        NULL,
    CONSTRAINT [PK_td_WageReqBenPADetTrans] PRIMARY KEY CLUSTERED ([intWagesReqBenPATransid] ASC)
);

