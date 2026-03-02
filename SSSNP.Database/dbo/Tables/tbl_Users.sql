CREATE TABLE [dbo].[tbl_Users] (
    [intUserid]   INT           IDENTITY (1000, 1) NOT NULL,
    [User_id]     VARCHAR (50)  NULL,
    [Password]    VARCHAR (50)  NULL,
    [User_name]   VARCHAR (100) NULL,
    [User_level]  VARCHAR (2)   NULL,
    [User_type]   VARCHAR (10)  NULL,
    [User_code]   VARCHAR (10)  NULL,
    [Status]      VARCHAR (1)   NULL,
    [PView]       VARCHAR (1)   NULL,
    [PAccess]     VARCHAR (1)   NULL,
    [Pwrite]      VARCHAR (1)   NULL,
    [Created_by]  INT           NULL,
    [Created_dt]  DATETIME      NULL,
    [Modified_by] INT           NULL,
    [Modified_dt] DATETIME      NULL,
    [Email]       VARCHAR (150) NULL,
    [Role]        VARCHAR (100) NULL,
    [GBVUser]     BIT           NULL,
    [IP_Approver] BIT           DEFAULT (NULL) NULL,
    CONSTRAINT [PK_tbl_Users] PRIMARY KEY CLUSTERED ([intUserid] ASC)
);



