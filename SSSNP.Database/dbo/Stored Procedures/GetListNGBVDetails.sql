
CREATE procedure [dbo].[GetListNGBVDetails]    
@NGBVSerialNumber  varchar(15)                               
      
as begin       

select * from NonGBVDetails
	where NonGBVMainID = (select NonGBVMainID from NonGBVMain where SerialNumber=@NGBVSerialNumber )
               
END
