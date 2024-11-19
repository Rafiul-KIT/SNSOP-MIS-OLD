

create procedure confirmAllPendingBeneficiary as
    begin
DECLARE @id int

DECLARE cur CURSOR FOR SELECT intBenTempId FROM tbl_BeneficiaryServiceDet where 1=1 and BenStatus=1
OPEN cur

FETCH NEXT FROM cur INTO @id

WHILE @@FETCH_STATUS = 0 BEGIN
    EXEC UpdateBenficiariesEnrollment @id
    FETCH NEXT FROM cur INTO @id
END

CLOSE cur
DEALLOCATE cur

end;



confirmAllPendingBeneficiary


2...................


insert into td_ProjectDet(intIP, PrjName, StartDate, EndDate, PrjSanctionDate, TargetBeneficiary,
                          ProjectCost, PrjInchargeName, PrjInchargeMobile, PrjInchargeEmail, Status, Created_by, Created_dt)
values(1001,'Project For Auto Payment by Firm','2024-10-01 00:00:00.000','2024-10-31 00:00:00.000','2024-10-13 00:00:00.000', '100000',
    10000000, 'Mr Incharge', '0912345678','southsudantgov@gmail.com','InProgress',1306,GETDATE());

insert into tr_ProjectTarget (intPrjid, intStateid, intCountieid, intPayamid, intBomasid, Proposedtarget, intAreaofWork, intWorkActivity, Created_by, Created_dt)
select 1251, s.intStateid, c.intCountieid, 0, 0, '', 0, 0, 1306, GETDATE()
from tbl_BeneficiaryDet b
inner join tm_States s on b.intStateid = s.intStateid
inner join tm_Counties c on b.intCountieid = c.intCountieid
where b.WorkCode is null and b.SelectionCriteria not like '%direct%'
group by s.intStateid, c.intCountieid;

create procedure CreateWorkProposalAsApproved
(
    @IPid int,
    @intPrjid int,
    @MeetingDate datetime,
    @intStateid int,
    @intCountieid int,
    @intPayamid int,
    @intBomasid int,
    @Status varchar(20),
    @Created_by int,
    @PDC_ApprvDate datetime,
    @StatusDate datetime,
    @Remarks varchar(150),
    @TypeofMeeting varchar(3),
    @screening_status tinyint
) as
begin
    exec InsertUpdWorkProposal null,null,@IPid,@intPrjid,@MeetingDate,@intStateid,
    @intCountieid,@intPayamid,@intBomasid,@Status,@Created_by,
    @PDC_ApprvDate,@StatusDate,@Remarks,@TypeofMeeting,@screening_status
    Declare @ids as int
    set @ids=@@IDENTITY
    update tbl_WorkMaster
    set WorkCode=
        (select concat(YEAR(GETDATE()), '-SNSOP-', (select PrjName from td_ProjectDet where intPrjid=@intPrjid), '-',@ids))
    where intWorkid = @ids

end;
go


begin transaction
DECLARE @intStateId int, @intCountyId int, @intPayamId int, @intBomaId int

DECLARE cur CURSOR FOR select distinct intStateid, intCountieid, intPayamid, intBomasid from tbl_BeneficiaryDet b where b.WorkCode is null and b.SelectionCriteria not like '%direct%' 
OPEN cur

FETCH NEXT FROM cur INTO @intStateId, @intCountyId, @intPayamId, @intBomaId

WHILE @@FETCH_STATUS = 0 BEGIN
    EXEC CreateWorkProposalAsApproved 1001, 1251, '2024-10-13 00:00:00.000',
         @intStateId, @intCountyId, @intPayamId, @intBomaId,
       'Approved', 1306, '2024-10-10 00:00:00.000', '2024-10-13 00:00:00.000', 'Auto Approved from backend', 'CC', 1
    FETCH NEXT FROM cur INTO @intStateId, @intCountyId, @intPayamId, @intBomaId
END

CLOSE cur
DEALLOCATE cur
commit;


3. 

