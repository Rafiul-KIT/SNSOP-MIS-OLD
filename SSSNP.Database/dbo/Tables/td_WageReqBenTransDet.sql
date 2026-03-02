CREATE TABLE [dbo].[td_WageReqBenTransDet] (
    [intWagesReqBenTransDelid] INT             IDENTITY (1000, 1) NOT NULL,
    [intWagesReqBenTransid]    INT             NULL,
    [intWagePaymentReqid]      INT             NULL,
    [intWorkid]                INT             NULL,
    [intBenid]                 INT             NULL,
    [TotAttendance]            INT             NULL,
    [WageRateUSD]              DECIMAL (18, 2) NULL,
    [WageRateUSDTotal]         DECIMAL (18, 2) NULL,
    [traineealternate]         VARCHAR (100)   NULL,
    [Status]                   VARCHAR (15)    NULL,
    [Created_by]               INT             NULL,
    [Created_dt]               DATETIME        NULL,
    [Modified_by]              INT             NULL,
    [Modified_dt]              DATETIME        NULL,
    [Deleted_dt]               DATETIME        NULL,
    [DeletedRemarks]           VARCHAR (180)   NULL,
    [InsertedStaffid]          INT             NULL,
    [InsertedStaffDt]          DATETIME        NULL,
    [DeletedStaffid]           INT             NULL,
    [DeletedStaffDt]           DATETIME        NULL,
    CONSTRAINT [PK_td_WageReqBenTransDet] PRIMARY KEY CLUSTERED ([intWagesReqBenTransDelid] ASC)
);

