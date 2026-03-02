CREATE Procedure [dbo].[getGroupssearch_Sp](

	@State int ,
	@County int ,
	@Payam int ,
	@Boma int ,
	@GroupType varchar(100) ,
	@GroupCode varchar(100),
	@GroupStrength int
	)
	As
	Select gm.GroupId,s.StateName as State,c.CountieName as County,p.PayamName as Payam,b.BomaName as Boma,
	  gm.GroupType,gm.GroupCode,gm.GroupStrength  from GroupMaster gm inner join
	   tm_States s on gm.State=s.intStateid inner join 
	   tm_Counties c on gm.County =c.intCountieid inner join
tm_Payams p on gm.Payam =p.intPayamid inner join
tm_Bomas b on gm.Boma =b.intBomasid

	    where 
	gm.State=(case when @State=0 then gm.State else @State end) and
	gm.Payam=(case when @Payam=0 then gm.Payam else @Payam end) and
	gm.Boma=(case when @Boma=0 then gm.Boma else @Boma end) and
	gm.GroupType=(case when @GroupType='' then gm.GroupType else @GroupType end) and
	gm.GroupCode=(case when @GroupCode='' then gm.GroupCode else @GroupCode end) and
	gm.GroupStrength=(case when @GroupStrength=0 then gm.GroupStrength else @GroupStrength end)




