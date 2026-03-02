CREATE TABLE [dbo].[tr_PW] (
    [intWork]           INT          IDENTITY (1000, 1) NOT NULL,
    [intMobilization]   INT          NULL,
    [AreaOfWork]        VARCHAR (50) NULL,
    [WorkActitvity]     VARCHAR (50) NULL,
    [NoofHousedhold]    VARCHAR (50) NULL,
    [NoofDays]          VARCHAR (50) NULL,
    [workCode]          VARCHAR (50) NULL,
    [EquipmentRequired] VARCHAR (50) NULL,
    [MaterialRequired]  VARCHAR (50) NULL,
    [SkillLabour]       VARCHAR (50) NULL,
    [StartDate]         VARCHAR (50) NULL,
    [Enddate]           VARCHAR (50) NULL,
    [Cost]              VARCHAR (50) NULL
);

