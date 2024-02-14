CREATE TABLE [dbo].[Beneficiary_API_Failure_Payments] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Household_Number] NVARCHAR (300) NULL,
    [work_id]          VARCHAR (8)    NULL,
    [trans_id]         VARCHAR (8)    NULL,
    [uuid]             VARCHAR (8)    NULL,
    [Failure_Message]  NVARCHAR (200) NULL,
    [PullDate]         DATETIME       NULL,
    [Create_Date]      DATETIME       CONSTRAINT [DF__Beneficia__Creat__1566D3BF] DEFAULT (getdate()) NULL,
    [Transfer_Date]    DATETIME       NULL,
    [Ipid]             VARCHAR (30)   NULL,
    [paid_to]          VARCHAR (300)  NULL,
    CONSTRAINT [PK_Beneficiary_API_Failure_Payments] PRIMARY KEY CLUSTERED ([ID] ASC)
);

