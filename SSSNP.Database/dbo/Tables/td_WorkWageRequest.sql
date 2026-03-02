CREATE TABLE [dbo].[td_WorkWageRequest] (
    [intWagePaymentReqid] INT             IDENTITY (1000, 1) NOT NULL,
    [intWorkid]           INT             NULL,
    [WageRateUSD]         DECIMAL (18, 2) NULL,
    [ReqFromDate]         DATETIME        NULL,
    [ReqToDate]           DATETIME        NULL,
    [ReqYear]             VARCHAR (5)     NULL,
    [ReqMonth]            VARCHAR (10)    NULL,
    [intPayingAgentid]    INT             NULL,
    [intCoOrdinatorid]    INT             NULL,
    [intSpecialistid]     INT             NULL,
    [intManagerid]        INT             NULL,
    [Exchangerateattime]  DECIMAL (18, 4) NULL,
    [ReqNo]               VARCHAR (40)    NULL,
    [Status]              INT             NULL,
    [Remarks]             VARCHAR (350)   NULL,
    [Created_by]          INT             NULL,
    [Created_dt]          DATETIME        CONSTRAINT [DF_td_WorkWageRequest_Created_dt] DEFAULT (getdate()) NULL,
    [Modified_by]         INT             NULL,
    [Modified_dt]         DATETIME        NULL,
    [ExchangeRateOLD]     DECIMAL (18, 4) NULL,
    [CApproveDate]        DATETIME        NULL,
    [SApproveDate]        DATETIME        NULL,
    [MApproveDate]        DATETIME        NULL,
    [PaymentCycle]        INT             NULL,
    [AmountInSSP]         DECIMAL (18, 2) DEFAULT ((0)) NULL,
    [Component]           NVARCHAR (200)  NULL,
    CONSTRAINT [PK_td_WorkWageRequest] PRIMARY KEY CLUSTERED ([intWagePaymentReqid] ASC),
    CONSTRAINT [FK_td_WorkWageRequest_tbl_WorkMaster] FOREIGN KEY ([intWorkid]) REFERENCES [dbo].[tbl_WorkMaster] ([intWorkid])
);












GO
CREATE NONCLUSTERED INDEX [IX_td_WorkWageRequest_intWorkidNew]
    ON [dbo].[td_WorkWageRequest]([intWorkid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_td_WorkWageRequest_ReqFromDateNew]
    ON [dbo].[td_WorkWageRequest]([ReqFromDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_td_WorkWageRequest_ReqToDateNew]
    ON [dbo].[td_WorkWageRequest]([ReqToDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_td_WorkWageRequest_intCoOrdinatoridNew]
    ON [dbo].[td_WorkWageRequest]([intCoOrdinatorid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_td_WorkWageRequest_intSpecialistidNew]
    ON [dbo].[td_WorkWageRequest]([intSpecialistid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_td_WorkWageRequest_intManageridNew]
    ON [dbo].[td_WorkWageRequest]([intManagerid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_td_WorkWageRequest_intPayingAgentidNew]
    ON [dbo].[td_WorkWageRequest]([intPayingAgentid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_td_WorkWageRequest_ReqNoNew]
    ON [dbo].[td_WorkWageRequest]([ReqNo] ASC);


GO


CREATE TRIGGER [dbo].[reportWageStatusTrigger]
   ON  [dbo].[td_WorkWageRequest]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update WageResultDetails_tblTemp set Wagestatus=
	(select stagename from tm_WageStages a
	inner join 	inserted as b
	on b.status=a.stageid
	)
	where intWagePaymentReqid=
	(select a.intWagePaymentReqid from WageResultDetails_tblTemp a
	inner join 	inserted as b
	on b.intWagePaymentReqid=a.intWagePaymentReqid
	)
END