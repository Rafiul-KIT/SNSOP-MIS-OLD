CREATE TABLE [dbo].[tr_Helpdesk] (
    [inthd_trid]  INT            IDENTITY (1000, 1) NOT NULL,
    [int_fdid]    INT            NOT NULL,
    [Hd_from]     INT            NULL,
    [Hd_To]       INT            NULL,
    [Remarks]     VARCHAR (2500) NULL,
    [Status]      VARCHAR (10)   NULL,
    [Created_by]  INT            NULL,
    [Created_dt]  DATETIME       NULL,
    [Modified_by] INT            NULL,
    [Modifed_dt]  DATETIME       NULL
);

