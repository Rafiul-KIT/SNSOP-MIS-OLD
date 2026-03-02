CREATE Procedure [dbo].[InsertFieldMonitoring]
(

@WorkId	int,
@DateofVisit	datetime,
@OutCome	varchar(50),
@Remarks	varchar(150),
@Photo	image,
@PhotFilename	varchar(50),
@Photo2	image,
@PhotoFilename2	varchar(50),
@Photo3	image,
@Photo3Filename	varchar(50),
@Created_by	int
	
)
as begin

insert into tbl_PhysicalMonitoringDet (WorkId, DateofVisit, OutCome, Remarks, Photo, PhotFilename, Photo2, PhotoFilename2, Photo3, Photo3Filename, Created_by, Created_dt)
values(@WorkId, @DateofVisit, @OutCome, @Remarks, @Photo, @PhotFilename, @Photo2, @PhotoFilename2, @Photo3, @Photo3Filename, @Created_by, GETDATE())
select 1000
return 1000

end




