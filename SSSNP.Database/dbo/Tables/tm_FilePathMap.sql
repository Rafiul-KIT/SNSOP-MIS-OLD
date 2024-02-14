CREATE TABLE [dbo].[tm_FilePathMap] (
    [intFilePaths]   INT           IDENTITY (1000, 1) NOT NULL,
    [FilePaths]      VARCHAR (380) NULL,
    [FileServerPath] VARCHAR (380) NULL,
    [BiomatricPath]  VARCHAR (380) NULL,
    CONSTRAINT [PK_tm_FilePathMap] PRIMARY KEY CLUSTERED ([intFilePaths] ASC)
);

