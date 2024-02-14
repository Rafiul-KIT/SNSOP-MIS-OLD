


CREATE PROCEDURE [dbo].[InsertAuditLog]
	@userId nvarchar(255),
	@pKey nvarchar(MAX),
	@xmlObjectLog xml,
	@commandMethod nvarchar(255),
	@commandText nvarchar(255),
	@operationType nvarchar(20)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [dbo].[AuditLogs]
           ([UserID]
		   ,[pKey]
           ,[XMLObjectLog]
           ,[CommandMethod]
           ,[CommandText]
		   ,[OperationType])
     VALUES
           (@userId
		   ,@pKey
           ,@xmlObjectLog
           ,@commandMethod
           ,@commandText
		   ,@operationType)
		   SELECT 1;

END
