CREATE TABLE [dbo].[Beneficiary_API_Failure_Beneficiary] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Household_Number] NVARCHAR (500) NULL,
    [NameofHousehold]  VARCHAR (180)  NULL,
    [Failure_Message]  NVARCHAR (200) NULL,
    [Create_Date]      DATETIME       CONSTRAINT [DF__BeneficiaBen__Creat__7721786A] DEFAULT (getdate()) NULL,
    [Transfer_Date]    DATETIME       NULL,
    [Ipid]             VARCHAR (30)   NULL,
    [intBomaid]        VARCHAR (8)    NULL,
    [PullDate]         DATETIME       NULL,
    CONSTRAINT [PK_Beneficiary_API_Failure_Beneficiary] PRIMARY KEY CLUSTERED ([ID] ASC)
);

