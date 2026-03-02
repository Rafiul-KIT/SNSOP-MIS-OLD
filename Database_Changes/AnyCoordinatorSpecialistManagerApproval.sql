-- Initial Reviewer, Financial Approver and Final Payment Authorizer: Wages Dashboard

CREATE procedure [dbo].[getWageProcessDashboardKIT](
    @intOfficerid varchar(8),
    @OfficerLevel varchar(20)
)
As
Begin
    if (@OfficerLevel = 'Cordinator')
        Begin

            select (select count(intWagePaymentReqid)
                    from td_WorkWageRequest
                    where 1 = 1
--                                                                                and intCoOrdinatorid like @intOfficerid
                      and td_WorkWageRequest.Status > 0
                      and td_WorkWageRequest.intWorkid is not null)                         as TotalReq,
                   (select count(intWagePaymentReqid)
                    from td_WorkWageRequest
                    where 1 = 1
--                     and intCoOrdinatorid like @intOfficerid
                      and (Status = 1 or Status = 101)
                      and td_WorkWageRequest.intWorkid is not null)                         as TotalReqPending,
                   (select count(intWagePaymentReqid)
                    from td_WorkWageRequest
                    where 1 = 1
--                     and intCoOrdinatorid like @intOfficerid
                      and Status >= 2
                      and Status <> 100
                      and td_WorkWageRequest.intWorkid is not null)                         as TotalReqAprroved,
                   (select count(intWagePaymentReqid)
                    from td_WorkWageRequest
                    where 1 = 1
--                     and intCoOrdinatorid like @intOfficerid
                      and Status = 100
                      and td_WorkWageRequest.intWorkid is not null)                         as TotalReqRejected,

                   (select count(intWagePaymentReqPAid)
                    from td_WorkWageRequestPA
                    where 1 = 1
--                     and intCoOrdinatorid like @intOfficerid
                      and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                       from td_WorkWageRequest as a
                                                                       where 1 = 1
--                                                                            and a.intCoOrdinatorid like @intOfficerid
                                                                         and a.Status = 6)) as TotalReqPay,
                   (select count(intWagePaymentReqPAid)
                    from td_WorkWageRequestPA
                    where 1 = 1
--                     and intCoOrdinatorid like @intOfficerid
                      and (Status = 6 or Status = 1001)
                      and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                       from td_WorkWageRequest as a
                                                                       where 1 = 1
--                                                                            and a.intCoOrdinatorid like @intOfficerid
                                                                         and a.Status = 6)) as TotalReqPendingPay,
                   (select count(intWagePaymentReqPAid)
                    from td_WorkWageRequestPA
                    where 1 = 1
--                     and intCoOrdinatorid like @intOfficerid
                      and Status >= 7
                      and (Status <> 1000)
                      and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                       from td_WorkWageRequest as a
                                                                       where 1 = 1
--                                                                            and a.intCoOrdinatorid like @intOfficerid
                                                                         and a.Status = 6)) as TotalReqAprrovedPay,
                   (select count(intWagePaymentReqPAid)
                    from td_WorkWageRequestPA
                    where 1 = 1
--                     and intCoOrdinatorid like @intOfficerid
                      and Status = 1000
                      and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                       from td_WorkWageRequest as a
                                                                       where 1 = 1
--                                                                            and a.intCoOrdinatorid like @intOfficerid
                                                                         and a.Status = 6)) as TotalReqRejectedPay

        End
    else
        if (@OfficerLevel = 'Specialist')
            Begin

                select (select count(intWagePaymentReqid)
                        from td_WorkWageRequest
                        where 1 = 1
--                             and intSpecialistid like @intOfficerid
                          and Status >= 2
                          and Status <> 100
                          and td_WorkWageRequest.intWorkid is not null)                         as TotalReq,
                       (select count(intWagePaymentReqid)
                        from td_WorkWageRequest
                        where 1 = 1
--                             and intSpecialistid like @intOfficerid
                          and (Status = 2 or Status = 102)
                          and td_WorkWageRequest.intWorkid is not null)                         as TotalReqPending,
                       (select count(intWagePaymentReqid)
                        from td_WorkWageRequest
                        where 1 = 1
--                             and intSpecialistid like @intOfficerid
                          and Status >= 3
                          and (Status <> 100 and Status <> 101)
                          and td_WorkWageRequest.intWorkid is not null)                         as TotalReqAprroved,
                       (select count(intWagePaymentReqid)
                        from td_WorkWageRequest
                        where 1 = 1
--                             and intSpecialistid like @intOfficerid
                          and Status = 101
                          and td_WorkWageRequest.intWorkid is not null)                         as TotalReqRejected,
                       (select count(intWagePaymentReqPAid)
                        from td_WorkWageRequestPA
                        where 1 = 1
--                             and intSpecialistid like @intOfficerid
                          and Status >= 7
                          and Status <> 1000
                          and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                           from td_WorkWageRequest as a
                                                                           where 1 = 1
--                                                                                and a.intSpecialistid like @intOfficerid
                                                                             and a.Status = 6)) as TotalReqPay,
                       (select count(intWagePaymentReqPAid)
                        from td_WorkWageRequestPA
                        where 1 = 1
--                             and intSpecialistid like @intOfficerid
                          and (Status = 7 or Status = 1002)
                          and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                           from td_WorkWageRequest as a
                                                                           where 1 = 1
--                                                                                and a.intSpecialistid like @intOfficerid
                                                                             and a.Status = 6)) as TotalReqPendingPay,
                       (select count(intWagePaymentReqPAid)
                        from td_WorkWageRequestPA
                        where 1 = 1
--                             and intSpecialistid like @intOfficerid
                          and Status >= 8
                          and (Status <> 1000 and Status <> 1001)
                          and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                           from td_WorkWageRequest as a
                                                                           where 1 = 1
--                                                                                and a.intSpecialistid like @intOfficerid
                                                                             and a.Status = 6)) as TotalReqAprrovedPay,
                       (select count(intWagePaymentReqPAid)
                        from td_WorkWageRequestPA
                        where 1 = 1
--                             and intSpecialistid like @intOfficerid
                          and Status = 1001
                          and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                           from td_WorkWageRequest as a
                                                                           where 1 = 1
--                                                                                and a.intSpecialistid like @intOfficerid
                                                                             and a.Status = 6)) as TotalReqRejectedPay
            End
        else
            if (@OfficerLevel = 'Manager')
                Begin

                    select (select count(intWagePaymentReqid)
                            from td_WorkWageRequest
                            where 1 = 1
--                                 and intManagerid like @intOfficerid
                              and Status >= 3
                              and Status <> 101
                              and Status <> 100
                              and td_WorkWageRequest.intWorkid is not null)                         as TotalReq,
                           (select count(intWagePaymentReqid)
                            from td_WorkWageRequest
                            where 1 = 1
--                                 and intManagerid like @intOfficerid
                              and Status = 3
                              and td_WorkWageRequest.intWorkid is not null)                         as TotalReqPending,
                           (select count(intWagePaymentReqid)
                            from td_WorkWageRequest
                            where 1 = 1
--                                 and intManagerid like @intOfficerid
                              and Status >= 4
                              and (Status <> 100 and Status <> 101 and Status <> 102 and Status <> 103 and
                                   Status <> 104)
                              and td_WorkWageRequest.intWorkid is not null)                         as TotalReqAprroved,
                           (select count(intWagePaymentReqid)
                            from td_WorkWageRequest
                            where 1 = 1
--                                 and intManagerid like @intOfficerid
                              and Status = 102
                              and td_WorkWageRequest.intWorkid is not null)                         as TotalReqRejected,
                           (select count(intWagePaymentReqPAid)
                            from td_WorkWageRequestPA
                            where 1 = 1
--                                 and intManagerid like @intOfficerid
                              and Status >= 8
                              and Status <> 1000
                              and Status <> 1001
                              and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                               from td_WorkWageRequest as a
                                                                               where 1 = 1
--                                                                                    and a.intManagerid like @intOfficerid
                                                                                 and a.Status = 6)) as TotalReqPay,
                           (select count(intWagePaymentReqPAid)
                            from td_WorkWageRequestPA
                            where 1 = 1
--                                 and intManagerid like @intOfficerid
                              and (Status = 8 or Status = 1003)
                              and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                               from td_WorkWageRequest as a
                                                                               where 1 = 1
--                                                                                    and a.intManagerid like @intOfficerid
                                                                                 and a.Status = 6)) as TotalReqPendingPay,
                           (select count(intWagePaymentReqPAid)
                            from td_WorkWageRequestPA
                            where 1 = 1
--                                 and intManagerid like @intOfficerid
                              and Status >= 9
                              and (Status <> 1000 and Status <> 1001 and Status <> 1002 and Status <> 1003 and
                                   Status <> 1004)
                              and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                               from td_WorkWageRequest as a
                                                                               where 1 = 1
--                                                                                    and a.intManagerid like @intOfficerid
                                                                                 and a.Status = 6)) as TotalReqAprrovedPay,
                           (select count(intWagePaymentReqPAid)
                            from td_WorkWageRequestPA
                            where 1 = 1
--                                 and intManagerid like @intOfficerid
                              and Status = 1002
                              and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                               from td_WorkWageRequest as a
                                                                               where 1 = 1
--                                                                                    and a.intManagerid like @intOfficerid
                                                                                 and a.Status = 6)) as TotalReqRejectedPay
                End
            else
                if (@OfficerLevel = 'PA')
                    Begin

                        select (select count(intWagePaymentReqid)
                                from td_WorkWageRequest
                                where intPayingAgentid like @intOfficerid
                                  and Status >= 4
                                  and (Status <> 100 and Status <> 101 and Status <> 102))                           as TotalReq,
                               (select count(intWagePaymentReqid)
                                from td_WorkWageRequest
                                where intPayingAgentid like @intOfficerid
                                  and Status = 4)                                                                    as TotalReqPending,
                               (select count(intWagePaymentReqid)
                                from td_WorkWageRequest
                                where intPayingAgentid like @intOfficerid
                                  and Status = 5)                                                                    as TotalReqPendingforAfterGeneration,
                               (select count(intWagePaymentReqid)
                                from td_WorkWageRequest
                                where intPayingAgentid like @intOfficerid
                                  and Status >= 6
                                  and (Status <> 100 and Status <> 101 and Status <> 102 and Status <> 103 and Status <>
                                                                                                               104)) as TotalReqAprroved,
                               (select count(intWagePaymentReqid)
                                from td_WorkWageRequest
                                where intPayingAgentid like @intOfficerid
                                  and Status = 103)                                                                  as TotalReqRejected,
                               (select count(intWagePaymentReqPAid)
                                from td_WorkWageRequestPA
                                where intPayingAgentid like @intOfficerid
                                  and Status >= 9
                                  and (Status <> 1000 and Status <> 1001 and Status <> 1002)
                                  and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                                   from td_WorkWageRequest as a
                                                                                   where a.intPayingAgentid like @intOfficerid
                                                                                     and a.Status = 6))              as TotalReqPay,
                               (select count(intWagePaymentReqPAid)
                                from td_WorkWageRequestPA
                                where intPayingAgentid like @intOfficerid
                                  and Status = 9)                                                                    as TotalReqPendingPay,
                               (select count(intWagePaymentReqPAid)
                                from td_WorkWageRequestPA
                                where intPayingAgentid like @intOfficerid
                                  and Status >= 10
                                  and (Status <> 1000 and Status <> 1001 and Status <> 1002 and Status <> 1003 and
                                       Status <> 104)
                                  and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                                   from td_WorkWageRequest as a
                                                                                   where a.intPayingAgentid like @intOfficerid
                                                                                     and a.Status = 6))              as TotalReqAprrovedPay,
                               (select count(intWagePaymentReqPAid)
                                from td_WorkWageRequestPA
                                where intPayingAgentid like @intOfficerid
                                  and Status = 1003
                                  and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                                   from td_WorkWageRequest as a
                                                                                   where a.intPayingAgentid like @intOfficerid
                                                                                     and a.Status = 6))              as TotalReqRejectedPay,
                               (select count(intWagePaymentReqid)
                                from td_WorkWageRequest
                                where intPayingAgentid like @intOfficerid
                                  and (Status >= 6 and
                                       (Status <> 100 and Status <> 101 and Status <> 102 and Status <> 103 and
                                        Status <> 104))
                                  and (select count(*)
                                       from td_WageReqBenDet as a
                                       where a.intWagePaymentReqid = td_WorkWageRequest.intWagePaymentReqid) <>
                                      (select count(*)
                                       from td_WageReqBenPADet as b
                                       where b.intWagePaymentReqid = td_WorkWageRequest.intWagePaymentReqid))        as TotalReqPendingAfterSelection,
                               (select count(intWagePaymentReqPAid)
                                from td_WorkWageRequestPA
                                where intPayingAgentid like @intOfficerid
                                  and Status = 1000)                                                                 as TotalReqRejectedPayCo
                    End

                else
                    if (@OfficerLevel = 'WB')
                        Begin

                            select (select count(intWagePaymentReqPAid)
                                    from td_WorkWageRequestPA
                                    where Status >= 9
                                      and (Status <> 1000 and Status <> 1001 and Status <> 1002)
                                      and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                                       from td_WorkWageRequest as a
                                                                                       where a.Status = 6)) as TotalReqPay,
                                   (select count(intWagePaymentReqPAid)
                                    from td_WorkWageRequestPA
                                    where Status = 9)                                                       as TotalReqPendingPay,
                                   (select count(intWagePaymentReqPAid)
                                    from td_WorkWageRequestPA
                                    where Status >= 10
                                      and (Status <> 1000 and Status <> 1001 and Status <> 1002 and Status <> 1003 and
                                           Status <> 104)
                                      and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                                       from td_WorkWageRequest as a
                                                                                       where a.Status = 6)) as TotalReqAprrovedPay,
                                   (select count(intWagePaymentReqPAid)
                                    from td_WorkWageRequestPA
                                    where Status = 1003
                                      and td_WorkWageRequestPA.intWagePaymentReqid in (select a.intWagePaymentReqid
                                                                                       from td_WorkWageRequest as a
                                                                                       where a.Status = 6)) as TotalReqRejectedPay
                        End


End

go

-- Initial Reviewer and Final Payment Authorizer Search Result

