CREATE TABLE [dbo].[td_IPDetails] (
    [intIP]                     INT           IDENTITY (1000, 1) NOT NULL,
    [intTST]                    INT           NULL,
    [IPName]                    VARCHAR (70)  NOT NULL,
    [TypeofIP]                  VARCHAR (1)   NULL,
    [AuthorisedPerson]          VARCHAR (50)  NULL,
    [ContactNo]                 VARCHAR (15)  NULL,
    [SecondaryContactNo]        VARCHAR (15)  NULL,
    [Email]                     VARCHAR (50)  NULL,
    [Address]                   VARCHAR (150) NULL,
    [Website]                   VARCHAR (50)  NULL,
    [SecondaryAuthorisedPerson] VARCHAR (50)  NULL,
    [SEmail]                    VARCHAR (50)  NULL,
    [SPerson]                   VARCHAR (50)  NULL,
    [Created_by]                INT           NULL,
    [Created_dt]                DATETIME      NULL,
    [Modified_by]               INT           NULL,
    [Modified_dt]               DATETIME      NULL,
    CONSTRAINT [PK_td_IPDetails] PRIMARY KEY CLUSTERED ([intIP] ASC)
);

