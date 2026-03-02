CREATE TABLE [dbo].[td_AllocationDaysDet] (
    [intNoofDays]    INT      IDENTITY (1000, 1) NOT NULL,
    [Allocationdays] INT      NULL,
    [Created_by]     INT      CONSTRAINT [DF_td_AllocationDaysDet_Created_by] DEFAULT ((1000)) NULL,
    [Created_dt]     DATETIME CONSTRAINT [DF_td_AllocationDaysDet_Created_dt] DEFAULT (getdate()) NULL,
    [Modified_by]    INT      NULL,
    [Modified_dt]    DATETIME NULL,
    CONSTRAINT [PK_td_AllocationDaysDet] PRIMARY KEY CLUSTERED ([intNoofDays] ASC)
);

