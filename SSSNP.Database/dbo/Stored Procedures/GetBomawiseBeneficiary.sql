CREATE Procedure [dbo].[GetBomawiseBeneficiary]      
(      
@intBomasid varchar(10)      
)      
as begin      
      
select *,tm_Bomas.BomaName,case when tbl_BeneficiaryDet.Sex='M' then 'Male' else 'Female' end as gender from dbo.tbl_BeneficiaryDet left outer join      
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid       
      
 where   
 --WorkCode is null  and  
  tbl_BeneficiaryDet.intBomasid like @intBomasid      
       
end




