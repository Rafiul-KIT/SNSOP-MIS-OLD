
alter table tbl_BeneficiaryServiceDet add household_photo_url varchar(255), alt1_photo_url varchar(255), alt2_photo_url varchar(255);
alter table tbl_BeneficiaryDet add household_photo_url varchar(255), alt1_photo_url varchar(255), alt2_photo_url varchar(255);

alter procedure [dbo].[UpdateBenficiariesEnrollmentComponent] -- UpdateBenficiariesEnrollmentOLD '20014'
(
    @intBenid varchar(8),
    @component varchar(50)
)
AS
Begin

    Declare @intBomaidPreName as varchar(180)
    set @intBomaidPreName=(select NameofHousehold from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)


    Declare @intBomaidPreSpouse as varchar(180)
    set @intBomaidPreSpouse=(select NameofSpouse from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)

    Declare @intBomaidNew as varchar(180)
    set @intBomaidNew=(select intBomasid from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)


    Declare @householdno as varchar(180)
    set @householdno=(select household_number from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)


    if((select count(*) from tbl_BeneficiaryServiceDet where  intBenTempid like @intBenid and BenStatus='2')<>0)
        Begin
            select 100
            return 100
        End
    Else




        Begin

            if((select count(*) from tbl_BeneficiaryDet where   tbl_BeneficiaryDet.household_number like @householdno and tbl_BeneficiaryDet.NameofHousehold like @intBomaidPreName and tbl_BeneficiaryDet.NameofSpouse like @intBomaidPreSpouse and tbl_BeneficiaryDet.intBomasid like @intBomaidNew )<>0)
                Begin
                    select 100
                    return 100
                End
            Else
                Begin
                    Declare @intBomaidPre as varchar(10)
                    set @intBomaidPre=(select intBomasid from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)





                    Declare @cnt as int
                    set @cnt=(select count(*) from  tbl_BeneficiaryDet where intBomasid like @intBomaidPre and BeneficiaryType not like '%CST%'  and EnrollmentNo not like '%CST%')

                    Declare @ids as int

                    insert into tbl_BeneficiaryDet(intIP,OLDEnrollmentNo,EnrollmentNo,NameofHousehold,FamilySize,Sex,Age,NameofSpouse,
                                                   NoofAdult,NoofDependents,FamilyIncome,intStateid,intCountieid,intPayamid,intBomasid,Address,ContactNo,Email,SecondLineHome,
                                                   IsDisability,BiometricNo,NameofBank,AccountNo,WorkCode,LegalStatus,Created_by,Created_dt,SOurceofIncome,RegYear,PortYear,
                                                   PortPhase,PortedSheetName,BDropout,DropoutRemarks,GroupId,BeneficiaryType,Name1stAlternatePayee,Name2ndAlternatePayee,
                                                   NumberMaleAdults,NumberFemaleAdults,NumberAdultsAgeAbove65,NumberAdultsAgeBetween45_64,NumberAdultsAgeBetween18_30,NumberAdultsAgeBetween30_45,
                                                   NumberFemaleChildren,NumberMaleChildren,NumberChildrenAgeAbove5years,NumberChildrenAgeAbove12years,NumberChildrenAgeAbove18years,
                                                   EthnicityTribeRgin,PhotoPath,PhotoName,NationalID,PCPortFlag,household_number,FirstAlternatePayeeGender,FirstAlternatePayeeAge,
                                                   FirstAlternatePayeeNationID,FirstAlternatePayeeMobile,FirstAlternatePayeePhotoName,FirstAlternatePayeePhotoPath,SecondAlternatePayeeGender,
                                                   SecondAlternatePayeeAge,SecondAlternateNationalID,SecondAlternatePayeeMobile,SecondAlternatepayeePhotoName,SecondAlternatePayeePhotoPath,
                                                   supervisor_id,member_number,female_dependants,male_dependants,latitude,longitude,id_url,TransferDate,TransferUpdateat,alternate_number,
                                                   Secondalternate_number,API_Status,SelectionCriteria, SelectionReason, household_photo_url, alt1_photo_url, alt2_photo_url)


                    select  intIP,OLDEnrollmentNo,NULL,NameofHousehold,FamilySize,Sex,Age,NameofSpouse,
                            NoofAdult,NoofDependents,FamilyIncome,intStateid,intCountieid,intPayamid,intBomasid,Address,ContactNo,Email,SecondLineHome,
                            IsDisability,BiometricNo,NameofBank,AccountNo,WorkCode,LegalStatus,Created_by,getdate(),SOurceofIncome,RegYear,PortYear,
                            PortPhase,PortedSheetName,BDropout,DropoutRemarks,null,BeneficiaryType,Name1stAlternatePayee,Name2ndAlternatePayee,
                            NumberMaleAdults,NumberFemaleAdults,NumberAdultsAgeAbove65,NumberAdultsAgeBetween45_64,NumberAdultsAgeBetween18_30,NumberAdultsAgeBetween30_45,
                            NumberFemaleChildren,NumberMaleChildren,NumberChildrenAgeAbove5years,NumberChildrenAgeAbove12years,NumberChildrenAgeAbove18years,
                            EthnicityTribeRgin,PhotoPath,PhotoName,NationalID,PCPortFlag,household_number,FirstAlternatePayeeGender,FirstAlternatePayeeAge,
                            FirstAlternatePayeeNationID,FirstAlternatePayeeMobile,FirstAlternatePayeePhotoName,FirstAlternatePayeePhotoPath,SecondAlternatePayeeGender,
                            SecondAlternatePayeeAge,SecondAlternateNationalID,SecondAlternatePayeeMobile,SecondAlternatepayeePhotoName,SecondAlternatePayeePhotoPath,
                            supervisor_id,member_number,female_dependants,male_dependants,latitude,longitude,id_url,TransferDate,TransferUpdateat,alternate_number,
                            Secondalternate_number,API_Status,@component, SelectionReason, household_photo_url, alt1_photo_url, alt2_photo_url
                    from tbl_BeneficiaryServiceDet where intBenTempid  like @intBenid

                    set @ids=@@IDENTITY

                    Update tbl_BeneficiaryServiceDet set BenStatus='2', Modified_dt=getdate(), Modified_by=1000 where intBenTempid  like @intBenid

                    Declare @BomaCode as varchar(10)
                    Declare @intBomaid as varchar(10)
                    set @intBomaid=(select intBomasid from tbl_BeneficiaryDet where intBenid like @ids)
                    set @BomaCode=(select b.BomaCode from tm_Bomas as b where b.intBomasid like @intBomaid)
                    Declare @Groupid as varchar(10)
                    set @Groupid=(select GroupId from tbl_BeneficiaryDet where intBenid like @ids)
                    Declare @GroupType as varchar(10)
                    set @GroupType=(select BeneficiaryType from tbl_BeneficiaryDet where intBenid like @ids)
