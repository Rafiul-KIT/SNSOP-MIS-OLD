
SET IDENTITY_INSERT [dbo].[lk_TypeOfGrievance] ON

MERGE INTO [dbo].[lk_TypeOfGrievance] AS Target
USING (VALUES
  (1,'Targeting/Registration',1)
 ,(2,'Payments',1)
 ,(3,'LIPWs',1)
 ,(4,'Service Standards',1)
 ,(5,'Fraud/Corruption allegation',1)
 ,(6,'General Complaint',1)
 ,(7,'Other',1)
 ,(8,'Question/Clarification',0)
 ,(9,'Targeting/Enrollment',0)
) AS Source ([TypeOfGrievanceID],[Name],[Active])
ON (Target.[TypeOfGrievanceID] = Source.[TypeOfGrievanceID])
WHEN MATCHED AND (
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL OR 
	NULLIF(Source.[Active], Target.[Active]) IS NOT NULL OR NULLIF(Target.[Active], Source.[Active]) IS NOT NULL) THEN
 UPDATE SET
  [Name] = Source.[Name], 
  [Active] = Source.[Active]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([TypeOfGrievanceID],[Name],[Active])
 VALUES(Source.[TypeOfGrievanceID],Source.[Name],Source.[Active]);
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
GO


SET IDENTITY_INSERT [dbo].[lk_TypeOfGrievance] OFF
GO
SET NOCOUNT OFF
GO


