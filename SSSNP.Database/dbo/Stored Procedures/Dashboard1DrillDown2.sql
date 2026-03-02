--select * from td_TstTeamDet      
CREATE Procedure [dbo].[Dashboard1DrillDown2] --'1001', '9'    
@id varchar(10),      
@status varchar(20)      
as begin      
if(@status='1')      
begin      
      
select td_IPDetails.*,td_TstTeamDet.Ministry,td_TstTeamDet.TstName from td_IPDetails inner join td_TstTeamDet on td_IPDetails.intTST=td_TstTeamDet.intTST   
where td_IPDetails.intIP like @id  
-- where (YEAR(td_IPDetails.Created_dt) = @Year)         
end      
else if(@status='2')      
begin      
     
 select DISTINCT (td_IPDetails.intIP),td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName    from tbl_BeneficiaryDet      
inner join dbo.td_IPDetails  on  tbl_BeneficiaryDet.intIP=td_IPDetails.intIP  inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid      
inner join tm_Counties on tbl_BeneficiaryDet.intCountieid=tm_Counties.intCountieid      
  where tbl_BeneficiaryDet.intIP=td_IPDetails.intIP  and td_IPDetails.intIP like @id    
   -- where (YEAR(Created_dt) = @Year)      
end      
else if(@status='3')      
begin      
      
 select distinct td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName   from tbl_BeneficiaryDet      
inner join dbo.td_IPDetails  on  tbl_BeneficiaryDet.intIP=td_IPDetails.intIP  inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid      
inner join tm_Counties on tbl_BeneficiaryDet.intCountieid=tm_Counties.intCountieid      
inner join tm_Payams on tbl_BeneficiaryDet.intPayamid=tm_Payams.intPayamid   where tbl_BeneficiaryDet.intIP=td_IPDetails.intIP and td_IPDetails.intIP like @id  
end      
else if(@status='4')      
begin      
      
select distinct td_IPDetails.intIP,td_IPDetails.IPName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName    from tbl_BeneficiaryDet      
inner join dbo.td_IPDetails  on  tbl_BeneficiaryDet.intIP=td_IPDetails.intIP        
inner  join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid      
inner join tm_Counties on tbl_BeneficiaryDet.intCountieid=tm_Counties.intCountieid      
inner join tm_Payams on tbl_BeneficiaryDet.intPayamid=tm_Payams.intPayamid      
inner  join tm_Bomas on tbl_BeneficiaryDet.intBomasid=tm_Bomas.intBomasid      
    
where tbl_BeneficiaryDet.intIP=td_IPDetails.intIP and td_IPDetails.intIP like @id  
      
end      
else if (@status='5')      
begin      
    --select tbl_BeneficiaryDet.*,td_TstTeamDet.TstName,td_TstTeamDet.Ministry, td_IPDetails.IPName from tbl_BeneficiaryDet inner join td_TstTeamDet on tbl_BeneficiaryDet.intTSTid=td_TstTeamDet.intTST inner join       
    --td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP      
--                  SELECT  distinct td_IPDetails.intIP, tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName ,tbl_WorkMaster.Status          
--        FROM          dbo.tbl_BeneficiaryDet inner join td_IPDetails on  tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP        
--         left outer join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid    
--         left outer join  tbl_WorkMaster on tbl_BeneficiaryDet.WorkCode=tbl_WorkMaster.intWorkid       
--inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid        
--inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid        
--inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid        
-- where tbl_BeneficiaryDet.intIP=td_IPDetails.intIP and td_IPDetails.intIP like @id and tbl_WorkMaster.ipid is not null  
      
          SELECT distinct  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName      
        FROM    dbo.tbl_WorkMaster
		inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP	
		inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid 
		inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid 
		inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid  			 
		left outer join tm_States on tm_States.intStateid=tm_Counties.intStateid    
   
  
   
        where td_IPDetails.intIP like @id  
        
        
        
    -- where (YEAR(tbl_BeneficiaryDet.Created_dt) = @Year)             
end      
      
else if(@status='6')      
begin      

 SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName      
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid    
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid    
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid    
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid     where tbl_WorkMaster.Status like 'New Proposal'       and td_IPDetails.intIP like @id  
       
--where   
----(YEAR(tbl_BeneficiaryDet.Created_dt) = @Year) and   
--(tbl_WorkMaster.Status  like 'New Proposal')            
      
