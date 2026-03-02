CREATE Procedure [dbo].[sp_Instfeedback]            
(      
@intPIAid int ,    
@hd_Username varchar(50) ,    
@intfb_id int ,    
@hd_desc varchar(900) ,     
@hd_uplddocname varchar(50),    
@hd_uplddoc image,    
@User_Type   varchar(5),    
@Created_by int,    
-- Modifed By Srikanth     
@generateid varchar(50) ,    
@anothermail varchar(50)    
            
)             
As            
Begin            
set nocount on           
SET XACT_ABORT ON          
      
     
insert into tm_Helpdesk         
(            
intPIAid ,    
hd_Username,    
intfb_id ,    
hd_desc ,    
hd_uplddocname ,    
hd_uplddoc ,    
User_Type ,    
Status,    
Created_by,          
Created_dt,    
hd_Code,    
anothermailid    
)            
Values            
(            
@intPIAid ,            
@hd_Username ,       
@intfb_id ,    
@hd_desc ,    
@hd_uplddocname ,    
@hd_uplddoc ,    
@User_Type ,    
'Open',    
@Created_by,    
getdate() ,    
 @generateid,    
 @anothermail    
)            
           
          
    
      
End




