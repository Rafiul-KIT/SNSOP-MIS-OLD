--created by suresh as on 1-19-16 for delete council memdetails  
CREATE Procedure [dbo].[DeleteCouncilMemsInvolved]    
(    
@intMemid varchar(10)    
)    
as begin    
    
delete from dbo.tr_CouncilMemDet where intMemid like @intMemid  
    
    
end