--if((select count(*) from tbl_BeneficiaryDet where intBenid like @ids and GroupId like '%CST%')=1)
                    if(@GroupType='CST')
                        Begin
                            if((select  count(*) from tbl_BeneficiaryDet where intBomasid like @intBomaid and BeneficiaryType like '%CST%' )  =1)
                                Begin
                                    Update tbl_BeneficiaryDet set EnrollmentNo= @BomaCode +'/' + 'CST'   +'/' + '001', GroupId=NULL where intBenid like @ids
                                End
                            else
                                Begin
                                    Declare @countBensGroupidCST as int
                                    set @countBensGroupidCST=(select  count(*) from tbl_BeneficiaryDet where intBomasid like @intBomaid and BeneficiaryType like '%CST%' )
                                    Declare @BenidsCSR as varchar(8)
                                    set @countBensGroupidCST=@countBensGroupidCST+1
                                    set @BenidsCSR= Case when LEN(@countBensGroupidCST)=1 then '00' + convert(varchar, @countBensGroupidCST) else Case when LEN(@countBensGroupidCST)=2 then '0' + convert(varchar, @countBensGroupidCST) else convert(varchar, @countBensGroupidCST)  end end


                                    Update tbl_BeneficiaryDet set EnrollmentNo= @BomaCode +'/' + 'CST'   +'/' + @BenidsCSR, GroupId=NULL where intBenid like @ids
                                End

                        End
                    Else
                        Begin



                            if(@cnt=0)
                                Begin

                                    Update tbl_BeneficiaryDet set EnrollmentNo= @BomaCode +'/' + '01' +'/' + '001', GroupId='01' where intBenid like @ids
                                End
                            else
                                Begin

                                    Declare @GroupidMAX as int
                                    set @GroupidMAX=(select MAX(Convert(int,isnull(GroupId,1))) from tbl_BeneficiaryDet where intBomasid like @intBomaid AND GroupId IS NOT NULL  and BeneficiaryType not like '%CST%' and EnrollmentNo not like '%CST%' )
                                    Declare @UpdateGroupidAA as varchar(8)
                                    set @UpdateGroupidAA= case when LEN(@GroupidMAX)=1 then '0'+ Convert(varchar,@GroupidMAX) else Convert(varchar,@GroupidMAX) end
                                    Declare @A1 as int
                                    Declare @A2 as int
                                    set @A1=(select  count(*) from tbl_BeneficiaryDet where intBomasid like @intBomaid and Convert(int,isnull(GroupId,0)) like @GroupidMAX AND GroupId IS NOT NULL and BeneficiaryType not like '%CST%'  and EnrollmentNo not like '%CST%' )
                                    set @A2=(select top 1  GroupStrength from GroupMaster where Convert(int,isnull(GroupCode,0)) like @GroupidMAX and Boma like @intBomaid and GroupCode not like '%CST%'  and GroupType not like '%CST%')



                                    if( @A1>=@A2 )
                                        Begin

                                            set @GroupidMAX=@GroupidMAX+1
                                            Declare @UpdateGroupid as varchar(8)
                                            set @UpdateGroupid= case when LEN(@GroupidMAX)=1 then '0'+ Convert(varchar,@GroupidMAX) else Convert(varchar,@GroupidMAX) end
                                            Update tbl_BeneficiaryDet set EnrollmentNo= @BomaCode +'/' + @UpdateGroupid   +'/' + '001', GroupId=@UpdateGroupid where intBenid like @ids

                                        End
                                    else
                                        Begin

                                            Declare @UpdateGroupida as varchar(8)
                                            set @UpdateGroupida= case when LEN(@GroupidMAX)=1 then '0'+ Convert(varchar,@GroupidMAX) else Convert(varchar,@GroupidMAX) end

                                            Declare @countBensGroupid as int
                                            set @countBensGroupid=(select  count(*) from tbl_BeneficiaryDet where intBomasid like @intBomaid and Convert(int,isnull(GroupId,0)) like @GroupidMAX and GroupId is not null and BeneficiaryType not like '%CST%'   and EnrollmentNo not like '%CST%')
                                            Declare @Benids as varchar(8)
                                            set @countBensGroupid=@countBensGroupid+1
                                            set @Benids= Case when LEN(@countBensGroupid)=1 then '00' + convert(varchar, @countBensGroupid) else Case when LEN(@countBensGroupid)=2 then '0' + convert(varchar, @countBensGroupid) else convert(varchar, @countBensGroupid)  end end


                                            Update tbl_BeneficiaryDet set EnrollmentNo= @BomaCode +'/' + @UpdateGroupida   +'/' + @Benids, GroupId=@UpdateGroupida where intBenid like @ids
                                        End


                                End

                        End



                    select @ids
                    return @ids
                End
        End
