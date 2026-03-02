CREATE TABLE [dbo].[tm_WageStages] (
    [intWageStageid] INT          IDENTITY (1, 1) NOT NULL,
    [Stageid]        INT          NULL,
    [StageName]      VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tm_WageStages] PRIMARY KEY CLUSTERED ([intWageStageid] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_tm_WageStages_StageidNew]
    ON [dbo].[tm_WageStages]([Stageid] ASC);

