CREATE TABLE [dbo].[tbl_BeneficiaryDetNewComponent] (
    [intNewCompoId] INT            IDENTITY (1000, 1) NOT NULL,
    [intBenid]      INT            NULL,
    [Component]     NVARCHAR (200) NULL,
    [Start_date]    DATETIME       NULL,
    [End_Date]      DATETIME       NULL,
    [Created_by]    INT            NULL,
    [Created_dt]    DATETIME       NULL
);

