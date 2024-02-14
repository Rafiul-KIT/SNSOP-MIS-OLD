CREATE TABLE [dbo].[tm_fbType] (
    [intfb_id]    INT          IDENTITY (1000, 1) NOT NULL,
    [Fb_Type]     VARCHAR (50) NOT NULL,
    [Fb_Status]   CHAR (1)     NOT NULL,
    [Created_by]  INT          NOT NULL,
    [Created_dt]  DATETIME     NOT NULL,
    [Modified_by] INT          NULL,
    [Modified_dt] DATETIME     NULL
);

