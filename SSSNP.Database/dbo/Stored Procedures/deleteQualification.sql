CREATE Procedure [dbo].[deleteQualification]      
      
(      
@id varchar(50)      
)      
as begin      

      
delete from dbo.tm_Qualification  where intQualification like @id      
   
   select 100
   return 100
      
      
end




