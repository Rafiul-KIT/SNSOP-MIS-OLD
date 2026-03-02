CREATE TABLE [dbo].[LK_TypeOfComplaint]
(
	[TypeOfComplaintID] INT NOT NULL PRIMARY KEY identity(1,1),
    [Name]              NVARCHAR (255) NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 1,
    
)
