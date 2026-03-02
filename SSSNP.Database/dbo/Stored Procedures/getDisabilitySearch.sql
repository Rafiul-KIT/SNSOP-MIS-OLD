CREATE Procedure [dbo].[getDisabilitySearch]          
(          
         
@Disability varchar(50)           
      
)          
as begin          
          
SELECT   intDisability, Disability      
FROM         tm_Disability  where Disability like @Disability        
                              
                                
                                
end




