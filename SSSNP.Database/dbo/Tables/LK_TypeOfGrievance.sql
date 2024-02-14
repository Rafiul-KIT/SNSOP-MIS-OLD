CREATE TABLE [dbo].[LK_TypeOfGrievance] (
    [TypeOfGrievanceID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (150) NOT NULL,
    [Active]            BIT            CONSTRAINT [DF_LK_TypeOfGrievance_Active] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_LK_TypeOfGrievance] PRIMARY KEY CLUSTERED ([TypeOfGrievanceID] ASC)
);

