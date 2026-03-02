CREATE TABLE [dbo].[tbl_Attachements] (
    [intImageId]           INT            IDENTITY (1000, 1) NOT NULL,
    [Type]                 VARCHAR (50)   NOT NULL,
    [intTypeId]            INT            NOT NULL,
    [PhotoFileName]        VARCHAR (50)   NULL,
    [Created_by]           INT            NULL,
    [Created_dt]           DATETIME       NULL,
    [Modified_by]          INT            NULL,
    [Modified_dt]          DATETIME       NULL,
    [File_Path]            NVARCHAR (500) NULL,
    [IntWorkDetailsTypeId] INT            NULL
);

