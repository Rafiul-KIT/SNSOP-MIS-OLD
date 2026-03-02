CREATE TABLE [dbo].[tm_Disability] (
    [intDisability] INT          IDENTITY (1000, 1) NOT NULL,
    [Disability]    VARCHAR (50) NOT NULL,
    [Created_by]    INT          NULL,
    [Created_dt]    DATETIME     NULL,
    [Modified_by]   INT          NULL,
    [Modified_dt]   DATETIME     NULL,
    CONSTRAINT [PK_tm_Disability] PRIMARY KEY CLUSTERED ([intDisability] ASC)
);

