CREATE Procedure [dbo].[GetBenPaidAmt]  
(  
@Month varchar(10),  
@Year varchar(10),  
@intBenid varchar(10)  
)  
as begin  
select isnull( SUM(PaidAmount),0) as Amt from tr_BenWagesPayment where intBenid like @intBenid and Year =@Year and Month=@Month  
  
end




