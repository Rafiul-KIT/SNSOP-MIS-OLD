CREATE Procedure  [dbo].[getStatesSearch]  
(  
@StateName varchar(50),  
@StateCode varchar(50)  
)  
as begin  
  
select * from tm_States where StateCode like @StateCode and StateName like '%'+ @StateName  +'%'  
  
  
  
  
end




