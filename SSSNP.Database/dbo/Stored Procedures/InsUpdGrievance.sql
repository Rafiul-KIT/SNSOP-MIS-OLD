
CREATE procedure  [dbo].[InsUpdGrievance]
   
(        
    @Grievance_RegistrationNo varchar(500) ,
    @intStateid int,
	@intCountieid  int ,
	@intPayamid  int ,
	@intBomasid  int ,
	@Complainant_Name  varchar(MAX)  ,
	@Complainant_EnrolmentNo  varchar(500)  ,
	@TypeOfComplaints  varchar(MAX) ,
	@DetailsComplainsReported  varchar(max)  ,
	@ComplaintRegisteredDate  date ,
	@Accused_Name  varchar (500) ,
	@Accused_EnrolmentNo  varchar(500)  ,
	@Accused_Complaints_Specification  varchar (max)  ,
	@Accused_ComplainsReferred  varchar(MAX) ,
	 @Created_by int,
	 @DocumentPath varchar(350),
	 @DocumentName varchar(250),
	@AddressedHow  varchar(MAX) ,
	@AddressedDate Datetime,
	  @Grievance_RegistrationNoOut varchar(500) OUTPUT
	    
)        
as begin  


 



	        
--if((select COUNT(*) from tbl_Grievance where Grievance_RegistrationNo=@Grievance_RegistrationNo )>0)
if(@Grievance_RegistrationNo !='')        
begin        
     
	
update tbl_Grievance set  intStateid=@intStateid, intCountieid=@intCountieid, intPayamid=@intPayamid,
intBomasid=@intBomasid,  Complainant_Name=@Complainant_Name,Complainant_EnrolmentNo=@Complainant_EnrolmentNo, TypeOfComplaints=@TypeOfComplaints,
DetailsComplainsReported=@DetailsComplainsReported, ComplaintRegisteredDate=@ComplaintRegisteredDate,Accused_Name=@Accused_Name, Accused_EnrolmentNo=@Accused_EnrolmentNo,
Accused_Complaints_Specification=@Accused_Complaints_Specification,Accused_ComplainsReferred=@Accused_ComplainsReferred, Modified_by=@Created_by, Modied_dt=GETDATE() ,
  DocumentName=@DocumentName,DocumentPath=@DocumentPath,AddressedDate=@AddressedDate,AddressedHow=@AddressedHow
   where Grievance_RegistrationNo=@Grievance_RegistrationNo 
  
      
set @Grievance_RegistrationNoOut=@Grievance_RegistrationNo  
     --select @Grievance_RegistrationNo 
    --return @Grievance_RegistrationNoOut
        
end        
else        
begin  
declare @Id int , @PreFix varchar(100),@Grievance_No varchar(500)
set @PreFix ='-PublicWorks-Grievance-' 
set  @Id = (select ISNULL(MAX(GrievanceId),0) + 1 FROM tbl_Grievance)
 set @Grievance_No = concat(DATEPART(YEAR,GETDATE()),@PreFix,RIGHT('00000' + CAST(@Id AS VARCHAR(5)), 5)) 


insert into tbl_Grievance (Grievance_RegistrationNo, intStateid, intCountieid, intPayamid,intBomasid,  Complainant_Name,Complainant_EnrolmentNo, TypeOfComplaints,DetailsComplainsReported, ComplaintRegisteredDate,Accused_Name, Accused_EnrolmentNo,Accused_Complaints_Specification,Accused_ComplainsReferred, Created_by, Created_dt,DocumentPath,DocumentName,AddressedHow,AddressedDate)        
values(@Grievance_No, @intStateid, @intCountieid, @intPayamid,@intBomasid, @Complainant_Name,@Complainant_EnrolmentNo,@TypeOfComplaints,@DetailsComplainsReported,@ComplaintRegisteredDate,@Accused_Name,@Accused_EnrolmentNo,@Accused_Complaints_Specification,@Accused_ComplainsReferred,@Created_by, GETDATE(),@DocumentPath,@DocumentName,@AddressedHow,@AddressedDate)        
          
set @Grievance_RegistrationNoOut = @Grievance_No    
--return @@Grievance_RegistrationNoOut      
end        
end




