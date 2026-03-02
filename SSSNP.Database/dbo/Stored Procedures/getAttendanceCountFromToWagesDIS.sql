CREATE procedure [dbo].[getAttendanceCountFromToWagesDIS]  
(  
	@intWorkWageReqId varchar(10) 
)  
as  
begin  
  
select distinct 
	ben.intBenid
	,ben.EnrollmentNo
	,ben.NameofHousehold
	,ben.Sex
	,ben.Age
	,ben.SelectionCriteria
	,ben.ContactNo
	,ben.Email
	,ben.FamilyIncome
	,tm_Bomas.BomaName
	,(
		select  top 1
			wr.TotAttendance
		from 
			td_WageReqBenDet  wr
			inner join tbl_BeneficiaryDet as a 
				on a.intBenid=wr.intBenid 

		where 
			 intWagePaymentReqid like @intWorkWageReqId
			)as cnt,
			0 as Amt
from 
	dbo.tbl_BeneficiaryDet  ben
	inner join td_WageReqBenDet wageBen on wageBen.intBenid=ben.intBenid
	inner join td_WorkWageRequest wwr on wwr.intWagePaymentReqid=wageBen.intWagePaymentReqid
	left outer join tm_Bomas 
		on tm_Bomas.intBomasid=ben.intBomasid 
where 
wwr.intWagePaymentReqid=@intWorkWageReqId
order by 
	ben.EnrollmentNo asc
end