CREATE Procedure [dbo].[getQualificationbyID]
(
@intQualification varchar(10)

)
as begin

select * from dbo.tm_Qualification where intQualification like @intQualification

end




