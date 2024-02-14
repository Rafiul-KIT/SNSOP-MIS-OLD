CREATE Procedure [dbo].[sp_Gethelpdbyid]                           
(                      
@fedid varchar(50)                    
              
)                             
As                            
Begin                            
select tm_Helpdesk.*,tm_fbType.Fb_Type from tm_Helpdesk              
inner join tm_fbType on tm_fbType.intfb_id=tm_Helpdesk.intfb_id              
 where int_fbid like @fedid              
                      
End




