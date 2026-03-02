CREATE TABLE [dbo].[tbl_WageAttachements] (
    [intImageId]          INT            IDENTITY (1000, 1) NOT NULL,
    [intWagePaymentReqid] INT            NULL,
    [Description]         NVARCHAR (MAX) NULL,
    [PhotoFileName]       VARCHAR (200)  NULL,
    [PhotoFilePath]       NVARCHAR (MAX) NULL,
    [Created_by]          INT            CONSTRAINT [DF_tbl_WageAttachements_Created_by] DEFAULT ((1000)) NULL,
    [Created_dt]          DATETIME       CONSTRAINT [DF_tbl_WageAttachements_Created_dt] DEFAULT (getdate()) NULL,
    [Modified_by]         INT            NULL,
    [Modified_dt]         DATETIME       NULL,
    CONSTRAINT [PK_tbl_WageAttachements] PRIMARY KEY CLUSTERED ([intImageId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_WageAttachements]
    ON [dbo].[tbl_WageAttachements]([intWagePaymentReqid] ASC);

