CREATE TABLE [dbo].[LK_GBVStatus] (
    [StatusID]     INT            IDENTITY (1, 1) NOT NULL,
    [StatusName]   NVARCHAR (100) NOT NULL,
    [GBVStatus]    BIT            NOT NULL,
    [NonGBVStatus] BIT            NOT NULL,
    CONSTRAINT [PK_LK_GBVStatus] PRIMARY KEY CLUSTERED ([StatusID] ASC)
);

