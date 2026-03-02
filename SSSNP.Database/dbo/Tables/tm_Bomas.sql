CREATE TABLE [dbo].[tm_Bomas] (
    [intBomasid]  INT          IDENTITY (1000, 1) NOT NULL,
    [intPayamid]  INT          NULL,
    [BomaCode]    VARCHAR (5)  NULL,
    [BomaName]    VARCHAR (50) NULL,
    [Population]  INT          NULL,
    [Latitude]    VARCHAR (15) NULL,
    [Longitude]   VARCHAR (15) NULL,
    [Created_by]  INT          NULL,
    [Created_dt]  DATETIME     NULL,
    [Modified_by] INT          NULL,
    [Modified_dt] DATETIME     NULL,
    CONSTRAINT [PK_tm_Bomas] PRIMARY KEY CLUSTERED ([intBomasid] ASC),
    CONSTRAINT [FK_tm_Bomas_tm_Payams] FOREIGN KEY ([intPayamid]) REFERENCES [dbo].[tm_Payams] ([intPayamid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tm_Bomas_intPayamidNew]
    ON [dbo].[tm_Bomas]([intPayamid] ASC);

