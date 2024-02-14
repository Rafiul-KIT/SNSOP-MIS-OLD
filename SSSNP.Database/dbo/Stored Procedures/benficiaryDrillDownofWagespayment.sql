CREATE Procedure [dbo].[benficiaryDrillDownofWagespayment]              
@intIP varchar(10),              
@status varchar(20)              
as begin                
  if(@status='1')              
  begin              
SELECT   distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet   inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                  
                            left outer  join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid       
                            -- left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE                    
                                          
                            tbl_BeneficiaryDet.intIP like   @intIP                
                                          
       end  
       else if(@status='999')              
  begin              
SELECT  distinct  tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet     inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                   
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid       
                            -- left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE                    
                                          
                            tbl_BeneficiaryDet.intIP like   @intIP                
                                          
       end                       
       else if(@status='M')             
                   
       begin              
SELECT  distinct  tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet   inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid               
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid  
                            --       inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE                    
                                          
                            tbl_BeneficiaryDet.intIP like @intIP    and tbl_BeneficiaryDet.Sex like 'M'            
                                          
       end             
                   
       else if(@status='F')             
                   
       begin              
SELECT   distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet    inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                   
            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid    
            --     inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
  inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE                    
                                          
                            tbl_BeneficiaryDet.intIP like @intIP    and tbl_BeneficiaryDet.Sex like 'F'            
                                          
       end             
                   
       else if(@status='2')              
       begin                                   
                                          
                                                
                                  SELECT  tr_BenWagesPayment.*,td_IPDetails.IPName,              
                                  tbl_BeneficiaryDet.*,tbl_WorkMaster.WorkCode as WorkCode1,tbl_WorkMaster.Status, tr_ProposedWorkAct.WorkName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName                 
                            FROM tr_BenWagesPayment                    
                            inner join   tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenWagesPayment.intBenid                  
                            inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                 
                               inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP                      inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid                    
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                                             
                            WHERE                    
                                              
                                       td_IPDetails.intIP like   @intIP                        
                                                                
   end              
                 
   else if(@status='3')              
   begin              
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet   inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                  
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid      
                            --   inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                           inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                            inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE               
                                          
                            tbl_BeneficiaryDet.intCountieid like @intIP                
              
                 
                 
   end              
   else if (@status='4')              
begin              
                               SELECT  tr_BenWagesPayment.*,td_IPDetails.IPName,              
                                  tbl_BeneficiaryDet.*,tbl_WorkMaster.WorkCode as WorkCode1,tbl_WorkMaster.Status, tr_ProposedWorkAct.WorkName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName                 
                            FROM tr_BenWagesPayment                    
                            inner join   tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenWagesPayment.intBenid                  
                         inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                
                               inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP                      inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid                    
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                                             
                            WHERE                    
                                              
                                       tm_Counties.intCountieid like   @intIP                        
                 
   end              
                 
   else if(@status='5')              
   begin              
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet    inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                 
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid       
                            --  inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                                                         WHERE                    
                                          
                            tbl_BeneficiaryDet.intPayamid like @intIP                
              
                 
                 
   end              
else if (@status='6')              
   begin              
                               SELECT  tr_BenWagesPayment.*,td_IPDetails.IPName,              
                                  tbl_BeneficiaryDet.*,tbl_WorkMaster.WorkCode as WorkCode1,tbl_WorkMaster.Status, tr_ProposedWorkAct.WorkName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName                 
                            FROM tr_BenWagesPayment                    
                            inner join   tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenWagesPayment.intBenid                  
  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                
                               inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP                     inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid                    
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                                             
                            WHERE                    
                                              
                                      tm_Payams.intPayamid like   @intIP                        
                                          
end          
else if(@status='7')              
   begin              
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet    inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid             
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid      
                                   left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                              -- inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                  inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid              
                            WHERE         ( (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP)) and          
                                          
                            tbl_BeneficiaryDet.intCountieid like @intIP                
              
                 
                 
   end              
             
   else if(@status='8')              
   begin              
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet    inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid             
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid        
                                 left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               --inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                    inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid         
                            WHERE          (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP) and           
                                          
                            tbl_BeneficiaryDet.intCountieid like @intIP      and tbl_BeneficiaryDet.Sex like 'F'          
              
                 
                 
   end              
   else if(@status='9')              
   begin              
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet    inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid              
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid            
                             left outer join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               left outer join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               left outer join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               left outer join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               left outer join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               --inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP       
                                 inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid    
                            WHERE (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP) and         
                                          
                            tbl_BeneficiaryDet.intCountieid like @intIP      and tbl_BeneficiaryDet.Sex like 'M'          
              
                 
                 
   end              
   else if(@status='10')              
   begin              
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                 
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid     
                            --    inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE                    
                                          
                            tbl_BeneficiaryDet.intPayamid like @intIP                
              
                 
                 
   end         
           
   else if(@status='11')              
   begin              
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName             
                            FROM tbl_BeneficiaryDet   inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                  
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid  
                            --       inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE                    
                                          
                            tbl_BeneficiaryDet.intPayamid like @intIP        and tbl_BeneficiaryDet.Sex like 'F'          
              
                 
                 
   end              
        
else if(@status='12')              
   begin              
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet    inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid            
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid 
                            --        inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE                    
                                          
                            tbl_BeneficiaryDet.intPayamid like @intIP        and tbl_BeneficiaryDet.Sex like 'M'          
              
                 
                 
   end              
         
   else if(@status='13')              
   begin        
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                   
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid   
                            --      inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
 inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE                    
                                          
                            tbl_BeneficiaryDet.intBomasid like @intIP                
              
                 
                 
   end         
           
   else if(@status='14')              
   begin              
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet   inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid               
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid  
                            --       inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
                               inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE                    
                                          
                            tbl_BeneficiaryDet.intBomasid like @intIP        and tbl_BeneficiaryDet.Sex like 'F'          
              
                 
                 
   end              
        
else if(@status='15')              
   begin              
   SELECT    distinct tbl_BeneficiaryDet.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,td_IPDetails.IPName                
                            FROM tbl_BeneficiaryDet   inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid               
                            left outer join tr_ProposedWorkAct on tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid 
                            --        inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork                
                               inner join tm_States on tm_States.intStateid=tbl_BeneficiaryDet.intStateid                
           inner join tm_Counties on tm_Counties.intCountieid=tbl_BeneficiaryDet.intCountieid                
                               inner join tm_Payams on tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid                
                               inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid                
                               inner join td_IPDetails on td_IPDetails.intIP=tbl_BeneficiaryDet.intIP              
                                             
                            WHERE                    
                                          
                            tbl_BeneficiaryDet.intBomasid like @intIP        and tbl_BeneficiaryDet.Sex like 'M'          
              
                 
                 
   end              
         
        
              
end




