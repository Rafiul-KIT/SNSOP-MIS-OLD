CREATE TABLE [dbo].[GroupMaster] (
    [GroupId]       INT           IDENTITY (1000, 1) NOT NULL,
    [State]         INT           NULL,
    [County]        INT           NULL,
    [Payam]         INT           NULL,
    [Boma]          INT           NULL,
    [GroupType]     VARCHAR (100) NULL,
    [GroupCode]     VARCHAR (100) NULL,
    [GroupStrength] INT           NOT NULL,
    [Created_by]    INT           NULL,
    [Created_dt]    DATETIME      NULL,
    [Modified_by]   INT           NULL,
    [Modified_dt]   DATETIME      NULL
);

