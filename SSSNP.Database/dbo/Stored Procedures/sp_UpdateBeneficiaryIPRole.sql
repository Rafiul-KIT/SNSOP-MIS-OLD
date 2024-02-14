


CREATE procedure [dbo].[sp_UpdateBeneficiaryIPRole]
  
@intBenid int ,	 
@NameofHousehold varchar(255),
@Age	varchar(20),
@Sex	varchar(20),
@Modified_by  int,
@component varchar(200)
as begin  

declare @oldComponent as varchar(200)
set @oldComponent = (select top 1 selectioncriteria from tbl_BeneficiaryDet Where intBenid = @intBenid)

  if @component=@oldComponent
	  begin
		Update tbl_BeneficiaryDet
			set NameofHousehold	= @NameofHousehold,
				Age = @Age,
				Sex = @Sex,
				Modified_by = @Modified_by ,
				Modified_dt = getdate()
			Where intBenid = @intBenid
		end
	else
		begin
		Update tbl_BeneficiaryDet
			set NameofHousehold	= @NameofHousehold,
				Age = @Age,
				Sex = @Sex,
				Modified_by = @Modified_by ,
				Modified_dt = getdate(),
				selectioncriteria=@component,
				WorkCode=null
			Where intBenid = @intBenid
			--Creation/update of record with latest component
			--exists at least a record with end_date = null: it must be ended and a new created
			declare @NewCompoID  as int
			set @NewCompoID =(select intNewCompoId from tbl_BeneficiaryDetNewComponent where intbenid=@intBenid)
			if @NewCompoID is not null
				begin
				--code to update record
					update tbl_BeneficiaryDetNewComponent  set end_date = GETDATE() where intNewCompoId = @NewCompoID
				end
			else
				begin
				--create new record with old value
					declare @startDate as datetime
					set @startDate = (select Created_dt from tbl_BeneficiaryDet where intBenid=@intBenid)
					insert into tbl_BeneficiaryDetNewComponent values 
					(@intBenid,@oldcomponent,@startdate, getdate(), @Modified_by, GETDATE())
				end
			--insertion of record in [tbl_BeneficiaryDetNewComponent]
			insert into tbl_BeneficiaryDetNewComponent values 
			(@intBenid,@component,getdate(), null, @Modified_by ,GETDATE())
			
		end
			 
end

