CREATE procedure [dbo].[getWageReqLocationDIS]  
(  
@intWagePaymentReqid varchar(8)  



)  
as  
begin  
  select top 1 StateName, CountieName,PayamName,BomaName from tbl_BeneficiaryDet a 
		inner join td_WageReqBenDet b on a.intBenid=b.intBenid
		inner join tm_States s on s.intStateid=a.intStateid
		inner join tm_Counties c on c.intCountieid=a.intCountieid
		inner join tm_Payams p on p.intPayamid=a.intPayamid
		inner join tm_Bomas bom on bom.intBomasid=a.intBomasid
where intWagePaymentReqid=@intWagePaymentReqid

end