End
go


-----------------------------


alter Procedure [dbo].[GetBenficiaryDetByIDForJobCard]
(
    @intBenid varchar(10)
)
as begin



    SELECT tAPI.intBenTempid , tbl_WorkMaster.WorkCode,tbl_BeneficiaryDet.Age,   tbl_BeneficiaryDet.intBenid, tbl_BeneficiaryDet.EnrollmentNo, tbl_BeneficiaryDet.NameofHousehold, tbl_BeneficiaryDet.intIP, tbl_BeneficiaryDet.FamilySize,
           tbl_BeneficiaryDet.Sex, tbl_BeneficiaryDet.NameofSpouse, tbl_BeneficiaryDet.NoofAdult, tbl_BeneficiaryDet.NoofDependents, tbl_BeneficiaryDet.FamilyIncome,
           tbl_BeneficiaryDet.Address, tbl_BeneficiaryDet.ContactNo, tbl_BeneficiaryDet.Email, tbl_BeneficiaryDet.BiometricNo,
           tbl_BeneficiaryDet.Created_dt, tm_Payams.PayamName, tm_States.StateName, tm_Counties.CountieName, tm_Bomas.BomaName,
           tbl_BeneficiaryDet.PhotoPath,tbl_BeneficiaryDet.PhotoName   ,
           tbl_BeneficiaryDet.FirstAlternatePayeePhotoName, tbl_BeneficiaryDet.FirstAlternatePayeePhotoPath, tbl_BeneficiaryDet.Name1stAlternatePayee ,
           tbl_BeneficiaryDet.SecondAlternatepayeePhotoName , tbl_BeneficiaryDet.SecondAlternatePayeePhotoPath, tbl_BeneficiaryDet.Name2ndAlternatePayee, tbl_BeneficiaryDet.household_number,
           tbl_BeneficiaryDet.household_photo_url, tbl_BeneficiaryDet.alt1_photo_url, tbl_BeneficiaryDet.alt2_photo_url
    FROM         tbl_BeneficiaryDet LEFT OUTER JOIN
                 tm_Bomas ON tbl_BeneficiaryDet.intBomasid = tm_Bomas.intBomasid left outer JOIN
                 tm_Payams ON tm_Payams.intPayamid = tm_Bomas.intPayamid left outer JOIN
                 tm_Counties ON tm_Counties.intCountieid = tm_Payams.intCountieid INNER JOIN
                 tm_States ON tm_States.intStateid = tm_Counties.intStateid left outer JOIN


                 --tm_WorkActivities ON tbl_BeneficiaryDet.WorkCode = tm_WorkActivities.intWorkActivity
                     tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid

                                    left outer join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid
                                    left join tbl_BeneficiaryServiceDet as tAPI on tAPI.household_number = tbl_BeneficiaryDet.household_number

    where tbl_BeneficiaryDet.intBenid like @intBenid


--select * from PWImages.dbo.tbl_Attachements where type like 'Appicant Photo' and intTypeId like @intBenid

end
go

-------------------------------------

create table td_WageRequestAfisSync
(
    id int identity constraint PK_td_WageRequestAfisSync
        primary key,
    intWagePaymentReqid int references td_WorkWageRequest,
    sync_status int,
    operation_dt datetime default getdate()
);

-------------------------

CREATE Procedure [dbo].[InsUpdWageRequestAfisSync]
(
    @intWagePaymentReqid int, @syncStatus int
) AS
Begin
    IF NOT EXISTS(SELECT id from td_WageRequestAfisSync where intWagePaymentReqid=@intWagePaymentReqid)
        INSERT INTO td_WageRequestAfisSync(intWagePaymentReqid, sync_status) values (@intWagePaymentReqid, @syncStatus)
    ELSE UPDATE td_WageRequestAfisSync set sync_status=@syncStatus, operation_dt=getdate() where intWagePaymentReqid=@intWagePaymentReqid
End
go

--------------------------

