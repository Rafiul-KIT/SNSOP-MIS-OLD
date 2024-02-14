

CREATE procedure [dbo].[APIDrillDownExport]                  
@intIP  varchar(15),
@status int

                         as
  begin 
      
	  IF (@STATUS = 2)
		  begin
				  select 
	det.intBenid, a.intIP as intIP, a.OLDEnrollmentNo as OLDEnrollmentNo, a.intBenTempid, det.NameofHousehold as BenfiName , a.EnrollmentNo, det.Age, isnull(case when det.Sex='F' then 'Female' else case when DET.Sex='M' then 'Male' else null end end,'No Data') as Householdgender, a.FamilySize as FamilySize, a.NameofSpouse , a.NoofAdult , a.NoofDependents, b.StateName, c.CountieName, d.PayamName, e.BomaName, a.ContactNo  , a.FamilyIncome, a.intCountieid, a.intStateid, a.intPayamid, a.intBomasid, a.Address, a.Email, a.SecondLineHome, a.IsDisability, a.intDisability, a.BiometricNo, a.NameofBank, a.AccountNo, a.WorkCode, a.LegalStatus, a.Created_by, a.Created_dt, a.Modified_by,a.Modified_dt, a.SOurceofIncome, a.RegYear, a.PortYear, a.PortPhase, a.PortedSheetName, a.BDropout, a.DropoutRemarks, a.GroupId, a.BeneficiaryType, a.Name1stAlternatePayee, a.Name2ndAlternatePayee, a.NumberMaleAdults, a.NumberFemaleAdults, a.NumberAdultsAgeAbove65, a.NumberAdultsAgeBetween45_64, a.NumberAdultsAgeBetween18_30, a.NumberAdultsAgeBetween30_45, a.NumberFemaleChildren, a.NumberMaleChildren, a.NumberChildrenAgeAbove5years, a.NumberChildrenAgeAbove12years, a.NumberChildrenAgeAbove18years, a.EthnicityTribeRgin, a.PhotoPath, a.PhotoName, a.NationalID, a.PCPortFlag, a.household_number, a.FirstAlternatePayeeGender, a.FirstAlternatePayeeAge, a.FirstAlternatePayeeNationID, a.FirstAlternatePayeeMobile, a.FirstAlternatePayeePhotoName, a.FirstAlternatePayeePhotoPath, a.SecondAlternatePayeeGender, a.SecondAlternatePayeeAge, a.SecondAlternateNationalID, a.SecondAlternatePayeeMobile, a.SecondAlternatepayeePhotoName, a.SecondAlternatePayeePhotoPath, a.supervisor_id, a.member_number,a.female_dependants, a.male_dependants, a.latitude, a.longitude, a.id_url, a.TransferDate, a.TransferUpdateat, a.alternate_number, a.Secondalternate_number, a.API_Status, a.householdnameupdate, a.householddate, det.SelectionCriteria  as Component, det.SelectionReason, det.TransferFlag, det.OLDBomaid, det.EnrollmentNoBeforeTransfer, det.BomaTransferDate, det.BomaTransferBy, det.thirdalternate_number, det.Name3rdAlternatePayee, det.FirstAlternateUpdDate, det.FirstAlternateUpdby, det.SecondAlternateUpdDate, det.SecondAlternateUpdby, det.TransferStaffid, det.TransferStaffDt, det.InsertedStaffid, det.InsertedStaffDt, det.ModifiedStaffid, det.ModifiedStaffdt, det.UFlag
	, det.ThirdAlternatePayeeGender, det.ThirdAlternatePayeeAge,det.ThirdAlternatePayeeNationID, det.ThirdAlternatePayeeMobile, det.ThirdAlternatepayeePhotoName, det.ThirdAlternatePayeePhotoPath
	from tbl_BeneficiaryServiceDet a
		inner join tbl_BeneficiaryDet det on a.household_number =det.household_number
		inner join tm_States b on b.intStateid = a.intStateid
		inner join tm_Counties c on c.intCountieid = a.intCountieid
		inner join tm_Payams d on d.intPayamid = a.intPayamid
		inner join tm_Bomas e on e.intBomasid = a.intBomasid
	
				where a.intIP = @intIP 
				and (a.BenStatus=2)
		  end

	  ELSE

	  BEGIN
			select 
			det.intBenid, a.intIP as intIP, a.OLDEnrollmentNo as OLDEnrollmentNo, a.intBenTempid, a.NameofHousehold as BenfiName , a.EnrollmentNo, a.Age, isnull(case when a.Sex='F' then 'Female' else case when a.Sex='M' then 'Male' else null end end,'No Data') as Householdgender, a.FamilySize as FamilySize, a.NameofSpouse , a.NoofAdult , a.NoofDependents, b.StateName, c.CountieName, d.PayamName, e.BomaName, a.ContactNo  , a.FamilyIncome, a.intCountieid, a.intStateid, a.intPayamid, a.intBomasid, a.Address, a.Email, a.SecondLineHome, a.IsDisability, a.intDisability, a.BiometricNo, a.NameofBank, a.AccountNo, a.WorkCode, a.LegalStatus, a.Created_by, a.Created_dt, a.Modified_by,a.Modified_dt, a.SOurceofIncome, a.RegYear, a.PortYear, a.PortPhase, a.PortedSheetName, a.BDropout, a.DropoutRemarks, a.GroupId, a.BeneficiaryType, a.Name1stAlternatePayee, a.Name2ndAlternatePayee, a.NumberMaleAdults, a.NumberFemaleAdults, a.NumberAdultsAgeAbove65, a.NumberAdultsAgeBetween45_64, a.NumberAdultsAgeBetween18_30, a.NumberAdultsAgeBetween30_45, a.NumberFemaleChildren, a.NumberMaleChildren, a.NumberChildrenAgeAbove5years, a.NumberChildrenAgeAbove12years, a.NumberChildrenAgeAbove18years, a.EthnicityTribeRgin, a.PhotoPath, a.PhotoName, a.NationalID, a.PCPortFlag, a.household_number, a.FirstAlternatePayeeGender, a.FirstAlternatePayeeAge, a.FirstAlternatePayeeNationID, a.FirstAlternatePayeeMobile, a.FirstAlternatePayeePhotoName, a.FirstAlternatePayeePhotoPath, a.SecondAlternatePayeeGender, a.SecondAlternatePayeeAge, a.SecondAlternateNationalID, a.SecondAlternatePayeeMobile, a.SecondAlternatepayeePhotoName, a.SecondAlternatePayeePhotoPath, a.supervisor_id, a.member_number,a.female_dependants, a.male_dependants, a.latitude, a.longitude, a.id_url, a.TransferDate, a.TransferUpdateat, a.alternate_number, a.Secondalternate_number, a.API_Status, a.householdnameupdate, a.householddate, a.SelectionCriteria  as Component, a.SelectionReason, det.TransferFlag, det.OLDBomaid, det.EnrollmentNoBeforeTransfer, det.BomaTransferDate, det.BomaTransferBy, det.thirdalternate_number, det.Name3rdAlternatePayee, det.FirstAlternateUpdDate, det.FirstAlternateUpdby, det.SecondAlternateUpdDate, det.SecondAlternateUpdby, det.TransferStaffid, det.TransferStaffDt, det.InsertedStaffid, det.InsertedStaffDt, det.ModifiedStaffid, det.ModifiedStaffdt, det.UFlag
			, det.ThirdAlternatePayeeGender, det.ThirdAlternatePayeeAge,det.ThirdAlternatePayeeNationID, det.ThirdAlternatePayeeMobile, det.ThirdAlternatepayeePhotoName, det.ThirdAlternatePayeePhotoPath
			from tbl_BeneficiaryServiceDet a
				inner join tm_States b on b.intStateid = a.intStateid
				inner join tm_Counties c on c.intCountieid = a.intCountieid
				inner join tm_Payams d on d.intPayamid = a.intPayamid
				inner join tm_Bomas e on e.intBomasid = a.intBomasid
				left join tbl_BeneficiaryDet det on a.household_number=det.household_number
			where a.intIP = @intIP 
			and (@status is null or a.BenStatus=@status)
		END 
		
end
GO

