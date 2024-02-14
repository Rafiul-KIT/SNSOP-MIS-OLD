CREATE Procedure [dbo].[drildownIPwiseTypesofWorkreport]  
@intTST varchar(15),                      
@status varchar(10)  
  
as begin  
  
if(@status='1')  
begin  
  
  SELECT    tr_ProposedWorkAct.*, tm_WorkActivities.WorkActivity, tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tbl_WorkMaster.WorkCode as WorkCodeNew,tbl_WorkMaster.Status      as WorkStatus,tm_AreaofWork.AreaofWork                
                            FROM tr_ProposedWorkAct      
                            inner join dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId     
                               inner join td_IPDetails on td_IPDetails.intIP= tbl_WorkMaster.IPid                      
                               inner join tm_WorkActivities on tm_WorkActivities.intWorkActivity=tr_ProposedWorkAct.intWorkActivity  
                               inner join tm_States on tbl_WorkMaster.intStateid=tm_States.intStateid  
                               inner join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid  
                               inner join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid  
                               inner  join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid  
                                 inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork
                            WHERE     tr_ProposedWorkAct.intAreaofWork like '1000' and tbl_WorkMaster.IPid like @intTST                     
                                                  
                              
                              
  
end  
else if(@status='2')  
begin  
SELECT  distinct  tr_ProposedWorkAct.*, tm_WorkActivities.WorkActivity, tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tbl_WorkMaster.WorkCode  as WorkCodeNew,tbl_WorkMaster.Status      as WorkStatus,tm_AreaofWork.AreaofWork                
                            FROM tr_ProposedWorkAct      
                            inner join dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId     
                               inner join td_IPDetails on td_IPDetails.intIP= tbl_WorkMaster.IPid                      
                               inner join tm_WorkActivities on tm_WorkActivities.intWorkActivity=tr_ProposedWorkAct.intWorkActivity  
                               inner join tm_States on tbl_WorkMaster.intStateid=tm_States.intStateid  
                               inner join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid  
                               inner join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid  
                               inner  join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid  
                                   inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork
                            WHERE     tr_ProposedWorkAct.intAreaofWork like '1001' and tbl_WorkMaster.IPid like @intTST                     
    
  
end  
  
else if(@status='3')  
begin  
SELECT    tr_ProposedWorkAct.*, tm_WorkActivities.WorkActivity, tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tbl_WorkMaster.WorkCode  as WorkCodeNew,tbl_WorkMaster.Status      as WorkStatus,tm_AreaofWork.AreaofWork                
                            FROM tr_ProposedWorkAct      
                            inner join dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId     
                               inner join td_IPDetails on td_IPDetails.intIP= tbl_WorkMaster.IPid                      
                               inner join tm_WorkActivities on tm_WorkActivities.intWorkActivity=tr_ProposedWorkAct.intWorkActivity  
                               inner join tm_States on tbl_WorkMaster.intStateid=tm_States.intStateid  
                               inner join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid  
                               inner join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid  
                               inner  join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid  
                                   inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork
                            WHERE     tr_ProposedWorkAct.intAreaofWork like '1002' and tbl_WorkMaster.IPid like @intTST                     
  
  
end
else if(@status='4')  
begin  
SELECT    tr_ProposedWorkAct.*, tm_WorkActivities.WorkActivity, tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,tbl_WorkMaster.WorkCode  as WorkCodeNew,tbl_WorkMaster.Status      as WorkStatus,tm_AreaofWork.AreaofWork                
                            FROM tr_ProposedWorkAct      
                            inner join dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId     
                               inner join td_IPDetails on td_IPDetails.intIP= tbl_WorkMaster.IPid                      
                               inner join tm_WorkActivities on tm_WorkActivities.intWorkActivity=tr_ProposedWorkAct.intWorkActivity  
                               inner join tm_States on tbl_WorkMaster.intStateid=tm_States.intStateid  
                               inner join tm_Counties on tbl_WorkMaster.intCountieid=tm_Counties.intCountieid  
                               inner join tm_Payams on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid  
                               inner  join tm_Bomas on tbl_WorkMaster.intBomasid=tm_Bomas.intBomasid  
                                   inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork
                            WHERE     tr_ProposedWorkAct.intAreaofWork like '1003' and tbl_WorkMaster.IPid like @intTST                     
  
  
end  
end




