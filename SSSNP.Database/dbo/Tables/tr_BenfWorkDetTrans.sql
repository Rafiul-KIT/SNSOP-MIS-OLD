CREATE TABLE [dbo].[tr_BenfWorkDetTrans] (
    [intBenfWorkTransid] INT           IDENTITY (1000, 1) NOT NULL,
    [intBenfWorkid]      INT           NULL,
    [intBenid]           INT           NULL,
    [intWorkid]          INT           NULL,
    [Created_by]         INT           NULL,
    [Created_dt]         DATETIME      NULL,
    [Modified_by]        INT           NULL,
    [Modifieid_dt]       DATETIME      NULL,
    [PortYear]           INT           NULL,
    [PortPhase]          INT           NULL,
    [PortedSheetName]    VARCHAR (180) NULL,
    [Deleted_by]         INT           NULL,
    [Deleted_dt]         DATETIME      NULL,
    [DeletedRemarks]     VARCHAR (280) NULL,
    [DeletedStaffid]     INT           NULL,
    [DeletedStaffDt]     DATETIME      NULL,
    [InsertedStaffid]    INT           NULL,
    [InsertedStaffDt]    DATETIME      NULL,
    CONSTRAINT [PK_tr_BenfWorkDetTrans] PRIMARY KEY CLUSTERED ([intBenfWorkTransid] ASC)
);

