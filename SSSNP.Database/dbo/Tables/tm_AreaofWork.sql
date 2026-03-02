CREATE TABLE [dbo].[tm_AreaofWork] (
    [intAreaofWork] INT          IDENTITY (1000, 1) NOT NULL,
    [AreaofWork]    VARCHAR (70) NOT NULL,
    [Created_by]    INT          NULL,
    [Created_dt]    DATETIME     NULL,
    [Modified_by]   INT          NULL,
    [Modified_dt]   DATETIME     NULL,
    CONSTRAINT [PK_tm_AreaofWork] PRIMARY KEY CLUSTERED ([intAreaofWork] ASC)
);

