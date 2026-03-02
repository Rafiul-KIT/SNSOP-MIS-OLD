CREATE TABLE [dbo].[AttachmentDraft] (
    [AttachmentID]              UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Filename]                  NVARCHAR (255)   NOT NULL,
    [FileAttached]              VARBINARY (MAX)  NOT NULL,
    [ContentType]               NVARCHAR (255)   NULL,
    [CreationTimestamp]         DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreationUserID]            INT              NULL,
    [LastModificationTimestamp] DATETIME         NULL,
    [LastModificationUserID]    INT              NULL,
    [Deleted]                   AS               (case when datediff(day,[CreationTimestamp],getdate())>(3) then (1) else (0) end),
    PRIMARY KEY CLUSTERED ([AttachmentID] ASC)
);