CREATE procedure [dbo].[GetWagesdashboarddrildownDetLatKIT](
    @intOfficerid varchar(8),
    @OfficerLevel varchar(20),
    @ReqStatus varchar(8),
    @intIPid varchar(8),
    @intStateid varchar(8),
    @intCountyid varchar(8),
    @intProjectid varchar(8),
    @intTypeofworkid varchar(8),
    @intPayamid varchar(8),
    @intBomaid varchar(8),
    @workcode varchar(180),
    @wagereqno varchar(130)
)
AS
Begin
    if (@OfficerLevel = 'Cordinator')
        Begin
            if (@ReqStatus = '1')
                Begin
                    select distinct td_WorkWageRequest.*
                                  , tbl_WorkMaster.WorkCode
                                  , td_IPDetails.IPName
                                  , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                  , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                  , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                  , '0'                                                       as intWagePaymentReqPAid
                                  , 0                                                         as AttCounter
                                  , wpc.WagePaymentCycle                                      as PaymentCycleName
                    from td_WorkWageRequest
                             inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                             left join tm_WagePaymentCycle wpc
                                       on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                    where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                      and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or tbl_WorkMaster.intStateid is null)
                      and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                           tbl_WorkMaster.intCountieid is null)
                      and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or tbl_WorkMaster.intPayamid is null)
                      and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                      and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or tbl_WorkMaster.intBomasid is null)
                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                      and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                      and td_WorkWageRequest.intWagePaymentReqid in
                          (select intWagePaymentReqid from td_WorkWageRequest where intCoOrdinatorid like @intOfficerid)

                    order by td_WorkWageRequest.ReqNo asc
                End
            else
                if (@ReqStatus = '2')
                    Begin
                        select distinct td_WorkWageRequest.*
                                      , tbl_WorkMaster.WorkCode
                                      , td_IPDetails.IPName
                                      , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                      , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                      , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                      , '0'                                                       as intWagePaymentReqPAid
                                      , 0                                                         as AttCounter
                                      , wpc.WagePaymentCycle                                      as PaymentCycleName
                        from td_WorkWageRequest
                                 inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                 left join tm_WagePaymentCycle wpc
                                           on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                        where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                          and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                               tbl_WorkMaster.intStateid is null)
                          and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                               tbl_WorkMaster.intCountieid is null)
                          and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                               tbl_WorkMaster.intPayamid is null)
                          and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                          and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                               tbl_WorkMaster.intBomasid is null)
                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                          and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                          and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                         from td_WorkWageRequest
                                                                         where 1 = 1
--                                                                            and intCoOrdinatorid like @intOfficerid
                                                                           and (Status = 1 or Status = 101))
                        order by td_WorkWageRequest.ReqNo asc
                    End
                else
                    if (@ReqStatus = '3')
                        Begin
                            select distinct td_WorkWageRequest.*
                                          , tbl_WorkMaster.WorkCode
                                          , td_IPDetails.IPName
                                          , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                          , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                          , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                          , '0'                                                       as intWagePaymentReqPAid
                                          , 0                                                         as AttCounter
                                          , wpc.WagePaymentCycle                                      as PaymentCycleName
                            from td_WorkWageRequest
                                     inner join tbl_WorkMaster
                                                on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                     left join tm_WagePaymentCycle wpc
                                               on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                            where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                              and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                   tbl_WorkMaster.intStateid is null)
                              and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                   tbl_WorkMaster.intCountieid is null)
                              and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                   tbl_WorkMaster.intPayamid is null)
                              and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                              and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                   tbl_WorkMaster.intBomasid is null)
                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                              and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                              and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                             from td_WorkWageRequest
                                                                             where intCoOrdinatorid like @intOfficerid
                                                                               and Status >= 2
                                                                               and Status <> 100)
                            order by td_WorkWageRequest.ReqNo asc
                        End
                    else
                        if (@ReqStatus = '4')
                            Begin
                                select distinct td_WorkWageRequest.*
                                              , tbl_WorkMaster.WorkCode
                                              , td_IPDetails.IPName
                                              , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                              , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                              , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                              , '0'                                                       as intWagePaymentReqPAid
                                              , 0                                                         as AttCounter
                                              , wpc.WagePaymentCycle                                      as PaymentCycleName
                                from td_WorkWageRequest
                                         inner join tbl_WorkMaster
                                                    on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                         left join tm_WagePaymentCycle wpc
                                                   on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                  and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                       tbl_WorkMaster.intStateid is null)
                                  and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                       tbl_WorkMaster.intCountieid is null)
                                  and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                       tbl_WorkMaster.intPayamid is null)
                                  and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                  and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                       tbl_WorkMaster.intBomasid is null)
                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                  and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                  and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                 from td_WorkWageRequest
                                                                                 where intCoOrdinatorid like @intOfficerid
                                                                                   and Status = 100)
                                order by td_WorkWageRequest.ReqNo asc
                            End
                        else
                            if (@ReqStatus = '5')
                                Begin
                                    select distinct td_WorkWageRequestPA.*
                                                  , tbl_WorkMaster.WorkCode
                                                  , td_IPDetails.IPName
                                                  , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                  , 0                                                           as AttCounter
                                                  , ''                                                          as PaymentCycleName
                                    from td_WorkWageRequestPA
                                             inner join tbl_WorkMaster
                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                    where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                      and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                           tbl_WorkMaster.intStateid is null)
                                      and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                           tbl_WorkMaster.intCountieid is null)
                                      and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                           tbl_WorkMaster.intPayamid is null)
                                      and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                      and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                           tbl_WorkMaster.intBomasid is null)
                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                      and td_WorkWageRequestPA.intWagePaymentReqid in ((select intWagePaymentReqid
                                                                                        from td_WorkWageRequest
                                                                                        where intCoOrdinatorid like @intOfficerid
                                                                                          and Status = 6))
                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid
                                                                                          from td_WorkWageRequestPA
                                                                                          where intCoOrdinatorid like @intOfficerid))
                                    order by td_WorkWageRequestPA.ReqNo asc
                                End
                            else
                                if (@ReqStatus = '6')
                                    Begin
                                        select distinct td_WorkWageRequestPA.*
                                                      , tbl_WorkMaster.WorkCode
                                                      , td_IPDetails.IPName
                                                      , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                      , 0                                                           as AttCounter
                                                      , ''                                                          as PaymentCycleName
                                        from td_WorkWageRequestPA
                                                 inner join tbl_WorkMaster
                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                        where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                          and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                               tbl_WorkMaster.intStateid is null)
                                          and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                               tbl_WorkMaster.intCountieid is null)
                                          and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                               tbl_WorkMaster.intPayamid is null)
                                          and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                          and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                               tbl_WorkMaster.intBomasid is null)
                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                          and td_WorkWageRequestPA.intWagePaymentReqid in ((select intWagePaymentReqid
                                                                                            from td_WorkWageRequest
                                                                                            where intCoOrdinatorid like @intOfficerid
                                                                                              and Status = 6))
                                          and
                                            td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid
                                                                                            from td_WorkWageRequestPA
                                                                                            where intCoOrdinatorid like @intOfficerid
                                                                                              and (Status = 6 or Status = 1001)))
                                        order by td_WorkWageRequestPA.ReqNo asc
                                    End

                                else
                                    if (@ReqStatus = '7')
                                        Begin
                                            select distinct td_WorkWageRequestPA.*
                                                          , tbl_WorkMaster.WorkCode
                                                          , td_IPDetails.IPName
                                                          , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                          , 0                                                           as AttCounter
                                                          , ''                                                          as PaymentCycleName
                                            from td_WorkWageRequestPA
                                                     inner join tbl_WorkMaster
                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                            where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                              and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                   tbl_WorkMaster.intStateid is null)
                                              and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                   tbl_WorkMaster.intCountieid is null)
                                              and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                   tbl_WorkMaster.intPayamid is null)
                                              and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                              and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                   tbl_WorkMaster.intBomasid is null)
                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                  ((select intWagePaymentReqid
                                                    from td_WorkWageRequest
                                                    where intCoOrdinatorid like @intOfficerid
                                                      and Status = 6))
                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                  ((select intWagePaymentReqPAid
                                                    from td_WorkWageRequestPA
                                                    where intCoOrdinatorid like @intOfficerid
                                                      and Status >= 7
                                                      and (Status <> 1000 and Status <> 1001 and Status <> 1002 and
                                                           Status <> 1003 and Status <> 104)))
                                            order by td_WorkWageRequestPA.ReqNo asc
                                        End
                                    else
                                        if (@ReqStatus = '8')
                                            Begin
                                                select distinct td_WorkWageRequestPA.*
                                                              , tbl_WorkMaster.WorkCode
                                                              , td_IPDetails.IPName
                                                              , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                              , 0                                                           as AttCounter
                                                              , ''                                                          as PaymentCycleName

                                                from td_WorkWageRequestPA
                                                         inner join tbl_WorkMaster
                                                                    on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                                  and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                       tbl_WorkMaster.intStateid is null)
                                                  and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                       tbl_WorkMaster.intCountieid is null)
                                                  and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                       tbl_WorkMaster.intPayamid is null)
                                                  and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                                  and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                       tbl_WorkMaster.intBomasid is null)
                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                  and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                  and td_WorkWageRequestPA.intWagePaymentReqid in
                                                      ((select intWagePaymentReqid
                                                        from td_WorkWageRequest
                                                        where intCoOrdinatorid like @intOfficerid
                                                          and Status = 6))
                                                  and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                      ((select intWagePaymentReqPAid
                                                        from td_WorkWageRequestPA
                                                        where intCoOrdinatorid like @intOfficerid
                                                          and Status = 1000))
                                                order by td_WorkWageRequestPA.ReqNo asc
                                            End


        End

------IP Approver
    else
        if (@OfficerLevel = 'IP')
            Begin
                if (@ReqStatus = '0')
                    Begin
                        select distinct td_WorkWageRequest.*
                                      , tbl_WorkMaster.WorkCode
                                      , td_IPDetails.IPName
                                      , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                      , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                      , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                      , '0'                                                       as intWagePaymentReqPAid
                                      , 0                                                         as AttCounter
                                      , wpc.WagePaymentCycle                                      as PaymentCycleName
                        from td_WorkWageRequest
                                 inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                 left join tm_WagePaymentCycle wpc
                                           on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                 inner join td_WageReqBenDet rrr
                                            on td_WorkWageRequest.intWagePaymentReqid = rrr.intWagePaymentReqid
                                 inner join tbl_BeneficiaryDet ben on rrr.intBenid = ben.intBenid

                        where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                          and (ben.intStateid like '%' + @intStateid + '%' or ben.intStateid is null)
                          and (ben.intCountieid like '%' + @intCountyid + '%' or ben.intCountieid is null)
                          and (ben.intPayamid like '%' + @intPayamid + '%' or ben.intPayamid is null)
                          and (ben.intBomasid like '%' + @intBomaid + '%' or ben.intBomasid is null)
                          and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                          and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                          and td_WorkWageRequest.intWagePaymentReqid in
                              (select intWagePaymentReqid from td_WorkWageRequest where Status = 0)
                        order by td_WorkWageRequest.ReqNo asc
                    End


            End

