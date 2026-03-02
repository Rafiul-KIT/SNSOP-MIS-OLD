CREATE TABLE [dbo].[tr_ProjectTarget] (
    [intPrjtarget]     INT          IDENTITY (1000, 1) NOT NULL,
    [intPrjid]         INT          NULL,
    [intStateid]       INT          NULL,
    [intCountieid]     INT          NULL,
    [intPayamid]       INT          NULL,
    [intBomasid]       INT          NULL,
    [Proposedtarget]   VARCHAR (50) NULL,
    [intAreaofWork]    INT          NULL,
    [intWorkActivity]  INT          NULL,
    [Created_by]       INT          NULL,
    [Created_dt]       DATETIME     NULL,
    [Modified_by]      INT          NULL,
    [Modified_dt]      DATETIME     NULL,
    [EquipmntQuantity] INT          NULL,
    [MaterialQuantity] INT          NULL,
    CONSTRAINT [PK_tr_ProjectTarget] PRIMARY KEY CLUSTERED ([intPrjtarget] ASC)
);

