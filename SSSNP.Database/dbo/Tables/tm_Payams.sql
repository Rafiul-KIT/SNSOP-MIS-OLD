CREATE TABLE [dbo].[tm_Payams] (
    [intPayamid]   INT          IDENTITY (1000, 1) NOT NULL,
    [intCountieid] INT          NULL,
    [PayamCode]    VARCHAR (5)  NULL,
    [PayamName]    VARCHAR (50) NULL,
    [Population]   INT          NULL,
    [Latitude]     VARCHAR (15) NULL,
    [Longitude]    VARCHAR (15) NULL,
    [NoofBomas]    INT          NULL,
    [Created_by]   INT          NULL,
    [Created_dt]   DATETIME     NULL,
    [Modified_dt]  DATETIME     NULL,
    [Modified_by]  INT          NULL,
    CONSTRAINT [PK_tm_Payams] PRIMARY KEY CLUSTERED ([intPayamid] ASC),
    CONSTRAINT [FK_tm_Payams_tm_Counties] FOREIGN KEY ([intCountieid]) REFERENCES [dbo].[tm_Counties] ([intCountieid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tm_Payams_intCountieidNew]
    ON [dbo].[tm_Payams]([intCountieid] ASC);

