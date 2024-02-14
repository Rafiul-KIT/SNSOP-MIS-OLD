CREATE Procedure [dbo].[getDesignationSearch]                
(                
               
@Designation varchar(50),  
@Type varchar(1)  
            
)                
as begin                
                
SELECT   intDesgid, Designation , case when Type='N' then 'National Level Team' else case when Type='S' then 'State Level Team' else 'County Level Team' end end as Type          
FROM         tm_Designations  where Designation like '%'+@Designation+'%' and Type like @Type              
                                    
                                      
                                      
end




