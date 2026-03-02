CREATE Procedure [dbo].[InsUpdBeneficiaryWages]    
(   
    
@intBenid int,    
@Year varchar(5),    
@Month varchar(10),    
@PaidDate datetime,    
@PaidAmount decimal(18, 2),    
@Created_by int,
@Wagerate varchar(10) ,   
  @intWorkid  varchar(8) 
     
)    
    
as begin    
    
       
if((select COUNT(*) from tr_BenWagesPayment where intBenid=@intBenid and Year=@Year and MONTH=@Month)>0)        
begin    
  
update  tr_BenWagesPayment set Year=@Year, Month=@Month, PaidDate=@PaidDate, PaidAmount=@PaidAmount,Wagerate=@Wagerate,Modified_by=@Created_by,Modified_dt=GETDATE(),intWorkid=@intWorkid   where intBenid like @intBenid  and Year=@Year and MONTH=@Month
select 1000        
return 1000        
end        
else        
begin        
insert into tr_BenWagesPayment(intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt,Wagerate,intWorkid)        
values(@intBenid, @Year, @Month, @PaidDate, @PaidAmount, @Created_by,  GETDATE(),@Wagerate,@intWorkid)        
select 1000        
return 1000        
end        
        
    
    
end




