CREATE TABLE [dbo].[Beneficiary_API_Failure] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Household_Number] NVARCHAR (500) NULL,
    [Failure_Message]  NVARCHAR (200) NULL,
    [Create_Date]      DATETIME       DEFAULT (getdate()) NULL,
    [Transfer_Date]    DATETIME       NULL,
    [Ipid]             VARCHAR (30)   NULL
);

