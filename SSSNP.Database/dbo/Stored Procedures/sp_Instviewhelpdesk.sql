CREATE Procedure [dbo].[sp_Instviewhelpdesk]              
(        
@ids int,      
@Status varchar(10),      
@Remarks varchar(250),          
@Created_by int        
          
              
)               
As              
Begin              
set nocount on             
SET XACT_ABORT ON            
        
       
insert into tr_Helpdesk            
(        
int_fdid,          
Status ,      
Remarks,      
Created_by,            
Created_dt      
)              
Values              
(        
@ids,        
@Status ,              
@Remarks,         
@Created_by,      
getdate()        
)              
             
            
      
        
End




