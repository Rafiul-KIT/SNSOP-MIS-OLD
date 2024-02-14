CREATE TABLE [dbo].[tbl_PhysicalMonitoringDet] (
    [intPhyMonitoring] INT           IDENTITY (1000, 1) NOT NULL,
    [WorkId]           INT           NOT NULL,
    [DateofVisit]      DATETIME      NULL,
    [OutCome]          VARCHAR (50)  NULL,
    [Remarks]          VARCHAR (150) NULL,
    [Photo]            IMAGE         NULL,
    [PhotFilename]     VARCHAR (50)  NULL,
    [Photo2]           IMAGE         NULL,
    [PhotoFilename2]   VARCHAR (50)  NULL,
    [Photo3]           IMAGE         NULL,
    [Photo3Filename]   VARCHAR (50)  NULL,
    [Created_by]       INT           NULL,
    [Created_dt]       DATETIME      NULL,
    [Modified_by]      INT           NULL,
    [Modified_dt]      DATETIME      NULL,
    CONSTRAINT [PK_tbl_PhysicalMonitoringDet] PRIMARY KEY CLUSTERED ([intPhyMonitoring] ASC)
);

