CREATE Procedure [dbo].[getIPSearch]                
(                
               
@IPName varchar(50),                  
@TypeofIP varchar(5),    
@Created_by varchar(5)       
)                
as begin                
                
SELECT   intIP, IPName,case when TypeofIP='P' then 'Private' else Case when TypeofIP='N' then 'NGO' else '' end end as TypeofIP  ,AuthorisedPerson,ContactNo,SecondaryContactNo,Email,[Address],Website          
FROM         td_IPDetails  where TypeofIP like @TypeofIP and IPName like '%'+@IPName +'%'    
and intTST like @Created_by
                                    
                                      
                                      
end




