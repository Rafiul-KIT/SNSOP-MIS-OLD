/*
	Created by Alexander Sudakov
	Created on 29-Jul-2021
	Function is for "SEA" part of PBI reports 

	Input Parameters: @uid -- session User_ID (not null)
					  @inc_Rep_dStart -- first date of MC Incident reported (can be null)
					  @inc_Rep_dEnd -- last date of MC Incident reported (can be null)
					  @ctry -- additional, specifies the country to be processed (can be null). If null then all countries visible for session User_ID will be processed

	Usage sample :
		declare @uid uniqueidentifier, @inc_Rep_dStart date, @inc_Rep_dEnd date, @ctry int
		select @uid='67B2D8D8-0F5F-4FEC-836D-1D9A0B7E28A5',	-- Member State User
				@inc_Rep_dStart='2019-01-01'
				@inc_Rep_dEnd='2021-10-31'
				@ctry = 204
		select * from dbo.sfPBI_MS_DBrd (@uid, @inc_dStart, @inc_dEnd, @ctry)
*/
create function [dbo].[sfPBI_SEA_DBrd] (@uid uniqueidentifier, @inc_Rep_dStart date, @inc_Rep_dEnd date, @ctry int)
returns @ret table (
	MC_Inc_ID uniqueidentifier, Mission_ID int, ComplaintNo varchar(50), Inc_Date_Reported datetime, Inc_Date datetime, Compl_Status_ID tinyint, Compl_Patern_ID bit, 
	sbj_Prsn_ID uniqueidentifier, sbj_Type_ID smallint, sbj_SubType_ID smallint,
	Vct_ID int, Vct_AgeGroup_ID tinyint, Vct_Patern_Resolved_ID tinyint,
	Algn_ID uniqueidentifier, Algn_ThAreaType_ID int, Algn_ThAreaSubType_ID int, 
	Inv_ID uniqueidentifier, Inv_Entity_ID int, Inv_Subst_ID int, Inv_Status_ID int,
	iAct_ID bigint, iActType tinyint, iAct_TakenBy_ID tinyint, iAct_Type_ID tinyint, iAct_SubType_ID int,
	fAct_ID bigint, fActType tinyint, fAct_TakenBy_ID tinyint, fAct_Type_ID tinyint, fAct_SubType_ID int,
	Subj_Text varchar(100), VctAge_Text varchar(100), Ptrn_Text varchar(100), Algn_Text nvarchar(max), Invg_Text nvarchar(max), iAct_Text nvarchar(max), fAct_Text nvarchar(max))
as
begin

declare @t_ctry table (ctry_id int)
if @ctry>0 insert @t_ctry values (@ctry)
else insert @t_ctry select Country_ID from dbo.sfGet_MS_Countries(@uid)

