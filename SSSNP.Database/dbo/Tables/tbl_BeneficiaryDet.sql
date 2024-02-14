CREATE TABLE [dbo].[tbl_BeneficiaryDet] (
    [intBenid]                      INT             IDENTITY (1000, 1) NOT NULL,
    [intIP]                         INT             NOT NULL,
    [OLDEnrollmentNo]               VARCHAR (50)    NULL,
    [EnrollmentNo]                  VARCHAR (50)    NULL,
    [NameofHousehold]               VARCHAR (100)   NULL,
    [FamilySize]                    INT             NULL,
    [Sex]                           VARCHAR (1)     NULL,
    [Age]                           INT             NULL,
    [NameofSpouse]                  VARCHAR (100)   NULL,
    [NoofAdult]                     INT             NULL,
    [NoofDependents]                INT             NULL,
    [FamilyIncome]                  DECIMAL (18, 2) NULL,
    [intStateid]                    INT             NULL,
    [intCountieid]                  INT             NULL,
    [intPayamid]                    INT             NULL,
    [intBomasid]                    INT             NULL,
    [Address]                       VARCHAR (150)   NULL,
    [ContactNo]                     VARCHAR (15)    NULL,
    [Email]                         VARCHAR (50)    NULL,
    [SecondLineHome]                VARCHAR (15)    NULL,
    [IsDisability]                  VARCHAR (1)     NULL,
    [intDisability]                 INT             NULL,
    [BiometricNo]                   VARCHAR (50)    NULL,
    [NameofBank]                    VARCHAR (50)    NULL,
    [AccountNo]                     VARCHAR (15)    NULL,
    [WorkCode]                      VARCHAR (50)    NULL,
    [LegalStatus]                   VARCHAR (50)    NULL,
    [Created_by]                    INT             NULL,
    [Created_dt]                    DATETIME        CONSTRAINT [DF_tbl_BeneficiaryDet_Created_dt] DEFAULT (getdate()) NULL,
    [Modified_by]                   INT             NULL,
    [Modified_dt]                   DATETIME        NULL,
    [SOurceofIncome]                VARCHAR (70)    NULL,
    [RegYear]                       VARCHAR (10)    NULL,
    [PortYear]                      INT             NULL,
    [PortPhase]                     INT             NULL,
    [PortedSheetName]               VARCHAR (180)   NULL,
    [BDropout]                      VARCHAR (8)     NULL,
    [DropoutRemarks]                VARCHAR (280)   NULL,
    [GroupId]                       VARCHAR (50)    NULL,
    [BeneficiaryType]               VARCHAR (50)    NULL,
    [Name1stAlternatePayee]         VARCHAR (100)   NULL,
    [Name2ndAlternatePayee]         VARCHAR (100)   NULL,
    [NumberMaleAdults]              INT             NULL,
    [NumberFemaleAdults]            INT             NULL,
    [NumberAdultsAgeAbove65]        INT             NULL,
    [NumberAdultsAgeBetween45_64]   INT             NULL,
    [NumberAdultsAgeBetween18_30]   INT             NULL,
    [NumberAdultsAgeBetween30_45]   INT             NULL,
    [NumberFemaleChildren]          INT             NULL,
    [NumberMaleChildren]            INT             NULL,
    [NumberChildrenAgeAbove5years]  INT             NULL,
    [NumberChildrenAgeAbove12years] INT             NULL,
    [NumberChildrenAgeAbove18years] INT             NULL,
    [EthnicityTribeRgin]            VARCHAR (100)   NULL,
    [PhotoPath]                     VARCHAR (200)   NULL,
    [PhotoName]                     VARCHAR (200)   NULL,
    [NationalID]                    VARCHAR (100)   NULL,
    [PCPortFlag]                    VARCHAR (1)     NULL,
    [household_number]              VARCHAR (500)   NULL,
    [FirstAlternatePayeeGender]     VARCHAR (1)     NULL,
    [FirstAlternatePayeeAge]        INT             NULL,
    [FirstAlternatePayeeNationID]   VARCHAR (20)    NULL,
    [FirstAlternatePayeeMobile]     VARCHAR (30)    NULL,
    [FirstAlternatePayeePhotoName]  VARCHAR (200)   NULL,
    [FirstAlternatePayeePhotoPath]  VARCHAR (200)   NULL,
    [SecondAlternatePayeeGender]    VARCHAR (1)     NULL,
    [SecondAlternatePayeeAge]       INT             NULL,
    [SecondAlternateNationalID]     VARCHAR (20)    NULL,
    [SecondAlternatePayeeMobile]    VARCHAR (12)    NULL,
    [SecondAlternatepayeePhotoName] VARCHAR (200)   NULL,
    [SecondAlternatePayeePhotoPath] VARCHAR (200)   NULL,
    [supervisor_id]                 VARCHAR (15)    NULL,
    [member_number]                 VARCHAR (15)    NULL,
    [female_dependants]             INT             NULL,
    [male_dependants]               INT             NULL,
    [latitude]                      VARCHAR (20)    NULL,
    [longitude]                     VARCHAR (20)    NULL,
    [id_url]                        VARCHAR (250)   NULL,
    [TransferDate]                  DATETIME        NULL,
    [TransferUpdateat]              DATETIME        NULL,
    [alternate_number]              VARCHAR (340)   NULL,
    [Secondalternate_number]        VARCHAR (340)   NULL,
    [API_Status]                    VARCHAR (200)   NULL,
    [householdnameupdate]           VARCHAR (1)     NULL,
    [householddate]                 DATETIME        NULL,
    [TransferFlag]                  VARCHAR (1)     NULL,
    [OLDBomaid]                     INT             NULL,
    [EnrollmentNoBeforeTransfer]    VARCHAR (30)    NULL,
    [BomaTransferDate]              DATETIME        NULL,
    [BomaTransferBy]                INT             NULL,
    [thirdalternate_number]         VARCHAR (340)   NULL,
    [Name3rdAlternatePayee]         VARCHAR (100)   NULL,
    [ThirdAlternatePayeeGender]     VARCHAR (1)     NULL,
    [ThirdAlternatePayeeAge]        INT             NULL,
    [ThirdAlternatePayeeNationID]   VARCHAR (20)    NULL,
    [ThirdAlternatePayeeMobile]     VARCHAR (12)    NULL,
    [ThirdAlternatepayeePhotoName]  VARCHAR (200)   NULL,
    [ThirdAlternatePayeePhotoPath]  VARCHAR (200)   NULL,
    [FirstAlternateUpdDate]         DATETIME        NULL,
    [FirstAlternateUpdby]           INT             NULL,
    [SecondAlternateUpdDate]        DATETIME        NULL,
    [SecondAlternateUpdby]          INT             NULL,
    [ThirdAlternateUpdDate]         DATETIME        NULL,
    [ThirdAlternateUpdby]           INT             NULL,
    [TransferStaffid]               INT             NULL,
    [TransferStaffDt]               DATETIME        NULL,
    [InsertedStaffid]               INT             NULL,
    [InsertedStaffDt]               DATETIME        NULL,
    [ModifiedStaffid]               INT             NULL,
    [ModifiedStaffdt]               DATETIME        NULL,
    [UFlag]                         INT             NULL,
    [SelectionCriteria]             NVARCHAR (200)  NULL,
    [SelectionReason]               NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_tbl_BeneficiaryDet] PRIMARY KEY CLUSTERED ([intBenid] ASC),
    CONSTRAINT [FK_tbl_BeneficiaryDet_tm_Bomas] FOREIGN KEY ([intBomasid]) REFERENCES [dbo].[tm_Bomas] ([intBomasid]),
    CONSTRAINT [FK_tbl_BeneficiaryDet_tm_Counties] FOREIGN KEY ([intCountieid]) REFERENCES [dbo].[tm_Counties] ([intCountieid]),
    CONSTRAINT [FK_tbl_BeneficiaryDet_tm_Payams] FOREIGN KEY ([intPayamid]) REFERENCES [dbo].[tm_Payams] ([intPayamid]),
    CONSTRAINT [FK_tbl_BeneficiaryDet_tm_States] FOREIGN KEY ([intStateid]) REFERENCES [dbo].[tm_States] ([intStateid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet]
    ON [dbo].[tbl_BeneficiaryDet]([intIP] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_1]
    ON [dbo].[tbl_BeneficiaryDet]([intCountieid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_2]
    ON [dbo].[tbl_BeneficiaryDet]([intStateid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_3]
    ON [dbo].[tbl_BeneficiaryDet]([intPayamid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_4]
    ON [dbo].[tbl_BeneficiaryDet]([intBomasid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_intIPNew]
    ON [dbo].[tbl_BeneficiaryDet]([intIP] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_EnrollmentNoONENew]
    ON [dbo].[tbl_BeneficiaryDet]([EnrollmentNo] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_intStateidNew]
    ON [dbo].[tbl_BeneficiaryDet]([intStateid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_intCountieidNew]
    ON [dbo].[tbl_BeneficiaryDet]([intCountieid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_intPayamidNew]
    ON [dbo].[tbl_BeneficiaryDet]([intPayamid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_intBomasidNew]
    ON [dbo].[tbl_BeneficiaryDet]([intBomasid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_WorkCodeNew]
    ON [dbo].[tbl_BeneficiaryDet]([WorkCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_Created_dtNew]
    ON [dbo].[tbl_BeneficiaryDet]([Created_dt] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_GroupIdNew]
    ON [dbo].[tbl_BeneficiaryDet]([GroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryDet_household_numberNew]
    ON [dbo].[tbl_BeneficiaryDet]([household_number] ASC);