end       
else if(@status='7')      
begin      
      
 SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName      
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid    
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid    
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid    
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid   where 
  (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Invoice Appovals' or tbl_WorkMaster.Status like 'Invoice Request' or tbl_WorkMaster.Status like 'Approved' )   and td_IPDetails.intIP like @id  
      
end      
else if(@status='8')      
begin      
    
SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName      
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid    
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid    
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid    
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid   where  tbl_WorkMaster.Status like 'Completed' and td_IPDetails.intIP like @id  
end      
else if(@status='99')      
begin      
    
SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName      
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid    
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid    
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid    
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid  
 where  tbl_WorkMaster.Status like 'Rejected' and td_IPDetails.intIP like @id  
end      

else if(@status='9')      
begin      
      
 SELECT    tbl_WorkMaster.*,tbl_BeneficiaryDet.*,tbl_BeneficiaryDet.WorkCode as WorkCode1,tbl_WorkMaster.Status , tr_ProposedWorkAct.WorkName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName          
                            FROM tbl_BeneficiaryDet                    
                            left outer join  tbl_WorkMaster on tbl_BeneficiaryDet.WorkCode=tbl_WorkMaster.intWorkid           
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid  
                            left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid          
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP     
                               inner join  tr_FinancialApproval on tr_FinancialApproval.intWorkId=tbl_BeneficiaryDet.WorkCode  
                                where tr_FinancialApproval.intWorkId in (select distinct tbl_BeneficiaryDet.WorkCode from tbl_BeneficiaryDet where td_IPDetails.intIP=tbl_BeneficiaryDet.intIP)    and td_IPDetails.intIP like @id  
            
--where (YEAR(tbl_BeneficiaryDet.Created_dt) = @Year)            
      
      
end      
  
else if(@status='10')      
begin      
      
 SELECT    tbl_WorkMaster.*,tbl_BeneficiaryDet.*,tbl_WorkMaster.WorkCode as WorkCode1,tbl_WorkMaster.Status , tr_ProposedWorkAct.WorkName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName          
                            FROM tbl_BeneficiaryDet                    
                           inner join (select distinct intBenid, Max(intWorkid) as intWorkid from tr_BenfWorkDet group by intBenid) as tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid    
                            left outer join tbl_WorkMaster on tbl_WorkMaster.intWorkid= tr_BenfWorkDet.intWorkid          
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid  
                            left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid          
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP     
                                  
          where  td_IPDetails.intIP like @id  
--where (YEAR(tbl_BeneficiaryDet.Created_dt) = @Year)            
      
      
end    
  
  
else if(@status='11')      
begin      
SELECT  distinct  tbl_WorkMaster.*,tbl_BeneficiaryDet.*,tbl_WorkMaster.WorkCode as WorkCode1,tbl_WorkMaster.Status , tr_ProposedWorkAct.WorkName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName         
 
                            FROM tbl_BeneficiaryDet  inner join (select distinct intBenid, Max(intWorkid) as intWorkid from tr_BenfWorkDet group by intBenid) as tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid    inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid           
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid  
                             left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork        
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid          
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP        
      where  --   (YEAR(tbl_BeneficiaryDet.Created_dt) = @Year)  and   
        tbl_BeneficiaryDet.Sex like 'M' and td_IPDetails.intIP like @id     
      
      
      
end      
else if(@status='12')      
begin      
SELECT   distinct tbl_WorkMaster.*,tbl_BeneficiaryDet.*,tbl_WorkMaster.WorkCode as WorkCode1,tbl_WorkMaster.Status , tr_ProposedWorkAct.WorkName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName        
  
                            FROM tbl_BeneficiaryDet  inner join (select distinct intBenid, Max(intWorkid) as intWorkid from tr_BenfWorkDet group by intBenid) as tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid   inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid      
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid  
                              left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid          
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP        
              
where   
--(YEAR(tbl_BeneficiaryDet.Created_dt) = @Year)  and   
tbl_BeneficiaryDet.Sex like 'F'    and td_IPDetails.intIP like @id  
      
      
end      
       
   else if(@status='13')      
begin      
      
 SELECT    tbl_WorkMaster.*,tbl_BeneficiaryDet.*,tbl_WorkMaster.WorkCode as WorkCode1,tbl_WorkMaster.Status , tr_ProposedWorkAct.WorkName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName          
                            FROM tbl_BeneficiaryDet                    
                           inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid    
                            left outer join tbl_WorkMaster on tbl_WorkMaster.intWorkid= tr_BenfWorkDet.intWorkid          
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid  
                            left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid          
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP     
                                  
          where  td_IPDetails.intIP like @id  
--where (YEAR(tbl_BeneficiaryDet.Created_dt) = @Year)            
      
      
end       
      
      
      
end




