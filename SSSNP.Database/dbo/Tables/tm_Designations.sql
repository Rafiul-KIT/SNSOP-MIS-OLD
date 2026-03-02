CREATE TABLE [dbo].[tm_Designations] (
    [intDesgid]   INT          IDENTITY (1000, 1) NOT NULL,
    [Type]        VARCHAR (1)  NULL,
    [Designation] VARCHAR (50) NULL,
    [Created_by]  INT          NULL,
    [Created_dt]  DATETIME     NULL,
    [Modified_by] INT          NULL,
    [Modified_dt] DATETIME     NULL,
    CONSTRAINT [PK_tm_Designations] PRIMARY KEY CLUSTERED ([intDesgid] ASC)
);

