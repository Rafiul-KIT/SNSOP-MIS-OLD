CREATE TABLE [dbo].[tr_CouncilMemDet] (
    [intMemid]         INT          IDENTITY (1000, 1) NOT NULL,
    [MemType]          VARCHAR (5)  NULL,
    [intCPBid]         INT          NULL,
    [AuthorizedPerson] VARCHAR (70) NULL,
    [Designation]      VARCHAR (50) NULL,
    [MobileNo]         VARCHAR (15) NULL,
    [Email]            VARCHAR (50) NULL,
    [Gender]           VARCHAR (1)  NULL,
    [Created_by]       INT          NULL,
    [Created_dt]       DATETIME     NULL,
    [Modified_by]      INT          NULL,
    CONSTRAINT [PK_tr_CouncilMemDet] PRIMARY KEY CLUSTERED ([intMemid] ASC)
);

