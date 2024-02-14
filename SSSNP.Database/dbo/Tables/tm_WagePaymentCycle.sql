CREATE TABLE [dbo].[tm_WagePaymentCycle] (
    [intPaymentCycleID] INT          IDENTITY (1, 1) NOT NULL,
    [WagePaymentCycle]  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tm_WagePaymentCycle] PRIMARY KEY CLUSTERED ([intPaymentCycleID] ASC)
);

