CREATE TABLE [dbo].[tm_WorkActivities] (
    [intWorkActivity] INT           IDENTITY (1000, 1) NOT NULL,
    [intAreaofWork]   INT           NOT NULL,
    [WorkActivity]    VARCHAR (150) NOT NULL,
    [Created_by]      INT           NULL,
    [Created_dt]      DATETIME      NULL,
    [Modified_by]     INT           NULL,
    [Modified_dt]     DATETIME      NULL,
    CONSTRAINT [PK_tm_WorkActivities] PRIMARY KEY CLUSTERED ([intWorkActivity] ASC)
);

