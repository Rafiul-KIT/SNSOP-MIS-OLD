CREATE TABLE [dbo].[td_ExchangeRateDet] (
    [intExachageid] INT             IDENTITY (1000, 1) NOT NULL,
    [AppliedDate]   DATETIME        CONSTRAINT [DF_td_ExchangeRateDet_AppliedDate] DEFAULT (getdate()) NULL,
    [ExchangeRate]  DECIMAL (18, 4) NULL,
    [Created_by]    INT             CONSTRAINT [DF_td_ExchangeRateDet_Created_by] DEFAULT ((1000)) NULL,
    [Created_dt]    DATETIME        CONSTRAINT [DF_td_ExchangeRateDet_Created_dt] DEFAULT (getdate()) NULL,
    [Modified_by]   INT             NULL,
    [Modified_dt]   DATETIME        NULL,
    [intStateid]    INT             NULL,
    [intCountyid]   INT             NULL,
    [WageRate]      DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_td_ExchangeRateDet] PRIMARY KEY CLUSTERED ([intExachageid] ASC)
);

