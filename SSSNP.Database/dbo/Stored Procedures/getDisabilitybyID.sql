CREATE Procedure [dbo].[getDisabilitybyID]      
(      
@intDisability varchar(10)      
)      
as begin      
      
SELECT      intDisability,Disability,Created_by,Created_dt  
FROM      dbo.tm_Disability    where intDisability like @intDisability      
      
end




