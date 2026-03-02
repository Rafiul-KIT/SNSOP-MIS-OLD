CREATE TABLE [dbo].[AuditLogs] (
    [AuditLogID]    UNIQUEIDENTIFIER CONSTRAINT [DF_AuditLogs_AuditLogID] DEFAULT (newid()) NOT NULL,
    [LogTimestamp]  DATETIME         CONSTRAINT [DF_Table_1_Timestamp] DEFAULT (getdate()) NOT NULL,
    [UserID]        NVARCHAR (255)   NULL,
    [pKey]          NVARCHAR (MAX)   NULL,
    [XMLObjectLog]  XML              NULL,
    [CommandMethod] NVARCHAR (255)   NULL,
    [CommandText]   NVARCHAR (255)   NULL,
    [OperationType] NVARCHAR (20)    NULL,
    CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED ([AuditLogID] ASC)
);

