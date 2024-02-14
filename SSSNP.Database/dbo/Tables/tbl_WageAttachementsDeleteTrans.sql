CREATE TABLE [dbo].[tbl_WageAttachementsDeleteTrans] (
    [intImageTransId]     INT            IDENTITY (1000, 1) NOT NULL,
    [intImageId]          INT            NOT NULL,
    [intWagePaymentReqid] INT            NULL,
    [Description]         NVARCHAR (MAX) NULL,
    [PhotoFileName]       VARCHAR (200)  NULL,
    [PhotoFilePath]       NVARCHAR (MAX) NULL,
    [Created_by]          INT            CONSTRAINT [DF_tbl_WageAttachementsDeleteTrans_Created_by] DEFAULT ((1000)) NULL,
    [Created_dt]          DATETIME       CONSTRAINT [DF_tbl_WageAttachementsDeleteTrans_Created_dt] DEFAULT (getdate()) NULL,
    [Modified_by]         INT            NULL,
    [Modified_dt]         DATETIME       NULL,
    [Deleted_by]          INT            NULL,
    [DeleteDate]          DATETIME       NULL
);

