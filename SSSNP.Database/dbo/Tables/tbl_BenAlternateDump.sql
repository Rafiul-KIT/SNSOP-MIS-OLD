CREATE TABLE [dbo].[tbl_BenAlternateDump] (
    [intTransid]       INT           IDENTITY (1, 1) NOT NULL,
    [id]               INT           NULL,
    [supervisor_id]    VARCHAR (8)   NULL,
    [household_number] VARCHAR (240) NULL,
    [alternate_number] VARCHAR (240) NULL,
    [alternate_name]   VARCHAR (180) NULL,
    [age]              VARCHAR (8)   NULL,
    [id_number]        VARCHAR (15)  NULL,
    [phone_number]     VARCHAR (15)  NULL,
    [gender]           VARCHAR (15)  NULL,
    [status]           VARCHAR (8)   NULL,
    [created_at]       DATETIME      NULL,
    [updated_at]       DATETIME      NULL,
    [stored_at]        DATETIME      NULL,
    [photo_url]        VARCHAR (180) NULL,
    CONSTRAINT [PK_tbl_BenAlternateDump] PRIMARY KEY CLUSTERED ([intTransid] ASC)
);

