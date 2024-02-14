CREATE TABLE [dbo].[GBVImportedFiles] (
    [ImportedID]          INT             IDENTITY (1, 1) NOT NULL,
    [CreationTimestamp]   DATETIME        CONSTRAINT [DF_GBVImportedFiles_CreationTimestamp] DEFAULT (getdate()) NOT NULL,
    [UserID]              INT             NOT NULL,
    [SourceFilename]      NVARCHAR (255)  NOT NULL,
    [UploadedFile]        VARBINARY (MAX) NOT NULL,
    [DestinationFilename] NVARCHAR (255)  NOT NULL,
    [GBVType]             NVARCHAR (10)   NOT NULL,
    [Deleted]             BIT             CONSTRAINT [DF_GBVImportedFiles_Deleted] DEFAULT ((0)) NOT NULL,
    [GBVImported]         BIT             CONSTRAINT [DF_GBVImportedFiles_Imported] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_GBVImportedFiles] PRIMARY KEY CLUSTERED ([ImportedID] ASC)
);

