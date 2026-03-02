CREATE Procedure [dbo].[workProgressBendet]    
(    
@WorkCode varchar(50)    
)    
    
as begin    
    
select tbl_BeneficiaryDet.* from dbo.tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid where tr_BenfWorkDet.intWorkid like @WorkCode    
    
select * from tbl_WorkProgress where intWorkid like @WorkCode    

--select * from PWImages.dbo.tbl_Attachements where type like 'Work Progress' and intTypeId like @WorkCode  
    
end




