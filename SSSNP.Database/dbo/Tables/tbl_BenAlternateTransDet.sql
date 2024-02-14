CREATE TABLE [dbo].[tbl_BenAlternateTransDet] (
    [intBenAlternateTransid]  INT           IDENTITY (1000, 1) NOT NULL,
    [intBenid]                INT           NULL,
    [AlternateType]           VARCHAR (40)  NULL,
    [household_number]        VARCHAR (300) NULL,
    [alternate_number]        VARCHAR (300) NULL,
    [NameAlternatePayee]      VARCHAR (100) NULL,
    [AlternatePayeeGender]    VARCHAR (1)   NULL,
    [AlternatePayeeAge]       INT           NULL,
    [AlternatePayeeNationID]  VARCHAR (20)  NULL,
    [AlternatePayeeMobile]    VARCHAR (12)  NULL,
    [AlternatePayeePhotoName] VARCHAR (200) NULL,
    [AlternatePayeePhotoPath] VARCHAR (200) NULL,
    [Created_by]              INT           NULL,
    [Created_dt]              DATETIME      NULL,
    [Modified_by]             INT           NULL,
    [Modified_dt]             DATETIME      NULL,
    CONSTRAINT [PK_tbl_BenAlternateTransDet] PRIMARY KEY CLUSTERED ([intBenAlternateTransid] ASC)
);

