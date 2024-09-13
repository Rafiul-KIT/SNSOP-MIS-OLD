use SNSOP_AFIS;

create trigger del_beneficiary_trigger
    on SNSOP_AFIS.dbo.beneficiary
    after delete
as
    begin
    delete tbsd from PBW_SNSOP.dbo.tbl_BeneficiaryServiceDet tbsd
                inner join deleted d on d.application_id=tbsd.household_number;
    end;