---------------IP Approver


        else
            if (@OfficerLevel = 'Specialist')
                Begin
                    if (@ReqStatus = '1')
                        Begin
                            select distinct td_WorkWageRequest.*
                                          , tbl_WorkMaster.WorkCode
                                          , td_IPDetails.IPName
                                          , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                          , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                          , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                          , '0'                                                       as intWagePaymentReqPAid
                                          , 0                                                         as AttCounter
                                          , wpc.WagePaymentCycle                                      as PaymentCycleName
                            from td_WorkWageRequest
                                     inner join tbl_WorkMaster
                                                on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                     left join tm_WagePaymentCycle wpc
                                               on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                            where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                              and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                   tbl_WorkMaster.intStateid is null)
                              and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                   tbl_WorkMaster.intCountieid is null)
                              and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                   tbl_WorkMaster.intPayamid is null)
                              and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                              and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                   tbl_WorkMaster.intBomasid is null)
                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                              and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                              and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                             from td_WorkWageRequest
                                                                             where intSpecialistid like @intOfficerid
                                                                               and Status >= 2
                                                                               and Status <> 100)
                            order by td_WorkWageRequest.ReqNo asc
                        End
                    else
                        if (@ReqStatus = '2')
                            Begin
                                select distinct td_WorkWageRequest.*
                                              , tbl_WorkMaster.WorkCode
                                              , td_IPDetails.IPName
                                              , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                              , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                              , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                              , '0'                                                       as intWagePaymentReqPAid
                                              , 0                                                         as AttCounter
                                              , wpc.WagePaymentCycle                                      as PaymentCycleName
                                from td_WorkWageRequest
                                         inner join tbl_WorkMaster
                                                    on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                         left join tm_WagePaymentCycle wpc
                                                   on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                  and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                       tbl_WorkMaster.intStateid is null)
                                  and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                       tbl_WorkMaster.intCountieid is null)
                                  and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                       tbl_WorkMaster.intPayamid is null)
                                  and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                  and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                       tbl_WorkMaster.intBomasid is null)
                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                  and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                  and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                 from td_WorkWageRequest
                                                                                 where intSpecialistid like @intOfficerid
                                                                                   and (Status = 2 or Status = 102))
                                order by td_WorkWageRequest.ReqNo asc
                            End
                        else
                            if (@ReqStatus = '3')
                                Begin
                                    select distinct td_WorkWageRequest.*
                                                  , tbl_WorkMaster.WorkCode
                                                  , td_IPDetails.IPName
                                                  , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                  , '0'                                                       as intWagePaymentReqPAid
                                                  , 0                                                         as AttCounter
                                                  , wpc.WagePaymentCycle                                      as PaymentCycleName
                                    from td_WorkWageRequest
                                             inner join tbl_WorkMaster
                                                        on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                             left join tm_WagePaymentCycle wpc
                                                       on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                    where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                      and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                           tbl_WorkMaster.intStateid is null)
                                      and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                           tbl_WorkMaster.intCountieid is null)
                                      and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                           tbl_WorkMaster.intPayamid is null)
                                      and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                      and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                           tbl_WorkMaster.intBomasid is null)
                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                      and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                      and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                     from td_WorkWageRequest
                                                                                     where intSpecialistid like @intOfficerid
                                                                                       and Status >= 3
                                                                                       and (Status <> 100 and Status <> 101))
                                    order by td_WorkWageRequest.ReqNo asc
                                End
                            else
                                if (@ReqStatus = '4')
                                    Begin
                                        select distinct td_WorkWageRequest.*
                                                      , tbl_WorkMaster.WorkCode
                                                      , td_IPDetails.IPName
                                                      , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                      , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                      , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                      , '0'                                                       as intWagePaymentReqPAid
                                                      , 0                                                         as AttCounter
                                                      , wpc.WagePaymentCycle                                      as PaymentCycleName
                                        from td_WorkWageRequest
                                                 inner join tbl_WorkMaster
                                                            on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                 left join tm_WagePaymentCycle wpc
                                                           on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                        where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                          and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                               tbl_WorkMaster.intStateid is null)
                                          and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                               tbl_WorkMaster.intCountieid is null)
                                          and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                               tbl_WorkMaster.intPayamid is null)
                                          and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                          and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                               tbl_WorkMaster.intBomasid is null)
                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                          and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                          and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                         from td_WorkWageRequest
                                                                                         where intSpecialistid like @intOfficerid
                                                                                           and Status = 101)
                                        order by td_WorkWageRequest.ReqNo asc
                                    End
                                else
                                    if (@ReqStatus = '5')
                                        Begin
                                            select distinct td_WorkWageRequestPA.*
                                                          , tbl_WorkMaster.WorkCode
                                                          , td_IPDetails.IPName
                                                          , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                          , 0                                                           as AttCounter
                                                          , ''                                                          as PaymentCycleName
                                            from td_WorkWageRequestPA
                                                     inner join tbl_WorkMaster
                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                            where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                              and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                   tbl_WorkMaster.intStateid is null)
                                              and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                   tbl_WorkMaster.intCountieid is null)
                                              and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                   tbl_WorkMaster.intPayamid is null)
                                              and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                              and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                   tbl_WorkMaster.intBomasid is null)
                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                  ((select intWagePaymentReqid
                                                    from td_WorkWageRequest
                                                    where intSpecialistid like @intOfficerid
                                                      and Status = 6))
                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                  ((select intWagePaymentReqPAid
                                                    from td_WorkWageRequestPA
                                                    where intSpecialistid like @intOfficerid
                                                      and Status >= 7
                                                      and Status <> 1000))
                                            order by td_WorkWageRequestPA.ReqNo asc
                                        End
                                    else
                                        if (@ReqStatus = '6')
                                            Begin
                                                select distinct td_WorkWageRequestPA.*
                                                              , tbl_WorkMaster.WorkCode
                                                              , td_IPDetails.IPName
                                                              , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                              , 0                                                           as AttCounter
                                                              , ''                                                          as PaymentCycleName

                                                from td_WorkWageRequestPA
                                                         inner join tbl_WorkMaster
                                                                    on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                                  and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                       tbl_WorkMaster.intStateid is null)
                                                  and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                       tbl_WorkMaster.intCountieid is null)
                                                  and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                       tbl_WorkMaster.intPayamid is null)
                                                  and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                                  and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                       tbl_WorkMaster.intBomasid is null)
                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                  and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'
                                                  and td_WorkWageRequestPA.intWagePaymentReqid in
                                                      ((select intWagePaymentReqid
                                                        from td_WorkWageRequest
                                                        where intSpecialistid like @intOfficerid
                                                          and Status = 6))
                                                  and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                      ((select intWagePaymentReqPAid
                                                        from td_WorkWageRequestPA
                                                        where intSpecialistid like @intOfficerid
                                                          and (Status = 7 or Status = 2 or Status = 1002)))
                                                order by td_WorkWageRequestPA.ReqNo asc
                                            End
                                        else
                                            if (@ReqStatus = '7')
                                                Begin
                                                    select distinct td_WorkWageRequestPA.*
                                                                  , tbl_WorkMaster.WorkCode
                                                                  , td_IPDetails.IPName
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                  , 0                                                           as AttCounter
                                                                  , ''                                                          as PaymentCycleName

                                                    from td_WorkWageRequestPA
                                                             inner join tbl_WorkMaster
                                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                    where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                                      and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                           tbl_WorkMaster.intStateid is null)
                                                      and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                           tbl_WorkMaster.intCountieid is null)
                                                      and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                           tbl_WorkMaster.intPayamid is null)
                                                      and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                                      and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                           tbl_WorkMaster.intBomasid is null)
                                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                      and td_WorkWageRequestPA.intWagePaymentReqid in
                                                          ((select intWagePaymentReqid
                                                            from td_WorkWageRequest
                                                            where intSpecialistid like @intOfficerid
                                                              and Status = 6))
                                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                          ((select intWagePaymentReqPAid
                                                            from td_WorkWageRequestPA
                                                            where intSpecialistid like @intOfficerid
                                                              and Status >= 8
                                                              and (Status <> 1000 and Status <> 1001)))
                                                    order by td_WorkWageRequestPA.ReqNo asc
                                                End
                                            else
                                                if (@ReqStatus = '8')
                                                    Begin
                                                        select distinct td_WorkWageRequestPA.*
                                                                      , tbl_WorkMaster.WorkCode
                                                                      , td_IPDetails.IPName
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                      , 0                                                           as AttCounter
                                                                      , ''                                                          as PaymentCycleName
                                                        from td_WorkWageRequestPA
                                                                 inner join tbl_WorkMaster
                                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                        where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                                          and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                               tbl_WorkMaster.intStateid is null)
                                                          and (
                                                            tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                            tbl_WorkMaster.intCountieid is null)
                                                          and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                               tbl_WorkMaster.intPayamid is null)
                                                          and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                                          and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                               tbl_WorkMaster.intBomasid is null)
                                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                          and td_WorkWageRequestPA.intWagePaymentReqid in
                                                              ((select intWagePaymentReqid
                                                                from td_WorkWageRequest
                                                                where intSpecialistid like @intOfficerid
                                                                  and Status = 6))
                                                          and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                              ((select intWagePaymentReqPAid
                                                                from td_WorkWageRequestPA
                                                                where intSpecialistid like @intOfficerid
                                                                  and Status = 1001))
                                                        order by td_WorkWageRequestPA.ReqNo asc
                                                    End
                End
            else
                if (@OfficerLevel = 'Manager')
                    Begin
                        if (@ReqStatus = '1')
                            Begin
                                select distinct td_WorkWageRequest.*
                                              , tbl_WorkMaster.WorkCode
                                              , td_IPDetails.IPName
                                              , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                              , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                              , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                              , '0'                                                       as intWagePaymentReqPAid
                                              , 0                                                         as AttCounter
                                              , ''                                                        as PaymentCycleName
                                              , wpc.WagePaymentCycle                                      as PaymentCycleName
                                from td_WorkWageRequest
                                         inner join tbl_WorkMaster
                                                    on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                         left join tm_WagePaymentCycle wpc
                                                   on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                where tbl_WorkMaster.IPid like @intIPid
                                  and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid is null)
                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                       tbl_WorkMaster.intCountieid is null)
                                  and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid is null)
                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                  and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid is null)
                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                  and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                  and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                 from td_WorkWageRequest
                                                                                 where intManagerid like @intOfficerid
                                                                                   and Status >= 3
                                                                                   and Status <> 101
                                                                                   and Status <> 100)
                                order by td_WorkWageRequest.ReqNo asc
                            End
                        else
                            if (@ReqStatus = '2')
                                Begin
                                    select distinct td_WorkWageRequest.*
                                                  , tbl_WorkMaster.WorkCode
                                                  , td_IPDetails.IPName
                                                  , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                  , '0'                                                       as intWagePaymentReqPAid
                                                  , 0                                                         as AttCounter
                                                  , wpc.WagePaymentCycle                                      as PaymentCycleName
                                    from td_WorkWageRequest
                                             inner join tbl_WorkMaster
                                                        on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                             left join tm_WagePaymentCycle wpc
                                                       on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                    where tbl_WorkMaster.IPid like @intIPid
                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                           tbl_WorkMaster.intStateid is null)
                                      and (tbl_WorkMaster.intCountieid like @intCountyid or
                                           tbl_WorkMaster.intCountieid is null)
                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                           tbl_WorkMaster.intPayamid is null)
                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                           tbl_WorkMaster.intBomasid is null)
                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                      and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                      and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                     from td_WorkWageRequest
                                                                                     where 1=1
