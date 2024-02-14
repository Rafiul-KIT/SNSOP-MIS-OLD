CREATE TABLE [dbo].[NonGBVAttachment] (
    [NonGBVAttachmentID]        INT             IDENTITY (1, 1) NOT NULL,
    [NonGBVDetailID]            INT             NOT NULL,
    [Filename]                  NVARCHAR (255)  NOT NULL,
    [FileAttached]              VARBINARY (MAX) NOT NULL,
    [ContentType]               NVARCHAR (255)  NULL,
    [CreationTimestamp]         DATETIME        CONSTRAINT [DF_NonGBVAttachment_CreationTimestamp] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]            INT             NULL,
    [LastModificationTimestamp] DATETIME        NULL,
    [LastModificationUserID]    INT             NULL,
    [Deleted]                   BIT             CONSTRAINT [DF_NonGBVAttachment_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_NonGBVAttachment] PRIMARY KEY CLUSTERED ([NonGBVAttachmentID] ASC),
    CONSTRAINT [FK_NonGBVAttachment_NonGBVDetails] FOREIGN KEY ([NonGBVDetailID]) REFERENCES [dbo].[NonGBVDetails] ([NonGBVDetailID])
);

