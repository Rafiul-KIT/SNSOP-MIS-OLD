CREATE TABLE [dbo].[LK_TypeOfGrievanceDetail] (
    [TypeOfGrievanceDetailID] INT            IDENTITY (1, 1) NOT NULL,
    [TypeOfGrievanceMainID]   INT            NOT NULL,
    [Name]                    NVARCHAR (150) NOT NULL,
    [Active]                  BIT            CONSTRAINT [DF_LK_TypeOfGrievanceDetail_Active] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_LK_TypeOfGrievanceDetail] PRIMARY KEY CLUSTERED ([TypeOfGrievanceDetailID] ASC),
    CONSTRAINT [FK_LK_TypeOfGrievanceDetail_LK_TypeOfGrievance] FOREIGN KEY ([TypeOfGrievanceMainID]) REFERENCES [dbo].[LK_TypeOfGrievance] ([TypeOfGrievanceID])
);

