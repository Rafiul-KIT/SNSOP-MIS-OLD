

CREATE PROCEDURE [dbo].[sp_BenTransferDetforAPI]

as
begin

select   b.intBenid,s.StateName,c.CountieName,p.PayamName,bom.BomaName as TransferedtoBomaName,

(select bm.BomaName  from  tm_Bomas as bm where bm.intBomasid=b.OLDBomaid) as OLDBomaName,
b.OLDBomaid as OLDBomaid,


 ip.IPName,  
b.EnrollmentNo,b.NameofHousehold,  b.NameofSpouse,b.household_number,
case when b.Sex='M' then 'Male' else 'Female' end as Gender
,b.PhotoName,  b.Name1stAlternatePayee,
b.alternate_number,b.Name2ndAlternatePayee,b.Secondalternate_number,


CONVERT(char(10), b.BomaTransferDate,126)  as TrasferDate,
s.intStateid as PresentStateid,
c.intCountieid as PresentCountieid,
p.intPayamid as PresentPayamid,
b.intBomasid as PresentBomasid


from dbo.tbl_BeneficiaryDet as b 
inner join tm_Bomas as bom on bom.intBomasid=b.intBomasid
inner join tm_Payams as p on p.intPayamid=bom.intPayamid
inner join tm_Counties as c on c.intCountieid=p.intCountieid
inner join tm_States as s on s.intStateid=c.intStateid
inner join td_IPDetails as ip on ip.intIP=b.intIP

 where
 
b.TransferFlag is not null 

 order by b.BomaTransferDate desc


 end