--                                                                                          and intManagerid like @intOfficerid
                                                                                       and Status = 3)
                                    order by td_WorkWageRequest.ReqNo asc
                                End
                            else
                                if (@ReqStatus = '3')
                                    Begin
                                        select distinct td_WorkWageRequest.*,
                                                        tbl_WorkMaster.WorkCode,
                                                        td_IPDetails.IPName,
                                                        Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates,
                                                        Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom,
                                                        Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo,
                                                        '0'                                                       as intWagePaymentReqPAid
                                        from td_WorkWageRequest
                                                 inner join tbl_WorkMaster
                                                            on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                        where tbl_WorkMaster.IPid like @intIPid
                                          and (tbl_WorkMaster.intStateid like @intStateid or
                                               tbl_WorkMaster.intStateid is null)
                                          and (tbl_WorkMaster.intCountieid like @intCountyid or
                                               tbl_WorkMaster.intCountieid is null)
                                          and (tbl_WorkMaster.intPayamid like @intPayamid or
                                               tbl_WorkMaster.intPayamid is null)
                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                          and (tbl_WorkMaster.intBomasid like @intBomaid or
                                               tbl_WorkMaster.intBomasid is null)
                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                          and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                          and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                         from td_WorkWageRequest
                                                                                         where intManagerid like @intOfficerid
                                                                                           and Status >= 4
                                                                                           and (Status <> 100 and
                                                                                                Status <> 101 and
                                                                                                Status <> 102 and
                                                                                                Status <> 103 and
                                                                                                Status <> 104))
                                        order by td_WorkWageRequest.ReqNo asc
                                    End
                                else
                                    if (@ReqStatus = '4')
                                        Begin
                                            select distinct td_WorkWageRequest.*
                                                          , tbl_WorkMaster.WorkCode
                                                          , td_IPDetails.IPName
                                                          , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                          , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                          , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                          , '0'                                                       as intWagePaymentReqPAid
                                                          , 0                                                         as AttCounter
                                                          , wpc.WagePaymentCycle                                      as PaymentCycleName
                                            from td_WorkWageRequest
                                                     inner join tbl_WorkMaster
                                                                on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                     left join tm_WagePaymentCycle wpc
                                                               on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                            where tbl_WorkMaster.IPid like @intIPid
                                              and (tbl_WorkMaster.intStateid like @intStateid or
                                                   tbl_WorkMaster.intStateid is null)
                                              and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                   tbl_WorkMaster.intCountieid is null)
                                              and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                   tbl_WorkMaster.intPayamid is null)
                                              and tbl_WorkMaster.intPrjid like @intProjectid
                                              and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                   tbl_WorkMaster.intBomasid is null)
                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                              and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                              and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                             from td_WorkWageRequest
                                                                                             where intManagerid like @intOfficerid
                                                                                               and Status = 102)
                                            order by td_WorkWageRequest.ReqNo asc
                                        End

                                    else
                                        if (@ReqStatus = '5')
                                            Begin
                                                select distinct td_WorkWageRequestPA.*
                                                              , tbl_WorkMaster.WorkCode
                                                              , td_IPDetails.IPName
                                                              , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                              , 0                                                           as AttCounter
                                                              , ''                                                          as PaymentCycleName

                                                from td_WorkWageRequestPA
                                                         inner join tbl_WorkMaster
                                                                    on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                where tbl_WorkMaster.IPid like @intIPid
                                                  and (tbl_WorkMaster.intStateid like @intStateid or
                                                       tbl_WorkMaster.intStateid is null)
                                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                       tbl_WorkMaster.intCountieid is null)
                                                  and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                       tbl_WorkMaster.intPayamid is null)
                                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                                  and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                       tbl_WorkMaster.intBomasid is null)
                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                  and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                  and td_WorkWageRequestPA.intWagePaymentReqid in
                                                      ((select intWagePaymentReqid
                                                        from td_WorkWageRequest
                                                        where intManagerid like @intOfficerid
                                                          and Status = 6))
                                                  and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                      ((select intWagePaymentReqPAid
                                                        from td_WorkWageRequestPA
                                                        where intManagerid like @intOfficerid
                                                          and Status >= 8
                                                          and Status <> 1000
                                                          and Status <> 1001))
                                                order by td_WorkWageRequestPA.ReqNo asc
                                            End
                                        else
                                            if (@ReqStatus = '6')
                                                Begin
                                                    select distinct td_WorkWageRequestPA.*
                                                                  , tbl_WorkMaster.WorkCode
                                                                  , td_IPDetails.IPName
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                  , 0                                                           as AttCounter
                                                                  , ''                                                          as PaymentCycleName

                                                    from td_WorkWageRequestPA
                                                             inner join tbl_WorkMaster
                                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                    where tbl_WorkMaster.IPid like @intIPid
                                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                                           tbl_WorkMaster.intStateid is null)
                                                      and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                           tbl_WorkMaster.intCountieid is null)
                                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                           tbl_WorkMaster.intPayamid is null)
                                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                           tbl_WorkMaster.intBomasid is null)
                                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                      and td_WorkWageRequestPA.intWagePaymentReqid in
                                                          ((select intWagePaymentReqid
                                                            from td_WorkWageRequest
                                                            where intManagerid like @intOfficerid
                                                              and Status = 6))
                                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                          ((select intWagePaymentReqPAid
                                                            from td_WorkWageRequestPA
                                                            where intManagerid like @intOfficerid
                                                              and (Status = 8 or Status = 1003)))
                                                    order by td_WorkWageRequestPA.ReqNo asc
                                                End
                                            else
                                                if (@ReqStatus = '7')
                                                    Begin
                                                        select distinct td_WorkWageRequestPA.*
                                                                      , tbl_WorkMaster.WorkCode
                                                                      , td_IPDetails.IPName
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                      , 0                                                           as AttCounter
                                                                      , ''                                                          as PaymentCycleName

                                                        from td_WorkWageRequestPA
                                                                 inner join tbl_WorkMaster
                                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                        where tbl_WorkMaster.IPid like @intIPid
                                                          and (tbl_WorkMaster.intStateid like @intStateid or
                                                               tbl_WorkMaster.intStateid is null)
                                                          and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                               tbl_WorkMaster.intCountieid is null)
                                                          and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                               tbl_WorkMaster.intPayamid is null)
                                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                                          and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                               tbl_WorkMaster.intBomasid is null)
                                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                          and td_WorkWageRequestPA.intWagePaymentReqid in
                                                              ((select intWagePaymentReqid
                                                                from td_WorkWageRequest
                                                                where intManagerid like @intOfficerid
                                                                  and Status = 6))
                                                          and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                              ((select intWagePaymentReqPAid
                                                                from td_WorkWageRequestPA
                                                                where intManagerid like @intOfficerid
                                                                  and Status >= 9
                                                                  and (Status <> 1000 and Status <> 1001 and
                                                                       Status <> 1002 and Status <> 1003 and
                                                                       Status <> 1004)))
                                                        order by td_WorkWageRequestPA.ReqNo asc
                                                    End
                                                else
                                                    if (@ReqStatus = '8')
                                                        Begin
                                                            select distinct td_WorkWageRequestPA.*
                                                                          , tbl_WorkMaster.WorkCode
                                                                          , td_IPDetails.IPName
                                                                          , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                          , 0                                                           as AttCounter
                                                                          , ''                                                          as PaymentCycleName
                                                            from td_WorkWageRequestPA
                                                                     inner join tbl_WorkMaster
                                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                            where tbl_WorkMaster.IPid like @intIPid
                                                              and (tbl_WorkMaster.intStateid like @intStateid or
                                                                   tbl_WorkMaster.intStateid is null)
                                                              and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                                   tbl_WorkMaster.intCountieid is null)
                                                              and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                                   tbl_WorkMaster.intPayamid is null)
                                                              and tbl_WorkMaster.intPrjid like @intProjectid
                                                              and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                                   tbl_WorkMaster.intBomasid is null)
                                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                                  ((select intWagePaymentReqid
                                                                    from td_WorkWageRequest
                                                                    where intManagerid like @intOfficerid
                                                                      and Status = 6))
                                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                                  ((select intWagePaymentReqPAid
                                                                    from td_WorkWageRequestPA
                                                                    where intManagerid like @intOfficerid
                                                                      and Status = 1002))
                                                            order by td_WorkWageRequestPA.ReqNo asc
                                                        End


                    End
                else
                    if (@OfficerLevel = 'PA')
                        Begin
                            if (@ReqStatus = '1')
                                Begin
                                    select distinct td_WorkWageRequest.*
                                                  , tbl_WorkMaster.WorkCode
                                                  , td_IPDetails.IPName
                                                  , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                  , '0'                                                       as intWagePaymentReqPAid
                                                  , 0                                                         as AttCounter
                                                  , wpc.WagePaymentCycle                                      as PaymentCycleName
                                    from td_WorkWageRequest
                                             inner join tbl_WorkMaster
                                                        on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                             left join tm_WagePaymentCycle wpc
                                                       on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                    where tbl_WorkMaster.IPid like @intIPid
                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                           tbl_WorkMaster.intStateid is null)
                                      and (tbl_WorkMaster.intCountieid like @intCountyid or
                                           tbl_WorkMaster.intCountieid is null)
                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                           tbl_WorkMaster.intPayamid is null)
                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                           tbl_WorkMaster.intBomasid is null)
                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                      and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                      and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                     from td_WorkWageRequest
                                                                                     where intPayingAgentid like @intOfficerid
                                                                                       and Status >= 4
                                                                                       and (Status <> 100 and Status <> 101 and Status <> 102))
                                    order by td_WorkWageRequest.ReqNo asc
                                End
                            else
                                if (@ReqStatus = '2')
                                    Begin
                                        select distinct td_WorkWageRequest.*
                                                      , tbl_WorkMaster.WorkCode
                                                      , td_IPDetails.IPName
                                                      , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                      , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                      , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                      , '0'                                                       as intWagePaymentReqPAid
                                                      , 0                                                         as AttCounter
                                                      , wpc.WagePaymentCycle                                      as PaymentCycleName
                                        from td_WorkWageRequest
                                                 inner join tbl_WorkMaster
                                                            on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                 left join tm_WagePaymentCycle wpc
                                                           on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                        where tbl_WorkMaster.IPid like @intIPid
                                          and (tbl_WorkMaster.intStateid like @intStateid or
                                               tbl_WorkMaster.intStateid is null)
                                          and (tbl_WorkMaster.intCountieid like @intCountyid or
                                               tbl_WorkMaster.intCountieid is null)
                                          and (tbl_WorkMaster.intPayamid like @intPayamid or
                                               tbl_WorkMaster.intPayamid is null)
                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                          and (tbl_WorkMaster.intBomasid like @intBomaid or
                                               tbl_WorkMaster.intBomasid is null)
                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                          and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                          and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                         from td_WorkWageRequest
                                                                                         where intPayingAgentid like @intOfficerid
                                                                                           and Status = 4)
                                        order by td_WorkWageRequest.ReqNo asc
                                    End
                                else
                                    if (@ReqStatus = '3')
                                        Begin


                                            select distinct td_WorkWageRequestPA.*
                                                          , tbl_WorkMaster.WorkCode
                                                          , td_IPDetails.IPName
                                                          , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                          , 0                                                           as AttCounter
                                                          , ''                                                          as PaymentCycleName

                                            from td_WorkWageRequestPA
                                                     inner join tbl_WorkMaster
                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid

                                            where tbl_WorkMaster.IPid like @intIPid
                                              and (tbl_WorkMaster.intStateid like @intStateid or
                                                   tbl_WorkMaster.intStateid is null)
                                              and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                   tbl_WorkMaster.intCountieid is null)
                                              and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                   tbl_WorkMaster.intPayamid is null)
                                              and tbl_WorkMaster.intPrjid like @intProjectid
                                              and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                   tbl_WorkMaster.intBomasid is null)
                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'


                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                  ((select intWagePaymentReqPAid
                                                    from td_WorkWageRequestPA
                                                    where intPayingAgentid like @intOfficerid))
                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                  (select intWagePaymentReqid
                                                   from td_WorkWageRequest
                                                   where intPayingAgentid like @intOfficerid
                                                     and Status >= 6
                                                     and (Status <> 100 and Status <> 101 and Status <> 102 and
                                                          Status <> 103 and Status <> 104))
                                            order by td_WorkWageRequestPA.ReqNo asc

                                        End
                                    else
                                        if (@ReqStatus = '4')
                                            Begin
                                                select distinct td_WorkWageRequest.*
                                                              , tbl_WorkMaster.WorkCode
                                                              , td_IPDetails.IPName
                                                              , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                              , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                              , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                              , '0'                                                       as intWagePaymentReqPAid
                                                              , 0                                                         as AttCounter
                                                              , wpc.WagePaymentCycle                                      as PaymentCycleName
                                                from td_WorkWageRequest
                                                         inner join tbl_WorkMaster
                                                                    on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                         left join tm_WagePaymentCycle wpc
                                                                   on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                                where tbl_WorkMaster.IPid like @intIPid
                                                  and (tbl_WorkMaster.intStateid like @intStateid or
                                                       tbl_WorkMaster.intStateid is null)
                                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                       tbl_WorkMaster.intCountieid is null)
                                                  and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                       tbl_WorkMaster.intPayamid is null)
                                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                                  and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                       tbl_WorkMaster.intBomasid is null)
                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                  and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                                  and td_WorkWageRequest.intWagePaymentReqid in
                                                      (select intWagePaymentReqid
                                                       from td_WorkWageRequest
                                                       where intPayingAgentid like @intOfficerid
                                                         and Status = 103)
                                                order by td_WorkWageRequest.ReqNo asc
                                            End

                                        else
                                            if (@ReqStatus = '5')
                                                Begin


                                                    select distinct td_WorkWageRequestPA.*
                                                                  , tbl_WorkMaster.WorkCode
                                                                  , td_IPDetails.IPName
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                  , 0                                                           as AttCounter
                                                                  , ''                                                          as PaymentCycleName

                                                    from td_WorkWageRequestPA
                                                             inner join tbl_WorkMaster
                                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                    where tbl_WorkMaster.IPid like @intIPid
                                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                                           tbl_WorkMaster.intStateid is null)
                                                      and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                           tbl_WorkMaster.intCountieid is null)
                                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                           tbl_WorkMaster.intPayamid is null)
                                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                           tbl_WorkMaster.intBomasid is null)
                                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                      and td_WorkWageRequestPA.intWagePaymentReqid in
                                                          ((select intWagePaymentReqid
                                                            from td_WorkWageRequest
                                                            where intPayingAgentid like @intOfficerid
                                                              and Status = 6))
                                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                          (select intWagePaymentReqPAid
                                                           from td_WorkWageRequestPA
                                                           where intPayingAgentid like @intOfficerid
                                                             and Status >= 9
                                                             and (Status <> 1000 and Status <> 1001 and Status <> 1002)
                                                             and td_WorkWageRequestPA.intWagePaymentReqid in
                                                                 (select a.intWagePaymentReqid
                                                                  from td_WorkWageRequest as a
                                                                  where a.intPayingAgentid like @intOfficerid
                                                                    and a.Status = 6))
                                                    order by td_WorkWageRequestPA.ReqNo asc
                                                End
                                            else
                                                if (@ReqStatus = '6')
                                                    Begin
                                                        select distinct td_WorkWageRequestPA.*
                                                                      , tbl_WorkMaster.WorkCode
                                                                      , td_IPDetails.IPName
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                      , 0                                                           as AttCounter
                                                                      , ''                                                          as PaymentCycleName

                                                        from td_WorkWageRequestPA
                                                                 inner join tbl_WorkMaster
                                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                        where tbl_WorkMaster.IPid like @intIPid
                                                          and (tbl_WorkMaster.intStateid like @intStateid or
                                                               tbl_WorkMaster.intStateid is null)
                                                          and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                               tbl_WorkMaster.intCountieid is null)
                                                          and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                               tbl_WorkMaster.intPayamid is null)
                                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                                          and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                               tbl_WorkMaster.intBomasid is null)
                                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                          and td_WorkWageRequestPA.intWagePaymentReqid in
                                                              ((select intWagePaymentReqid
                                                                from td_WorkWageRequest
                                                                where intPayingAgentid like @intOfficerid
                                                                  and Status = 5))
                                                          and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                              ((select intWagePaymentReqPAid
                                                                from td_WorkWageRequestPA
                                                                where intPayingAgentid like @intOfficerid
                                                                  and Status = 6))
                                                        order by td_WorkWageRequestPA.ReqNo asc
                                                    End
                                                else
                                                    if (@ReqStatus = '7')
                                                        Begin
                                                            select distinct td_WorkWageRequestPA.*
                                                                          , tbl_WorkMaster.WorkCode
                                                                          , td_IPDetails.IPName
                                                                          , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                          , 0                                                           as AttCounter
                                                                          , ''                                                          as PaymentCycleName

                                                            from td_WorkWageRequestPA
                                                                     inner join tbl_WorkMaster
                                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                            where tbl_WorkMaster.IPid like @intIPid
                                                              and (tbl_WorkMaster.intStateid like @intStateid or
                                                                   tbl_WorkMaster.intStateid is null)
                                                              and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                                   tbl_WorkMaster.intCountieid is null)
                                                              and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                                   tbl_WorkMaster.intPayamid is null)
                                                              and tbl_WorkMaster.intPrjid like @intProjectid
                                                              and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                                   tbl_WorkMaster.intBomasid is null)
                                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                                  ((select intWagePaymentReqid
                                                                    from td_WorkWageRequest
                                                                    where intPayingAgentid like @intOfficerid
                                                                      and Status = 6))
                                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                                  ((select intWagePaymentReqPAid
                                                                    from td_WorkWageRequestPA
                                                                    where intPayingAgentid like @intOfficerid
                                                                      and Status >= 10
                                                                      and (Status <> 1000 and Status <> 1001 and
                                                                           Status <> 1002 and Status <> 1003 and
                                                                           Status <> 104)))
                                                            order by td_WorkWageRequestPA.ReqNo asc
                                                        End
                                                    else
                                                        if (@ReqStatus = '8')
                                                            Begin
                                                                select distinct td_WorkWageRequestPA.*
                                                                              , tbl_WorkMaster.WorkCode
                                                                              , td_IPDetails.IPName
                                                                              , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                              , 0                                                           as AttCounter
                                                                              , ''                                                          as PaymentCycleName

                                                                from td_WorkWageRequestPA
                                                                         inner join tbl_WorkMaster
                                                                                    on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                                where tbl_WorkMaster.IPid like @intIPid
                                                                  and (tbl_WorkMaster.intStateid like @intStateid or
                                                                       tbl_WorkMaster.intStateid is null)
                                                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                                       tbl_WorkMaster.intCountieid is null)
                                                                  and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                                       tbl_WorkMaster.intPayamid is null)
                                                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                                                  and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                                       tbl_WorkMaster.intBomasid is null)
                                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                                  and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                                  and td_WorkWageRequestPA.intWagePaymentReqid in
                                                                      ((select intWagePaymentReqid
                                                                        from td_WorkWageRequest
                                                                        where intPayingAgentid like @intOfficerid
                                                                          and Status = 6))
                                                                  and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                                      ((select intWagePaymentReqPAid
                                                                        from td_WorkWageRequestPA
                                                                        where intPayingAgentid like @intOfficerid
                                                                          and Status = 1003))
                                                                order by td_WorkWageRequestPA.ReqNo asc
                                                            End


                                                        else
                                                            if (@ReqStatus = '50')
                                                                Begin
                                                                    select distinct td_WorkWageRequest.*
                                                                                  , tbl_WorkMaster.WorkCode
                                                                                  , td_IPDetails.IPName
                                                                                  , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                                                  , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                                                  , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                                                  , '0'                                                       as intWagePaymentReqPAid
                                                                                  , 0                                                         as AttCounter
                                                                                  , wpc.WagePaymentCycle                                      as PaymentCycleName
                                                                    from td_WorkWageRequest
                                                                             inner join tbl_WorkMaster
                                                                                        on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                                             left join tm_WagePaymentCycle wpc
                                                                                       on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                                                    where tbl_WorkMaster.IPid like @intIPid
                                                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                                                           tbl_WorkMaster.intStateid is null)
                                                                      and (
                                                                        tbl_WorkMaster.intCountieid like @intCountyid or
                                                                        tbl_WorkMaster.intCountieid is null)
                                                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                                           tbl_WorkMaster.intPayamid is null)
                                                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                                           tbl_WorkMaster.intBomasid is null)
                                                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                                      and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                                                      and td_WorkWageRequest.intWagePaymentReqid in
                                                                          (select intWagePaymentReqid
                                                                           from td_WorkWageRequest
                                                                           where intPayingAgentid like @intOfficerid
                                                                             and (Status >= 6 and
                                                                                  (Status <> 100 and Status <> 101 and
                                                                                   Status <> 102 and Status <> 103 and
                                                                                   Status <> 104))
                                                                             and (select count(*)
                                                                                  from td_WageReqBenDet as a
                                                                                  where a.intWagePaymentReqid = td_WorkWageRequest.intWagePaymentReqid) <>
                                                                                 (select count(*)
                                                                                  from td_WageReqBenPADet as b
                                                                                  where b.intWagePaymentReqid = td_WorkWageRequest.intWagePaymentReqid))
                                                                    order by td_WorkWageRequest.ReqNo asc

                                                                End
                                                            else
                                                                if (@ReqStatus = '60')
                                                                    Begin
                                                                        select distinct td_WorkWageRequestPA.*
                                                                                      , tbl_WorkMaster.WorkCode
                                                                                      , td_IPDetails.IPName
                                                                                      , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                                      , 0                                                           as AttCounter
                                                                                      , ''                                                          as PaymentCycleName

                                                                        from td_WorkWageRequestPA
                                                                                 inner join tbl_WorkMaster
                                                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                                        where tbl_WorkMaster.IPid like @intIPid
                                                                          and (tbl_WorkMaster.intStateid like
                                                                               @intStateid or
                                                                               tbl_WorkMaster.intStateid is null)
                                                                          and (tbl_WorkMaster.intCountieid like
                                                                               @intCountyid or
                                                                               tbl_WorkMaster.intCountieid is null)
                                                                          and (tbl_WorkMaster.intPayamid like
                                                                               @intPayamid or
                                                                               tbl_WorkMaster.intPayamid is null)
                                                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                                                          and (
                                                                            tbl_WorkMaster.intBomasid like @intBomaid or
                                                                            tbl_WorkMaster.intBomasid is null)
                                                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'


                                                                          and
                                                                            td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                                            ((select intWagePaymentReqPAid
                                                                              from td_WorkWageRequestPA
                                                                              where intPayingAgentid like @intOfficerid
                                                                                and Status = 1000))
                                                                        order by td_WorkWageRequestPA.ReqNo asc
                                                                    End


                        End

                    else
                        if (@OfficerLevel = 'WB')
                            Begin


                                if (@ReqStatus = '5')
                                    Begin


                                        select distinct td_WorkWageRequestPA.*,
                                                        tbl_WorkMaster.WorkCode,
                                                        td_IPDetails.IPName,
                                                        Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates,
                                                        Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom,
                                                        Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                        from td_WorkWageRequestPA
                                                 inner join tbl_WorkMaster
                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                        where tbl_WorkMaster.IPid like @intIPid
                                          and (tbl_WorkMaster.intStateid like @intStateid or
                                               tbl_WorkMaster.intStateid is null)
                                          and (tbl_WorkMaster.intCountieid like @intCountyid or
                                               tbl_WorkMaster.intCountieid is null)
                                          and (tbl_WorkMaster.intPayamid like @intPayamid or
                                               tbl_WorkMaster.intPayamid is null)
                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                          and (tbl_WorkMaster.intBomasid like @intBomaid or
                                               tbl_WorkMaster.intBomasid is null)
                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                          and td_WorkWageRequestPA.intWagePaymentReqid in
                                              ((select intWagePaymentReqid from td_WorkWageRequest where Status = 6))
                                          and
                                            td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid
                                                                                            from td_WorkWageRequestPA
                                                                                            where Status >= 9
                                                                                              and (Status <> 1000 and Status <> 1001 and Status <> 1002)
                                                                                              and
                                                                                                td_WorkWageRequestPA.intWagePaymentReqid in
                                                                                                (select a.intWagePaymentReqid
                                                                                                 from td_WorkWageRequest as a
                                                                                                 where a.Status = 6)))
                                        order by td_WorkWageRequestPA.ReqNo asc
                                    End
                                else
                                    if (@ReqStatus = '6')
                                        Begin
                                            select distinct td_WorkWageRequestPA.*,
                                                            tbl_WorkMaster.WorkCode,
                                                            td_IPDetails.IPName,
                                                            Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates,
                                                            Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom,
                                                            Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                            from td_WorkWageRequestPA
                                                     inner join tbl_WorkMaster
                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                            where tbl_WorkMaster.IPid like @intIPid
                                              and (tbl_WorkMaster.intStateid like @intStateid or
                                                   tbl_WorkMaster.intStateid is null)
                                              and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                   tbl_WorkMaster.intCountieid is null)
                                              and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                   tbl_WorkMaster.intPayamid is null)
                                              and tbl_WorkMaster.intPrjid like @intProjectid
                                              and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                   tbl_WorkMaster.intBomasid is null)
                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                  ((select intWagePaymentReqid from td_WorkWageRequest where Status = 6))
                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                  ((select intWagePaymentReqPAid from td_WorkWageRequestPA where Status = 9))
                                            order by td_WorkWageRequestPA.ReqNo asc
                                        End
                                    else
                                        if (@ReqStatus = '7')
                                            Begin
                                                select distinct td_WorkWageRequestPA.*,
                                                                tbl_WorkMaster.WorkCode,
                                                                td_IPDetails.IPName,
                                                                Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates,
                                                                Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom,
                                                                Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                from td_WorkWageRequestPA
                                                         inner join tbl_WorkMaster
                                                                    on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                where tbl_WorkMaster.IPid like @intIPid
                                                  and (tbl_WorkMaster.intStateid like @intStateid or
                                                       tbl_WorkMaster.intStateid is null)
                                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                       tbl_WorkMaster.intCountieid is null)
                                                  and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                       tbl_WorkMaster.intPayamid is null)
                                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                                  and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                       tbl_WorkMaster.intBomasid is null)
                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                  and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                  and td_WorkWageRequestPA.intWagePaymentReqid in
                                                      ((select intWagePaymentReqid from td_WorkWageRequest where Status = 6))
                                                  and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                      ((select intWagePaymentReqPAid
                                                        from td_WorkWageRequestPA
                                                        where Status >= 10
                                                          and (Status <> 1000 and Status <> 1001 and Status <> 1002 and
                                                               Status <> 1003 and Status <> 104)
                                                          and td_WorkWageRequestPA.intWagePaymentReqid in
                                                              (select a.intWagePaymentReqid
                                                               from td_WorkWageRequest as a
                                                               where a.Status = 6)))
                                                order by td_WorkWageRequestPA.ReqNo asc
                                            End
                                        else
                                            if (@ReqStatus = '8')
                                                Begin
                                                    select distinct td_WorkWageRequestPA.*,
                                                                    tbl_WorkMaster.WorkCode,
                                                                    td_IPDetails.IPName,
                                                                    Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates,
                                                                    Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom,
                                                                    Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                    from td_WorkWageRequestPA
                                                             inner join tbl_WorkMaster
                                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                    where tbl_WorkMaster.IPid like @intIPid
                                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                                           tbl_WorkMaster.intStateid is null)
                                                      and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                           tbl_WorkMaster.intCountieid is null)
                                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                           tbl_WorkMaster.intPayamid is null)
                                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                           tbl_WorkMaster.intBomasid is null)
                                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                      and td_WorkWageRequestPA.intWagePaymentReqid in
                                                          ((select intWagePaymentReqid from td_WorkWageRequest where Status = 6))
                                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                          ((select intWagePaymentReqPAid
                                                            from td_WorkWageRequestPA
                                                            where Status = 1003))
                                                    order by td_WorkWageRequestPA.ReqNo asc
                                                End
                            End