CREATE Procedure [dbo].[getBenfWorkWageDetailsServiceKIT]
(
    @intWagePaymentReqid varchar(10)
) AS
Begin

    select DISTINCT
        wr.intWagePaymentReqid, CONVERT(char(10), wr.ReqFromDate, 126) AS reqFromDate, CONVERT(char(10), wr.ReqToDate, 126) AS reqToDate,
        wr.Exchangerateattime as exchangerateattime, wr.WageRateUSD as wageRateUSD, wr.ReqNo as reqNo, bw.intWagesReqBenTransid,
        b.intBenid, b.EnrollmentNo as enrollmentNo, b.household_number, w.intWorkid, w.WorkCode as workCode, bw.TotAttendance as totAttendance,
        bw.WageRateUSDTotal wageRateUSDTotal, CONVERT(varchar(10), wr.Created_dt, 103) AS dateofCreation, '0' AS status,
        wpc.WagePaymentCycle AS paymentCycle, ISNULL(bw.AmountInSSP, 0) AS wageRateSSPTotal,
        b.Age, b.NameofHousehold, b.ContactNo, case when b.Sex = 'M' then 0 when b.Sex='F' then 1 end as gender,
        b.intStateid as state, b.intCountieid as county, b.intPayamid as payam, b.intBomasid as boma,
        b.Name1stAlternatePayee, b.FirstAlternatePayeeAge,
        case when b.FirstAlternatePayeeGender = 'M' then 0 when b.FirstAlternatePayeeGender='F' then 1 end as FirstAlternatePayeeGender,
        b.FirstAlternatePayeeMobile,
        b.Name2ndAlternatePayee, b.SecondAlternatePayeeAge,
        case when b.SecondAlternatePayeeGender = 'M' then 0 when b.SecondAlternatePayeeGender='F' then 1 end as SecondAlternatePayeeGender,
        b.SecondAlternatePayeeMobile
    FROM            dbo.td_WageReqBenDet AS bw INNER JOIN
                    dbo.tbl_BeneficiaryDet AS b ON b.intBenid = bw.intBenid INNER JOIN
                    dbo.tbl_WorkMaster AS w ON w.intWorkid = bw.intWorkid INNER JOIN
                    dbo.td_WorkWageRequest AS wr ON wr.intWorkid = w.intWorkid AND wr.intWagePaymentReqid = bw.intWagePaymentReqid
                                               LEFT OUTER JOIN dbo.tm_WagePaymentCycle AS wpc ON wpc.intPaymentCycleID = wr.PaymentCycle
                                               LEFT JOIN dbo.td_WageRequestAfisSync AS wras ON wr.intWagePaymentReqid = wras.intWagePaymentReqid
    WHERE        (wr.Status = '3') AND (b.household_number IS NOT NULL)
      AND (wr.intWagePaymentReqid like @intWagePaymentReqid and (sync_status != 1 or sync_status is null))

End
go

---------

create table tbl_Nominee
(
    id                          int identity,
    application_id              varchar(100),
    nominee_first_name          varchar(100),
    nominee_middle_name         varchar(100),
    nominee_last_name           varchar(100),
    nominee_nick_name           varchar(100),
    relationship_with_household int,
    nominee_age                 int,
    nominee_gender              int,
    is_read_write               bit,
    nominee_occupation          int,
    other_occupation            varchar(255),
    relationship_other          varchar(100)
)
go

-------

CREATE procedure [dbo].[insertNomineeInfo]

    @applicationId	varchar(100),
    @nomineeFirstName varchar(100),
    @nomineeMiddleName varchar(100),
    @nomineeLastName varchar(100),
    @nomineeNickName varchar(100),
    @relationshipWithHouseholdHead int,
    @relationshipOther varchar(100),
    @nomineeAge	int,
    @nomineeGender int,
    @isReadWrite bit,
    @nomineeOccupation int,
    @otherOccupation	varchar(100),
    @ReturnVal int output

as begin


    insert into tbl_Nominee
    (
        application_id,
        nominee_first_name,
        nominee_middle_name,
        nominee_last_name,
        nominee_nick_name,
        relationship_with_household,
        nominee_age,
        nominee_gender,
        is_read_write,
        nominee_occupation,
        other_occupation,
        relationship_other
    )
    values
        (

            @applicationId,
            @nomineeFirstName,
            @nomineeMiddleName,
            @nomineeLastName,
            @nomineeNickName,
            @relationshipWithHouseholdHead,
            @nomineeAge,
            @nomineeGender,
            @isReadWrite,
            @nomineeOccupation,
            @otherOccupation,
            @relationshipOther
        )
    Declare @ids  as int
    set @ids=@@IDENTITY
    set @ReturnVal = @ids

    return @ReturnVal
end
go


-----------------------

alter procedure [dbo].[HouseHoldNumberVerificationWagesNewwithBoma]
(
@household_number varchar(120),
@reqno varchar(8),
@work_id varchar(8),
@Bomaid varchar(10) -- BomaId length changed from 8 to 10
)
As

Begin
--Created by: Srikanth
--Created on; 24-03-2018
select intBenid,intIP from tbl_BeneficiaryDet where household_number like @household_number

select * from tbl_WorkMaster where intWorkid like @work_id

