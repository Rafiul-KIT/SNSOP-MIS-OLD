CREATE TABLE [dbo].[GBVDetails] (
    [GBVDetailID]                      INT            IDENTITY (1, 1) NOT NULL,
    [GBVMainID]                        INT            NOT NULL,
    [DescriptionOfTheIncident]         NVARCHAR (MAX) NULL,
    [ReportedToManagementHR]           BIT            NULL,
    [ReportedDetails]                  NVARCHAR(MAX)  NULL,
    [FollowUpActionFeedback]           NVARCHAR (MAX) NOT NULL,
    [IsSurvivorSatisfied]              BIT            NULL,
    [SurvivorDetails]                  NVARCHAR(MAX)  NULL,
    [ClosingDateAfterManagementAction] DATETIME       NULL,
    [CreationTimestamp]                DATETIME       CONSTRAINT [DF_GBVDetails_CreationTimestamp] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]                   INT            NULL,
    [LastModificationTimestamp]        DATETIME       NULL,
    [LastModificationUserID]           INT            NULL,
    [Deleted]                          BIT            CONSTRAINT [DF_GBVDetails_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_GBVDetails] PRIMARY KEY CLUSTERED ([GBVDetailID] ASC),
    CONSTRAINT [FK_GBVDetails_GBVMain] FOREIGN KEY ([GBVMainID]) REFERENCES [dbo].[GBVMain] ([GBVMainID])
);

