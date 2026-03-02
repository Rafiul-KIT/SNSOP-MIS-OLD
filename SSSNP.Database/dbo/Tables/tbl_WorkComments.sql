CREATE TABLE [dbo].[tbl_WorkComments] (
    [intWorkCommentId] INT           IDENTITY (1000, 1) NOT NULL,
    [intWorkid]        INT           NOT NULL,
    [Comment]          VARCHAR (200) NULL,
    [SetStatus]        VARCHAR (25)  NULL,
    [Created_by]       INT           NOT NULL,
    [Created_dt]       DATETIME      NOT NULL,
    CONSTRAINT [PK_tbl_WorkComments] PRIMARY KEY CLUSTERED ([intWorkCommentId] ASC),
    CONSTRAINT [FK_tbl_WorkComments_tbl_WorkMaster] FOREIGN KEY ([intWorkid]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);

