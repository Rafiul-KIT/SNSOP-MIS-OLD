CREATE procedure [dbo].[GetEnrollmentNoByintBenid1] -- GetEnrollmentNoByintBenid1'%','%','1009','%','%','%'
@IP varchar(10),
@project varchar(10),
@state varchar(10),
@country varchar(10),
@Payam varchar(10),
@Boma varchar(10)
as 
begin 

select be.*,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName from tbl_WorkMaster be
 inner join tm_States on tm_States.intStateid=be.intStateid      
inner join tm_Counties  on tm_Counties.intCountieid=be.intCountieid      
inner join tm_Payams on tm_Payams.intPayamid=be.intPayamid      
inner join tm_Bomas on tm_Bomas.intBomasid=be.intBomasid

 where IPid like @IP and be.intStateid like @state and be.intCountieid like @country and be.intPayamid like @Payam and be.intBomasid like @Boma
end


