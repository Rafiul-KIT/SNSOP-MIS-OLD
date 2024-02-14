CREATE TABLE [dbo].[td_TstTeamDet] (
    [intTST]        INT           IDENTITY (1000, 1) NOT NULL,
    [Ministry]      VARCHAR (15)  NOT NULL,
    [TstName]       VARCHAR (100) NOT NULL,
    [intStateid]    INT           NULL,
    [intCountieid]  INT           NULL,
    [intPayamid]    INT           NULL,
    [Address]       VARCHAR (150) NULL,
    [Email]         VARCHAR (50)  NOT NULL,
    [ContactNo]     VARCHAR (15)  NULL,
    [Type]          VARCHAR (1)   NULL,
    [intDesgid]     INT           NULL,
    [Status]        VARCHAR (50)  NULL,
    [PhotoUpload]   IMAGE         NULL,
    [PhotoFilename] VARCHAR (50)  NULL,
    [Created_by]    INT           NULL,
    [Created_dt]    DATETIME      NULL,
    [Modified_by]   INT           NULL,
    [Modified_dt]   DATETIME      NULL,
    CONSTRAINT [PK_td_TstTeamDet] PRIMARY KEY CLUSTERED ([intTST] ASC),
    CONSTRAINT [FK_td_TstTeamDet_tm_States] FOREIGN KEY ([intStateid]) REFERENCES [dbo].[tm_States] ([intStateid])
);