;with 
dsc_stp as (
	select * from (values
		(1,1056),
		(2,1058),
		(3,1063),
		(4,1062),
		(5,1065),
		(6,1055),
		(7,1057),
		(8,1061),
		(9,1060),
		(10,1059),
		(11,1064)
	) t (ait_id, stp_id)
)
,x as (
select distinct i.MC_Inc_ID, i.Inc_Mission_ID Mission_ID, i.ComplaintNo, 
	cast(i.Inc_DateReported as date) Inc_Date_Reported, 
	case when sx.all_year between 1900 and 2052 and sx.all_month between 1 and 12 then datefromparts(sx.all_year,sx.all_month,1)
		 when i.StartYear between 1900 and 2052 and i.StartMonth between 1 and 12 then datefromparts(i.EndYear,i.EndMonth,1)
		 when i.EndYear between 1900 and 2052 and i.EndMonth between 1 and 12 then datefromparts(i.EndYear,i.EndMonth,1)
		 else isnull(i.Inc_Date,i.Inc_DateReported) end Inc_Date,
	--iif(i.Inc_Date is null and i.StartYear is not null, 
	--	isnull(datefromparts(i.EndYear,i.EndMonth,1),getdate()),
	--	coalesce(datefromparts(sx.all_year,sx.all_month,1), i.Inc_Date, i.Inc_DateReported)) Inc_Date,
	i.Inc_Close_Date,
	iif(i.Inc_Close_Date is null,0,1) Compl_Status_ID,
	--cast(iif(sum(vct.ALL_PaternityResolved) over(partition by i.MC_Inc_ID)>0,1,0) as bit) Compl_Patern_ID,
	max(isnull(vct.Compl_Patern_ID, 0)) over(partition by i.MC_Inc_ID) Compl_Patern_ID,

	sbj.MC_Prsn_ID sbj_Prsn_ID,
	sbj.ALL_PersonnelType sbj_Type_ID,
	sbj.ALL_PersonnelSubType sbj_SubType_ID, 

	vct.ALL_VictimId Vct_ID, 
	isnull(vct.ALL_AgeGroupId,0) Vct_AgeGroup_ID, -- 1:Child / 2:Adult / 3:Unknown
	vct.ALL_PaternityResolved Vct_Patern_Resolved_ID,

	sx.ALL_AllegationId Algn_ID,
	case when ait.ALL_AllegationTypeId=1 then 149
		 when ait.ALL_AllegationTypeId=2 then 148
		 else -1
	end Algn_Type_ID,	 
	ait.ALL_DescriptionId Algn_Descr_ID,

	cast(cast(sbj_inv.ALL_InvestigationId as varbinary) as uniqueidentifier) Inv_ID,
	iif(sbj_inv.ALL_SubjectId is null, null,sx_inv.ALL_InvEntity) ALL_InvEntity,

	case when ao.ALL_Result=6 then 1
		 when ao.ALL_Result=7 then 0
		 when ao.ALL_Result in (1,3,4,5) then 2
		 else -1
	end Inv_Subst_ID,

	case when ao.ALL_Result in (1,3,4,5) then 1	-- Not Completed
		 when ao.ALL_Result in (6,7) then 2		-- Completed
		 else 0									-- Not Conducted
	end Inv_Status_ID,

	iact.ALL_ActionId iAct_ID,
	iif(iact.ALL_ActionId is null, null, 1) iActType_ID,
	iact.ALL_ActionBy+1 iAct_TakenBy_ID,						-- 0:UN / 1:NG ==>> 1:UN / 2:NG / 3:OE (Outside Entity)
	case when iact.ALL_ActionId is null then null
		 when iact.ALL_SuspensionStart is not null then 2		-- 2:Disciplinary
		 when iact.ALL_ActionByUNId between 1 and 4 then 1	-- 1:Admin
		 when iact.ALL_ActionByNGId = 1 then 1				-- 1:Admin
		 when iact.ALL_ActionByNGId = 6 then 2				-- 2:Disciplinary
		 else 3												-- 3:Other
	end iAct_Type_ID,
	case when iact.ALL_ActionId is null then null
		 else iif(iact.ALL_ActionBy=0, isnull(nullif(iact.ALL_ActionByUNId,12),5)+100, isnull(nullif(iact.ALL_ActionByNGId,7),2)+150) end iAct_SubType_ID,

	(-1)*fact.ALL_FinalActionId fAct_ID,
	iif(fact.ALL_FinalActionId is null, null, 2) fActType_ID,
	fact.ALL_ActionBy+1 fAct_TakenBy_ID,						-- 0:UN / 1:NG ==>> 1:UN / 2:NG / 3:OE (Outside Entity)
	iif(fact.ALL_ProceedingId not in (1,2), 3, fact.ALL_ProceedingId) fAct_Type_ID,
	case when fact.ALL_FinalActionId is null then null
		 else iif(fact.ALL_ActionBy=0, isnull(nullif(fact.ALL_ActionByUNId,12),5)+200, isnull(nullif(fact.ALL_ActionByNGId,14),10)+250) end fAct_SubType_ID

from MC_Incident i
	join MC_Inc_Prsn_Link l on i.MC_Inc_ID=l.MC_Inc_ID and isnull(l.IsDeleted,0)=0 
	join SEA_Allegation sx on i.MC_Inc_ID=sx.ALL_IncidentId and isnull(sx.IsDeleted,0)=0
	join SEA_Allegation_Incident ai on sx.ALL_AllegationId=ai.ALL_AllegationId and isnull(ai.IsDeleted,0)=0
	join SEA_Allegation_Incident_Type ait on ai.ALL_AllegationId=ait.ALL_AllegationId and ai.ALL_IncidentId=ait.ALL_IncidentId and ait.ALL_DescriptionId<12 and isnull(ait.IsDeleted,0)=0
	join SEA_Allegation_Subject sbj on sx.ALL_AllegationId=sbj.ALL_AllegationId and isnull(sbj.IsDeleted,0)=0  and l.MC_Prsn_ID=sbj.MC_Prsn_ID 
	join SEA_Allegation_Subject_Incident_Type sit on sbj.ALL_SubjectId=sit.ALL_SubjectId and isnull(sit.IsDeleted,0)=0 and ait.ALL_IncidentTypeId=sit.ALL_IncidentTypeId
	join @t_ctry ctry on sbj.ALL_Nationality=coalesce(nullif(ctry.ctry_id,0), sbj.ALL_Nationality)
	left join SEA_Allegation_Outcome ao on sbj.ALL_AllegationId=ao.ALL_AllegationId and sbj.ALL_SubjectId=ao.ALL_SubjectId and sit.ALL_IncidentTypeId=ao.ALL_IncidentTypeId and isnull(ao.IsDeleted,0)=0
	left join SEA_Allegation_Investigation sx_inv on sx.ALL_AllegationId=sx_inv.ALL_AllegationId and isnull(sx_inv.IsDeleted,0)=0 and ALL_InvEntity<>10
	left join SEA_Allegation_Subject_Investigation sbj_inv on sbj.ALL_SubjectId=sbj_inv.ALL_SubjectId and sx_inv.ALL_InvestigationId=sbj_inv.ALL_InvestigationId and isnull(sbj_inv.IsDeleted,0)=0
	left join SEA_Allegation_InterimAction iact on sx.ALL_AllegationId=iact.ALL_AllegationId and sbj.ALL_SubjectId=iact.ALL_SubjectId and isnull(iact.IsDeleted,0)=0 and isnull(iact.ALL_ActionByUNId,0)+isnull(iact.ALL_ActionByNGId,0)>0 
	left join SEA_Allegation_FinalAction fact on sx.ALL_AllegationId=fact.ALL_AllegationId and sbj.ALL_SubjectId=fact.ALL_SubjectId and isnull(fact.IsDeleted,0)=0 and isnull(fact.ALL_ActionByUNId,0)+isnull(fact.ALL_ActionByNGId,0)>0
	left join (select vct.ALL_AllegationId, vis.ALL_IncidentTypeId, vct.ALL_VictimId, vis.ALL_SubjectId, vct.ALL_AgeGroupId, vct.ALL_PaternityResolved,  --vct.ALL_Age, vct.ALL_Gender
					case when ALL_StatusOfPaternityId = 3 then 1
						 when ALL_StatusOfPaternityId > 3 then 0
						 when ALL_StatusOfPaternityId in (1,2) and ALL_PaternityEstablished in (1,3) then 1
						 else 0
					end Compl_Patern_ID
				 from SEA_Allegation_Victim vct 
					join SEA_Allegation_Victim_Subject vis on vct.ALL_VictimId=vis.ALL_VictimId and isnull(vis.IsDeleted,0)=0
				) vct on sx.ALL_AllegationId=vct.ALL_AllegationId and ait.ALL_IncidentTypeId=vct.ALL_IncidentTypeId and sbj.ALL_SubjectId=vct.ALL_SubjectId
	left join xDBSEA sea on i.MC_Inc_ID=sea.MC_Inc_ID
where Inc_Prsn_Case_Role&8=8 
	and sbj.ALL_PersonnelType in (2,3)
	and sea.rpt_id is null
	and i.Inc_DateReported between isnull(@inc_Rep_dStart,'1900-01-01') and isnull(@inc_Rep_dEnd,'2049-12-31')
)
,b as (
	select MC_Inc_ID inc_id, sbj_Prsn_ID sbj_id, 150 Both
	from x
	where Algn_Type_ID>0
	group by MC_Inc_ID, sbj_Prsn_ID
	having min(Algn_Type_ID)<>max(Algn_Type_ID)
)
,v as (
	select distinct MC_Inc_ID, 
		stuff( (select concat(', '+char(10), count(Vct_AgeGroup_ID), case Vct_AgeGroup_ID when 1 then ' Child' when 2 then ' Adult' else ' Unknown' end)
				from (select MC_Inc_ID, Vct_ID, Vct_AgeGroup_ID from x where Vct_ID is not null group by MC_Inc_ID, Vct_ID, Vct_AgeGroup_ID) v
				where MC_Inc_ID=x.MC_Inc_ID
				group by MC_Inc_ID, Vct_AgeGroup_ID
				for xml path('')),1,3,'') VctAge_Text
	from x where Vct_ID is not null
) 
,p as (
	select distinct MC_Inc_ID, 
		stuff( (select concat(', '+char(10), count(Vct_Patern_Resolved_ID), case Vct_Patern_Resolved_ID when 0 then ' N/A' when 1 then ' Yes' when 2 then ' No' else ' Pending' end)
				from (select MC_Inc_ID, Vct_ID, Vct_Patern_Resolved_ID from x where Vct_ID is not null and Compl_Patern_ID=1 group by MC_Inc_ID, Vct_ID, Vct_Patern_Resolved_ID) v
				where MC_Inc_ID=x.MC_Inc_ID
				group by MC_Inc_ID, Vct_Patern_Resolved_ID
				for xml path('')),1,3,'') Ptrn_Text
	from x where Vct_ID is not null and Compl_Patern_ID=1
)
,s as (
	select distinct MC_Inc_ID, 
		stuff( (select concat(', '+char(10), count(sbj_SubType_ID), ' ', isnull(nullif(rtrim(sc.Staff_SubCat_Abbr),''),'Unknown'))
				--from sst 
				from (select MC_Inc_ID, sbj_Prsn_ID, sbj_SubType_ID from x where sbj_Prsn_ID is not null group by MC_Inc_ID, sbj_Prsn_ID, sbj_SubType_ID) s
					left join MC_Lookup..Lookup_Staff_SubCategory sc on s.sbj_SubType_ID=sc.Staff_SubCat_Code
				where MC_Inc_ID=x.MC_Inc_ID
				group by MC_Inc_ID, sbj_SubType_ID, sc.Staff_SubCat_Abbr
				for xml path('')),1,3,'') Subj_Text
	from x where sbj_Prsn_ID is not null
)
,g as (
	select distinct MC_Inc_ID, 
		stuff( (select ', '+concat(char(10), '- ', isnull(nullif(rtrim(TA_Type),''),'Unknown'))
				from (select distinct x.MC_Inc_ID, 'Unknown' TA_Type
					  from x
					  where Algn_ID is not null and Algn_Type_ID=-1
					  union
					  select distinct x.MC_Inc_ID, isnull(replace(dsc.ALL_Text,'[Legacy] ',''), 'N/A') 
					  from x left join MC_Lookup..Lookup_Allegation_Description dsc on x.Algn_Descr_ID=dsc.ALL_DescriptionId
					 where Algn_ID is not null ) s
				where MC_Inc_ID=x.MC_Inc_ID
				for xml path('')),1,3,'') Algn_Text
	from x where Algn_ID is not null
)
,i as (
	select distinct MC_Inc_ID, 
		stuff( (select ', '+concat(char(10), '- ',InvEntity)
				from (select distinct x.MC_Inc_ID, concat(case x.Inv_Subst_ID when 0 then 'Not Substantiated' when 1 then 'Substantiated' when 2 then 'Investigation Pending' else 'Investigation' end, ' (by '+inv.ALL_Text+')') InvEntity
					  from x left join MC_Lookup..Lookup_Allegation_InvestigatingEntities inv on x.ALL_InvEntity=inv.ALL_InvestigatingEntityId
					  where Inv_ID is not null) s
				where MC_Inc_ID=x.MC_Inc_ID
				for xml path('')),1,3,'') Invg_Text
	from x where Inv_ID is not null
)
--,i2 as (
--select distinct MC_Inc_ID,
--		stuff( (select distinct ', '+  
--					concat(char(10), '- ', iif(Vct_Patern_Resolved_ID=0, 'No paternity claims', concat(count(Vct_Patern_Resolved_ID), ' paternity claims. Outcome:', case Vct_Patern_Resolved_ID when 0 then 'N/A' when 1 then 'Yes' when 2 then 'No' else 'Pending' end)), '. / ',
--							case x.Inv_Subst_ID when 0 then 'Not Substantiated' when 1 then 'Substantiated' when 2 then 'Investigation Pending' else 'Investigation' end, ' (by '+inv.ALL_Text+')' )  
--				from x left join MC_Lookup..Lookup_Allegation_InvestigatingEntities inv on x.ALL_InvEntity=inv.ALL_InvestigatingEntityId 
--				where MC_Inc_ID=t.MC_Inc_ID and x.Vct_ID is not null and x.Inv_ID is not null
--				group by x.MC_Inc_ID, x.Algn_ID, x.Inv_Subst_ID, x.Vct_Patern_Resolved_ID, inv.ALL_Text
--				for xml path('')),
--			1,3,'') Invg_Text
--from x t where Inv_ID is not null
--)
,r0 as (
	select distinct MC_Inc_ID, iAct_SubType_ID, iAct_TakenBy_ID, count(*) over(partition by MC_Inc_ID, iAct_SubType_ID) ip_cnt
	from (select distinct MC_Inc_ID, iAct_SubType_ID, iAct_TakenBy_ID, sbj_Prsn_ID from x where iAct_ID is not null) r2
)
,r as (
	select distinct MC_Inc_ID, row_number() over(partition by MC_Inc_ID order by MC_Inc_ID) rn, 
		stuff( (select ', '+concat(char(10), '- ',iAct)
				from (select distinct  x.MC_Inc_ID, concat(ip_cnt,iif(ip_cnt=1,' subject: ',' subjects: '), isnull(a_un.ALL_Text,a_ng.ALL_Text)+' (by '+iif(x.iAct_TakenBy_ID=1, 'UN)', 'TCC/PCC)')) iAct
					  from r0 x 
						left join MC_Lookup..Lookup_Allegation_InterimActionsByUN a_un on x.iAct_SubType_ID-100=a_un.ALL_ActionByUNId and x.iAct_TakenBy_ID=1
						left join MC_Lookup..Lookup_Allegation_InterimActionsByNG a_ng on x.iAct_SubType_ID-150=a_ng.ALL_ActionByNGId and x.iAct_TakenBy_ID=2) s
				where MC_Inc_ID=x.MC_Inc_ID
				for xml path('')),1,3,'') iAct_Text
	from r0 x 
)
,c0 as (
	select MC_Inc_ID, fAct_SubType_ID, fAct_TakenBy_ID, count(*) over(partition by MC_Inc_ID, fAct_SubType_ID) fp_cnt
	from (select distinct MC_Inc_ID, fAct_SubType_ID, fAct_TakenBy_ID, sbj_Prsn_ID from x where fAct_ID is not null) c2
)
,c as (
	select distinct MC_Inc_ID, row_number() over(partition by MC_Inc_ID order by MC_Inc_ID) rn, 
		stuff( (select ', '+concat(char(10), '- ',fAct)
				from (select distinct  x.MC_Inc_ID, concat(fp_cnt,iif(fp_cnt=1,' subject: ',' subjects: '), isnull(a_un.ALL_Text,a_ng.ALL_Text)+' (by '+iif(x.fAct_TakenBy_ID=1, 'UN)', 'TCC/PCC)')) fAct
					  from c0 x
						left join MC_Lookup..Lookup_Allegation_FinalActionsByUN a_un on x.fAct_SubType_ID-200=a_un.ALL_ActionByUNId and x.fAct_TakenBy_ID=1
						left join MC_Lookup..Lookup_Allegation_FinalActionsByNG a_ng on x.fAct_SubType_ID-250=a_ng.ALL_ActionByNGId and x.fAct_TakenBy_ID=2) s
				where MC_Inc_ID=x.MC_Inc_ID
				for xml path('')),1,3,'') fAct_Text
	from c0 x
)