End
go


-- Financial Approver Search Result

CREATE procedure [dbo].[GetWagesdashboarddrildownDetLatAttachCountKIT](
    @intOfficerid varchar(8),
    @OfficerLevel varchar(20),
    @ReqStatus varchar(8),
    @intIPid varchar(8),
    @intStateid varchar(8),
    @intCountyid varchar(8),
    @intProjectid varchar(8),
    @intTypeofworkid varchar(8),
    @intPayamid varchar(8),
    @intBomaid varchar(8),
    @workcode varchar(180),
    @wagereqno varchar(130)
)
AS
Begin
    if (@OfficerLevel = 'Cordinator')
        Begin
            if (@ReqStatus = '1')
                Begin
                    select distinct td_WorkWageRequest.*
                                  , tbl_WorkMaster.WorkCode
                                  , td_IPDetails.IPName
                                  , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                  , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                  , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                  , '0'                                                       as intWagePaymentReqPAid
                                  , 0                                                         as AttCounter
                                  , wpc.WagePaymentCycle                                      as PaymentCycleName
                    from td_WorkWageRequest
                             inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                             left join tm_WagePaymentCycle wpc
                                       on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                    where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                      and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or tbl_WorkMaster.intStateid is null)
                      and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                           tbl_WorkMaster.intCountieid is null)
                      and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or tbl_WorkMaster.intPayamid is null)
                      and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                      and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or tbl_WorkMaster.intBomasid is null)
                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                      and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                      and td_WorkWageRequest.intWagePaymentReqid in
                          (select intWagePaymentReqid from td_WorkWageRequest where intCoOrdinatorid like @intOfficerid)

                    order by td_WorkWageRequest.ReqNo asc
                End
            else
                if (@ReqStatus = '2')
                    Begin
                        select distinct td_WorkWageRequest.*
                                      , tbl_WorkMaster.WorkCode
                                      , td_IPDetails.IPName
                                      , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                      , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                      , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                      , '0'                                                       as intWagePaymentReqPAid
                                      , 0                                                         as AttCounter
                                      , wpc.WagePaymentCycle                                      as PaymentCycleName
                        from td_WorkWageRequest
                                 inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                 left join tm_WagePaymentCycle wpc
                                           on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                        where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                          and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                               tbl_WorkMaster.intStateid is null)
                          and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                               tbl_WorkMaster.intCountieid is null)
                          and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                               tbl_WorkMaster.intPayamid is null)
                          and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                          and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                               tbl_WorkMaster.intBomasid is null)
                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                          and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                          and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                         from td_WorkWageRequest
                                                                         where intCoOrdinatorid like @intOfficerid
                                                                           and (Status = 1 or Status = 101))
                        order by td_WorkWageRequest.ReqNo asc
                    End
                else
                    if (@ReqStatus = '3')
                        Begin
                            select distinct td_WorkWageRequest.*
                                          , tbl_WorkMaster.WorkCode
                                          , td_IPDetails.IPName
                                          , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                          , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                          , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                          , '0'                                                       as intWagePaymentReqPAid
                                          , 0                                                         as AttCounter
                                          , wpc.WagePaymentCycle                                      as PaymentCycleName
                            from td_WorkWageRequest
                                     inner join tbl_WorkMaster
                                                on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                     left join tm_WagePaymentCycle wpc
                                               on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                            where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                              and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                   tbl_WorkMaster.intStateid is null)
                              and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                   tbl_WorkMaster.intCountieid is null)
                              and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                   tbl_WorkMaster.intPayamid is null)
                              and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                              and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                   tbl_WorkMaster.intBomasid is null)
                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                              and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                              and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                             from td_WorkWageRequest
                                                                             where intCoOrdinatorid like @intOfficerid
                                                                               and Status >= 2
                                                                               and Status <> 100)
                            order by td_WorkWageRequest.ReqNo asc
                        End
                    else
                        if (@ReqStatus = '4')
                            Begin
                                select distinct td_WorkWageRequest.*
                                              , tbl_WorkMaster.WorkCode
                                              , td_IPDetails.IPName
                                              , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                              , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                              , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                              , '0'                                                       as intWagePaymentReqPAid
                                              , 0                                                         as AttCounter
                                              , wpc.WagePaymentCycle                                      as PaymentCycleName
                                from td_WorkWageRequest
                                         inner join tbl_WorkMaster
                                                    on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                         left join tm_WagePaymentCycle wpc
                                                   on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                  and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                       tbl_WorkMaster.intStateid is null)
                                  and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                       tbl_WorkMaster.intCountieid is null)
                                  and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                       tbl_WorkMaster.intPayamid is null)
                                  and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                  and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                       tbl_WorkMaster.intBomasid is null)
                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                  and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                  and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                 from td_WorkWageRequest
                                                                                 where intCoOrdinatorid like @intOfficerid
                                                                                   and Status = 100)
                                order by td_WorkWageRequest.ReqNo asc
                            End
                        else
                            if (@ReqStatus = '5')
                                Begin
                                    select distinct td_WorkWageRequestPA.*
                                                  , tbl_WorkMaster.WorkCode
                                                  , td_IPDetails.IPName
                                                  , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                  , 0                                                           as AttCounter
                                                  , ''                                                          as PaymentCycleName
                                    from td_WorkWageRequestPA
                                             inner join tbl_WorkMaster
                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                    where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                      and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                           tbl_WorkMaster.intStateid is null)
                                      and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                           tbl_WorkMaster.intCountieid is null)
                                      and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                           tbl_WorkMaster.intPayamid is null)
                                      and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                      and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                           tbl_WorkMaster.intBomasid is null)
                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                      and td_WorkWageRequestPA.intWagePaymentReqid in ((select intWagePaymentReqid
                                                                                        from td_WorkWageRequest
                                                                                        where intCoOrdinatorid like @intOfficerid
                                                                                          and Status = 6))
                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid
                                                                                          from td_WorkWageRequestPA
                                                                                          where intCoOrdinatorid like @intOfficerid))
                                    order by td_WorkWageRequestPA.ReqNo asc
                                End
                            else
                                if (@ReqStatus = '6')
                                    Begin
                                        select distinct td_WorkWageRequestPA.*
                                                      , tbl_WorkMaster.WorkCode
                                                      , td_IPDetails.IPName
                                                      , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                      , 0                                                           as AttCounter
                                                      , ''                                                          as PaymentCycleName
                                        from td_WorkWageRequestPA
                                                 inner join tbl_WorkMaster
                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                        where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                          and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                               tbl_WorkMaster.intStateid is null)
                                          and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                               tbl_WorkMaster.intCountieid is null)
                                          and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                               tbl_WorkMaster.intPayamid is null)
                                          and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                          and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                               tbl_WorkMaster.intBomasid is null)
                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                          and td_WorkWageRequestPA.intWagePaymentReqid in ((select intWagePaymentReqid
                                                                                            from td_WorkWageRequest
                                                                                            where intCoOrdinatorid like @intOfficerid
                                                                                              and Status = 6))
                                          and
                                            td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid
                                                                                            from td_WorkWageRequestPA
                                                                                            where intCoOrdinatorid like @intOfficerid
                                                                                              and (Status = 6 or Status = 1001)))
                                        order by td_WorkWageRequestPA.ReqNo asc
                                    End

                                else
                                    if (@ReqStatus = '7')
                                        Begin
                                            select distinct td_WorkWageRequestPA.*
                                                          , tbl_WorkMaster.WorkCode
                                                          , td_IPDetails.IPName
                                                          , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                          , 0                                                           as AttCounter
                                                          , ''                                                          as PaymentCycleName
                                            from td_WorkWageRequestPA
                                                     inner join tbl_WorkMaster
                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                            where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                              and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                   tbl_WorkMaster.intStateid is null)
                                              and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                   tbl_WorkMaster.intCountieid is null)
                                              and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                   tbl_WorkMaster.intPayamid is null)
                                              and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                              and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                   tbl_WorkMaster.intBomasid is null)
                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                  ((select intWagePaymentReqid
                                                    from td_WorkWageRequest
                                                    where intCoOrdinatorid like @intOfficerid
                                                      and Status = 6))
                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                  ((select intWagePaymentReqPAid
                                                    from td_WorkWageRequestPA
                                                    where intCoOrdinatorid like @intOfficerid
                                                      and Status >= 7
                                                      and (Status <> 1000 and Status <> 1001 and Status <> 1002 and
                                                           Status <> 1003 and Status <> 104)))
                                            order by td_WorkWageRequestPA.ReqNo asc
                                        End
                                    else
                                        if (@ReqStatus = '8')
                                            Begin
                                                select distinct td_WorkWageRequestPA.*
                                                              , tbl_WorkMaster.WorkCode
                                                              , td_IPDetails.IPName
                                                              , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                              , 0                                                           as AttCounter
                                                              , ''                                                          as PaymentCycleName
                                                from td_WorkWageRequestPA
                                                         inner join tbl_WorkMaster
                                                                    on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                                  and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                       tbl_WorkMaster.intStateid is null)
                                                  and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                       tbl_WorkMaster.intCountieid is null)
                                                  and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                       tbl_WorkMaster.intPayamid is null)
                                                  and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                                  and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                       tbl_WorkMaster.intBomasid is null)
                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                  and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                  and td_WorkWageRequestPA.intWagePaymentReqid in
                                                      ((select intWagePaymentReqid
                                                        from td_WorkWageRequest
                                                        where intCoOrdinatorid like @intOfficerid
                                                          and Status = 6))
                                                  and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                      ((select intWagePaymentReqPAid
                                                        from td_WorkWageRequestPA
                                                        where intCoOrdinatorid like @intOfficerid
                                                          and Status = 1000))
                                                order by td_WorkWageRequestPA.ReqNo asc
                                            End


        End
    else
        if (@OfficerLevel = 'Specialist')
            Begin
                if (@ReqStatus = '1')
                    Begin
                        select distinct td_WorkWageRequest.*
                                      , tbl_WorkMaster.WorkCode
                                      , td_IPDetails.IPName
                                      , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                      , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                      , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                      , '0'                                                       as intWagePaymentReqPAid
                                      , 0                                                         as AttCounter
                                      , wpc.WagePaymentCycle                                      as PaymentCycleName
                        from td_WorkWageRequest
                                 inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                 left join tm_WagePaymentCycle wpc
                                           on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                        where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                          and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                               tbl_WorkMaster.intStateid is null)
                          and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                               tbl_WorkMaster.intCountieid is null)
                          and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                               tbl_WorkMaster.intPayamid is null)
                          and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                          and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                               tbl_WorkMaster.intBomasid is null)
                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                          and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                          and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                         from td_WorkWageRequest
                                                                         where intSpecialistid like @intOfficerid
                                                                           and Status >= 2
                                                                           and Status <> 100)
                        order by td_WorkWageRequest.ReqNo asc
                    End
                else
                    if (@ReqStatus = '2')
                        Begin
                            select distinct td_WorkWageRequest.*
                                          , tbl_WorkMaster.WorkCode
                                          , td_IPDetails.IPName
                                          , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                          , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                          , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                          , '0'                                                       as intWagePaymentReqPAid
                                          , attCount.attCounter
                                          , wpc.WagePaymentCycle                                      as PaymentCycleName
                            from td_WorkWageRequest
                                     inner join tbl_WorkMaster
                                                on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                     left join (select intwagepaymentreqid, count(*) attCounter
                                                from tbl_wageattachements
                                                group by intwagepaymentreqid) as attCount
                                               on attCount.intwagepaymentreqid = td_WorkWageRequest.intwagepaymentreqid
                                     left join tm_WagePaymentCycle wpc
                                               on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                            where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                              and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                   tbl_WorkMaster.intStateid is null)
                              and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                   tbl_WorkMaster.intCountieid is null)
                              and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                   tbl_WorkMaster.intPayamid is null)
                              and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                              and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                   tbl_WorkMaster.intBomasid is null)
                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                              and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                              and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                             from td_WorkWageRequest
                                                                             where 1 = 1