select * from td_WorkWageRequest where intWorkid like @work_id and intWagePaymentReqid like @reqno

select * from  td_WorkWageRequest where intWorkid like @work_id and intWagePaymentReqid like @reqno and intWagePaymentReqid in (select a.intWagePaymentReqid from td_WageReqBenDet as a where a.intWorkid like @work_id and a.intBenid in (select intBenid from  tbl_BeneficiaryDet where household_number like @household_number))

select a.intWagesReqBenTransid from td_WageReqBenDet as a where a.intWorkid like @work_id and a.intBenid in (select intBenid from  tbl_BeneficiaryDet where household_number like @household_number) and  intWagePaymentReqid like @reqno

if @work_id<>'999'
begin
select * from tbl_WorkMaster where intWorkid like @work_id and intBomasid like @Bomaid
end
else
begin
select * from tbl_WorkMaster where intWorkid like @work_id 
end
End
go


-----------------------------------

delete from tm_WorkActivities where 1=1;
delete from tm_AreaofWork where 1=1;

insert into tm_AreaofWork(AreaofWork) values
('Community Access Road (Maintenance and Rehabilitation)'), ('Flood Control Measures'), ('Forests and Agroforests'),
('Semi-Permanent Constructions'), ('Small Scale Food Production'),
('Social Infastructure Maintenance and Rehabilitation'),('Waste and Sanitation Management'),('Water Harvesting');

alter table tm_WorkActivities add climate_smart int;

insert into tm_WorkActivities(intAreaofWork, WorkActivity, climate_smart) values
((select intAreaofWork from tm_AreaofWork where AreaofWork='Small Scale Food Production'), 'Small Scale Farming', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Flood Control Measures'), 'Drainage Works and Maintenance', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Community Access Road (Maintenance and Rehabilitation)'), 'Road Maintenance and Rehabilitation', 2),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Semi-Permanent Constructions'), 'Bee Keeping', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Forests and Agroforests'), 'Trees Plantation', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Social Infastructure Maintenance and Rehabilitation'), 'Construction of community facility', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Flood Control Measures'), 'Flood Control Dykes', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Small Scale Food Production'), 'Agriculture/Farming Activities', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Community Access Road (Maintenance and Rehabilitation)'), 'Access Roads Clearance', 2),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Water Harvesting'), 'Shallow well Digging', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Semi-Permanent Constructions'), 'Brick Laying', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Waste and Sanitation Management'), 'Waste collection', 2),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Forests and Agroforests'), 'Tree Nursery beds', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Forests and Agroforests'), 'Seedling tree planting', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Social Infastructure Maintenance and Rehabilitation'), 'Rehabilitation of community center', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Waste and Sanitation Management'), 'Cleaning of public facilities', 2),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Flood Control Measures'), 'Opening of drainage system', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Waste and Sanitation Management'), 'Garbage Collection', 2),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Semi-Permanent Constructions'), 'Box culvert', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Waste and Sanitation Management'), 'Cleaning of Tarmac road', 2),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Waste and Sanitation Management'), 'Digging of pitlatrines', 2),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Semi-Permanent Constructions'), 'Moulding Blocks for the office', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Small Scale Food Production'), 'Farming', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Social Infastructure Maintenance and Rehabilitation'), 'Fencing of school', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Social Infastructure Maintenance and Rehabilitation'), 'Construction of boma center', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Community Access Road (Maintenance and Rehabilitation)'), 'Community Access Roads construction', 2),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Semi-Permanent Constructions'), 'Construction of public toilet', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Semi-Permanent Constructions'), 'Construction of shelters', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Small Scale Food Production'), 'Kitchen gardening', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Social Infastructure Maintenance and Rehabilitation'), 'Fencing of Bore Holes', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Social Infastructure Maintenance and Rehabilitation'), 'Construction of temporary learning structure', 0),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Flood Control Measures'), 'Road-side drainage systems', 1),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Waste and Sanitation Management'), 'General Cleaning and landscaping of common areas', 2),
((select intAreaofWork from tm_AreaofWork where AreaofWork='Small Scale Food Production'), 'Rehabilitation and / or Maintenance of small scale irrigation schemes', 1)
;

----------------------------------------

alter table tbl_WorkMaster add screening_status tinyint;

alter Procedure [dbo].[InsertUpdWorkProposal]          
(          
@intWorkid int,        
@WorkCode varchar(50),        
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
@screening_status tinyint -- this new column is added and insert & update op changed accordingly
)          
as begin          
          
          
if(@intWorkid>0)          
begin          
if((select COUNT(*) from tbl_WorkMaster where WorkCode=@WorkCode and intWorkid<> @intWorkid)>0)          
begin          
select 999          
return 999          
end          
else          
begin          
update tbl_WorkMaster set TypeofMeeting=@TypeofMeeting, Status=@Status,StatusDate=@StatusDate,PDC_ApprvDate=@PDC_ApprvDate,Remarks=@Remarks, MeetingDate=@MeetingDate, intStateid=@intStateid,intCountieid=@intCountieid,intPayamid=@intPayamid,intBomasid=@intBomasid,Modified_by= @Created_by,Modified_dt= GETDATE(),screening_status=@screening_status
where intWorkid like @intWorkid          
select 1          
return 1          
end          
end          
else          
begin          
if((select COUNT(*) from tbl_WorkMaster where WorkCode=@WorkCode)>0)          
begin          
select 999          
return 999          
end          
else          
begin          
insert into dbo.tbl_WorkMaster (intTSTid, TypeofMeeting,StatusDate,PDC_ApprvDate,Remarks, WorkCode, IPid, intPrjid, MeetingDate, intStateid, intCountieid, intPayamid, intBomasid,  Status,   Created_by, Created_dt, screening_status)
values((select top 1 intTST from dbo.td_IPDetails where intIP like @IPid), @TypeofMeeting,@StatusDate,@PDC_ApprvDate,@Remarks,@WorkCode, @IPid, @intPrjid, @MeetingDate, @intStateid, @intCountieid, @intPayamid, @intBomasid,  @Status,   @Created_by, GETDATE(), @screening_status)
select @@IDENTITY          
return @@IDENTITY          
end          
end          
end
go

