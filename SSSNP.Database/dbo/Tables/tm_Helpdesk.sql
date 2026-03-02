CREATE TABLE [dbo].[tm_Helpdesk] (
    [int_fbid]       INT            IDENTITY (1000, 1) NOT NULL,
    [hd_Code]        VARCHAR (50)   NULL,
    [intPIAid]       INT            NOT NULL,
    [hd_Username]    VARCHAR (50)   NOT NULL,
    [intfb_id]       INT            NOT NULL,
    [hd_desc]        VARCHAR (2500) NULL,
    [hd_uplddocname] VARCHAR (50)   NULL,
    [hd_uplddoc]     IMAGE          NULL,
    [Status]         VARCHAR (10)   NULL,
    [User_Type]      VARCHAR (5)    NULL,
    [Remarks]        VARCHAR (250)  NULL,
    [Hd_from]        INT            NULL,
    [Hd_To]          INT            NULL,
    [Created_by]     INT            NOT NULL,
    [Created_dt]     DATETIME       NOT NULL,
    [Modified_by]    INT            NULL,
    [Modified_dt]    DATETIME       NULL,
    [anothermailid]  VARCHAR (50)   NULL
);

