CREATE TABLE [dbo].[Beneficiary_API_Failure_Attedance] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Household_Number] NVARCHAR (300) NULL,
    [AttedanceDate]    DATETIME       NULL,
    [work_id]          VARCHAR (8)    NULL,
    [Failure_Message]  NVARCHAR (200) NULL,
    [PullDate]         DATETIME       NULL,
    [Create_Date]      DATETIME       CONSTRAINT [DF__Beneficia__Creat__174F1C31] DEFAULT (getdate()) NULL,
    [Transfer_Date]    DATETIME       NULL,
    [Ipid]             VARCHAR (30)   NULL,
    CONSTRAINT [PK_Beneficiary_API_Failure_Attedance] PRIMARY KEY CLUSTERED ([ID] ASC)
);