-----------------------------------

alter Procedure [dbo].[getWorkProposalsearchLatestIP]
(
    @intStateid varchar(10),
    @intCountieid varchar(10),
    @intPayamid varchar(10),
    @intBomasid varchar(10),
    @WorkCode varchar(50),
    @IPid varchar(50),
    @intTSTid  varchar(50) ,
    @Status varchar(50) ,
    @AreaofWork  varchar(50) ,
    @WorkActivity varchar(50),
    @ProjectId varchar(10) ,
    @intUserId varchar(10),
    @screening_status varchar(10)
)
as begin
    SELECT distinct td_IPDetails.IPName ,tbl_WorkMaster.Status, tbl_WorkMaster.intWorkid, tbl_WorkMaster.WorkCode,  tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, tm_States.StateName, tbl_WorkMaster.intWorkid, tbl_WorkMaster.MeetingDate,
                    tbl_WorkMaster.PDC_ApprvDate, tbl_WorkMaster.Status
    FROM         tbl_WorkMaster INNER JOIN
                 tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid
                                inner join tm_Counties  on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid
                                inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid
                                inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid  inner join
                 td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
                                left outer join  tr_ProposedWorkAct on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkid
    where tbl_WorkMaster.intStateid like @intStateid and tbl_WorkMaster.intCountieid like @intCountieid and tbl_WorkMaster.intPayamid like @intPayamid
      and tbl_WorkMaster.intBomasid  like @intBomasid   and tbl_WorkMaster.intTSTid like @intTSTid and tbl_WorkMaster.IPid like @IPid  and (isnull(tbl_WorkMaster.Status,'') like @Status)
      and tbl_WorkMaster.created_by like @intUserId
      --and tbl_WorkMaster.Status !='IP-Submitted'
      --and tbl_WorkMaster.Status !='ES-Rejected'
      --and tbl_WorkMaster.Status !='QE-Approved'
      and (tbl_WorkMaster.WorkCode like '%'+@WorkCode+'%' )
      and (isnull(tr_ProposedWorkAct.intAreaofWork,'') like  @AreaofWork  )
      and (isnull(tr_ProposedWorkAct.intWorkActivity,'') like @WorkActivity  )
      and (isnull(tbl_WorkMaster.intPrjid,'') like  @ProjectId)
      and (isnull(tbl_WorkMaster.screening_status,'') like @screening_status)
    --or tr_ProposedWorkAct.WorkCode like  '%'+@WorkCode+'%')
end
go


----------------------------------


alter Procedure [dbo].[getWorkProposalsearchLatestDraft]
(
    @intStateid varchar(10),
    @intCountieid varchar(10),
    @intPayamid varchar(10),
    @intBomasid varchar(10),
    @WorkCode varchar(50),
    @Status varchar(50),
    @IPid varchar(50),
    @intTSTid  varchar(50) ,
    @AreaofWork  varchar(50) ,
    @WorkActivity varchar(50),
    @ProjectId varchar(10)
)
as begin

    SELECT distinct td_IPDetails.IPName ,tbl_WorkMaster.Status, tbl_WorkMaster.intWorkid, tbl_WorkMaster.WorkCode,  tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, tm_States.StateName, tbl_WorkMaster.intWorkid, tbl_WorkMaster.MeetingDate,
                    tbl_WorkMaster.PDC_ApprvDate, tbl_WorkMaster.Status
    FROM         tbl_WorkMaster INNER JOIN
                 tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid
                                inner join tm_Counties  on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid
                                inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid
                                inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid  inner join
                 td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
                                left outer join  tr_ProposedWorkAct on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkid
    where tbl_WorkMaster.intStateid like @intStateid and tbl_WorkMaster.intCountieid like @intCountieid and tbl_WorkMaster.intPayamid like @intPayamid
      and tbl_WorkMaster.intBomasid  like @intBomasid   and tbl_WorkMaster.intTSTid like @intTSTid and tbl_WorkMaster.IPid like @IPid
      --and tbl_WorkMaster.Status !='New Proposal'
      and tbl_WorkMaster.Status !='Approved'
      and tbl_WorkMaster.Status !='Funds Allocation'
      and tbl_WorkMaster.Status !='Fund Released'
      --and tbl_WorkMaster.Status !='InProgress'
      and tbl_WorkMaster.Status !='Closed'
      --and tbl_WorkMaster.Status !='Rejected'
      and tbl_WorkMaster.Status like '%'+ @Status + '%'
      and (tbl_WorkMaster.WorkCode like '%'+@WorkCode+'%' )
      and (isnull(tr_ProposedWorkAct.intAreaofWork,'') like  @AreaofWork  )
      and (isnull(tr_ProposedWorkAct.intWorkActivity,'') like @WorkActivity  )
      and (isnull(tbl_WorkMaster.intPrjid,'') like  @ProjectId)
      and tbl_WorkMaster.screening_status is not null and tbl_WorkMaster.screening_status != '2'
    --or tr_ProposedWorkAct.WorkCode like  '%'+@WorkCode+'%')