--                                                                                and intSpecialistid like @intOfficerid
                                                                               and (Status = 2 or Status = 102))
                            order by td_WorkWageRequest.ReqNo asc
                        End
                    else
                        if (@ReqStatus = '3')
                            Begin
                                select distinct td_WorkWageRequest.*
                                              , tbl_WorkMaster.WorkCode
                                              , td_IPDetails.IPName
                                              , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                              , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                              , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                              , '0'                                                       as intWagePaymentReqPAid
                                              , 0                                                         as AttCounter
                                              , wpc.WagePaymentCycle                                      as PaymentCycleName
                                from td_WorkWageRequest
                                         inner join tbl_WorkMaster
                                                    on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                         left join tm_WagePaymentCycle wpc
                                                   on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                  and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                       tbl_WorkMaster.intStateid is null)
                                  and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                       tbl_WorkMaster.intCountieid is null)
                                  and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                       tbl_WorkMaster.intPayamid is null)
                                  and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                  and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                       tbl_WorkMaster.intBomasid is null)
                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                  and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                  and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                 from td_WorkWageRequest
                                                                                 where intSpecialistid like @intOfficerid
                                                                                   and Status >= 3
                                                                                   and (Status <> 100 and Status <> 101))
                                order by td_WorkWageRequest.ReqNo asc
                            End
                        else
                            if (@ReqStatus = '4')
                                Begin
                                    select distinct td_WorkWageRequest.*
                                                  , tbl_WorkMaster.WorkCode
                                                  , td_IPDetails.IPName
                                                  , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                  , '0'                                                       as intWagePaymentReqPAid
                                                  , 0                                                         as AttCounter
                                                  , wpc.WagePaymentCycle                                      as PaymentCycleName
                                    from td_WorkWageRequest
                                             inner join tbl_WorkMaster
                                                        on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                             left join tm_WagePaymentCycle wpc
                                                       on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                    where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                      and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                           tbl_WorkMaster.intStateid is null)
                                      and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                           tbl_WorkMaster.intCountieid is null)
                                      and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                           tbl_WorkMaster.intPayamid is null)
                                      and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                      and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                           tbl_WorkMaster.intBomasid is null)
                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                      and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                      and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                     from td_WorkWageRequest
                                                                                     where intSpecialistid like @intOfficerid
                                                                                       and Status = 101)
                                    order by td_WorkWageRequest.ReqNo asc
                                End
                            else
                                if (@ReqStatus = '5')
                                    Begin
                                        select distinct td_WorkWageRequestPA.*
                                                      , tbl_WorkMaster.WorkCode
                                                      , td_IPDetails.IPName
                                                      , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                      , 0                                                           as AttCounter
                                                      , ''                                                          as PaymentCycleName
                                        from td_WorkWageRequestPA
                                                 inner join tbl_WorkMaster
                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                        where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                          and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                               tbl_WorkMaster.intStateid is null)
                                          and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                               tbl_WorkMaster.intCountieid is null)
                                          and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                               tbl_WorkMaster.intPayamid is null)
                                          and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                          and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                               tbl_WorkMaster.intBomasid is null)
                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                          and td_WorkWageRequestPA.intWagePaymentReqid in ((select intWagePaymentReqid
                                                                                            from td_WorkWageRequest
                                                                                            where intSpecialistid like @intOfficerid
                                                                                              and Status = 6))
                                          and
                                            td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid
                                                                                            from td_WorkWageRequestPA
                                                                                            where intSpecialistid like @intOfficerid
                                                                                              and Status >= 7
                                                                                              and Status <> 1000))
                                        order by td_WorkWageRequestPA.ReqNo asc
                                    End
                                else
                                    if (@ReqStatus = '6')
                                        Begin
                                            select distinct td_WorkWageRequestPA.*
                                                          , tbl_WorkMaster.WorkCode
                                                          , td_IPDetails.IPName
                                                          , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                          , 0                                                           as AttCounter
                                                          , ''                                                          as PaymentCycleName
                                            from td_WorkWageRequestPA
                                                     inner join tbl_WorkMaster
                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                            where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                              and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                   tbl_WorkMaster.intStateid is null)
                                              and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                   tbl_WorkMaster.intCountieid is null)
                                              and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                   tbl_WorkMaster.intPayamid is null)
                                              and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                              and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                   tbl_WorkMaster.intBomasid is null)
                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'
                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                  ((select intWagePaymentReqid
                                                    from td_WorkWageRequest
                                                    where intSpecialistid like @intOfficerid
                                                      and Status = 6))
                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                  ((select intWagePaymentReqPAid
                                                    from td_WorkWageRequestPA
                                                    where intSpecialistid like @intOfficerid
                                                      and (Status = 7 or Status = 2 or Status = 1002)))
                                            order by td_WorkWageRequestPA.ReqNo asc
                                        End
                                    else
                                        if (@ReqStatus = '7')
                                            Begin
                                                select distinct td_WorkWageRequestPA.*
                                                              , tbl_WorkMaster.WorkCode
                                                              , td_IPDetails.IPName
                                                              , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                              , 0                                                           as AttCounter
                                                              , ''                                                          as PaymentCycleName
                                                from td_WorkWageRequestPA
                                                         inner join tbl_WorkMaster
                                                                    on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                                  and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                       tbl_WorkMaster.intStateid is null)
                                                  and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                       tbl_WorkMaster.intCountieid is null)
                                                  and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                       tbl_WorkMaster.intPayamid is null)
                                                  and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                                  and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                       tbl_WorkMaster.intBomasid is null)
                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                  and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                  and td_WorkWageRequestPA.intWagePaymentReqid in
                                                      ((select intWagePaymentReqid
                                                        from td_WorkWageRequest
                                                        where intSpecialistid like @intOfficerid
                                                          and Status = 6))
                                                  and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                      ((select intWagePaymentReqPAid
                                                        from td_WorkWageRequestPA
                                                        where intSpecialistid like @intOfficerid
                                                          and Status >= 8
                                                          and (Status <> 1000 and Status <> 1001)))
                                                order by td_WorkWageRequestPA.ReqNo asc
                                            End
                                        else
                                            if (@ReqStatus = '8')
                                                Begin
                                                    select distinct td_WorkWageRequestPA.*
                                                                  , tbl_WorkMaster.WorkCode
                                                                  , td_IPDetails.IPName
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                  , 0                                                           as AttCounter
                                                                  , ''                                                          as PaymentCycleName
                                                                  , ''                                                          as PaymentCycleName
                                                    from td_WorkWageRequestPA
                                                             inner join tbl_WorkMaster
                                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                    where tbl_WorkMaster.IPid like '%' + @intIPid + '%'
                                                      and (tbl_WorkMaster.intStateid like '%' + @intStateid + '%' or
                                                           tbl_WorkMaster.intStateid is null)
                                                      and (tbl_WorkMaster.intCountieid like '%' + @intCountyid + '%' or
                                                           tbl_WorkMaster.intCountieid is null)
                                                      and (tbl_WorkMaster.intPayamid like '%' + @intPayamid + '%' or
                                                           tbl_WorkMaster.intPayamid is null)
                                                      and tbl_WorkMaster.intPrjid like '%' + @intProjectid + '%'
                                                      and (tbl_WorkMaster.intBomasid like '%' + @intBomaid + '%' or
                                                           tbl_WorkMaster.intBomasid is null)
                                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                      and td_WorkWageRequestPA.intWagePaymentReqid in
                                                          ((select intWagePaymentReqid
                                                            from td_WorkWageRequest
                                                            where intSpecialistid like @intOfficerid
                                                              and Status = 6))
                                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                          ((select intWagePaymentReqPAid
                                                            from td_WorkWageRequestPA
                                                            where intSpecialistid like @intOfficerid
                                                              and Status = 1001))
                                                    order by td_WorkWageRequestPA.ReqNo asc
                                                End
            End
        else
            if (@OfficerLevel = 'Manager')
                Begin
                    if (@ReqStatus = '1')
                        Begin
                            select distinct td_WorkWageRequest.*
                                          , tbl_WorkMaster.WorkCode
                                          , td_IPDetails.IPName
                                          , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                          , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                          , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                          , '0'                                                       as intWagePaymentReqPAid
                                          , 0                                                         as AttCounter
                                          , wpc.WagePaymentCycle                                      as PaymentCycleName
                            from td_WorkWageRequest
                                     inner join tbl_WorkMaster
                                                on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                     left join tm_WagePaymentCycle wpc
                                               on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle

                            where tbl_WorkMaster.IPid like @intIPid
                              and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid is null)
                              and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid is null)
                              and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid is null)
                              and tbl_WorkMaster.intPrjid like @intProjectid
                              and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid is null)
                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                              and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                              and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                             from td_WorkWageRequest
                                                                             where intManagerid like @intOfficerid
                                                                               and Status >= 3
                                                                               and Status <> 101
                                                                               and Status <> 100)
                            order by td_WorkWageRequest.ReqNo asc
                        End
                    else
                        if (@ReqStatus = '2')
                            Begin
                                select distinct td_WorkWageRequest.*
                                              , tbl_WorkMaster.WorkCode
                                              , td_IPDetails.IPName
                                              , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                              , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                              , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                              , '0'                                                       as intWagePaymentReqPAid
                                              , 0                                                         as AttCounter
                                              , wpc.WagePaymentCycle                                      as PaymentCycleName
                                from td_WorkWageRequest
                                         inner join tbl_WorkMaster
                                                    on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                         left join tm_WagePaymentCycle wpc
                                                   on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                where tbl_WorkMaster.IPid like @intIPid
                                  and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid is null)
                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                       tbl_WorkMaster.intCountieid is null)
                                  and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid is null)
                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                  and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid is null)
                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                  and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                  and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                 from td_WorkWageRequest
                                                                                 where intManagerid like @intOfficerid
                                                                                   and Status = 3)
                                order by td_WorkWageRequest.ReqNo asc
                            End
                        else
                            if (@ReqStatus = '3')
                                Begin
                                    select distinct td_WorkWageRequest.*
                                                  , tbl_WorkMaster.WorkCode
                                                  , td_IPDetails.IPName
                                                  , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                  , '0'                                                       as intWagePaymentReqPAid
                                                  , 0                                                         as AttCounter
                                                  , wpc.WagePaymentCycle                                      as PaymentCycleName
                                    from td_WorkWageRequest
                                             inner join tbl_WorkMaster
                                                        on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                             left join tm_WagePaymentCycle wpc
                                                       on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                    where tbl_WorkMaster.IPid like @intIPid
                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                           tbl_WorkMaster.intStateid is null)
                                      and (tbl_WorkMaster.intCountieid like @intCountyid or
                                           tbl_WorkMaster.intCountieid is null)
                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                           tbl_WorkMaster.intPayamid is null)
                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                           tbl_WorkMaster.intBomasid is null)
                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                      and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                      and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                     from td_WorkWageRequest
                                                                                     where intManagerid like @intOfficerid
                                                                                       and Status >= 4
                                                                                       and (Status <> 100 and
                                                                                            Status <> 101 and
                                                                                            Status <> 102 and
                                                                                            Status <> 103 and
                                                                                            Status <> 104))
                                    order by td_WorkWageRequest.ReqNo asc
                                End
                            else
                                if (@ReqStatus = '4')
                                    Begin
                                        select distinct td_WorkWageRequest.*
                                                      , tbl_WorkMaster.WorkCode
                                                      , td_IPDetails.IPName
                                                      , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                      , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                      , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                      , '0'                                                       as intWagePaymentReqPAid
                                                      , 0                                                         as AttCounter
                                                      , wpc.WagePaymentCycle                                      as PaymentCycleName
                                        from td_WorkWageRequest
                                                 inner join tbl_WorkMaster
                                                            on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                 left join tm_WagePaymentCycle wpc
                                                           on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                        where tbl_WorkMaster.IPid like @intIPid
                                          and (tbl_WorkMaster.intStateid like @intStateid or
                                               tbl_WorkMaster.intStateid is null)
                                          and (tbl_WorkMaster.intCountieid like @intCountyid or
                                               tbl_WorkMaster.intCountieid is null)
                                          and (tbl_WorkMaster.intPayamid like @intPayamid or
                                               tbl_WorkMaster.intPayamid is null)
                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                          and (tbl_WorkMaster.intBomasid like @intBomaid or
                                               tbl_WorkMaster.intBomasid is null)
                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                          and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                          and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                         from td_WorkWageRequest
                                                                                         where intManagerid like @intOfficerid
                                                                                           and Status = 102)
                                        order by td_WorkWageRequest.ReqNo asc
                                    End

                                else
                                    if (@ReqStatus = '5')
                                        Begin
                                            select distinct td_WorkWageRequestPA.*
                                                          , tbl_WorkMaster.WorkCode
                                                          , td_IPDetails.IPName
                                                          , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                          , 0                                                           as AttCounter
                                                          , ''                                                          as PaymentCycleName
                                            from td_WorkWageRequestPA
                                                     inner join tbl_WorkMaster
                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                            where tbl_WorkMaster.IPid like @intIPid
                                              and (tbl_WorkMaster.intStateid like @intStateid or
                                                   tbl_WorkMaster.intStateid is null)
                                              and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                   tbl_WorkMaster.intCountieid is null)
                                              and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                   tbl_WorkMaster.intPayamid is null)
                                              and tbl_WorkMaster.intPrjid like @intProjectid
                                              and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                   tbl_WorkMaster.intBomasid is null)
                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                  ((select intWagePaymentReqid
                                                    from td_WorkWageRequest
                                                    where intManagerid like @intOfficerid
                                                      and Status = 6))
                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                  ((select intWagePaymentReqPAid
                                                    from td_WorkWageRequestPA
                                                    where intManagerid like @intOfficerid
                                                      and Status >= 8
                                                      and Status <> 1000
                                                      and Status <> 1001))
                                            order by td_WorkWageRequestPA.ReqNo asc
                                        End
                                    else
                                        if (@ReqStatus = '6')
                                            Begin
                                                select distinct td_WorkWageRequestPA.*
                                                              , tbl_WorkMaster.WorkCode
                                                              , td_IPDetails.IPName
                                                              , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                              , 0                                                           as AttCounter
                                                              , ''                                                          as PaymentCycleName
                                                from td_WorkWageRequestPA
                                                         inner join tbl_WorkMaster
                                                                    on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                where tbl_WorkMaster.IPid like @intIPid
                                                  and (tbl_WorkMaster.intStateid like @intStateid or
                                                       tbl_WorkMaster.intStateid is null)
                                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                       tbl_WorkMaster.intCountieid is null)
                                                  and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                       tbl_WorkMaster.intPayamid is null)
                                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                                  and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                       tbl_WorkMaster.intBomasid is null)
                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                  and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                  and td_WorkWageRequestPA.intWagePaymentReqid in
                                                      ((select intWagePaymentReqid
                                                        from td_WorkWageRequest
                                                        where intManagerid like @intOfficerid
                                                          and Status = 6))
                                                  and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                      ((select intWagePaymentReqPAid
                                                        from td_WorkWageRequestPA
                                                        where intManagerid like @intOfficerid
                                                          and (Status = 8 or Status = 1003)))
                                                order by td_WorkWageRequestPA.ReqNo asc
                                            End
                                        else
                                            if (@ReqStatus = '7')
                                                Begin
                                                    select distinct td_WorkWageRequestPA.*
                                                                  , tbl_WorkMaster.WorkCode
                                                                  , td_IPDetails.IPName
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                  , 0                                                           as AttCounter
                                                                  , ''                                                          as PaymentCycleName
                                                    from td_WorkWageRequestPA
                                                             inner join tbl_WorkMaster
                                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                    where tbl_WorkMaster.IPid like @intIPid
                                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                                           tbl_WorkMaster.intStateid is null)
                                                      and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                           tbl_WorkMaster.intCountieid is null)
                                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                           tbl_WorkMaster.intPayamid is null)
                                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                           tbl_WorkMaster.intBomasid is null)
                                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                      and td_WorkWageRequestPA.intWagePaymentReqid in
                                                          ((select intWagePaymentReqid
                                                            from td_WorkWageRequest
                                                            where intManagerid like @intOfficerid
                                                              and Status = 6))
                                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                          ((select intWagePaymentReqPAid
                                                            from td_WorkWageRequestPA
                                                            where intManagerid like @intOfficerid
                                                              and Status >= 9
                                                              and (Status <> 1000 and Status <> 1001 and
                                                                   Status <> 1002 and Status <> 1003 and
                                                                   Status <> 1004)))
                                                    order by td_WorkWageRequestPA.ReqNo asc
                                                End
                                            else
                                                if (@ReqStatus = '8')
                                                    Begin
                                                        select distinct td_WorkWageRequestPA.*
                                                                      , tbl_WorkMaster.WorkCode
                                                                      , td_IPDetails.IPName
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                      , 0                                                           as AttCounter
                                                                      , ''                                                          as PaymentCycleName
                                                        from td_WorkWageRequestPA
                                                                 inner join tbl_WorkMaster
                                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                        where tbl_WorkMaster.IPid like @intIPid
                                                          and (tbl_WorkMaster.intStateid like @intStateid or
                                                               tbl_WorkMaster.intStateid is null)
                                                          and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                               tbl_WorkMaster.intCountieid is null)
                                                          and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                               tbl_WorkMaster.intPayamid is null)
                                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                                          and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                               tbl_WorkMaster.intBomasid is null)
                                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                          and td_WorkWageRequestPA.intWagePaymentReqid in
                                                              ((select intWagePaymentReqid
                                                                from td_WorkWageRequest
                                                                where intManagerid like @intOfficerid
                                                                  and Status = 6))
                                                          and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                              ((select intWagePaymentReqPAid
                                                                from td_WorkWageRequestPA
                                                                where intManagerid like @intOfficerid
                                                                  and Status = 1002))
                                                        order by td_WorkWageRequestPA.ReqNo asc
                                                    End


                End
            else
                if (@OfficerLevel = 'PA')
                    Begin
                        if (@ReqStatus = '1')
                            Begin
                                select distinct td_WorkWageRequest.*
                                              , tbl_WorkMaster.WorkCode
                                              , td_IPDetails.IPName
                                              , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                              , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                              , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                              , '0'                                                       as intWagePaymentReqPAid
                                              , 0                                                         as AttCounter
                                              , wpc.WagePaymentCycle                                      as PaymentCycleName
                                from td_WorkWageRequest
                                         inner join tbl_WorkMaster
                                                    on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                         left join tm_WagePaymentCycle wpc
                                                   on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                where tbl_WorkMaster.IPid like @intIPid
                                  and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid is null)
                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                       tbl_WorkMaster.intCountieid is null)
                                  and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid is null)
                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                  and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid is null)
                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                  and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                  and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                 from td_WorkWageRequest
                                                                                 where intPayingAgentid like @intOfficerid
                                                                                   and Status >= 4
                                                                                   and (Status <> 100 and Status <> 101 and Status <> 102))
                                order by td_WorkWageRequest.ReqNo asc
                            End
                        else
                            if (@ReqStatus = '2')
                                Begin
                                    select distinct td_WorkWageRequest.*
                                                  , tbl_WorkMaster.WorkCode
                                                  , td_IPDetails.IPName
                                                  , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                  , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                  , '0'                                                       as intWagePaymentReqPAid
                                                  , 0                                                         as AttCounter
                                                  , wpc.WagePaymentCycle                                      as PaymentCycleName
                                    from td_WorkWageRequest
                                             inner join tbl_WorkMaster
                                                        on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                             left join tm_WagePaymentCycle wpc
                                                       on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                    where tbl_WorkMaster.IPid like @intIPid
                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                           tbl_WorkMaster.intStateid is null)
                                      and (tbl_WorkMaster.intCountieid like @intCountyid or
                                           tbl_WorkMaster.intCountieid is null)
                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                           tbl_WorkMaster.intPayamid is null)
                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                           tbl_WorkMaster.intBomasid is null)
                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                      and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                      and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                     from td_WorkWageRequest
                                                                                     where intPayingAgentid like @intOfficerid
                                                                                       and Status = 4)
                                    order by td_WorkWageRequest.ReqNo asc
                                End
                            else
                                if (@ReqStatus = '3')
                                    Begin


                                        select distinct td_WorkWageRequestPA.*,
                                                        tbl_WorkMaster.WorkCode,
                                                        td_IPDetails.IPName,
                                                        Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates,
                                                        Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom,
                                                        Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                ,
                                                        0                                                           as AttCounter
                                        from td_WorkWageRequestPA
                                                 inner join tbl_WorkMaster
                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                        where tbl_WorkMaster.IPid like @intIPid
                                          and (tbl_WorkMaster.intStateid like @intStateid or
                                               tbl_WorkMaster.intStateid is null)
                                          and (tbl_WorkMaster.intCountieid like @intCountyid or
                                               tbl_WorkMaster.intCountieid is null)
                                          and (tbl_WorkMaster.intPayamid like @intPayamid or
                                               tbl_WorkMaster.intPayamid is null)
                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                          and (tbl_WorkMaster.intBomasid like @intBomaid or
                                               tbl_WorkMaster.intBomasid is null)
                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'


                                          and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                              ((select intWagePaymentReqPAid
                                                from td_WorkWageRequestPA
                                                where intPayingAgentid like @intOfficerid))
                                          and td_WorkWageRequestPA.intWagePaymentReqid in
                                              (select intWagePaymentReqid
                                               from td_WorkWageRequest
                                               where intPayingAgentid like @intOfficerid
                                                 and Status >= 6
                                                 and (Status <> 100 and Status <> 101 and Status <> 102 and
                                                      Status <> 103 and Status <> 104))
                                        order by td_WorkWageRequestPA.ReqNo asc
                                    End
                                else
                                    if (@ReqStatus = '4')
                                        Begin
                                            select distinct td_WorkWageRequest.*
                                                          , tbl_WorkMaster.WorkCode
                                                          , td_IPDetails.IPName
                                                          , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                          , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                          , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                          , '0'                                                       as intWagePaymentReqPAid
                                                          , 0                                                         as AttCounter
                                                          , wpc.WagePaymentCycle                                      as PaymentCycleName
                                            from td_WorkWageRequest
                                                     inner join tbl_WorkMaster
                                                                on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                     left join tm_WagePaymentCycle wpc
                                                               on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                            where tbl_WorkMaster.IPid like @intIPid
                                              and (tbl_WorkMaster.intStateid like @intStateid or
                                                   tbl_WorkMaster.intStateid is null)
                                              and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                   tbl_WorkMaster.intCountieid is null)
                                              and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                   tbl_WorkMaster.intPayamid is null)
                                              and tbl_WorkMaster.intPrjid like @intProjectid
                                              and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                   tbl_WorkMaster.intBomasid is null)
                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                              and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                              and td_WorkWageRequest.intWagePaymentReqid in (select intWagePaymentReqid
                                                                                             from td_WorkWageRequest
                                                                                             where intPayingAgentid like @intOfficerid
                                                                                               and Status = 103)
                                            order by td_WorkWageRequest.ReqNo asc
                                        End

                                    else
                                        if (@ReqStatus = '5')
                                            Begin


                                                select distinct td_WorkWageRequestPA.*,
                                                                tbl_WorkMaster.WorkCode,
                                                                td_IPDetails.IPName,
                                                                Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates,
                                                                Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom,
                                                                Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                        ,
                                                                0                                                           as AttCounter
                                                from td_WorkWageRequestPA
                                                         inner join tbl_WorkMaster
                                                                    on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                where tbl_WorkMaster.IPid like @intIPid
                                                  and (tbl_WorkMaster.intStateid like @intStateid or
                                                       tbl_WorkMaster.intStateid is null)
                                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                       tbl_WorkMaster.intCountieid is null)
                                                  and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                       tbl_WorkMaster.intPayamid is null)
                                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                                  and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                       tbl_WorkMaster.intBomasid is null)
                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                  and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                  and td_WorkWageRequestPA.intWagePaymentReqid in
                                                      ((select intWagePaymentReqid
                                                        from td_WorkWageRequest
                                                        where intPayingAgentid like @intOfficerid
                                                          and Status = 6))
                                                  and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                      (select intWagePaymentReqPAid
                                                       from td_WorkWageRequestPA
                                                       where intPayingAgentid like @intOfficerid
                                                         and Status >= 9
                                                         and (Status <> 1000 and Status <> 1001 and Status <> 1002)
                                                         and td_WorkWageRequestPA.intWagePaymentReqid in
                                                             (select a.intWagePaymentReqid
                                                              from td_WorkWageRequest as a
                                                              where a.intPayingAgentid like @intOfficerid
                                                                and a.Status = 6))
                                                order by td_WorkWageRequestPA.ReqNo asc
                                            End
                                        else
                                            if (@ReqStatus = '6')
                                                Begin
                                                    select distinct td_WorkWageRequestPA.*
                                                                  , tbl_WorkMaster.WorkCode
                                                                  , td_IPDetails.IPName
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                  , 0                                                           as AttCounter
                                                                  , ''                                                          as PaymentCycleName
                                                    from td_WorkWageRequestPA
                                                             inner join tbl_WorkMaster
                                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                    where tbl_WorkMaster.IPid like @intIPid
                                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                                           tbl_WorkMaster.intStateid is null)
                                                      and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                           tbl_WorkMaster.intCountieid is null)
                                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                           tbl_WorkMaster.intPayamid is null)
                                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                           tbl_WorkMaster.intBomasid is null)
                                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                      and td_WorkWageRequestPA.intWagePaymentReqid in
                                                          ((select intWagePaymentReqid
                                                            from td_WorkWageRequest
                                                            where intPayingAgentid like @intOfficerid
                                                              and Status = 5))
                                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                          ((select intWagePaymentReqPAid
                                                            from td_WorkWageRequestPA
                                                            where intPayingAgentid like @intOfficerid
                                                              and Status = 6))
                                                    order by td_WorkWageRequestPA.ReqNo asc
                                                End
                                            else
                                                if (@ReqStatus = '7')
                                                    Begin
                                                        select distinct td_WorkWageRequestPA.*
                                                                      , tbl_WorkMaster.WorkCode
                                                                      , td_IPDetails.IPName
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                      , 0                                                           as AttCounter
                                                                      , ''                                                          as PaymentCycleName
                                                        from td_WorkWageRequestPA
                                                                 inner join tbl_WorkMaster
                                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                        where tbl_WorkMaster.IPid like @intIPid
                                                          and (tbl_WorkMaster.intStateid like @intStateid or
                                                               tbl_WorkMaster.intStateid is null)
                                                          and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                               tbl_WorkMaster.intCountieid is null)
                                                          and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                               tbl_WorkMaster.intPayamid is null)
                                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                                          and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                               tbl_WorkMaster.intBomasid is null)
                                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                          and td_WorkWageRequestPA.intWagePaymentReqid in
                                                              ((select intWagePaymentReqid
                                                                from td_WorkWageRequest
                                                                where intPayingAgentid like @intOfficerid
                                                                  and Status = 6))
                                                          and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                              ((select intWagePaymentReqPAid
                                                                from td_WorkWageRequestPA
                                                                where intPayingAgentid like @intOfficerid
                                                                  and Status >= 10
                                                                  and (Status <> 1000 and Status <> 1001 and
                                                                       Status <> 1002 and Status <> 1003 and
                                                                       Status <> 104)))
                                                        order by td_WorkWageRequestPA.ReqNo asc
                                                    End
                                                else
                                                    if (@ReqStatus = '8')
                                                        Begin
                                                            select distinct td_WorkWageRequestPA.*
                                                                          , tbl_WorkMaster.WorkCode
                                                                          , td_IPDetails.IPName
                                                                          , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                          , 0                                                           as AttCounter
                                                                          , ''                                                          as PaymentCycleName
                                                            from td_WorkWageRequestPA
                                                                     inner join tbl_WorkMaster
                                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                            where tbl_WorkMaster.IPid like @intIPid
                                                              and (tbl_WorkMaster.intStateid like @intStateid or
                                                                   tbl_WorkMaster.intStateid is null)
                                                              and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                                   tbl_WorkMaster.intCountieid is null)
                                                              and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                                   tbl_WorkMaster.intPayamid is null)
                                                              and tbl_WorkMaster.intPrjid like @intProjectid
                                                              and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                                   tbl_WorkMaster.intBomasid is null)
                                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                                  ((select intWagePaymentReqid
                                                                    from td_WorkWageRequest
                                                                    where intPayingAgentid like @intOfficerid
                                                                      and Status = 6))
                                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                                  ((select intWagePaymentReqPAid
                                                                    from td_WorkWageRequestPA
                                                                    where intPayingAgentid like @intOfficerid
                                                                      and Status = 1003))
                                                            order by td_WorkWageRequestPA.ReqNo asc
                                                        End


                                                    else
                                                        if (@ReqStatus = '50')
                                                            Begin
                                                                select distinct td_WorkWageRequest.*
                                                                              , tbl_WorkMaster.WorkCode
                                                                              , td_IPDetails.IPName
                                                                              , Convert(varchar(11), td_WorkWageRequest.Created_dt, 106)  as RDates
                                                                              , Convert(varchar(11), td_WorkWageRequest.ReqFromDate, 106) as ReqFrom
                                                                              , Convert(varchar(11), td_WorkWageRequest.ReqToDate, 106)   as ReqTo
                                                                              , '0'                                                       as intWagePaymentReqPAid
                                                                              , 0                                                         as AttCounter
                                                                              , wpc.WagePaymentCycle                                      as PaymentCycleName
                                                                from td_WorkWageRequest
                                                                         inner join tbl_WorkMaster
                                                                                    on td_WorkWageRequest.intWorkid = tbl_WorkMaster.intWorkid
                                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                                         left join tm_WagePaymentCycle wpc
                                                                                   on wpc.intPaymentCycleID = td_WorkWageRequest.PaymentCycle
                                                                where tbl_WorkMaster.IPid like @intIPid
                                                                  and (tbl_WorkMaster.intStateid like @intStateid or
                                                                       tbl_WorkMaster.intStateid is null)
                                                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                                       tbl_WorkMaster.intCountieid is null)
                                                                  and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                                       tbl_WorkMaster.intPayamid is null)
                                                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                                                  and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                                       tbl_WorkMaster.intBomasid is null)
                                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                                  and td_WorkWageRequest.ReqNo like '%' + @wagereqno + '%'
                                                                  and td_WorkWageRequest.intWagePaymentReqid in
                                                                      (select intWagePaymentReqid
                                                                       from td_WorkWageRequest
                                                                       where intPayingAgentid like @intOfficerid
                                                                         and (Status >= 6 and
                                                                              (Status <> 100 and Status <> 101 and
                                                                               Status <> 102 and Status <> 103 and
                                                                               Status <> 104))
                                                                         and (select count(*)
                                                                              from td_WageReqBenDet as a
                                                                              where a.intWagePaymentReqid = td_WorkWageRequest.intWagePaymentReqid) <>
                                                                             (select count(*)
                                                                              from td_WageReqBenPADet as b
                                                                              where b.intWagePaymentReqid = td_WorkWageRequest.intWagePaymentReqid))
                                                                order by td_WorkWageRequest.ReqNo asc

                                                            End
                                                        else
                                                            if (@ReqStatus = '60')
                                                                Begin
                                                                    select distinct td_WorkWageRequestPA.*
                                                                                  , tbl_WorkMaster.WorkCode
                                                                                  , td_IPDetails.IPName
                                                                                  , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                                                  , 0                                                           as AttCounter
                                                                                  , ''                                                          as PaymentCycleName

                                                                    from td_WorkWageRequestPA
                                                                             inner join tbl_WorkMaster
                                                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid

                                                                    where tbl_WorkMaster.IPid like @intIPid
                                                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                                                           tbl_WorkMaster.intStateid is null)
                                                                      and (
                                                                        tbl_WorkMaster.intCountieid like @intCountyid or
                                                                        tbl_WorkMaster.intCountieid is null)
                                                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                                           tbl_WorkMaster.intPayamid is null)
                                                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                                           tbl_WorkMaster.intBomasid is null)
                                                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'


                                                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                                          ((select intWagePaymentReqPAid
                                                                            from td_WorkWageRequestPA
                                                                            where intPayingAgentid like @intOfficerid
                                                                              and Status = 1000))
                                                                    order by td_WorkWageRequestPA.ReqNo asc
                                                                End


                    End

                else
                    if (@OfficerLevel = 'WB')
                        Begin


                            if (@ReqStatus = '5')
                                Begin


                                    select distinct td_WorkWageRequestPA.*
                                                  , tbl_WorkMaster.WorkCode
                                                  , td_IPDetails.IPName
                                                  , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                  , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                  , 0                                                           as AttCounter
                                                  , ''                                                          as PaymentCycleName
                                    from td_WorkWageRequestPA
                                             inner join tbl_WorkMaster
                                                        on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                             inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                    where tbl_WorkMaster.IPid like @intIPid
                                      and (tbl_WorkMaster.intStateid like @intStateid or
                                           tbl_WorkMaster.intStateid is null)
                                      and (tbl_WorkMaster.intCountieid like @intCountyid or
                                           tbl_WorkMaster.intCountieid is null)
                                      and (tbl_WorkMaster.intPayamid like @intPayamid or
                                           tbl_WorkMaster.intPayamid is null)
                                      and tbl_WorkMaster.intPrjid like @intProjectid
                                      and (tbl_WorkMaster.intBomasid like @intBomaid or
                                           tbl_WorkMaster.intBomasid is null)
                                      and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                      and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                      and td_WorkWageRequestPA.intWagePaymentReqid in
                                          ((select intWagePaymentReqid from td_WorkWageRequest where Status = 6))
                                      and td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid
                                                                                          from td_WorkWageRequestPA
                                                                                          where Status >= 9
                                                                                            and (Status <> 1000 and Status <> 1001 and Status <> 1002)
                                                                                            and
                                                                                              td_WorkWageRequestPA.intWagePaymentReqid in
                                                                                              (select a.intWagePaymentReqid
                                                                                               from td_WorkWageRequest as a
                                                                                               where a.Status = 6)))
                                    order by td_WorkWageRequestPA.ReqNo asc
                                End
                            else
                                if (@ReqStatus = '6')
                                    Begin
                                        select distinct td_WorkWageRequestPA.*
                                                      , tbl_WorkMaster.WorkCode
                                                      , td_IPDetails.IPName
                                                      , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                      , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                      , 0                                                           as AttCounter
                                                      , ''                                                          as PaymentCycleName
                                        from td_WorkWageRequestPA
                                                 inner join tbl_WorkMaster
                                                            on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                 inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                        where tbl_WorkMaster.IPid like @intIPid
                                          and (tbl_WorkMaster.intStateid like @intStateid or
                                               tbl_WorkMaster.intStateid is null)
                                          and (tbl_WorkMaster.intCountieid like @intCountyid or
                                               tbl_WorkMaster.intCountieid is null)
                                          and (tbl_WorkMaster.intPayamid like @intPayamid or
                                               tbl_WorkMaster.intPayamid is null)
                                          and tbl_WorkMaster.intPrjid like @intProjectid
                                          and (tbl_WorkMaster.intBomasid like @intBomaid or
                                               tbl_WorkMaster.intBomasid is null)
                                          and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                          and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                          and td_WorkWageRequestPA.intWagePaymentReqid in
                                              ((select intWagePaymentReqid from td_WorkWageRequest where Status = 6))
                                          and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                              ((select intWagePaymentReqPAid from td_WorkWageRequestPA where Status = 9))
                                        order by td_WorkWageRequestPA.ReqNo asc
                                    End
                                else
                                    if (@ReqStatus = '7')
                                        Begin
                                            select distinct td_WorkWageRequestPA.*
                                                          , tbl_WorkMaster.WorkCode
                                                          , td_IPDetails.IPName
                                                          , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                          , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                          , 0                                                           as AttCounter
                                                          , ''                                                          as PaymentCycleName
                                            from td_WorkWageRequestPA
                                                     inner join tbl_WorkMaster
                                                                on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                     inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                            where tbl_WorkMaster.IPid like @intIPid
                                              and (tbl_WorkMaster.intStateid like @intStateid or
                                                   tbl_WorkMaster.intStateid is null)
                                              and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                   tbl_WorkMaster.intCountieid is null)
                                              and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                   tbl_WorkMaster.intPayamid is null)
                                              and tbl_WorkMaster.intPrjid like @intProjectid
                                              and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                   tbl_WorkMaster.intBomasid is null)
                                              and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                              and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                              and td_WorkWageRequestPA.intWagePaymentReqid in
                                                  ((select intWagePaymentReqid from td_WorkWageRequest where Status = 6))
                                              and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                  ((select intWagePaymentReqPAid
                                                    from td_WorkWageRequestPA
                                                    where Status >= 10
                                                      and (Status <> 1000 and Status <> 1001 and Status <> 1002 and
                                                           Status <> 1003 and Status <> 104)
                                                      and td_WorkWageRequestPA.intWagePaymentReqid in
                                                          (select a.intWagePaymentReqid
                                                           from td_WorkWageRequest as a
                                                           where a.Status = 6)))
                                            order by td_WorkWageRequestPA.ReqNo asc
                                        End
                                    else
                                        if (@ReqStatus = '8')
                                            Begin
                                                select distinct td_WorkWageRequestPA.*
                                                              , tbl_WorkMaster.WorkCode
                                                              , td_IPDetails.IPName
                                                              , Convert(varchar(11), td_WorkWageRequestPA.Created_dt, 106)  as RDates
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqFromDate, 106) as ReqFrom
                                                              , Convert(varchar(11), td_WorkWageRequestPA.ReqToDate, 106)   as ReqTo
                                                              , 0                                                           as AttCounter
                                                              , ''                                                          as PaymentCycleName
                                                from td_WorkWageRequestPA
                                                         inner join tbl_WorkMaster
                                                                    on td_WorkWageRequestPA.intWorkid = tbl_WorkMaster.intWorkid
                                                         inner join td_IPDetails on td_IPDetails.intIP = tbl_WorkMaster.IPid
                                                where tbl_WorkMaster.IPid like @intIPid
                                                  and (tbl_WorkMaster.intStateid like @intStateid or
                                                       tbl_WorkMaster.intStateid is null)
                                                  and (tbl_WorkMaster.intCountieid like @intCountyid or
                                                       tbl_WorkMaster.intCountieid is null)
                                                  and (tbl_WorkMaster.intPayamid like @intPayamid or
                                                       tbl_WorkMaster.intPayamid is null)
                                                  and tbl_WorkMaster.intPrjid like @intProjectid
                                                  and (tbl_WorkMaster.intBomasid like @intBomaid or
                                                       tbl_WorkMaster.intBomasid is null)
                                                  and tbl_WorkMaster.WorkCode like '%' + @workcode + '%'
                                                  and td_WorkWageRequestPA.ReqNo like '%' + @wagereqno + '%'

                                                  and td_WorkWageRequestPA.intWagePaymentReqid in
                                                      ((select intWagePaymentReqid from td_WorkWageRequest where Status = 6))
                                                  and td_WorkWageRequestPA.intWagePaymentReqPAid in
                                                      ((select intWagePaymentReqPAid
                                                        from td_WorkWageRequestPA
                                                        where Status = 1003))
                                                order by td_WorkWageRequestPA.ReqNo asc
                                            End
                        End
End
go