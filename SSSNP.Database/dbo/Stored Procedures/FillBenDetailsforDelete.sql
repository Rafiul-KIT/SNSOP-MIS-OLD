CREATE procedure [dbo].[FillBenDetailsforDelete]    -- exec APIDrillDown '1001','1','%','%','%','%','%','DRE/01/001'                       
@intIP  varchar(15),      
--@status varchar(10),
@BenfiName varchar(200),
@state varchar(10),
@county varchar(10),
@payam varchar(10),
@boma varchar(10),
@Enrollment varchar(100)                           
      
as begin      
--Srikanth oin 02072018 - Delete Allotment After Confirmation            
select intBenid,NameofHousehold as BenfiName,EnrollmentNo,b.StateName,c.CountieName,d.PayamName,e.BomaName,ContactNo 
from tbl_BeneficiaryDet a
	inner join tm_States b on b.intStateid = a.intStateid
	inner join tm_Counties c on c.intCountieid = a.intCountieid
	inner join tm_Payams d on d.intPayamid = a.intPayamid
	inner join tm_Bomas e on e.intBomasid = a.intBomasid
where a.intIP like @intIP 
and a.NameofHousehold like '%'+@BenfiName+'%' and a.intStateid like @state and a.intCountieid like @county
and a.intPayamid like @payam and a.intBomasid like @boma and a.EnrollmentNo like @Enrollment
and a.intBenid in (select wb.intBenid from tr_BenfWorkDet as wb)
--and BenStatus = @BenStatus                
         
      
end 	
