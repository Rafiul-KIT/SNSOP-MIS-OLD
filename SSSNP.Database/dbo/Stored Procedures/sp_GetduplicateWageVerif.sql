
CREATE procedure [dbo].[sp_GetduplicateWageVerif]
as
begin
select a.ReqNo, a.intWagePaymentReqid,a.intWorkid,a.ReqFromDate,a.ReqToDate,a.Status, (select count(distinct p.intBenid) from tr_BenfWorkDet as p where p.intWorkid=a.intWorkid) as Total, count(c.intWagesReqBenTransid) as cnt from td_WorkWageRequest as a inner join
(
select intWorkid,ReqFromDate,ReqToDate  from td_WorkWageRequest where Created_dt>= Convert(datetime,'08/22/2018') group by intWorkid,ReqFromDate,ReqToDate having count(*)>1
)
as b on b.intWorkid=b.intWorkid and a.ReqFromDate=b.ReqFromDate and a.ReqToDate=b.ReqToDate
inner join td_WageReqBenDet as c on c.intWagePaymentReqid=a.intWagePaymentReqid
group by a.ReqNo, a.intWagePaymentReqid,a.intWorkid,a.ReqFromDate,a.ReqToDate,a.Status
order by a.intWorkid,a.ReqFromDate,a.ReqToDate

end
