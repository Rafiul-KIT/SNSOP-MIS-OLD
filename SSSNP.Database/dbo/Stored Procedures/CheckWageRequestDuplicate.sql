
CREATE procedure [dbo].[CheckWageRequestDuplicate]    
(   
    
@Benids nvarchar(MAX)  
,@year varchar(20)   
,@month varchar(20)   
,@paymentCycle varchar(20)   
,@bomaID varchar(20)   
)

as begin
    
CREATE  TABLE #temp_beneficiariesID (
    id varchar(8)
);

insert into #temp_beneficiariesID(id)

SELECT  
     Split.a.value('.', 'VARCHAR(100)') AS String  
 FROM  (SELECT   
         CAST ('<M>' + REPLACE(@Benids, ',', '</M><M>') + '</M>' AS XML) AS String  
     ) AS A CROSS APPLY String.nodes ('/M') AS Split(a);
        

select count (distinct intbenid) from td_WageReqBenDet a
	inner join #temp_beneficiariesID b
	on a.intBenid=b.id
where intWagePaymentReqid in
(
	select distinct a.intWagePaymentReqid  from td_WorkWageRequest a
		inner join td_WageReqBenDet b
		on a.intWagePaymentReqid=b.intWagePaymentReqid
		inner join tbl_BeneficiaryDet c
		on b.intBenid=c.intBenid
	where intBomasid=@bomaID
		and ((a.reqyear=@year and a.reqmonth=@month) OR a.paymentcycle= @paymentCycle)
)

drop table #temp_beneficiariesID
   
end