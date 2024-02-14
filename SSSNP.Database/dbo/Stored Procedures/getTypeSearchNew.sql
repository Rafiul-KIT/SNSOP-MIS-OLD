CREATE Procedure [dbo].[getTypeSearchNew]                        
(                          
@Type varchar(30) ,  
@intStateid varchar(30),  
@intCountieid varchar(30),  
 @intPayamid varchar(30),  
 @intBomasid varchar(30),
 @ipName varchar(100)                            
)                          
as      
if(@Type='S')      
 begin      
                                               
 SELECT * from tm_States       
 where intStateid like @intStateid      
       
 end   
 else if(@Type='C')      
       
 begin      
              
   SELECT tm_Counties.intCountieid,tm_Counties.CountieName,tm_States.StateName                  
          FROM  tm_Counties inner join tm_States ON tm_Counties.intStateid = tm_States.intStateid      
           where intCountieid like  @intCountieid      
            
  end                         
 else if(@Type='P')      
 begin       
               
SELECT  tm_Payams.intPayamid,tm_Payams.PayamName,tm_Counties.CountieName                  
       FROM  tm_Payams inner join tm_Counties ON tm_Payams.intCountieid = tm_Counties.intCountieid       
       where intPayamid like  @intPayamid      
             
  end                            
                            
else if(@Type='B')      
 begin       
                                  
   SELECT  tm_Bomas.intBomasid,tm_Bomas.BomaName,tm_Payams.PayamName                  
        FROM  tm_Bomas inner join tm_Payams ON tm_Bomas.intPayamid = tm_Payams.intPayamid       
        where intBomasid like  @intBomasid      
                      
 end     
else if(@Type='IP')      
 begin      
                                               
 SELECT * from td_IPDetails       
 where IPName like @ipName      
       
 end




