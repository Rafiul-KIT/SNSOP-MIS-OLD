CREATE procedure [dbo].[getBenfWorkDetailsService]
As
Begin
select b.intBenid, b.EnrollmentNo,b.household_number,w.intWorkid,w.WorkCode,Convert(varchar(11),bw.Created_dt,103) as DateofCreation,w.intStateid,w.intCountieid,w.intPayamid,w.intBomasid from tr_BenfWorkDet as bw inner join tbl_BeneficiaryDet as b on b.intBenid=bw.intBenid inner join tbl_WorkMaster as w on w.intWorkid=bw.intWorkid  where  b.household_number is not null

End