

SET IDENTITY_INSERT [dbo].[lk_typeofGrievancedetail] ON

MERGE INTO [dbo].[lk_typeofGrievancedetail] AS Target
USING (VALUES
  (1,1,'Exclusion error',1)
 ,(2,1,'Inclusion error',1)
 ,(3,1,'Name spelling error',1)
 ,(4,1,'Name missing on list',1)
 ,(5,1,'Validation error',1)
 ,(6,2,'Delayed payment',1)
 ,(7,2,'Non-payment',1)
 ,(8,2,'Underpayment',1)
 ,(9,2,'Overpayment',1)
 ,(10,1,'Household duplication',1)
 ,(11,2,'Fingerprint failure',1)
 ,(12,2,'Lost ID',1)
 ,(13,3,'Land consent',1)
 ,(15,3,'Trespass',1)
 ,(16,3,'Property/environmental damage',1)
 ,(17,3,'Waste management/Disposal',1)
 ,(18,3,'Community disturbance',1)
 ,(19,3,'Insufficient tools/tools in poor shape',1)
 ,(20,3,'COVID-19 protocols not followed',1)
 ,(21,3,'Work norm violation',1)
 ,(22,3,'Shelter for children are not available',1)
 ,(23,3,'Eligible participants not assigned lighter work',1)
 ,(24,6,'Question/Enquiry',1)
 ,(25,6,'Lack of information',1)
 ,(26,6,'Request for information',1)
 ,(27,6,'Beneficiary misconduct',1)
 ,(28,6,'Committee member misconduct',1)
 ,(29,6,'UNOPS staff misconduct',1)
 ,(30,6,'Other',1)
 ,(31,4,'Quality of service/works',1)
 ,(33,4,'Other',1)
 ,(34,5,'Bribery',1)
 ,(35,5,'Extortion of beneficiary payment',1)
 ,(36,5,'Theft of project property',1)
 ,(37,5,'Manipulation of targeting and registration process',1)
 ,(38,5,'Other',1)
 ,(39,1,'Other',1)
 ,(40,2,'Other',1)
 ,(41,3,'Other',1)
) AS Source ([TypeOfGrievanceDetailID],[TypeOfGrievanceMainID],[Name],[Active])
ON (Target.[TypeOfGrievanceDetailID] = Source.[TypeOfGrievanceDetailID])
WHEN MATCHED AND (
	NULLIF(Source.[TypeOfGrievanceMainID], Target.[TypeOfGrievanceMainID]) IS NOT NULL OR NULLIF(Target.[TypeOfGrievanceMainID], Source.[TypeOfGrievanceMainID]) IS NOT NULL OR 
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL OR 
	NULLIF(Source.[Active], Target.[Active]) IS NOT NULL OR NULLIF(Target.[Active], Source.[Active]) IS NOT NULL) THEN
 UPDATE SET
  [TypeOfGrievanceMainID] = Source.[TypeOfGrievanceMainID], 
  [Name] = Source.[Name], 
  [Active] = Source.[Active]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([TypeOfGrievanceDetailID],[TypeOfGrievanceMainID],[Name],[Active])
 VALUES(Source.[TypeOfGrievanceDetailID],Source.[TypeOfGrievanceMainID],Source.[Name],Source.[Active]);
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT

GO

SET IDENTITY_INSERT [dbo].[lk_typeofGrievancedetail] OFF
GO
SET NOCOUNT OFF
GO


