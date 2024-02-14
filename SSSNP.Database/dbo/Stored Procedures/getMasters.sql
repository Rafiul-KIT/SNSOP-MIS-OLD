create procedure getMasters

as
Begin

select 
s.intStateid,s.StateName,c.intCountieid,c.CountieName,p.intPayamid,p.PayamName,b.intBomasid,b.BomaName

 from tm_Bomas as b inner join tm_Payams as p on p.intPayamid=b.intPayamid inner join tm_Counties as c on c.intCountieid=p.intCountieid inner join tm_States as s on s.intStateid=c.intStateid
 order by s.StateName,c.CountieName,p.PayamName,b.BomaName
 End