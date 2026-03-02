CREATE TABLE [dbo].[tm_Counties] (
    [intCountieid] INT          IDENTITY (1000, 1) NOT NULL,
    [intStateid]   INT          NOT NULL,
    [CountieCode]  VARCHAR (5)  NULL,
    [CountieName]  VARCHAR (50) NOT NULL,
    [Population]   INT          NULL,
    [Latitude]     VARCHAR (15) NULL,
    [Longitude]    VARCHAR (15) NULL,
    [NoofBlocks]   INT          NULL,
    [Created_by]   INT          NULL,
    [Created_dt]   DATETIME     NULL,
    [Modified_by]  INT          NULL,
    [Modified_dt]  DATETIME     NULL,
    CONSTRAINT [PK_tm_Counties] PRIMARY KEY CLUSTERED ([intCountieid] ASC),
    CONSTRAINT [FK_tm_Counties_tm_States] FOREIGN KEY ([intStateid]) REFERENCES [dbo].[tm_States] ([intStateid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tm_Counties_intStateidNew]
    ON [dbo].[tm_Counties]([intStateid] ASC);

