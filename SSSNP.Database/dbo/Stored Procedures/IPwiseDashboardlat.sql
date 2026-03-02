
CREATE Procedure [dbo].[IPwiseDashboardlat]  
  
@id varchar(8)  
as begin  

-- Modified by: Srikanth
-- Modified on : 11-01-2018
-- Purpose : Resolve - Run time error while multiple project haveing in IP which is done by samiran team
  
select * from Vw_Databoards where intIP like @id
  
end
