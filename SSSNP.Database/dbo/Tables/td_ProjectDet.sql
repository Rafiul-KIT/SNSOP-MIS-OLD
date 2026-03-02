CREATE TABLE [dbo].[td_ProjectDet] (
    [intPrjid]          INT             IDENTITY (1000, 1) NOT NULL,
    [intIP]             INT             NOT NULL,
    [PrjName]           VARCHAR (50)    NOT NULL,
    [StartDate]         DATETIME        NULL,
    [EndDate]           DATETIME        NULL,
    [PrjSanctionDate]   DATETIME        NULL,
    [TargetBeneficiary] VARCHAR (50)    NULL,
    [ProjectCost]       DECIMAL (18, 2) NULL,
    [PrjInchargeName]   VARCHAR (50)    NULL,
    [PrjInchargeMobile] VARCHAR (15)    NULL,
    [PrjInchargeEmail]  VARCHAR (50)    NULL,
    [Status]            VARCHAR (50)    NULL,
    [Created_by]        INT             NULL,
    [Created_dt]        DATETIME        NULL,
    [Modified_by]       INT             NULL,
    [Modified_dt]       DATETIME        NULL,
    CONSTRAINT [PK_td_ProjectDet_1] PRIMARY KEY CLUSTERED ([intPrjid] ASC),
    CONSTRAINT [FK_td_ProjectDet_td_IPDetails] FOREIGN KEY ([intIP]) REFERENCES [dbo].[td_IPDetails] ([intIP])
);

