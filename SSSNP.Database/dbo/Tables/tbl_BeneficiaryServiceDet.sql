CREATE TABLE [dbo].[tbl_BeneficiaryServiceDet] (
    [intBenTempid]                  INT             IDENTITY (1000, 1) NOT NULL,
    [intIP]                         INT             NOT NULL,
    [household_number]              VARCHAR (500)   NULL,
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
    [BenStatus]                     VARCHAR (50)    CONSTRAINT [DF_tbl_BeneficiaryServiceDet_BenStatus] DEFAULT ('1') NULL,
    [Created_by]                    INT             NULL,
    [Created_dt]                    DATETIME        CONSTRAINT [DF_tbl_BeneficiaryServiceDet_Created_dt] DEFAULT (getdate()) NULL,
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
    [PCPortFlag]                    VARCHAR (1)     CONSTRAINT [DF_tbl_BeneficiaryServiceDet_PCPortFlag] DEFAULT ('N') NULL,
    [FirstAlternatePayeeGender]     VARCHAR (1)     NULL,
    [FirstAlternatePayeeAge]        INT             NULL,
    [FirstAlternatePayeeNationID]   VARCHAR (20)    NULL,
    [FirstAlternatePayeeMobile]     VARCHAR (30)    NULL,
    [FirstAlternatePayeePhotoName]  VARCHAR (500)   NULL,
    [FirstAlternatePayeePhotoPath]  VARCHAR (500)   NULL,
    [SecondAlternatePayeeGender]    VARCHAR (1)     NULL,
    [SecondAlternatePayeeAge]       INT             NULL,
    [SecondAlternateNationalID]     VARCHAR (20)    NULL,
    [SecondAlternatePayeeMobile]    VARCHAR (12)    NULL,
    [SecondAlternatepayeePhotoName] VARCHAR (500)   NULL,
    [SecondAlternatePayeePhotoPath] VARCHAR (500)   NULL,
    [supervisor_id]                 VARCHAR (15)    NULL,
    [member_number]                 VARCHAR (15)    NULL,
    [female_dependants]             INT             NULL,
    [male_dependants]               INT             NULL,
    [latitude]                      VARCHAR (20)    NULL,
    [longitude]                     VARCHAR (20)    NULL,
    [id_url]                        VARCHAR (250)   NULL,
    [TransferDate]                  DATETIME        NULL,
    [TransferUpdateat]              DATETIME        NULL,
    [alternate_number]              VARCHAR (450)   NULL,
    [Secondalternate_number]        VARCHAR (450)   NULL,
    [API_Status]                    VARCHAR (200)   NULL,
    [intBenid]                      INT             NULL,
    [Remarks]                       NVARCHAR (MAX)  NULL,
    [householdnameupdate]           VARCHAR (1)     NULL,
    [householddate]                 DATETIME        NULL,
    [SelectionCriteria]             NVARCHAR (200)  NULL,
    [SelectionReason]               NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_tbl_BeneficiaryServiceDet] PRIMARY KEY CLUSTERED ([intBenTempid] ASC),
    CONSTRAINT [FK_tbl_BeneficiaryServiceDet_td_IPDetails] FOREIGN KEY ([intIP]) REFERENCES [dbo].[td_IPDetails] ([intIP]),
    CONSTRAINT [FK_tbl_BeneficiaryServiceDet_tm_Bomas] FOREIGN KEY ([intBomasid]) REFERENCES [dbo].[tm_Bomas] ([intBomasid]),
    CONSTRAINT [FK_tbl_BeneficiaryServiceDet_tm_Counties] FOREIGN KEY ([intCountieid]) REFERENCES [dbo].[tm_Counties] ([intCountieid]),
    CONSTRAINT [FK_tbl_BeneficiaryServiceDet_tm_Payams] FOREIGN KEY ([intPayamid]) REFERENCES [dbo].[tm_Payams] ([intPayamid]),
    CONSTRAINT [FK_tbl_BeneficiaryServiceDet_tm_States] FOREIGN KEY ([intStateid]) REFERENCES [dbo].[tm_States] ([intStateid])
);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryServiceDet_intIPNew]
    ON [dbo].[tbl_BeneficiaryServiceDet]([intIP] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryServiceDet_EnrollmentNoNew]
    ON [dbo].[tbl_BeneficiaryServiceDet]([EnrollmentNo] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryServiceDet_intStateidNew]
    ON [dbo].[tbl_BeneficiaryServiceDet]([intStateid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryServiceDet_intCountieidNew]
    ON [dbo].[tbl_BeneficiaryServiceDet]([intCountieid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryServiceDet_intPayamidNew]
    ON [dbo].[tbl_BeneficiaryServiceDet]([intPayamid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryServiceDet_intBomasidNew]
    ON [dbo].[tbl_BeneficiaryServiceDet]([intBomasid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryServiceDet_WorkCodeNew]
    ON [dbo].[tbl_BeneficiaryServiceDet]([WorkCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryServiceDet_Created_dtNew]
    ON [dbo].[tbl_BeneficiaryServiceDet]([Created_dt] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryServiceDet_GroupIdNew]
    ON [dbo].[tbl_BeneficiaryServiceDet]([GroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tbl_BeneficiaryServiceDet_household_numberNew]
    ON [dbo].[tbl_BeneficiaryServiceDet]([household_number] ASC);

