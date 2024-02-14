CREATE Procedure  [dbo].[DrillDownTypesofWorkswiseReport]          
@intAreaofWork varchar(8),          
@Status varchar(30),          
@Type varchar(20)          
          
as begin          
          
if(@Type='1' or @Type='2'  or @Type='4' or @Type='94')          
begin          
          
          
SELECT distinct tbl_WorkMaster.*,tr_ProposedWorkAct.WorkName,tr_ProposedWorkAct.EstManPower,tr_ProposedWorkAct.EstDays,          
tr_ProposedWorkAct.EstEquipmnts,tr_ProposedWorkAct.EstMaterial,tr_ProposedWorkAct.EstSkilLabour,tr_ProposedWorkAct.EstWorkStartDate,tr_ProposedWorkAct.EstWorkEndDate,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName        
  
   FROM tr_ProposedWorkAct inner join  dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId           
inner join    tm_AreaofWork on  tr_ProposedWorkAct.intAreaofWork =tm_AreaofWork.intAreaofWork           
inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
            
                                    
                            WHERE             
                            tm_AreaofWork.intAreaofWork like @intAreaofWork and tbl_WorkMaster.Status like @Status                      
                                                                                
end          
          
else if(@Type='3')          
begin          
          
          
SELECT distinct tbl_WorkMaster.*,tr_ProposedWorkAct.WorkName,tr_ProposedWorkAct.EstManPower,tr_ProposedWorkAct.EstDays,          
tr_ProposedWorkAct.EstEquipmnts,tr_ProposedWorkAct.EstMaterial,tr_ProposedWorkAct.EstSkilLabour,tr_ProposedWorkAct.EstWorkStartDate,tr_ProposedWorkAct.EstWorkEndDate,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName        
  
   FROM tr_ProposedWorkAct inner join  dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId           