end
go


-----------------------------

alter Procedure [dbo].[getWorkProposalsearchLatestDraftReviewed]
(
    @intStateid varchar(10),
    @intCountieid varchar(10),
    @intPayamid varchar(10),
    @intBomasid varchar(10),
    @WorkCode varchar(50),
    @Status varchar(50),
    @IPid varchar(50),
    @intTSTid  varchar(50) ,
    @AreaofWork  varchar(50) ,
    @WorkActivity varchar(50),
    @ProjectId varchar(10)
)
as begin

    SELECT distinct td_IPDetails.IPName ,tbl_WorkMaster.Status, tbl_WorkMaster.intWorkid, tbl_WorkMaster.WorkCode,  tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, tm_States.StateName, tbl_WorkMaster.intWorkid, tbl_WorkMaster.MeetingDate,
                    tbl_WorkMaster.PDC_ApprvDate, tbl_WorkMaster.Status
    FROM         tbl_WorkMaster INNER JOIN
                 tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid
                                inner join tm_Counties  on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid
                                inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid
                                inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid  inner join
                 td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
                                left outer join  tr_ProposedWorkAct on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkid
    where tbl_WorkMaster.intStateid like @intStateid and tbl_WorkMaster.intCountieid like @intCountieid and tbl_WorkMaster.intPayamid like @intPayamid
      and tbl_WorkMaster.intBomasid  like @intBomasid   and tbl_WorkMaster.intTSTid like @intTSTid and tbl_WorkMaster.IPid like @IPid
      --and tbl_WorkMaster.Status !='New Proposal'
      --and tbl_WorkMaster.Status !='Approved'
      and tbl_WorkMaster.Status !='Funds Allocation'
      and tbl_WorkMaster.Status !='Fund Released'
      --and tbl_WorkMaster.Status !='InProgress'
      and tbl_WorkMaster.Status !='Closed'
      --and tbl_WorkMaster.Status !='Rejected'
      --and tbl_WorkMaster.Status !='Draft'
      and tbl_WorkMaster.Status like '%'+ @Status + '%'
      and (tbl_WorkMaster.WorkCode like '%'+@WorkCode+'%' )
      and (isnull(tr_ProposedWorkAct.intAreaofWork,'') like  @AreaofWork  )
      and (isnull(tr_ProposedWorkAct.intWorkActivity,'') like @WorkActivity  )
      and (isnull(tbl_WorkMaster.intPrjid,'') like  @ProjectId)
      and tbl_WorkMaster.screening_status is not null and tbl_WorkMaster.screening_status != '2'
    --or tr_ProposedWorkAct.WorkCode like  '%'+@WorkCode+'%')
end
go


--------------------------------------

alter Procedure [dbo].[getWorkProposalsearchLatest]
(
    @intStateid varchar(10),
    @intCountieid varchar(10),
    @intPayamid varchar(10),
    @intBomasid varchar(10),
    @WorkCode varchar(50),
    @IPid varchar(50),
    @intTSTid  varchar(50) ,
    @Status varchar(50) ,
    @AreaofWork  varchar(50) ,
    @WorkActivity varchar(50),
    @ProjectId varchar(10)
)
as begin
    SELECT distinct td_IPDetails.IPName ,tbl_WorkMaster.Status, tbl_WorkMaster.intWorkid, tbl_WorkMaster.WorkCode,  tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, tm_States.StateName, tbl_WorkMaster.intWorkid, tbl_WorkMaster.MeetingDate,
                    tbl_WorkMaster.PDC_ApprvDate, tbl_WorkMaster.Status
    FROM         tbl_WorkMaster INNER JOIN
                 tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid
                                inner join tm_Counties  on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid
                                inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid
                                inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid  inner join
                 td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
                                left outer join  tr_ProposedWorkAct on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkid
    where tbl_WorkMaster.intStateid like @intStateid and tbl_WorkMaster.intCountieid like @intCountieid and tbl_WorkMaster.intPayamid like @intPayamid
      and tbl_WorkMaster.intBomasid  like @intBomasid   and tbl_WorkMaster.intTSTid like @intTSTid and tbl_WorkMaster.IPid like @IPid  and tbl_WorkMaster.Status like @Status
      and tbl_WorkMaster.Status !='IP-Submitted'
      and tbl_WorkMaster.Status !='QE-Rejected'
      and tbl_WorkMaster.Status !='ES-Rejected'
      and tbl_WorkMaster.Status !='QE-Approved'
      and (tbl_WorkMaster.WorkCode like '%'+@WorkCode+'%' )
      and (isnull(tr_ProposedWorkAct.intAreaofWork,'') like  @AreaofWork  )
      and (isnull(tr_ProposedWorkAct.intWorkActivity,'') like @WorkActivity  )
      and (isnull(tbl_WorkMaster.intPrjid,'') like  @ProjectId)
      and tbl_WorkMaster.screening_status is not null and tbl_WorkMaster.screening_status != 2
    --or tr_ProposedWorkAct.WorkCode like  '%'+@WorkCode+'%')
