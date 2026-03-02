create procedure [dbo].[insertWorkComment]
(
@IntWorkid	int,
@Comment varchar(200), 
@setStatus varchar(25),
@Created_by int
)

As
Begin
INSERT INTO [dbo].[tbl_WorkComments]
           ([intWorkid]
           ,[Comment]
           ,[SetStatus]
           ,[Created_by]
           ,[Created_dt])
     VALUES
           (@IntWorkid
           ,@Comment
           ,@setStatus
           ,@Created_by
           ,Getdate())

End