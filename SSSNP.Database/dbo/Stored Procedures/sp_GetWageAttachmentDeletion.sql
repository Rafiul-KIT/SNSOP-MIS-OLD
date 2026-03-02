create procedure sp_GetWageAttachmentDeletion
@workCode varchar(120),
@intStateid varchar(8),
@intCountieid varchar(8),
@intPayamid varchar(8),
@intBomasid varchar(8) ,
@IPid varchar(8),
@ReqNo varchar(8)
as
begin
select  ip.IPName,s.StateName,c.CountieName,paym.PayamName,bom.BomaName, tbl_WorkMaster.WorkCode,

td_WorkWageRequest.ReqNo, CONVERT(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as FromDate, CONVERT(varchar(11), td_WorkWageRequest.ReqToDate, 106) as Todate,
tbl_WageAttachementsDeleteTrans.Description,tbl_WageAttachementsDeleteTrans.intImageTransId,tbl_WageAttachementsDeleteTrans.PhotoFileName,
CONVERT(varchar(11),tbl_WageAttachementsDeleteTrans.DeleteDate,106) as DeletedDate

from tbl_WageAttachementsDeleteTrans inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=tbl_WageAttachementsDeleteTrans.intWagePaymentReqid
inner join tbl_WorkMaster on tbl_WorkMaster.intworkid=td_WorkWageRequest.intWorkid
inner join    dbo.tm_Bomas AS bom ON bom.intBomasid = tbl_WorkMaster.intBomasid INNER JOIN
                         dbo.tm_Payams AS paym ON paym.intPayamid = bom.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = paym.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = tbl_WorkMaster.IPid
						 where 
						 
						  td_WorkWageRequest.ReqNo like '%'+@ReqNo+'%' and 
					bom.intBomasid like @intBomasid and paym.intPayamid like @intPayamid and c.intCountieid like @intCountieid
and s.intStateid like @intStateid and ip.intIP like @IPid  and tbl_WorkMaster.WorkCode like '%'+@workCode+'%' 

end