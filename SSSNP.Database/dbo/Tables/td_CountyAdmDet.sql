CREATE TABLE [dbo].[td_CountyAdmDet] (
    [intCountyAdm]  INT           IDENTITY (1000, 1) NOT NULL,
    [CountyAdmName] VARCHAR (100) NOT NULL,
    [intStateid]    INT           NULL,
    [intCountieid]  INT           NULL,
    [Address]       VARCHAR (150) NULL,
    [Email]         VARCHAR (50)  NULL,
    [ContactNo]     VARCHAR (15)  NULL,
    [Created_by]    INT           NULL,
    [Created_dt]    DATETIME      NULL,
    [Modified_by]   INT           NULL,
    [Modified_dt]   DATETIME      NULL,
    CONSTRAINT [PK_td_CountyAdmDet] PRIMARY KEY CLUSTERED ([intCountyAdm] ASC)
);

