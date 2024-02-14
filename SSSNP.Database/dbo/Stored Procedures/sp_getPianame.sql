CREATE Procedure [dbo].[sp_getPianame]      
(    
@intPIAid int    
)    
As      
begin    
select * from td_TstTeamDet where intTST =@intPIAid    
end




