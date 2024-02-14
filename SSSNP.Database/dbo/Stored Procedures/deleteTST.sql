--created by Suresh as on 30-11-2015      
--Purpose : to delete Bomas by id      
CREATE Procedure [dbo].[deleteTST]      
              
(              
@id varchar(50)              
)              
as begin              
        
              
delete from dbo.td_TstTeamDet  where intTST like @id              
           
   select 100        
   return 100        
              
              
end




