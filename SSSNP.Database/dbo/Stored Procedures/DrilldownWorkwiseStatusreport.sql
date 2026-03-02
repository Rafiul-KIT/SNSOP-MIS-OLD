--select * from tbl_WorkMaster      
CREATE Procedure [dbo].[DrilldownWorkwiseStatusreport]      
@intTST varchar(15),                              
@intIP  varchar(15),      
@status varchar(10)                             
      
as begin      
      
if(@status='1')      
begin      
      
      
      
select DISTINCT td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP  inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
inner join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP and tm_Counties.intCountieid in (select distinct intCountieid  from tbl_WorkMaster )                 
         
      
end      
else if(@status='2')      
begin      
select distinct td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP  inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
inner join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
inner join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid      
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP  and tm_Payams.intPayamid in (select distinct intPayamid  from tbl_WorkMaster)                 
      
      
end      
else if(@status='3')      
begin      
select  distinct td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP  inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
inner join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
inner join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid      
inner join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid      
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP  and tm_Bomas.intBomasid in (select distinct intBomasid  from tbl_WorkMaster)                 
      
      
end      
else if(@status='4')      
begin      
      
select tbl_WorkMaster.*,td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP        
left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
left outer join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
left outer join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid      
left outer join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid      
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP      
end      
      
else if(@status='5')      
begin      
      
select tbl_WorkMaster.*,td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP        
left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
left outer join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
left outer join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid      
left outer join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid      
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP and tbl_WorkMaster.Status like 'New Proposal'      
      
      
end      
else if(@status='6')      
begin      
      
select tbl_WorkMaster.*,td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP        
left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
left outer join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
left outer join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid      
left outer join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid      
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP and tbl_WorkMaster.Status like 'Inprogress'      
      
      
end      
else if(@status='7')      
begin      
select tbl_WorkMaster.*,td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP        
left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
left outer join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
left outer join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid      
left outer join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid      
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP and tbl_WorkMaster.Status like 'Closed'      
      
      
end      
else if(@status='8')      
begin      
      
select tbl_WorkMaster.*,td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP        
left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
left outer join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
left outer join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid      
left outer join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid      
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP and tbl_WorkMaster.intWorkid in (select distinct intWorkId   from tr_FinancialApproval)      
      
      
      
      
      
end      
else if(@status='9')      
begin      
select tbl_BeneficiaryDet.*,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName from tbl_BeneficiaryDet inner join td_IPDetails on tbl_BeneficiaryDet.intIP=td_IPDetails.intIP       
  inner join tm_States on tbl_BeneficiaryDet.intStateid=tm_States.intStateid inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid      
  inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid      
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP  and tbl_BeneficiaryDet.WorkCode is not null       
      
      
end      
      
else if(@status='10')      
begin      
select tbl_BeneficiaryDet.*,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName from tbl_BeneficiaryDet inner join td_IPDetails on tbl_BeneficiaryDet.intIP=td_IPDetails.intIP       
  inner join tm_States on tbl_BeneficiaryDet.intStateid=tm_States.intStateid inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid      
  inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid      
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP  and tbl_BeneficiaryDet.WorkCode is not null and tbl_BeneficiaryDet.Sex like 'F'      
      
      
end      
else if(@status='11')      
begin      
      
select tbl_BeneficiaryDet.*,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName from tbl_BeneficiaryDet inner join td_IPDetails on tbl_BeneficiaryDet.intIP=td_IPDetails.intIP       
  inner join tm_States on tbl_BeneficiaryDet.intStateid=tm_States.intStateid inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid      
  inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid     
where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP  and tr_BenfWorkDet.intWorkid  is not null and tbl_BeneficiaryDet.Sex like 'M'      
      
end      
else if(@status='12')  
begin  
select distinct td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP  inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
inner join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
inner join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid      
where td_IPDetails.intTST like @intTST  and  tm_Counties.intCountieid like @intIP   
   
 --and tm_Payams.intPayamid in (select distinct intPayamid  from tbl_WorkMaster)                 
  
  
  
end  
else if(@status='13')      
begin      
select  distinct td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP  inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
inner join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
inner join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid      
inner join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid      
where td_IPDetails.intTST like @intTST  and tm_Counties.intCountieid like @intIP   
--and tm_Bomas.intBomasid in (select distinct intBomasid  from tbl_WorkMaster)                 
      
      
end    

else if(@status='14')      
begin      
select  distinct td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName    from tbl_WorkMaster      
inner join dbo.td_IPDetails  on  tbl_WorkMaster.IPid=td_IPDetails.intIP  inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
inner join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid      
inner join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid      
inner join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid      
where td_IPDetails.intTST like @intTST  and tm_Payams.intPayamid like @intIP
--and tm_Bomas.intBomasid in (select distinct intBomasid  from tbl_WorkMaster)                 
      
      
end    

  
      
      
      
end




