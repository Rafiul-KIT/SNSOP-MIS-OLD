CREATE TABLE [dbo].[GBVAttachmentDedicated]
(
    [GBVAttachmentID]        INT             IDENTITY (1, 1) NOT NULL,
    [GBVID]                  INT             NOT NULL,
    [Filename]                  NVARCHAR (255)  NOT NULL,
    [FileAttached]              VARBINARY (MAX) NOT NULL,
    [ContentType]               NVARCHAR (255)  NULL,
    [CreationTimestamp]         DATETIME        CONSTRAINT [DF_GBVAttachmentDedicated_CreationTimestamp] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]            INT             NULL,
    [LastModificationTimestamp] DATETIME        NULL,
    [LastModificationUserID]    INT             NULL,
    
    [Deleted]                   BIT             CONSTRAINT [DF_GBVAttachmentDedicated_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_GBVAttachmentDedicated] PRIMARY KEY CLUSTERED ([GBVAttachmentID] ASC),
    
);