insert @ret
select x.MC_Inc_ID, Mission_ID, ComplaintNo, Inc_Date_Reported, Inc_Date, Compl_Status_ID, Compl_Patern_ID,
	sbj_Prsn_ID, sbj_Type_ID, sbj_SubType_ID,
	Vct_ID, Vct_AgeGroup_ID, Vct_Patern_Resolved_ID,
	Algn_ID,  isnull(b.Both, x.Algn_Type_ID) Algn_ThAreaType_ID,  isnull(d.stp_id,-1) Algn_ThAreaSubType_ID,
	Inv_ID,  iif(Inv_ID is null or ALL_InvEntity>9,-1, ALL_InvEntity+60) Inv_Entity_ID,  Inv_Subst_ID,  Inv_Status_ID,
	iAct_ID, iActType_ID, iAct_TakenBy_ID, iAct_Type_ID, iAct_SubType_ID,
	fAct_ID, fActType_ID, fAct_TakenBy_ID, fAct_Type_ID, fAct_SubType_ID,
	s.Subj_Text, v.VctAge_Text, p.Ptrn_Text, g.Algn_Text, i.Invg_Text, r.iAct_Text, c.fAct_Text
from x 
	left join b on x.MC_Inc_ID=b.inc_id and x.sbj_Prsn_ID=b.sbj_id
	left join dsc_stp d on x.Algn_Descr_ID=d.ait_id
	left join v on x.MC_Inc_ID=v.MC_Inc_ID
	left join p on x.MC_Inc_ID=p.MC_Inc_ID
	left join s on x.MC_Inc_ID=s.MC_Inc_ID
	left join g on x.MC_Inc_ID=g.MC_Inc_ID
	left join i on x.MC_Inc_ID=i.MC_Inc_ID
	left join r on x.MC_Inc_ID=r.MC_Inc_ID and r.rn=1
	left join c on x.MC_Inc_ID=c.MC_Inc_ID and c.rn=1
