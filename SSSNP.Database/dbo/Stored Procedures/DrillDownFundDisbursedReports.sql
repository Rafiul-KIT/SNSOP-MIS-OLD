--exec DrillDownFundDisbursedReports '1080','7'
CREATE Procedure [dbo].[DrillDownFundDisbursedReports]          
@intIP varchar(10),          
@status Varchar(20)          
          
as begin          
if(@status='1')          
begin          
          
          
                 SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   td_IPDetails.intIP like @intIP and tbl_WorkMaster.Status like '%'            
     end          
     else if(@status='2')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   td_IPDetails.intIP like @intIP and (tbl_WorkMaster.Status <> 'New Proposal' or tbl_WorkMaster.Status <>'Rejected')          
               
     end                                                     
               
     else if(@status='3')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName  
                
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   td_IPDetails.intIP like @intIP and (tbl_WorkMaster.Status like 'New Proposal' )                                
               
     end          
       
     else if(@status='89')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   td_IPDetails.intIP like @intIP and (tbl_WorkMaster.Status like 'Rejected' )                                
               
     end          
               
 else if(@status='4')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   td_IPDetails.intIP like @intIP and (tbl_WorkMaster.Status  like 'Inprogress' or tbl_WorkMaster.Status  like 'Invoice Appovals'  or tbl_WorkMaster.Status  like 'Invoice Request'  or tbl_WorkMaster.Status like 'Approved' 
)                                
                --((SELECT     COUNT(*) AS Expr1                                
                --            FROM          dbo.tbl_WorkMaster          
                --            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)) -    (SELECT  COUNT(*) AS Expr1                                
                --            FROM          dbo.tbl_WorkMaster                           
                --            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and (tbl_WorkMaster.Status like 'Inprogress'))                                
                --                )                            
                --              AS TotalInprogress ,                
                          
               
     end          
     else if(@status='5')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   td_IPDetails.intIP like @intIP and (tbl_WorkMaster.Status like 'Inprogress' )                                
               
     end        
             
             
     else if(@status='6')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Counties.intCountieid like @intIP                         
               
     end        
             
     else if(@status='7')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Counties.intCountieid like @intIP   and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Invoice Appovals' or  tbl_WorkMaster.Status like 'Invoice Request' or tbl_WorkMaster.Status like 'Approved' )                            
               
     end        
               
     else if(@status='8')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Payams.intPayamid like @intIP           
               
     end        
     else if(@status='9')          
begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Payams.intPayamid like @intIP  and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Invoice Appovals' or tbl_WorkMaster.Status like 'Invoice Request' or tbl_WorkMaster.Status like 'Approved' ) 
               
     end        
             
   else if(@status='10')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   td_IPDetails.intIP like @intIP and (tbl_WorkMaster.Status like 'Completed' )                                
               
     end        
           
    else if(@status='11')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Counties.intCountieid like @intIP and (tbl_WorkMaster.Status like 'New Proposal' )                                
               
     end      
           
     else if(@status='12')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Counties.intCountieid like @intIP and (tbl_WorkMaster.Status like 'Completed' )                                
               
     end   
     else if(@status='90')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Counties.intCountieid like @intIP and (tbl_WorkMaster.Status like 'Rejected' )                                
               
     end   
          
               
                                                          
                                        
   else if(@status='13')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Payams.intPayamid like @intIP and (tbl_WorkMaster.Status like '%' )                                
               
     end      
       
       
   else if(@status='14')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Payams.intPayamid like @intIP and (tbl_WorkMaster.Status like 'New Proposal' )                                
               
     end      
       
   else if(@status='15')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Payams.intPayamid like @intIP and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Invoice Appovals' or  tbl_WorkMaster.Status like 'Invoice Request' or tbl_WorkMaster.Status like 'Approved' ) 
                           
               
     end      
       
       
   else if(@status='16')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Payams.intPayamid like @intIP and (tbl_WorkMaster.Status  like 'Completed' )                                
               
     end      
     
   else if(@status='91')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Payams.intPayamid like @intIP and (tbl_WorkMaster.Status  like 'Rejected' )                                
               
     end      
     
     
   else if(@status='17')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Bomas.intBomasid like @intIP and (tbl_WorkMaster.Status  like '%' )                                
               
     end      
     
   else if(@status='18')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Bomas.intBomasid like @intIP and (tbl_WorkMaster.Status  like 'New Proposal' )                                
               
     end      
     
   else if(@status='19')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Bomas.intBomasid like @intIP  and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Invoice Appovals' or  tbl_WorkMaster.Status like 'Invoice Request' or tbl_WorkMaster.Status like 'Approved' ) 
                          
               
     end      
     
   else if(@status='20')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                                             
                            WHERE   tm_Bomas.intBomasid like @intIP and (tbl_WorkMaster.Status  like 'Completed' )                                
               
     end      
     
   else if(@status='93')          
     begin          
     SELECT  tbl_WorkMaster.*,td_IPDetails.IPName,td_IPDetails.AuthorisedPerson,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName            
        FROM          dbo.tbl_WorkMaster inner join td_IPDetails on  tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP       left outer join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid          
inner join tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid          
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid          
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid          
                         
                                        
                            WHERE   tm_Bomas.intBomasid like @intIP and (tbl_WorkMaster.Status  like 'Rejected' )                                
               
     end      
     
     
                        
          
          
          
          
end




