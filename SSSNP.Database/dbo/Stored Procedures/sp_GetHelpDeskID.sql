CREATE Procedure [dbo].[sp_GetHelpDeskID]      
    
(    
@PIA int    
)    
      
As      
Begin      
    
     
select top 1 hd_Code from tm_Helpdesk where intPIAid=@PIA order by int_fbid desc    
      
end




