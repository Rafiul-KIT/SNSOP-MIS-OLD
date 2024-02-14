CREATE TABLE [dbo].[tm_States] (
    [intStateid]   INT          IDENTITY (1000, 1) NOT NULL,
    [StateName]    VARCHAR (50) NOT NULL,
    [StateCode]    VARCHAR (5)  NULL,
    [Latitude]     VARCHAR (15) NULL,
    [Longitude]    VARCHAR (15) NULL,
    [Population]   INT          NULL,
    [NoofCounties] INT          NULL,
    [Created_by]   INT          NULL,
    [Created_dt]   DATETIME     NULL,
    [Modified_by]  INT          NULL,
    [Modified_dt]  DATETIME     NULL,
    CONSTRAINT [PK_tm_States] PRIMARY KEY CLUSTERED ([intStateid] ASC)
);