inner join    tm_AreaofWork on  tr_ProposedWorkAct.intAreaofWork =tm_AreaofWork.intAreaofWork           
inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
            
                                    
                            WHERE             
                            tm_AreaofWork.intAreaofWork like @intAreaofWork and    (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Invoice Appovals' or  tbl_WorkMaster.Status like 'Invoice Request' or tbl_WorkMaster.Status like 'Approved' )                          
end          
else if(@Type='5')          
begin          
          
                         SELECT  distinct                                        
                         tbl_WorkMaster.*,tr_ProposedWorkAct.WorkName,tr_ProposedWorkAct.EstManPower,tr_ProposedWorkAct.EstDays,          
tr_ProposedWorkAct.EstEquipmnts,tr_ProposedWorkAct.EstMaterial,tr_ProposedWorkAct.EstSkilLabour,tr_ProposedWorkAct.EstWorkStartDate,tr_ProposedWorkAct.EstWorkEndDate,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName        
  
                            FROM tr_FinancialApproval                    
                            inner join                       
                                                    
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId          
                                           
                                inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid =tbl_WorkMaster.intWorkid          
                               inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid         
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid       
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                            
                               where               
                                          
        tm_AreaofWork.intAreaofWork like @intAreaofWork            
                                        
                             
                
          
          
end          
else if(@Type='6' or @Type='7' or @Type='8')          
begin          
          
SELECT  distinct   tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName          
                            FROM tbl_BeneficiaryDet 
                             inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid 
                            inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid         inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid          
                                       
                            WHERE              
                            tm_AreaofWork.intAreaofWork like @intAreaofWork and tbl_BeneficiaryDet.Sex like @Status          
                                              
                                                        
                                                                
                                        
                                        
          
          
end        
        
else if(@Type='9')          
begin          
          
                         SELECT                                          
                         distinct(tbl_WorkMaster.WorkCode),tbl_WorkMaster.*,tr_ProposedWorkAct.WorkName,tr_ProposedWorkAct.EstManPower,tr_ProposedWorkAct.EstDays,          
tr_ProposedWorkAct.EstEquipmnts,tr_ProposedWorkAct.EstMaterial,tr_ProposedWorkAct.EstSkilLabour,tr_ProposedWorkAct.EstWorkStartDate,tr_ProposedWorkAct.EstWorkEndDate,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName       
   
                            FROM tr_FinancialApproval                    
                            inner join                       
                                                    
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId            
                                           
                                left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid =tbl_WorkMaster.intWorkid          
                               left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                                 inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid    
                               where     (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and            
        --                            tm_Payams.intPayamid like @intAreaofWork      
        tm_Counties.intCountieid like @intAreaofWork       
        --  and tm_AreaofWork.intAreaofWork like '1000'         
                                        
              
                
          
          
end          
        
else if(@Type='10')          
begin          
          
                         SELECT                   distinct                       
                         tbl_WorkMaster.*,tr_ProposedWorkAct.WorkName,tr_ProposedWorkAct.EstManPower,tr_ProposedWorkAct.EstDays,          
tr_ProposedWorkAct.EstEquipmnts,tr_ProposedWorkAct.EstMaterial,tr_ProposedWorkAct.EstSkilLabour,tr_ProposedWorkAct.EstWorkStartDate,tr_ProposedWorkAct.EstWorkEndDate,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName        
  
                            FROM tr_FinancialApproval                    
                            inner join                       
                                                    
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId          
                                           
                                inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid =tbl_WorkMaster.intWorkid          
                               inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                                         
                               where               
                                          
        tm_Counties.intCountieid like @intAreaofWork   and tm_AreaofWork.intAreaofWork like '1001'         
                                        
                             
                
          
          
end          
        
else if(@Type='11')          
begin          
          
                         SELECT           distinct                               
                         tbl_WorkMaster.*,tr_ProposedWorkAct.WorkName,tr_ProposedWorkAct.EstManPower,tr_ProposedWorkAct.EstDays,          
tr_ProposedWorkAct.EstEquipmnts,tr_ProposedWorkAct.EstMaterial,tr_ProposedWorkAct.EstSkilLabour,tr_ProposedWorkAct.EstWorkStartDate,tr_ProposedWorkAct.EstWorkEndDate,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName        
  
                            FROM tr_FinancialApproval                    
                            inner join                       
                                                    
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId          
                                           
                                inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid =tbl_WorkMaster.intWorkid          
                               inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
           
                               where               
                                          
        tm_Counties.intCountieid like @intAreaofWork   and tm_AreaofWork.intAreaofWork like '1002'         
                                        
                             
                
          
          
end          
        
        
else if(@Type='12')          
begin          
          
                         SELECT   distinct                                       
                         tbl_WorkMaster.*,tr_ProposedWorkAct.WorkName,tr_ProposedWorkAct.EstManPower,tr_ProposedWorkAct.EstDays,          
tr_ProposedWorkAct.EstEquipmnts,tr_ProposedWorkAct.EstMaterial,tr_ProposedWorkAct.EstSkilLabour,tr_ProposedWorkAct.EstWorkStartDate,tr_ProposedWorkAct.EstWorkEndDate,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName        
  
              FROM tr_FinancialApproval                    
                            inner join                       
                                                    
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId          
                                           
                                inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid =tbl_WorkMaster.intWorkid          
                               inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                                         
                               where               
                                          
        tm_Counties.intCountieid like @intAreaofWork   and tm_AreaofWork.intAreaofWork like '1003'         
                                        
                             
                
          
          
end          
        
else if(@Type='13')          
begin          
          
                         SELECT   Distinct                                       
                         tbl_WorkMaster.*,tr_ProposedWorkAct.WorkName,tr_ProposedWorkAct.EstManPower,tr_ProposedWorkAct.EstDays,          
tr_ProposedWorkAct.EstEquipmnts,tr_ProposedWorkAct.EstMaterial,tr_ProposedWorkAct.EstSkilLabour,tr_ProposedWorkAct.EstWorkStartDate,tr_ProposedWorkAct.EstWorkEndDate,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName        
  
                            FROM tr_FinancialApproval                    
                            inner join                       
                                                    
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId          
                                           
                                inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid =tbl_WorkMaster.intWorkid          
                               inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                                         
                               where               
                                   tm_Payams.intPayamid like @intAreaofWork      
        --tm_Counties.intCountieid like @intAreaofWork       
        --  and tm_AreaofWork.intAreaofWork like '1000'         
                                        
                             
                
          
          
end          
    
else if(@Type='14')          
begin          
          
                         SELECT     distinct                                     
                         tbl_WorkMaster.*,tr_ProposedWorkAct.WorkName,tr_ProposedWorkAct.EstManPower,tr_ProposedWorkAct.EstDays,          
tr_ProposedWorkAct.EstEquipmnts,tr_ProposedWorkAct.EstMaterial,tr_ProposedWorkAct.EstSkilLabour,tr_ProposedWorkAct.EstWorkStartDate,tr_ProposedWorkAct.EstWorkEndDate,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName        
  
                            FROM tr_FinancialApproval                    
                            inner join                       
                                                    
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId          
                                           
                                inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid =tbl_WorkMaster.intWorkid          
                               inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork          
                               inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                                         
                               where               
                                   tm_Bomas.intBomasid like @intAreaofWork      
        --tm_Counties.intCountieid like @intAreaofWork       
        --  and tm_AreaofWork.intAreaofWork like '1000'         
                                        
                             
                
          
          
end          
        
        
        
          
          
end




