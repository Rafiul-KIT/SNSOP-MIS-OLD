CREATE Procedure [dbo].[getCouncilMems]  
(  
@type varchar(10)  
)  
as begin  
  
  if(@type='TST')
  begin
select intTST as intMemid, TstName as AuthorizedPerson from dbo.td_TstTeamDet 
  end
  else 
  
  begin
  select * from dbo.tr_CouncilMemDet where MemType like @type  
  end
end




