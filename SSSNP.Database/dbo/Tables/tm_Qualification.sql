CREATE TABLE [dbo].[tm_Qualification] (
    [intQualification]  INT          IDENTITY (1000, 1) NOT NULL,
    [QualificationName] VARCHAR (50) NOT NULL,
    [Created_by]        INT          NULL,
    [Created_dt]        DATETIME     NULL,
    [Modified_by]       INT          NULL,
    [Modified_dt]       DATETIME     NULL,
    CONSTRAINT [PK_tm_Qualification] PRIMARY KEY CLUSTERED ([intQualification] ASC)
);

