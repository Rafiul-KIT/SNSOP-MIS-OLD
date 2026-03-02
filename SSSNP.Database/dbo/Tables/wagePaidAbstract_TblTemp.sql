CREATE TABLE [dbo].[wagePaidAbstract_TblTemp] (
    [IPName]        VARCHAR (40)    NULL,
    [intIP]         INT             NULL,
    [StateName]     VARCHAR (40)    NULL,
    [CountieName]   VARCHAR (40)    NULL,
    [PayamName]     VARCHAR (40)    NULL,
    [BomaName]      VARCHAR (40)    NULL,
    [WorkCode]      VARCHAR (80)    NULL,
    [FromDate]      VARCHAR (11)    NULL,
    [ToDate]        VARCHAR (11)    NULL,
    [TotalMembers]  INT             NULL,
    [TotalWorkDays] INT             NULL,
    [AmountPaidUSD] DECIMAL (18, 2) NULL,
    [AmountPaidSSP] DECIMAL (18, 2) NULL,
    [PaymentDate]   VARCHAR (11)    NULL,
    [PaymentCycle]  VARCHAR (50)    NULL,
    [intStateid]    INT             NULL,
    [intCountieid]  INT             NULL,
    [intPayamId]    INT             NULL,
    [intBomasId]    INT             NULL
);

