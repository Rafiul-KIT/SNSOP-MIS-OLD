-- =============================================
-- Author:		<Author,,Rushi>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE Procedure [dbo].[procBenUpdatettendance] 
	-- Add the parameters for the stored procedure here
	@WorkCode int,  
@intBenid int,  
@AttendanceDate datetime,  
@Status varchar(10),  
@Created_by int  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure heree 
	update  dbo.tr_BenAttendanceDet set Status=@Status,Modified_by=@Created_by,Modified_dt=GETDATE() where  intBenid=@intBenid and  AttendanceDate=@AttendanceDate
  
select 1  
return 1  
END




