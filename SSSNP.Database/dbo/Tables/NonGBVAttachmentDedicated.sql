CREATE TABLE [dbo].[NonGBVAttachmentDedicated]
(
    [NonGBVAttachmentID]        INT             IDENTITY (1, 1) NOT NULL,
    [NonGBVID]                  INT             NOT NULL,
    [Filename]                  NVARCHAR (255)  NOT NULL,
    [FileAttached]              VARBINARY (MAX) NOT NULL,
    [ContentType]               NVARCHAR (255)  NULL,
    [CreationTimestamp]         DATETIME        CONSTRAINT [DF_NonGBVAttachmentDedicated_CreationTimestamp] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]            INT             NULL,
    [LastModificationTimestamp] DATETIME        NULL,
    [LastModificationUserID]    INT             NULL,
    
    [Deleted]                   BIT             CONSTRAINT [DF_NonGBVAttachmentDedicated_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_NonGBVAttachmentDedicated] PRIMARY KEY CLUSTERED ([NonGBVAttachmentID] ASC),
    
);


