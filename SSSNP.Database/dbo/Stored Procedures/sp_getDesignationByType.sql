--created by siva as 30-11-2015    
--Purpose: to get Designation by Type in Dropdown    
    
CREATE Procedure [dbo].[sp_getDesignationByType]
(    
@Type varchar(10)    
)    
as begin    
    
select * from dbo.tm_Designations where Type like @Type    
    
end




