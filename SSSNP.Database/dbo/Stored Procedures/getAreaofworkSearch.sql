CREATE Procedure [dbo].[getAreaofworkSearch]        
(        
       
@AreaofWork varchar(50)         
    
)        
as begin        
        
SELECT   intAreaofWork, AreaofWork    
FROM         tm_AreaofWork  where AreaofWork like @AreaofWork      
                            
                              
                              
end




