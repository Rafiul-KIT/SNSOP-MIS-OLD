CREATE TABLE [dbo].[tr_BenfingerPrintDet] (
    [intFingerPrintid]   INT           IDENTITY (1, 1) NOT NULL,
    [household_number]   VARCHAR (500) NULL,
    [beneficiary_type]   VARCHAR (8)   NULL,
    [fingerprint_number] VARCHAR (8)   NULL,
    [fingerprint]        VARCHAR (MAX) NULL,
    [Created_by]         INT           CONSTRAINT [DF_tr_BenfingerPrintDet_Created_by] DEFAULT ((1000)) NULL,
    [Created_dt]         DATETIME      CONSTRAINT [DF_tr_BenfingerPrintDet_Created_dt] DEFAULT (getdate()) NULL,
    [Modified_by]        INT           NULL,
    [Modified_dt]        DATETIME      NULL,
    CONSTRAINT [PK_tr_BenfingerPrintDet] PRIMARY KEY CLUSTERED ([intFingerPrintid] ASC)
);

