CREATE TABLE [dbo].[Beneficiary_API_Failure_Alternate] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Household_Number] NVARCHAR (300) NULL,
    [alternate_number] NVARCHAR (300) NULL,
    [name]             VARCHAR (180)  NULL,
    [Failure_Message]  NVARCHAR (200) NULL,
    [PullDate]         DATETIME       NULL,
    [Create_Date]      DATETIME       CONSTRAINT [DF__Beneficia__Creat__137E8B4D] DEFAULT (getdate()) NULL,
    [Transfer_Date]    DATETIME       NULL,
    [Ipid]             VARCHAR (30)   NULL,
    CONSTRAINT [PK_Beneficiary_API_Failure_Alternate] PRIMARY KEY CLUSTERED ([ID] ASC)
);