end
go

--------------------------------

alter Procedure [dbo].[getWorkProgresssearchNew_Sp]
(
    @intStateid varchar(10),
    @intCountieid varchar(10),
    @intPayamid varchar(10),
    @intBomasid varchar(10),
    @WorkCode varchar(50),
    @IPid varchar(50),
    @intTSTid  varchar(50) ,
    @Status varchar(50) ,
    @AreaofWork  varchar(50) ,
    @WorkActivity varchar(50)
)
as begin
    SELECT distinct td_IPDetails.IPName ,tbl_WorkMaster.Status, tbl_WorkMaster.intWorkid,
                    tbl_WorkMaster.WorkCode,  tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName,
                    tm_States.StateName, tbl_WorkMaster.intWorkid, tbl_WorkMaster.MeetingDate,
                    tbl_WorkMaster.PDC_ApprvDate, tbl_WorkMaster.Status
    FROM         tbl_WorkMaster INNER JOIN
                 tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid
                                inner join tm_Counties  on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid
                                inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid
                                inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid  inner join
                 td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
                                left outer join  tr_ProposedWorkAct on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkid
    where
            tbl_WorkMaster.intWorkid <> 999 -- to exclude DIS project
      and tbl_WorkMaster.intStateid like @intStateid and tbl_WorkMaster.intCountieid like @intCountieid and tbl_WorkMaster.intPayamid like @intPayamid
      and tbl_WorkMaster.intBomasid  like @intBomasid   and tbl_WorkMaster.intTSTid like @intTSTid and tbl_WorkMaster.IPid like @IPid  and tbl_WorkMaster.Status like @Status
      and (tbl_WorkMaster.WorkCode like '%'+@WorkCode+'%' )
      and (isnull(tr_ProposedWorkAct.intAreaofWork,'') like  @AreaofWork  )
      and (isnull(tr_ProposedWorkAct.intWorkActivity,'') like @WorkActivity  )
      and (tbl_WorkMaster.Status not in( 'New Proposal','Rejected'))
      and tbl_WorkMaster.screening_status is not null and tbl_WorkMaster.screening_status != 2
    --or tr_ProposedWorkAct.WorkCode like  '%'+@WorkCode+'%')
end
go


----------------------------------------

alter Procedure [dbo].[getWorkProgressearch]
(
    @intStateid varchar(10),
    @intCountieid varchar(10),
    @intPayamid varchar(10),
    @intBomasid varchar(10),
    @WorkCode varchar(50),
    @IPid varchar(50),
    @intTSTid  varchar(50)
)
as begin
    SELECT distinct td_IPDetails.IPName, tbl_WorkMaster.Status,  tbl_WorkMaster.intWorkid,
                    tm_States.StateName, tm_Counties.CountieName, tm_Bomas.BomaName, tm_Payams.PayamName, tbl_WorkMaster.WorkCode,
                    td_ProjectDet.PrjName,td_ProjectDet.intIP
    FROM         td_ProjectDet INNER JOIN
                 tbl_WorkMaster ON td_ProjectDet.intPrjid = tbl_WorkMaster.intPrjid INNER JOIN
                 tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid inner join
                 tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid inner join
                 tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid inner join
                 tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid     inner join
                 td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
                               left outer join  tr_ProposedWorkAct on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkid
    where
            tbl_WorkMaster.intWorkid <> 999 -- to exclude DIS project
      and tbl_WorkMaster.intStateid like @intStateid and tbl_WorkMaster.intCountieid like @intCountieid and tbl_WorkMaster.intPayamid like @intPayamid
      and tbl_WorkMaster.intBomasid  like @intBomasid and tbl_WorkMaster.intTSTid like @intTSTid and tbl_WorkMaster.IPid like @IPid
      --and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Invoice Appovals' or tbl_WorkMaster.Status like 'Closed')
      and (tbl_WorkMaster.Status not in( 'New Proposal','Rejected'))
      and (tbl_WorkMaster.WorkCode like '%'+@WorkCode+'%')
      and tbl_WorkMaster.screening_status is not null and tbl_WorkMaster.screening_status != 2
    --or tr_ProposedWorkAct.WorkCode like  '%'+@WorkCode+'%')
end
go

-------------------------------------

alter table tbl_Users add email_receiver tinyint;

-----------------------------

create table tbl_audit_payroll_download
(
    AuditId      nvarchar(50) not null
        constraint PK_tbl_audit_payroll_download
            primary key,
    UserId       int,
    DownloadTime datetime,
    intStateid   int,
    intCountieid int,
    intPayamid   int,
    intBomasid   int
)
go

--------------------------------