union all
select MC_Inc_ID, Mission_ID, ComplaintNo, Inc_Date_Reported, Inc_Date, Compl_Status_ID, Compl_Patern_ID,
	sbj_Prsn_ID, sbj_Type_ID, sbj_SubType_ID,
	Vct_ID, Vct_AgeGroup_ID, Vct_Patern_Resolved_ID,
	Algn_ID, Algn_ThAreaType_ID, Algn_ThAreaSubType_ID,
	Inv_ID,  Inv_Entity_ID,  Inv_Subst_ID,  Inv_Status_ID,
	iAct_ID, iActType_ID, iAct_TakenBy_ID, iAct_Type_ID, iAct_SubType_ID,
	fAct_ID, fActType_ID, fAct_TakenBy_ID, fAct_Type_ID, fAct_SubType_ID,
	Subj_Text, VctAge_Text, Ptrn_Text, Algn_Text, Invg_Text, iAct_Text, fAct_Text
from xDBSEA x
	join @t_ctry ctry on x.sbj_Prsn_Nat_ID=coalesce(nullif(ctry.ctry_id,0), x.sbj_Prsn_Nat_ID)
where Inc_Date_Reported between isnull(@inc_Rep_dStart,'1900-01-01') and isnull(@inc_Rep_dEnd,'2049-12-31')
option(recompile)

return
end;