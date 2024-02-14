-- Modified By Srikanth on 15-6-2012 for Searching View Help Desk    
CREATE Procedure [dbo].[sp_Gethelpd]      
(     
@fedid varchar(50),    
@statid varchar(10),    
@piaid varchar(6),    
@refno varchar(15)    
)    
As    
SELECT DISTINCT     
                      tm_Helpdesk.hd_Username, tm_Helpdesk.intPIAid, tm_Helpdesk.hd_Code, CONVERT(varchar, tm_Helpdesk.Created_dt, 106) AS regdate, ISNULL(CONVERT(varchar,     
                      tm_Helpdesk.Modified_dt, 106), '') AS respdate, tm_Helpdesk.intfb_id AS Expr2, tm_Helpdesk.hd_desc, tm_Helpdesk.Status, tm_Helpdesk.User_Type, tm_Helpdesk.Created_dt,    
                      tm_Helpdesk.Remarks, tm_Helpdesk.int_fbid, tm_Helpdesk.hd_uplddocname, tm_fbType.Fb_Type    
FROM         tm_fbType INNER JOIN    
                      tm_Helpdesk ON tm_fbType.intfb_id = tm_Helpdesk.intfb_id    
where tm_Helpdesk.intfb_id like @fedid and tm_Helpdesk.Status like @statid and tm_Helpdesk.created_by like @piaid and tm_Helpdesk.hd_Code like @refno order by tm_Helpdesk.Created_dt desc    
RETURN