create procedure LinkBeneficiaryToSubproject
(
    @WorkStartDate datetime,
    @Created_by int,
    @Incharge varchar(50)
) as
begin
    DECLARE @intWorkid int, @intStateId int, @intCountyId int, @intPayamId int, @intBomaId int
    DECLARE cur CURSOR FOR
        select intWorkid, intStateid, intCountieid, intPayamid, intBomasid
        FROM tbl_WorkMaster where Status='Approved';
    OPEN cur

    FETCH NEXT FROM cur INTO @intWorkid, @intStateId, @intCountyId, @intPayamId, @intBomaId

    WHILE @@FETCH_STATUS = 0
        BEGIN
            begin transaction
                insert into tbl_WorkProgress (Incharge, intWorkid, WorkStartDate, Status, Remarks,  Created_by, Created_dt)
                values(@Incharge,@intWorkid, @WorkStartDate, 'InProgress', '',  @Created_by, GETDATE());
                update tbl_WorkMaster set Status='InProgress' where intWorkid=@intWorkid;
                insert into tr_BenfWorkDet(intBenid, intWorkid, Created_by, Created_dt)
                select intBenid, @intWorkid, @Created_by, GETDATE() from tbl_BeneficiaryDet
                where intStateid=@intStateId and intCountieid=@intCountyId and intPayamid=@intPayamId
                  and intBomasid=@intBomaId and WorkCode is null and SelectionCriteria <> 'Component 2 (Direct Income Support)';
                update tbl_BeneficiaryDet set WorkCode=@intWorkid,Modified_by=@Created_by,Modified_dt=GETDATE()
                where intStateid=@intStateId and intCountieid=@intCountyId and intPayamid=@intPayamId
                  and intBomasid=@intBomaId and WorkCode is null and SelectionCriteria <> 'Component 2 (Direct Income Support)';
            commit;
            FETCH NEXT FROM cur INTO @intWorkid, @intStateId, @intCountyId, @intPayamId, @intBomaId
        END

    CLOSE cur
    DEALLOCATE cur
end;
go

LinkBeneficiaryToSubproject '2024-11-07', 1306, 'Fahim';

----------------------


create procedure CreateWageRequestAsFinSpecApproved (
    @Exchangerateattime decimal(18, 4),
    @PaymentCycle int,
    @WageRateUSD decimal(18, 2),
    @ReqYear varchar(5),
    @ReqMonth varchar(10),
    @Component varchar(255),
    @ReqFromDate datetime,
    @ReqToDate datetime,
    @TotalAttendance int
) as
begin
    Declare @intCoOrdinatorid as int;
    set @intCoOrdinatorid = (select top 1 intUserid from tbl_Users where User_type = 'Cordinator' and Status = 'Y');
    Declare @intFinSpecid as int;
    set @intFinSpecid = (select top 1 intUserid from tbl_Users where User_type = 'Specialist' and Status = 'Y');
    Declare @intManagerid as int;
    set @intManagerid = (select top 1 intUserid from tbl_Users where User_type = 'Manager' and Status = 'Y');

    DECLARE @intWorkid int;

    DECLARE cur CURSOR FOR
        select tWP.intWorkid
        from tbl_WorkProgress tWP
                 left join td_WorkWageRequest tWWR on tWP.intWorkid = tWWR.intWorkid
        where tWWR.intWagePaymentReqid is null
          and tWP.Status = 'InProgress';
    OPEN cur

    FETCH NEXT FROM cur INTO @intWorkid;

    WHILE @@FETCH_STATUS = 0
        BEGIN
            begin transaction
                insert into td_WorkWageRequest
                (intWorkid, WageRateUSD, ReqFromDate, ReqToDate, ReqYear, ReqMonth, intCoOrdinatorid, intSpecialistid,
                 intManagerid,
                 Exchangerateattime, Status, Remarks, Created_by, Created_dt, Modified_by, Modified_dt,
                 ExchangeRateOLD, CApproveDate, SApproveDate, MApproveDate, PaymentCycle, AmountInSSP, Component)
                select @intWorkid,
                       @WageRateUSD,
                       @ReqFromDate,
                       @ReqToDate,
                       @ReqYear,
                       @ReqMonth,
                       @intCoOrdinatorid,
                       @intFinSpecid,
                       @intManagerid,
                       @Exchangerateattime,
                       3,
                       'Auto approve wage request',
                       1306,
                       GETDATE(),
                       @intFinSpecid,
                       GETDATE(),
                       51.45,
                       GETDATE(),
                       GETDATE(),
                       GETDATE(),
                       @PaymentCycle,
                       @Exchangerateattime * @WageRateUSD,
                       @Component;

                Declare @ids as int
                set @ids = @@IDENTITY

                Update td_WorkWageRequest
                set ReqNo= convert(varchar, YEAR(getdate())) + '-SNSOPWR-' + convert(varchar, @ids)
                where intWagePaymentReqid = @ids

                insert into td_WageReqBenDet(intWagePaymentReqid, intWorkid, intBenid, TotAttendance,
                                             WageRateUSD, WageRateUSDTotal, Status,
                                             Created_by, Created_dt, ExchangeRateOLD, ExchangeRateNew)
                select @ids,
                       @intWorkid,
                       intBenid,
                       @TotalAttendance,
                       @WageRateUSD,
                       @WageRateUSD * @TotalAttendance,
                       'Request',
                       1306,
                       GETDATE(),
                       51.45,
                       @Exchangerateattime
                from tr_BenfWorkDet
                where intWorkid = @intWorkid;
            commit;
            FETCH NEXT FROM cur INTO @intWorkid;
        end;
end;
go

CreateWageRequestAsFinSpecApproved 130.26,1,2.7,
'2024','10',
'Component 1 (Public Works)','2024-10-01 00:00:00.000',
'2024-10-31 00:00:00.000',20;


