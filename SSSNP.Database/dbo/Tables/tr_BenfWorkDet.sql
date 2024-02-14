CREATE TABLE [dbo].[tr_BenfWorkDet] (
    [intBenfWorkid]   INT           IDENTITY (1000, 1) NOT NULL,
    [intBenid]        INT           NULL,
    [intWorkid]       INT           NULL,
    [Created_by]      INT           NULL,
    [Created_dt]      DATETIME      NULL,
    [Modified_by]     INT           NULL,
    [Modifieid_dt]    DATETIME      NULL,
    [PortYear]        INT           NULL,
    [PortPhase]       INT           NULL,
    [PortedSheetName] VARCHAR (180) NULL,
    CONSTRAINT [PK_tr_BenfWorkDet] PRIMARY KEY CLUSTERED ([intBenfWorkid] ASC),
    CONSTRAINT [FK_tr_BenfWorkDet_tbl_BeneficiaryDet] FOREIGN KEY ([intBenid]) REFERENCES [dbo].[tbl_BeneficiaryDet] ([intBenid]),
    CONSTRAINT [FK_tr_BenfWorkDet_tbl_WorkMaster] FOREIGN KEY ([intWorkid]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenfWorkDet]
    ON [dbo].[tr_BenfWorkDet]([intBenid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenfWorkDet_1]
    ON [dbo].[tr_BenfWorkDet]([intWorkid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenfWorkDet_intBenidNew]
    ON [dbo].[tr_BenfWorkDet]([intBenid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tr_BenfWorkDet_intWorkidNew]
    ON [dbo].[tr_BenfWorkDet]([intWorkid] ASC);

