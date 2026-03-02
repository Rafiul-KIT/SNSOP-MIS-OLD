CREATE Procedure [dbo].[CountywiseReport]                            
                            
(                            
@intTST varchar(15),                            
@intIP  varchar(15)                           
)                            
as begin          
          
          
SELECT *
                                              
FROM                                     
                    Vw_CountrywiseReportLat  as c    
                                
                       where  c.intIP like @intIP                
  end




