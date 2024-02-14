CREATE Procedure [dbo].[getBenWorkDetailsbyBenid]
(
@intBenid varchar(8)
)
As
-- Project : SNSDP Public Works
-- Purpose : View Works for Benficiary
-- Created by : Srikath
-- Created date: 28-02-2017
Begin

select w.WorkCode,w.Status from tbl_WorkMaster as w inner join tr_BenfWorkDet as b on b.intWorkid=w.intWorkid where b.intBenid like @intBenid order by w.WorkCode

End



