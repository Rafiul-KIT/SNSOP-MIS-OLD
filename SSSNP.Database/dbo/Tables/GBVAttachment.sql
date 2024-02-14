CREATE TABLE [dbo].[GBVAttachment] (
    [GBVAttachmentID]           INT             IDENTITY (1, 1) NOT NULL,
    [GBVMainID]                 INT             NOT NULL,
    [Filename]                  NVARCHAR (255)  NOT NULL,
    [FileAttached]              VARBINARY (MAX) NOT NULL,
    [ContentType]               NVARCHAR (255)  NULL,
    [CreationTimestamp]         DATETIME        CONSTRAINT [DF_GBVAttachment_CreationTimestamp] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]            INT             NULL,
    [LastModificationTimestamp] DATETIME        NULL,
    [LastModificationUserID]    INT             NULL,
    [Deleted]                   BIT             CONSTRAINT [DF_GBVAttachment_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_GBVAttachment] PRIMARY KEY CLUSTERED ([GBVAttachmentID] ASC),
    CONSTRAINT [FK_GBVAttachment_GBVMain] FOREIGN KEY ([GBVMainID]) REFERENCES [dbo].[GBVMain] ([GBVMainID])
);

