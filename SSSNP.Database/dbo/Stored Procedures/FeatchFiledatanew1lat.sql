CREATE Procedure [dbo].[FeatchFiledatanew1lat]      
(      
@intbenid varchar(10),  
@Attendancedate datetime      
)      
as begin      
select * from tr_BenWagesPayment where intBenid like @intbenid  and    
(@Attendancedate between FromDate and ToDate)  
 --(convert(varchar,cast(@Attendancedate as datetime),103)   
--between  convert(varchar,cast( FromDate as datetime),103) and convert(varchar,cast(ToDate as datetime),103)     )  
end




