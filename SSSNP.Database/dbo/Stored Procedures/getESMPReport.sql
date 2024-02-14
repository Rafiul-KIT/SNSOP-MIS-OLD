CREATE procedure [dbo].[getESMPReport]
(
@FromDate datetime,
@ToDate datetime,
@IPid varchar(8)
)
as
Begin
if(@Fromdate<>'' and @ToDate<>'')
Begin
select td_IPDetails.IPName, td_IPDetails.intIP,

tm_States.StateName,
tm_Counties.CountieName,
tm_Payams.PayamName,
tm_Bomas.BomaName,
tm_Bomas.intBomasid,

count(*) as TotalPlans from EnvironmentManagmentPlan inner join tbl_WorkMaster
on tbl_WorkMaster.intWorkid=Convert(int, EnvironmentManagmentPlan.WorkCodeID) inner join td_IPDetails on tbl_WorkMaster.IPid=td_IPDetails.intIP
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid
inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
where tbl_WorkMaster.Ipid like @IPid
 and WorkCodeID is not null
and EnvironmentManagmentPlan.Created_dt between @Fromdate and @ToDate+1
group by td_IPDetails.IPName, td_IPDetails.intIP,tm_States.StateName,
tm_Counties.CountieName,
tm_Payams.PayamName,
tm_Bomas.BomaName,tm_Bomas.intBomasid order by td_IPDetails.IPName asc


End
else
Begin
select td_IPDetails.IPName, td_IPDetails.intIP,

tm_States.StateName,
tm_Counties.CountieName,
tm_Payams.PayamName,
tm_Bomas.BomaName,
tm_Bomas.intBomasid,

count(*) as TotalPlans from EnvironmentManagmentPlan inner join tbl_WorkMaster
on tbl_WorkMaster.intWorkid=Convert(int, EnvironmentManagmentPlan.WorkCodeID) inner join td_IPDetails on tbl_WorkMaster.IPid=td_IPDetails.intIP
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid
inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
where tbl_WorkMaster.Ipid like @IPid
 and WorkCodeID is not null
group by td_IPDetails.IPName, td_IPDetails.intIP,tm_States.StateName,
tm_Counties.CountieName,
tm_Payams.PayamName,
tm_Bomas.BomaName,tm_Bomas.intBomasid order by td_IPDetails.IPName asc
End
End