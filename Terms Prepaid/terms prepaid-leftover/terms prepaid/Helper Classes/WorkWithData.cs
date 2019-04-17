using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using lanta.SQLConfig;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using terms_prepaid;
using terms_prepaid.Properties;
using MessageBox = System.Windows.Forms.MessageBox;


namespace terms_prepaid.Helpers
{


    public class WorkWithData
    {   
        #region consts

        private const string selectdogovorsetting = @"select top 1
		DG_CODE
	   ,DG_SOR_CODE
	   ,DG_PROCENT
	   ,DG_DISCOUNT
	   ,DG_PPAYMENTDATE
	   ,DG_PAYMENTDATE
	   ,DG_DISCOUNTSUM
       ,DG_RazmerP
	   ,DG_VISADATE
       ,DG_Price
       ,DG_RATE
       ,DG_TYPECOUNT
       ,DG_PAYED
       ,NS_QUERYFORDATE
       ,NS_NAME
       ,DA_STATUS
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PPAYMENTDATE' order by HI_DATE desc) as PPAYMENTDATEWHO
,(select top 1 HI_DATE as [Date] 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PPAYMENTDATE' order by HI_DATE desc) as PPAYMENTDATEWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PAYMENTDATE' order by HI_DATE desc) as PAYMENTDATEWHO
,(select top 1 HI_DATE as [Date] 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PAYMENTDATE' order by HI_DATE desc) as PAYMENTDATEWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_RazmerP' order by HI_DATE desc) as Razmer_PWHO
,(select top 1 HI_DATE as [Date] 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_RazmerP' order by HI_DATE desc) as Razmer_PWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_DISCOUNT' order by HI_DATE desc) as DISCOUNTWHO
,(select top 1 HI_DATE as [Date] 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_DISCOUNT' order by HI_DATE desc) as DISCOUNTWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_SOR_CODE' order by HI_DATE desc) as StatusWHO
,(select top 1 HI_DATE as [Date] 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_SOR_CODE' order by HI_DATE desc) as StatusWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PRICE' order by HI_DATE desc) as PriceWHO
,(select top 1 HI_DATE as [Date] 
from History inner join HistoryDetail on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PRICE' order by HI_DATE desc) as PriceWHY
from tbl_Dogovor
left join mk_DogovorAdd on dg_code collate Cyrillic_General_CI_AS =DA_DGCODE collate Cyrillic_General_CI_AS
left join mk_NewStatuses on NS_ID =DA_STaTUs
where DG_code = @dgcode";
        private const string updateDogovorSettings = @"update  tbl_Dogovor set 
	   DG_PROCENT =@PrePaydType
	   ,DG_PPAYMENTDATE=@ppaymentdate
	   ,DG_PAYMENTDATE=@paymentdate
	   ,DG_RazmerP=@prerayd
where DG_code = @dgcode
exec dbo.mk_dogovor_recalc @dg_code=@dgcode 
";
        private const string selectAviaBron = @"SELECT  
      distinct 
	  [DL_key]
      ,[airport_from_iata]
      ,[airport_to_iata]
      ,[date_from]
      ,[date_to]
      ,[avialine]
      ,[reis]
      ,[terminal_from]
      ,[terminal_to]
	  ,ai.AL_NAME
	  --,at.id_turist
	  ,ab.n_bron_seller
	  ,ab.id
	  ,apFrom.AP_NAME as airPortFrom
	  ,apTo.AP_name as airPortTo
	  ,cdFrom.CT_NAME as CityFrom
	  ,cdTo.CT_NAME as CityTo
	  ,cFrom.CN_NAME as CountryFrom
	  ,cTo.CN_NAME as CountryTo
  FROM [dbo].[mk_avia_reis] as ar 
  inner join mk_avia_reis_class as rc on rc.id_reis= ar.id
  inner join mk_avia_clases_by_ticket as cbt on cbt.id_class=rc.id
  inner join mk_avia_ticket as at on at.id =cbt.id_ticket
  inner join mk_avia_bron as ab on ab.id = at.id_bron
  left join Airline as ai on ai.AL_CODE = ar.avialine
  left join Airport as apFrom on apFrom.AP_CODE = ar.airport_from_iata
  left join Airport as apTo on apTo.AP_CODE = ar.airport_to_iata
  left join CityDictionary as cdFrom on apFrom.AP_CTKEY =cdFrom.CT_key
  left join CityDictionary as cdTo on apTo.AP_CTKEY =cdTo.CT_key
  left join Country as cFrom on cFrom.CN_KEY = cdFrom.CT_CNKEY
  left join Country as cTo on cTo.CN_KEY = cdTo.CT_CNKEY
 where ab.id = @idAvBron
 order by date_from";
        private const string selectaviaturist = @"select TU_NAMELAT as NAME,TU_FNAMELAT  AS FNAME ,TU_SNAMELAT AS SNAME,n_ticket AS TICKET from mk_avia_ticket 
inner join tbl_Turist on  tu_key =id_turist
where id_bron = @idBron
";
        private const string selectidAviaBrion = @"SELECT      
	  top 1
	  ab.id	  
  FROM [dbo].[mk_avia_reis] as ar 
  inner join mk_avia_reis_class as rc on rc.id_reis= ar.id
  inner join mk_avia_clases_by_ticket as cbt on cbt.id_class=rc.id
  inner join mk_avia_ticket as at on at.id =cbt.id_ticket
  inner join mk_avia_bron as ab on ab.id = at.id_bron
  where ar.DL_key =@dlkey";
        private const string selectAnnalateJornal = @"SELECT [AN_KEY]
      ,[DG_CODE]
      ,[DATE_OF_CREATE]
      ,[DATE_OF_OK]
      ,MANAGE_OK 
      ,case when [IS_FULL] = 1 then convert(bit,1) else convert(bit,0) end as IS_FULL
      ,[IS_PENALTY]
      ,[DATE_OF_CALCULATE]
      ,[MANAGE_CALCULATE]
      ,[REASON]
      ,[REASON_TEXT]
      ,[DOG_NUMBER]
      ,UPPER([CL_NAMERUS])+' '+UPPER([CL_FNAMERUS])+' '+UPPER([CL_SNAMERUS]) as name
      ,[CL_NAMERUS]
      ,[CL_FNAMERUS]
      ,[CL_SNAMERUS]
      ,[PR_BOSS]
      ,[PR_FULLNAME]	
      ,ManegerOk.US_Fullname as MANEGEROK
      ,ManagerCalc.US_Fullname  as MANAGERCALC
      ,BronirOk.US_FullName as BronOk 
      ,BRONIR_OK_DATE
      ,CloseManager.US_FullName  as  CloseManag 
      ,CLOSETASK_DATE
      ,pt.PT_NAME
      ,[PT_NEEDBRON]
      ,[PT_NEEDREALIZE]
      ,[PT_ButtonText]
      ,[PT_MessageText]
      ,[PT_QuetionText]
      ,[PT_NEEDPENALTY]  
	  FROM [dbo].[mk_Anulate] 
      left join UserList as ManegerOk on ManegerOk.US_KEY = MANAGE_OK
	  left join UserList as ManagerCalc on ManagerCalc.US_KEY = MANAGE_CALCULATE
      left join UserList as BronirOk on BronirOk.Us_key = BRONIR_OK
      left join UserList as CloseManager on  CloseManager.us_key =MANAG_CLOSETASK
      left join mk_Petition_Types as PT on PT_KEY =REASON  ";
        private const string insertProblemOk = @"INSERT INTO [dbo].[mk_ProblemsOk]
           ([PO_PROBLEMCODE]
           ,[PO_WHO]
           ,[PO_DATE]
           ,[PO_DGCODE]
           ,[PO_OKCODE]
           ,[PO_MESSAGE])
     VALUES
           (@PROBLEMCODE
           ,@WHO
           ,GetDate()
           ,@DGCODE
           ,@okCODE
           ,@OkMessage)";
        private const string selectTuristChanges = @"DECLARE @DGCodeMT varchar(10)
DECLARE @PerriodBeg DateTime
DECLARE @PerriodEnd DateTime
select @DGCodeMT= MCD_DGCOD, @PerriodBeg=dateadd(minute,-10,MDC_DATE_INSERT),@PerriodEnd=MDC_DATE_INSERT from mk_ChangesDogovor  where MCD_ID=@idmcd
SELECT  hd_text,hd_valueold as Remark1, hd_valuenew as Remark2, hi_text
FROM   HistoryDetail (NOLOCK)
		inner join history (nolock) on hi_id = hd_hiid
        WHERE HD_hiid in (select hi_id from history (nolock) where 
            HI_DGCod = @DGCodeMT and
            HI_Date between @PerriodBeg and @PerriodEnd and 
                HI_Type = 'TURIST' and HI_MOD ='UPD')
order by hd_id";
        private const string selectAnnulationByKey = @" select  AN_KEY,
DATE_OF_CREATE,
isnull(is_full,0) as is_full ,
isnull(IS_PENALTY,0) as IS_PENALTY,
DATE_OF_CALCULATE,
DATE_OF_OK,
ManagerOk.US_FullName as ManagOk,
REASON_TEXT,
CL_FNAMERUS,
CL_NAMERUS,
CL_SNAMERUS , 
ManagerCalculate.US_FullName as ManagCalc,
BronirOk.US_FullName as BronOk ,
BRONIR_OK_DATE,
CloseManager.US_FullName  as  CloseManag, 
CLOSETASK_DATE,
pt.PT_NAME
      ,[PT_NEEDBRON]
      ,[PT_NEEDREALIZE]
      ,[PT_ButtonText]
      ,[PT_MessageText]
      ,[PT_QuetionText]
      ,[PT_NEEDPENALTY]
      ,[PT_MessageNoText]
 from mk_Anulate 
 left join UserList as ManagerOk  on ManagerOk.US_KEY = MANAGE_OK
 left join UserList as ManagerCalculate on ManagerCalculate.Us_key = MANAGE_CALCULATE
 left join UserList as BronirOk on BronirOk.Us_key = BRONIR_OK
 left join UserList as CloseManager on  CloseManager.us_key =MANAG_CLOSETASK 
 left join mk_Petition_Types as PT on PT_KEY =REASON
 where AN_KEY =@ankey
  order by DATE_OF_CREATE desc";
        private const string selectAnnulation = @" select  AN_KEY,
DATE_OF_CREATE,
isnull(is_full,0) as is_full ,
isnull(IS_PENALTY,0) as IS_PENALTY,
DATE_OF_CALCULATE,
DATE_OF_OK,
ManagerOk.US_FullName as ManagOk,
REASON_TEXT,
CL_FNAMERUS,
CL_NAMERUS,
CL_SNAMERUS , 
ManagerCalculate.US_FullName as ManagCalc,
BronirOk.US_FullName as BronOk ,
BRONIR_OK_DATE,
CloseManager.US_FullName  as  CloseManag, 
CLOSETASK_DATE,
pt.PT_NAME  
 from mk_Anulate 
 left join UserList as ManagerOk  on ManagerOk.US_KEY = MANAGE_OK
 left join UserList as ManagerCalculate on ManagerCalculate.Us_key = MANAGE_CALCULATE
 left join UserList as BronirOk on BronirOk.Us_key = BRONIR_OK
 left join UserList as CloseManager on  CloseManager.us_key =MANAG_CLOSETASK 
 left join mk_Petition_Types as PT on PT_KEY =REASON
 where DG_CODE =@dgcode
  order by DATE_OF_CREATE desc";
        private const string selChanges = @"select package,saildate,date_change from dbo.mk_dogovor_change_iternary() where dg_code =@dgcode";
        private const string selOldItinerary = @"select activityDate as 'День',
locName as 'Порт',
case when arrival like '00:00:00'  or arrival like 'уточн:00'  then '-'   when arrival like '[0-9][0-9]:[0-9][0-9]:[0-9][0-9]' then substring(arrival,1,5) else arrival end  as 'Прибытие',
case when departure like '00:00:00'  or departure like 'уточн:00'  then '-'  when departure like '[0-9][0-9]:[0-9][0-9]:[0-9][0-9]' then substring(departure,1,5) else departure  end as 'Отправление' 
from dbo.Itinerary_history where package =@pak and sailDate=@saildate  and date_of =@changedate and [status] = 1 order by activityDate, convert(datetime, convert(varchar(10),activitydate,101)+ ' ' + case when charindex('уточн',arrival)>0 then REPLACE(arrival,'уточн','00:00')
                                                         when charindex('24',arrival)=1 then ''
                                                          when arrival='-' then ''
                                                          else  arrival end ),
                                                      convert(datetime, convert(varchar(10),activitydate,101)+ ' ' +     
                                                      case when charindex('уточн',departure)>0 then REPLACE(departure,'уточн','00:00')
                                                         when charindex('24',departure)=1 then '23:59:59'
                                                          when departure='-' then ''
                                                          else  departure end )           ";
    
        private const string select_changes = @"SELECT row_number() over (order by mcd_id) as rownum
       ,[mcd_id]
     , [MCD_DGCOD]
      ,case when MCC_NEED_DATE = 1 then MCC_NAME+isnull(' '+convert(varchar(10),MDC_DATE_INSERT,04),'') else MCC_NAME end as MCC_NAME
      ,[MCD_CHANGE_CODE]
      ,[MCD_Accept]
      ,[MCD_WHO_ACCEPT]
      ,[MDC_DATE_ACCEPT]
      
             FROM [dbo].[mk_ChangesDogovor] join
         dbo.mk_ChangesCodes on MCD_CHANGE_CODE=MCC_ID
        where MCD_Accept =0 and MCD_DGCOD = @dgcode";
        private const string insert_history = @"INSERT INTO [dbo].[History]
           ([HI_DGCOD]
           ,[HI_DATE]
           ,[HI_WHO]
           ,[HI_TEXT]
           ,[HI_MOD]
           ,[HI_REMARK]
)
     VALUES
           (@dgcode,
           GetDate(),
           @who,
           @text,
           @mod,
           @remark)";
        private const string selectItinerary = @"select distinct  activitydate  ,locname_ru  , case  when arrival='00:00:00' then '-'  else substring (arrival,1,5) end as arrival ,  case when departure ='00:00:00' then'-'  else  substring (departure,1,5) end as departure,
  convert(datetime, convert(varchar(10),activitydate,101)+ ' ' + case when charindex('уточн',arrival)>0 then REPLACE(arrival,'уточн','00:00')
                                                         when charindex('24',arrival)=1 then ''
                                                          when arrival='-' then ''
                                                          else  arrival end ) as tt
from  dbo.MK_iternary1() where dg_code=@dogovor
order by  convert(datetime, convert(varchar(10),activitydate,101)+ ' ' + case when charindex('уточн',arrival)>0 then REPLACE(arrival,'уточн','00:00')
                                                         when charindex('24',arrival)=1 then ''
                                                          when arrival='-' then ''
                                                          else  arrival end )  ";
       
        
        private const string selectProblemDogovors = @"SELECT [mp_dgcod] as DG_CODE
      ,[mp_Turdate] as DG_TURDATE
      ,[mp_MainMan] as DG_MAINMEN
      ,[mp_NDAY] as DG_NDAY
      ,[mp_NMEN] as DG_NMEN
      ,[mp_crdate] as DG_CRDATE
      ,[mp_price] as DG_PRICE
      ,[mp_bronir] as bronir
      ,[mp_bronKey] as bron_key
      ,[mp_managKey] as manag_key
      ,[mp_manag] as manag
      ,[mp_payd] as DG_PAYED
      ,[mp_optionDateEnd] as optionDateEnd
      ,[mp_dhCreateDate] as DH_CREATEDATE
      ,(select count(mp_problemCode) from mk_ProblemsBrons where mp_dgcod =mpb.mp_dgcod )  as countProblem
      ,(select count(distinct MCD_CHANGE_CODE) from dbo.mk_ChangesDogovor where isnull(MCD_Accept,0)=0 and MCD_DGCOD= mp_dgcod ) as countchanges
      ,[mp_paymentDate] as DG_PAYMENTDATE
      ,[mp_visastatuscode] as da_visaStatus
      ,mp_visastatus as VisaStatus
  FROM [dbo].[mk_ProblemsBrons] as mpb
    left join mk_VisaStatuses  with (nolock) on mp_visastatus = VS_Key
  where mp_problemCode =@ProblemCode
  group by mp_dgcod,
									
									 mp_Turdate,
									 mp_MainMan,
									 mp_NDAY,
									 mp_NMEN,
									 mp_crdate,
									 mp_price,
									 mp_bronir,
									 mp_bronKey,
									 mp_managKey,
									 mp_manag,
									 mp_payd,
									 mp_optionDateEnd,
									 mp_dhCreateDate,
									 mp_paymentDate,
                                     mp_visastatus,
                                     mp_visastatuscode";
        private const string selectDogovorListAdd = @"SELECT TOP 1 [tbl_dogovor_list_key]
      ,[PaymantDate]
      ,[PaymantValue]
      ,[HandWho]
      ,[PPaymentdaDate]
      ,[Komission]
      ,[Pacage]
      ,[shipcode]
      ,[brandcode]
      ,[cl_id]
      ,[Tipe_of_Komission]
      ,[dg_code]
      ,[bron]
      ,dateadd(day,DL_DAY-1,DL_TURDATE) as date
  FROM [dbo].[mk_DogovorListAdd]
inner join tbl_dogovorList on tbl_dogovor_list_key=Dl_key
  where tbl_dogovor_list_key = @dlkey";
        private const string selectDogovorList = @"SELECT [DL_DGCOD]
      ,DATEADD(day,DL_DAY-1,[DL_TURDATE]) as DL_TURDATE
      ,[DL_KEY]
      ,[DL_PAKETKEY]
      ,[DL_TRKEY]
      ,[DL_SVKEY]
      ,replace([DL_NAME],'#1111111','') as DL_NAME
      ,[DL_DAY]
      ,[DL_CODE]
      ,[DL_SUBCODE1]
      ,[DL_SUBCODE2]
      ,[DL_NMEN]
      ,[DL_NDAYS]
      ,[DL_COST]
      ,[DL_REALNETTO]
      ,[DL_BRUTTO]
      ,[DL_CONTROL]
      ,[CR_NAME]
  FROM [dbo].[tbl_DogovorList]
 left join dbo.Controls on [DL_CONTROL] =[CR_KEY]
where DL_DGCOD =@dgcode  and DL_SVKEY <> 3161
order by DL_TURDATE";

        private const string selectDogovorCreater = @"SELECT [DL_CREATOR]
  FROM [dbo].[tbl_DogovorList]
where DL_DGCOD =@dgcode";

        const string selectProblemCodes = @"SELECT [mpc_id]
                                                    ,[mpc_parametr]
                                                    ,[mpc_name]
                                                    ,[mpc_Visible]
                                                    ,[mpc_TableName]
                                                FROM [dbo].[mk_ProblemCodes] where mpc_Visible = 1";
        
        public const string selectTurists = @"
            --declare @dgcode varchar(10)
            --set @dgcode='COS50102A2' 
            SELECT 
                 [TU_KEY]
                ,[TU_NAMERUS]
                ,[TU_NAMELAT]
                ,[TU_FNAMERUS]
                ,[TU_FNAMELAT]
                ,[TU_SNAMERUS]
                ,isnull(convert(varchar(10),[TU_BIRTHDAY],104),'') as TU_BIRTHDAY
                ,isNull(TU_SNAMELAT,'') as TU_SNAMELAT
                ,isnull([TU_PASPORTTYPE],'') as TU_PASPORTTYPE
                ,isNull([TU_PASPORTNUM],'') as TU_PASPORTNUM
                ,isnull(convert(varchar(10),[TU_PASPORTDATE],104),'') as TU_PASPORTDATE
                ,isnull(convert(varchar(10),[TU_PASPORTDATEEND],104),'') as TU_PASPORTDATEEND
                ,[TU_PASPORTBYWHOM]
                ,isnull([TU_PASPRUSER],'')as TU_PASPRUSER
                ,isnull([TU_PASPRUNUM],'')as TU_PASPRUNUM
                ,isnull(convert(varchar(10),[TU_PASPRUDATE],104),'') as TU_PASPRUDATE
                ,[TU_PASPRUBYWHOM]
             FROM [dbo].[tbl_Turist]
             where tu_dgcod=@dgcode";
        private const string selAction = @"SELECT [DL_key]
      ,[actions_id]
      ,[Text]
      ,[isBonus]
  FROM [dbo].[mk_actions_options]
  where actions_id >0 and  DL_key =@dlkey and  isBonus= @bon";
        private const string selProbem = @"select row_number() over (order by mp_problemCode) as rownum,*  from 
(SELECT --row_number() over (order by mp_problemCode) as rownum
        [mp_dgcod]
      ,[mp_problemCode]
      ,mpc_name
      ,-1 as ProblemStatusCode
      ,Null as ProblemStatusDate
      ,PS_NAME as ProblemStatus
      ,'' as ProblemMessage
  FROM [dbo].[mk_ProblemsBrons]
  inner join dbo.mk_ProblemCodes on mp_problemCode=mpc_id
  inner join dbo.mk_ProblemStatuses on ps_Id=-1
  where mp_dgcod=@dgcode
  --order by mp_problemCode
  union
  select PO_DGCODE,PO_PROBLEMCODE,mpc_name, PO_OKCODE,PO_DATE,PS_NAME +isnull(' '+ convert(varchar(10),PO_DATE,04),'') as ProblemStatus,PO_MESSAGE as problemMessage
  from dbo.mk_ProblemsOk
  inner join dbo.mk_ProblemCodes on PO_PROBLEMCODE=mpc_id
  inner join dbo.mk_ProblemStatuses on ps_Id=PO_OKCODE
  where PO_DGCODE=@dgcode ) as probTabl
  order by mp_problemCode";
        private const string update_changesDogovor = @"Update mk_ChangesDogovor set MCD_Accept = @accept,  MCD_WHO_ACCEPT =@user ,MDC_DATE_ACCEPT =GetDate() where  MCD_ID = @id";

        const string updateAnnulateOk =
              @"Update mk_Anulate set  DATE_OF_OK =Getdate(),MANAGE_OK =isnull((select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()),0) where AN_KEY = @key";
        const string updateAnnulateClose =
             @"Update mk_Anulate set  CLOSETASK_DATE =Getdate(),MANAG_CLOSETASK =isnull((select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()),0) where AN_KEY = @key";
        const string updateAnnulateBronOk =
             @"Update mk_Anulate set  BRONIR_OK_DATE =Getdate(),BRONIR_OK =isnull((select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()),0) where AN_KEY = @key";
        const string updateAnnulateCalc =
                @"Update mk_Anulate set  DATE_OF_CALCULATE =Getdate(),MANAGE_CALCULATE =isnull((select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()),0) ,IS_PENALTY = @penalty where AN_KEY = @key";

        private const string selProbleOkCodes = @"SELECT  [PS_ID] 
      ,[PS_NAME] 
  FROM [dbo].[mk_ProblemStatuses]
  where [PS_VISIBLE] = 1 ";

        private const string selectPartner = @"select  top 1 PR_NAME,PR_FULLNAME,PR_EMAIL,PR_Phones, +isnull(PR_CITY+',','') + isnull(PR_ADRESS,'') as PR_ADRESS from tbl_Partners  where PR_key = @prkey";

        private const string selectRegions = @"SELECT [LR_Key] as [key]
                                                ,[LR_Name] as name
                                            FROM [dbo].[LantaT_Region]";
        private const string selectRegionByDogovor = @"select top 1 DA_region from mk_DogovorAdd where  DA_DGCODE =@dgcode";
        #endregion
      
        public static void UpdateFileStatusOk(string filename)
        {
            using (SqlCommand com = new SqlCommand(@"update Lanta_PersonalArea_Download set pad_PrintDate = getDate(),pad_PrintManager =(select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()),pad_Description = '' where pad_FileName =@filename", Connection))
            {
                com.Parameters.AddWithValue("@filename", filename);
                com.ExecuteNonQuery();
            }
            DataTable turistsTable = GetDownloadedFileTurists(filename);
            string type = GetDownloadedFileType(filename);
            string dgcode = GetDownloadedFileDgcode(filename);
            foreach (DataRow row in turistsTable.Rows)
            {
                string message = "Из личного кабинета загружен документ " + type + " на туриста " +
                                 row.Field<string>("turistName") + ". Состояние документа - ПРОВЕРЕН.";
                InsertHistory(dgcode,message,"DR","");
            }
        }
        
        public static string GetDownloadedFileDgcode(string filename)
        {
            string rezult = string.Empty;
            using (SqlCommand com = new SqlCommand("select top 1 dg_code from Lanta_PersonalArea_Download as pad inner join tbl_Dogovor as dg on pad.pad_DGKEY=dg.DG_Key where pad.pad_FileName =@filename", _connection))
            {
                com.Parameters.AddWithValue("@filename", filename);
                rezult = (string)com.ExecuteScalar();
            }
            return rezult;

        }
        public static DataTable GetDownloadedFileTurists(string filename)
        {
            DataTable turistTable = new DataTable();
            using (
                SqlDataAdapter adapter =
                    new SqlDataAdapter(
                        "select isnull(TU_NAMELAT,'')+' '+isnull(TU_FNAMELAT,'') as turistName from tbl_Turist where TU_KEY in(select pad_tukey from Lanta_PersonalArea_Download where pad_FileName =@fileName)",
                        _connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@filename", filename);
                adapter.Fill(turistTable);
            }
            return turistTable;
        }
        public static string GetDownloadedFileType(string filename)
        {
            string rezult= string.Empty;
            using (SqlCommand com = new SqlCommand("select top 1 dds.ddgName from Lanta_PersonalArea_Download as pad  inner join Lanta_DictDocSpr as dds on dds.ddgID = pad.pad_KeyDocuments where pad.pad_FileName =@fileName", _connection))
            {
                com.Parameters.AddWithValue("@filename", filename);
                rezult = (string) com.ExecuteScalar();
            }
            return rezult;

        } 
        public static DataTable GetOptionDates(string dgcode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("mk_services_options",Connection))
            {
                adapter.SelectCommand.CommandType =CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.Fill(dt);
            }
            return dt;
        }
        public static void UpdateFileStatusNo(string filename,string desc)
        {
            
            using (SqlCommand com = new SqlCommand(@"update Lanta_PersonalArea_Download set pad_PrintDate = getDate(),pad_PrintManager =(select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()),pad_Description = @desc  where pad_FileName =@filename", Connection))
            {
                com.Parameters.AddWithValue("@filename", filename);
                com.Parameters.AddWithValue("@desc", desc);
                com.ExecuteNonQuery();
            }
            DataTable turistsTable = GetDownloadedFileTurists(filename);
            string type = GetDownloadedFileType(filename);
            string dgcode = GetDownloadedFileDgcode(filename);
            foreach (DataRow row in turistsTable.Rows)
            {
                string message = "Из личного кабинета загружен документ " + type + " на туриста " +
                                 row.Field<string>("turistName") + ". Состояние документа - НЕ ПОДХОДИТ: " + desc;
                InsertHistory(dgcode, message, "DR", "");
            }
        }
        public static int? GetRegionByDogovor(string dgcode)
       {
           int? idreg;
           using (SqlCommand com = new SqlCommand(selectRegionByDogovor,Connection))
           {
               com.Parameters.AddWithValue("@dgcode", dgcode);
               var temp = com.ExecuteScalar();
               
               if (temp == DBNull.Value)
               {
                   idreg = null;
               }
               else
               {
                   idreg = (int?) temp;
               }
                
           }
           return idreg;
       }
        public static object GetDateOptionByDlkey(int dl_key)
        {

            using (SqlCommand com = new SqlCommand("select OP_DATe_end from mk_options where OP_dlkey =@dlkey and  op_id in(select max(op_id) from mk_options group by OP_DLKEY)", Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dl_key);
                object obj = com.ExecuteScalar();
                if (obj==DBNull.Value||obj==null) return null;
                return (DateTime) obj;
            }
        }
        
        public static DataTable GetRegions()
        {
            DataTable _dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectRegions,Connection))
            {
                adapter.Fill(_dt);
            }
            return _dt;
        }
        public static DataTable FindTurists(string partnerName)
        {
            DataTable _dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("mk_GetTurists", Connection))
            {
                SqlCommand com = adapter.SelectCommand;
                com.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(partnerName.Trim()))
                {
                    com.Parameters.AddWithValue("@turist", partnerName.Trim());
                }
                adapter.Fill(_dt);
            }
            return _dt;
        }
        
        public static DataTable FindPartners(string partnerName)
        {
            DataTable _dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("mk_GetPartners",Connection))
            {
                SqlCommand com = adapter.SelectCommand;
                com.CommandType =CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(partnerName.Trim()))
                {
                    com.Parameters.AddWithValue("@PartnerName", partnerName.Trim());
                }
                adapter.Fill(_dt);
            }
            return _dt;
        }
        public static DataRow GetPartnerData(int prkey)
        {
            DataRow row;
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectPartner, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@prkey", prkey);
                DataTable _dt = new DataTable();
                adapter.Fill(_dt);
                row = _dt.Rows[0];
            }
            return row;
        }
        public static DataTable GetTuristsByItinerary(DateTime dtBegin,DateTime dtEnd, string turist,string dgcode,bool needCruise )
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("mk_turusts_by_marshrut",Connection))
            {
                SqlCommand com = adapter.SelectCommand;
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@datebegin", dtBegin);
                com.Parameters.AddWithValue("@dateend", dtEnd);
                if (!(string.IsNullOrEmpty(turist.Trim())))
                {
                    com.Parameters.AddWithValue("@Turist",turist.Trim());
                }
                if (needCruise)
                {
                    com.Parameters.AddWithValue("@withcruise", 1);
                }
                else
                {
                    com.Parameters.AddWithValue("@withcruise", 0);
                }
                if (!(string.IsNullOrEmpty(dgcode.Trim())))
                {
                    com.Parameters.AddWithValue("@dgcode", dgcode.Trim());
                }
                adapter.Fill(dt);
            }
            return dt;
        }
        public static DataTable GetProblemOkStatuses()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selProbleOkCodes,Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }
        public static void ChekDogovorState(int? dogovorID,string dgcode)
        {

            using (SqlCommand com = new SqlCommand("dbo.mk_CheckStateDogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dgcode", dgcode);
                if (dogovorID != null)
                {
                    com.Parameters.AddWithValue("@dogovorId", dogovorID);
                }
                com.ExecuteNonQuery();
            }
        }
        public static DataTable GetMessageDogovors()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select distinct hi_dgcod from history left join mk_messageStatus on HI_ID=MS_HIID  where isnull(MS_IsRead,0) =0 and  hi_mod in ('MTM','WWW')",Connection);
            adapter.Fill(dt);
            return dt;
        }
        public static DataTable GetTuristChanges(int id)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter =new SqlDataAdapter(selectTuristChanges,Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@idmcd", id);
                adapter.Fill(dt);
            }
            return dt;
        }
        public static void UpdateAnnulateClose(int id)
        {

            using (SqlCommand com = new SqlCommand(updateAnnulateClose, Connection))
            {
                com.Parameters.AddWithValue("@key", id);
                com.ExecuteNonQuery();

            }
        }
        public static void UpdateAnnulateOk(int id)
        {
          
            using (SqlCommand com = new SqlCommand(updateAnnulateOk,Connection))
            {
                com.Parameters.AddWithValue("@key", id);
                com.ExecuteNonQuery();
               
            }
        }
        public static void UpdateAnnulateBronOk(int id)
        {

            using (SqlCommand com = new SqlCommand(updateAnnulateBronOk, Connection))
            {
                com.Parameters.AddWithValue("@key", id);
                com.ExecuteNonQuery();

            }
        }

        public static DataTable GetStatuses()
        {
            DataTable rez= new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("select *  from dbo.mk_GetStatusesDogovor()",Connection))
            {
                adapter.Fill(rez);
            }
            return rez;
        }

        static public void UpdateProblemOk(int codeProblem, int okCode,string okMessage,string dgcode)
        {
            using (SqlCommand com = new SqlCommand(insertProblemOk,Connection))
            {
                com.Parameters.AddWithValue("@PROBLEMCODE", codeProblem);
                com.Parameters.AddWithValue("@WHO", GetUserName());
                com.Parameters.AddWithValue("@DGCODE", dgcode);
                com.Parameters.AddWithValue("@okCODE", okCode);
                com.Parameters.AddWithValue("@okMessage", okMessage);
                com.ExecuteNonQuery();
            }
        }
        public static DataRow GetDogovorSettings(string dgcode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectdogovorsetting, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.Fill(dt);
            }
            return dt.Rows[0];
        } 
        public static void UpdateAnnulateCalculate(int id,bool needPen =true)
        {
            
           
            bool flag = false;
            if (needPen)
            {
                DialogResult dr = MessageBox.Show("При обработки заявления возникли штрафы?", "", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    flag = true;
                }
            }
            using (SqlCommand com = new SqlCommand(updateAnnulateCalc, Connection))
            {
                com.Parameters.AddWithValue("@key", id);
                if (!needPen)
                {
                    com.Parameters.AddWithValue("@penalty", DBNull.Value);
                }
                else
                {
                    com.Parameters.AddWithValue("@penalty", flag);
                }
                
                com.ExecuteNonQuery();

            }
        }
        public static DataTable GetDaysTasks()
        {
            DataTable dt=  new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("mk_GetDayTasks",Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dt);
            }
            return dt;
        }
        public static DataTable GetAviaName(int dlkey)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("mk_GetAviaName",Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dlkey", dlkey);
                adapter.Fill(dt);
            }
            return dt;
        }
        public static DataTable GetAnnulateJournal(bool ViewOk =false,bool ViewCalc=false)
        {
            DataTable dt = new DataTable();

            string whereString = "\n where MANAGE_OK is null and MANAG_CLOSETASK is null";
            string whereString2 = "\n where MANAGE_OK is null and MANAGE_CALCULATE is null and MANAG_CLOSETASK is null";
            string whereString3 = "\n where MANAGE_CALCULATE is null and MANAG_CLOSETASK is null";
            string query = selectAnnalateJornal;
            if (!ViewOk && !ViewCalc)
            {
                query += whereString2;
            }
            else if (!ViewCalc)
            {
                query += whereString3;
            }
            else if (!ViewOk)
            {
                query += whereString;
            }
            using (SqlDataAdapter adapter = new SqlDataAdapter(query,Connection) )
            {
                adapter.Fill(dt);
            }
            return dt;
        }
        public static DataTable GetAnnulationByDogovor(string dgcode)
        {
            DataTable _dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectAnnulation,Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.Fill(_dt);

            }
            return _dt;
        }
        public static bool IsAnnulation(string dgcode)
        {
            if (GetAnnulationByDogovor(dgcode).Rows.Count > 0) 
                return true;
            return false;
        }
        
        public static int GetCountAnnulate()
        {
           return GetAnnulateJournal().Rows.Count;
        }
        public  static DataTable GetFiltersData()
        {
            DataTable dt = new DataTable();
            using (
                SqlDataAdapter adapter =
                    new SqlDataAdapter(@"select  [key_filter] ,[name],[group_filter] from [mk_filters_setting] where visible=1 order by group_filter,key_filter  ",
                                       Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }
        //public static DataTable GetAnnulation(string dgcode)
        //{
        //    DataTable dt = new DataTable();
        //    const string selAnnul = @"";
        //    using (SqlDataAdapter adapter = new SqlDataAdapter(selectAnnalateJornal,Connection))
        //    {
                
        //    }
        //    return dt;
        //}
        public static DataTable GetAnnulateReasons()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT  [AR_Key] as [key]
      ,[AR_NAME] as name
      
  FROM [dbo].[AnnulReasons]", Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }
        public static void SetAnnulateDogovor(string dgcode, int reason)
        {
            using (SqlCommand com = new SqlCommand("mk_putevka_anulate",Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dg_code", dgcode);
                com.Parameters.AddWithValue("@reason", reason);
                com.ExecuteNonQuery();
            }
        }
        public static DataSet GetAllProblems()
        {
            int myId = GetUserID();
            DataSet problemsSet = new DataSet();
            DataTable problemCodes = GetProblemCodes();
            foreach (DataRow row in problemCodes.Rows)
            {
                string tableName = row.Field<string>("mpc_TableName");
                problemsSet.Tables.Add(new DataTable(tableName));
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectProblemDogovors, Connection))
                {
                    SqlCommand com = adapter.SelectCommand;
                    //com.CommandType = CommandType.StoredProcedure;
                    //string[] paramerts = row.Field<string>("mpc_parametr").Replace("\t","").Replace("\n","").Split(',');
                    //foreach (string paramert in paramerts)
                    //{
                    //    string[] paramertDetail = paramert.Split('=');
                    //    com.Parameters.AddWithValue(paramertDetail[0].Trim(),int.Parse(paramertDetail[1].Trim()));
                       
                    //}
                    com.Parameters.AddWithValue("@ProblemCode", row.Field<int>("mpc_id"));
                    adapter.Fill(problemsSet.Tables[tableName]);
                }
            }
            DataTable allTable = problemsSet.Tables[1].Clone();
            allTable.TableName = "ALL";
            problemsSet.Tables.Add(allTable);
              DataTable allTableMY = problemsSet.Tables[1].Clone();
            allTableMY.TableName = "ALLMY";
            problemsSet.Tables.Add(allTableMY);
            List<DataTable> myTables = new List<DataTable>();
            foreach (DataTable table in problemsSet.Tables)
            {

                if (table.TableName == "ALL" || table.TableName == "ALLMY") continue;
                
                DataTable tempTable = table.Clone();
                tempTable.TableName = table.TableName + "MY";
                myTables.Add(tempTable);
                foreach (DataRow row in table.Rows)
                {

    
                    
                    if (!(problemsSet.Tables["ALL"].Select("DG_CODE='" + row.Field<string>("DG_CODE")+"'").Length > 0))
                    {
                       problemsSet.Tables["ALL"].ImportRow(row);
                       if (row.Field<int?>("bron_key") == myId || row.Field<int?>("manag_key") == myId)
                       {
                           problemsSet.Tables["ALLMY"].ImportRow(row);
                       }
                    }
                    if (row.Field<int?>("bron_key") == myId || row.Field<int?>("manag_key") == myId)
                    {
                        tempTable.ImportRow(row);
                    }
                }
            }
            problemsSet.Tables.AddRange(myTables.ToArray());
            return problemsSet;
        }
        public static bool IsNewMessage(string dg_code)
        {
            DataTable dt = new DataTable();
            using (
                SqlDataAdapter adapter =
                    new SqlDataAdapter(
                        "Select HI_ID,HI_MOD from History where HI_MOD in ('MTM','WWW') and HI_ID not in (select MS_HIID from dbo.mk_messageStatus where MS_isRead=1  and ms_uskey=@user) and HI_DGCOD =@p2 ",
                        Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@p2", dg_code);
                adapter.SelectCommand.Parameters.AddWithValue("@user", GetUserID());
                adapter.Fill(dt);
            }
            if (dt.Rows.Count > 0) return true;
            else return false;
        }
        public static DateTime GetDateForStatus(string dgcode, string query)
        {
            DateTime date= new DateTime();
            try
            {
                using (SqlCommand com = new SqlCommand(query, Connection))
                {
                    com.Parameters.AddWithValue("@dg_code",dgcode);
                    date = (DateTime) com.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                
                date = new DateTime();
            }
            return date;
        }
        public static DateTime GetDateForStatus(string dgcode, int status)
        {
            string query = string.Empty;
            using (SqlCommand com = new SqlCommand("select NS_QUERYFORDATE from mk_NewStatuses where NS_ID = @idstatus", Connection))
            {
                com.Parameters.AddWithValue("@idstatus", status);
                query = (string) com.ExecuteScalar();
            }
            return GetDateForStatus(dgcode, query);
        }

        public static DataTable GetItinerary(string dgcode)
        {
          //  string pakege = string.Empty;
          //  DateTime sailDate;
          //  using (SqlDataAdapter adapter = new SqlDataAdapter(selectDogovorListAdd,_connection))
          //  {
          //      DataTable dt = new DataTable();
          //      adapter.SelectCommand.Parameters.AddWithValue("@dlkey", dlKey);
          //      if(dt.Rows.Count<1)return new DataTable();
          //      pakege = dt.Rows[0].Field<string>("Pacage");
          //      sailDate = dt.Rows[0].Field<DateTime>("Date");
          //  }
          ////  sailDate = GetDogovorList().Select("dl_key=" + dlKey.ToString())[0].Field<DateTime>("DL_TURDATE");
            DataTable rezultable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectItinerary,Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Dogovor", dgcode);
                adapter.Fill(rezultable);
            }
            return rezultable;

        }

        
        public static DataTable GetChanges(string dgcode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(select_changes,Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.Fill(dt);
            }



            dt.Columns.Add("view", typeof (Image));
            foreach (DataRow row in dt.Rows)
            {
                if (row.Field<int>("MCD_CHANGE_CODE") == 1)
                {
                    row["view"] = Resources.view;
                }
                else
                {
                    row["view"] = Resources.empty;
                }
            }
            return dt;

        }
        public static void InsertHistory(string dgcode,string text,string mod,string remark)
        {
            using (SqlCommand com = new SqlCommand(insert_history, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dgcode", dgcode);
                com.Parameters.AddWithValue("@who", WorkWithData.GetUserName());
                com.Parameters.AddWithValue("@text", text);
                com.Parameters.AddWithValue("@mod", mod);
                com.Parameters.AddWithValue("@remark", remark);
                com.ExecuteNonQuery();
            }
        }
        public static DataTable GetOldItinerary(string dgcode)
        {
            DataTable dt = new DataTable();
            
            string pakage = string.Empty;
            DateTime sailDate, changeDate;
            using (SqlDataAdapter adapter = new SqlDataAdapter(selChanges,Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                DataTable tempTable = new DataTable();
                adapter.Fill(tempTable);
                if (tempTable.Rows.Count < 0)
                {
                    return null;
                }
                pakage = tempTable.Rows[0].Field<string>("package");
                sailDate=tempTable.Rows[0].Field<DateTime>("saildate");
                changeDate = tempTable.Rows[0].Field<DateTime>("date_change");
            }
            
            using (SqlDataAdapter adapter = new SqlDataAdapter(selOldItinerary, ConnectionTS))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@pak", pakage);
                adapter.SelectCommand.Parameters.AddWithValue("@saildate", sailDate);
                adapter.SelectCommand.Parameters.AddWithValue("@changedate", changeDate);
                adapter.Fill(dt);
            }
            return dt;
        }
       
        public static DataTable GetProblemCodes()
        {

            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectProblemCodes,Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }
        public static int GetAviaBronID(int dlkey)
        {
            int idAvBron=-1;
            using (SqlCommand com = new SqlCommand(selectidAviaBrion, Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlkey);
                var tmp = com.ExecuteScalar();
                if (tmp!=null&&!tmp.Equals(DBNull.Value))
                {
                    idAvBron = (int) tmp;
                }
            }
            return idAvBron;
        }
        public  static DataTable  GetAviaTable(int aviaBronID)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectAviaBron, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@idAvBron", aviaBronID);
                adapter.Fill(dt);
            }
            return dt;
        }
        public static string GetUserName()
        {
            string user = "";
            using (SqlCommand com = new SqlCommand(@"(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())", Connection))
            {
               user= (string)com.ExecuteScalar();
            }
            return user;
        }
        public static int GetUserID()
        {
            int id = 0;
            using (SqlCommand com = new SqlCommand(@"select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()", Connection))
            {
                id = (int) com.ExecuteScalar();
            }
            return id;
        }


        public static string[] rezervedText = {"бронь оплачена, но не подтвеждена у партнера;","бронь не оплачена, а опция скоро закончится;","бронь оплачена, но не заказана у партнера"};
        public static string GetSuperProblem()
        {
            string rezult = string.Empty;
            int i = 1;
            using (SqlDataAdapter adapter = new SqlDataAdapter("dbo.mk_search_dogovor",Connection))
            {
                adapter.SelectCommand.CommandType=CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status", 15);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", 1);
                DataTable dt =new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    rezult += i.ToString() + ") № " + row.Field<string>("dg_code") + " — " + rezervedText[0] + "\n";
                    i++;
                }

            }
            using (SqlDataAdapter adapter = new SqlDataAdapter("dbo.mk_search_dogovor", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status", 9);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", 1);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    rezult += i.ToString() + ") № " + row.Field<string>("dg_code") + " — " + rezervedText[1] + "\n";
                    i++;
                }

            }
            using (SqlDataAdapter adapter = new SqlDataAdapter("dbo.mk_search_dogovor", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status", 16);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", 1);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    rezult += i.ToString() + ") № " + row.Field<string>("dg_code") + " — " + rezervedText[2] + "\n";
                    i++;
                }

            }
            return rezult;
        }
        public static DataTable GetDownloadedFiles(string dgcode)
        {
            DataTable _dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("select * from mk_getDownloadedFiles(@DGCODE)",Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@DGCODE", dgcode);
                adapter.Fill(_dt);

            }
            return _dt;
        }
        public static DataSet GetSetChanges(string dgcode)
        {
            DataSet ds = new DataSet();
            DataTable templateTable = new DataTable();
            templateTable.Columns.Add("ChangeValue", typeof (string));
            templateTable.Columns.Add("OldValue", typeof(string));
            templateTable.Columns.Add("NewValue", typeof(string));
            DataTable generalTable = new DataTable("GeneralTable");
            generalTable.Columns.Add("ChangeValue", typeof(string));
            generalTable.Columns.Add("NewValue", typeof(string));
            // ds.Tables.Add(generalTable);
            DataSet dogovorDataSet = GetDogovorDate(dgcode);
            DataTable touristHistory = dogovorDataSet.Tables["HistoryTourist"],
                      touristReal = dogovorDataSet.Tables["RealTourist"],
                      serviseHistory = dogovorDataSet.Tables["HistoryServices"],
                      serviseReal = dogovorDataSet.Tables["RealServices"]; 
            //проверка туристов

                List<string> deletedTurist = new List<string>();
                foreach (DataRow row in touristHistory.Rows)
                {
                    if (!(touristReal.Select("TU_KEY=" + row.Field<int>("TU_KEY").ToString()).Length > 0))
                    {
                        String touristname = (string.IsNullOrEmpty(row.Field<string>("Tu_NameLat"))
                                                  ? row.Field<string>("Tu_NameRus")
                                                  : row.Field<string>("Tu_NameLat")) + " " +
                                             (string.IsNullOrEmpty(row.Field<string>("Tu_FNameLat"))
                                                  ? row.Field<string>("Tu_FNameRus")
                                                  : row.Field<string>("Tu_FNameLat"));
                        deletedTurist.Add(touristname);
                    }
                }
                foreach (string s in deletedTurist)
                {
                    generalTable.Rows.Add("Удален турист",s);
                }
           


                List<string> insertedTurist = new List<string>();
                foreach (DataRow row in touristReal.Rows)
                {
                    if (!(touristHistory.Select("TU_KEY=" + row.Field<int>("TU_KEY").ToString()).Length > 0))
                    {
                        String touristname = (string.IsNullOrEmpty(row.Field<string>("Tu_NameLat"))
                                                  ? row.Field<string>("Tu_NameRus")
                                                  : row.Field<string>("Tu_NameLat")) + " " +
                                             (string.IsNullOrEmpty(row.Field<string>("Tu_FNameLat"))
                                                  ? row.Field<string>("Tu_FNameRus")
                                                  : row.Field<string>("Tu_FNameLat"));
                        insertedTurist.Add(touristname);
                    }
                }
                foreach (string s in insertedTurist)
                {
                    generalTable.Rows.Add("Добавлен турист", s);
                }
            

            foreach (DataRow row in touristReal.Rows)
            {
                if ((touristHistory.Select("TU_KEY=" + row.Field<int>("TU_KEY").ToString()).Length > 0))
                {
                    DataTable tempTable = templateTable.Copy();
                    String touristname = (string.IsNullOrEmpty(row.Field<string>("Tu_NameLat"))
                                                  ? row.Field<string>("Tu_NameRus")
                                                  : row.Field<string>("Tu_NameLat")) + " " +
                                             (string.IsNullOrEmpty(row.Field<string>("Tu_FNameLat"))
                                                  ? row.Field<string>("Tu_FNameRus")
                                                  : row.Field<string>("Tu_FNameLat"));
                    tempTable.TableName = touristname;
                    DataRow row1 = touristHistory.Select("TU_KEY=" + row.Field<int>("TU_KEY").ToString())[0];
                    chekValue(row, row1, tempTable, "Tu_NameLat","Фамилия латиницей");
                    chekValue(row, row1, tempTable, "Tu_NameRus", "Фамилия");
                    chekValue(row, row1, tempTable, "Tu_FNameLat", "Имя латиницей");
                    chekValue(row, row1, tempTable, "Tu_FNameRus", "Имя");
                    //chekValue(row, row1, tempTable, "Tu_SNameLat", "Отчество латиницей");
                    //chekValue(row, row1, tempTable, "Tu_SNameRus", "Отчество");
                    //chekValue(row, row1, tempTable, "Tu_PasportType", "Серия загранпаспорта");
                    //chekValue(row, row1, tempTable, "Tu_PasportNum", "Номер загранпаспорта");
                    //chekValue(row, row1, tempTable, "Tu_PasportDateEnd", "Дата окончания загранпаспорта");
                    //chekValue(row, row1, tempTable, "Tu_PaspRuSer", "Серия общегражданского паспорта");
                    //chekValue(row, row1, tempTable, "Tu_PaspRuNum", "Номер общегражданского паспорта");
                    //chekValue(row, row1, tempTable, "Tu_PaspRuDate", "Дата выдачи паспорта");
                    //chekValue(row, row1, tempTable, "Tu_PaspRuByWhom", "Кем выдан паспорт");
                    //chekValue(row, row1, tempTable, "Tu_Citizen", "Гражданство");

                    if (tempTable.Rows.Count >  0)
                    {
                        ds.Tables.Add(tempTable);
                    }
                }

            }
            
            //Проверка услуг 
            foreach (DataRow row in serviseReal.Rows)
            {
                if (!(serviseHistory.Select("dl_key=" + row.Field<int>("dl_key").ToString()).Length > 0))
                {
                    generalTable.Rows.Add("Добавлена услуга", row.Field<string>("dl_name"));
                }
            }
            foreach (DataRow row in serviseHistory.Rows)
            {
                if (!(serviseReal.Select("dl_key=" + row.Field<int>("dl_key").ToString()).Length > 0))
                {
                    generalTable.Rows.Add("Добавлена услуга", row.Field<string>("dl_name"));
                }
            }
            foreach (DataRow row in serviseReal.Rows)
            {
                if (serviseHistory.Select("dl_key=" + row.Field<int>("dl_key").ToString()).Length > 0)
                {
                    DataTable tempTable = templateTable.Copy();
                    tempTable.TableName = row.Field<string>("dl_name");
                    DataRow row1 = serviseHistory.Select("dl_key=" + row.Field<int>("dl_key").ToString())[0];
                    chekValue(row, row1, tempTable, "dl_turdate", "Дата заезда");
                    chekValue(row, row1, tempTable, "dl_ndays", "Продолжительность");
                    chekValue(row, row1, tempTable, "dl_brutto", "Стоимость");
                    if (!((row["dl_svkey"].Equals(row1["dl_svkey"]) && row["dl_code"].Equals(row1["dl_code"]) && row["dl_subcode1"].Equals(row1["dl_subcode1"]) && row["dl_subcode2"].Equals(row1["dl_subcode2"]))))
                    {
                        tempTable.Rows.Add("Услуга изменена", row1.Field<string>("dl_name"), "");
                    }
                    if (tempTable.Rows.Count > 0)
                    {
                        ds.Tables.Add(tempTable);
                    }
                }
            }
         
            
            
            if (generalTable.Rows.Count > 0)
            {
                ds.Tables.Add(generalTable);
            }
            return ds;

        }
        static void chekValue(DataRow real, DataRow history, DataTable rezult, String column ,String value)
        {
            //var remark1 = real[column]; var remark2 = history[column];
            //MessageBox.Show(remark1.ToString());
            //MessageBox.Show(remark2.ToString());
            if (!(real[column].Equals(history[column])))
            {
                if (!(real[column] == null && history[column] == null))
                {
                    rezult.Rows.Add(value, history[column].ToString(), real[column].ToString());
                }
            }
        }
        public static DataSet GetDogovorDate(string dgcode)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter("mk_DogovorHistoryDate",Connection))
            {
               adapter.SelectCommand.CommandType =CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.Fill(ds);
            }
            foreach (DataTable table in ds.Tables)
            {
                if (table.Rows.Count > 0)
                {
                    table.TableName = table.Rows[0].Field<string>("tablename");
                }
            }
            return ds;
        } 
        public static void AcceptChange(int id_change)
        {
            using (SqlCommand com = new SqlCommand(update_changesDogovor,Connection))
            {
                com.Parameters.AddWithValue("@accept", true);
                com.Parameters.AddWithValue("@user", GetUserName());
                com.Parameters.AddWithValue("@id", id_change);
                com.ExecuteNonQuery();
            }
        }
        public static DataTable GetPaydDogovors()
        {
            DataTable _dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("mk_payd_dogovors",Connection))
            {
                adapter.SelectCommand.CommandType =CommandType.StoredProcedure;
                // if (GetUserID() == 2011) adapter.SelectCommand.Parameters.AddWithValue("@uskey", 2055);
                adapter.Fill(_dt);
            }
            return _dt;
        }
        static public DataTable GetAllProblems(string dgcode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selProbem, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.Fill(dt);
            }
           // dt.Columns.Add("image1",typeof(string));
            dt.Columns.Add("image2", typeof(Image));
            Boolean flim1 = false, flim2 = false;
            foreach (DataRow row in dt.Rows)
            {
                switch (row.Field<int>("mp_problemCode"))
                {

                    case 7:
                    case 14:
                    case 15:
                        if (row.Field<int>("ProblemStatusCode") != -1)
                        {
                            row["image2"] = Resources.view;
                            flim2 = true;
                        }
                        else
                        {
                            row["image2"] = Resources.empty;
                            row["ProblemStatus"] = "Нажмите для выбора статуса";
                        }
                        break;
                    case 12:
                       // row["image1"] = Resources.empty;
                        row["image2"] = Resources.view;
                        flim2 = true;
                        break;
                    default:
                         row["image2"] = Resources.empty;
                        break;
                }
                
            }

            if (!flim2)
            {
                dt.Columns.Remove("image2");
            }
            return dt;
        }

        static public DataTable GetDogovorList(string dgcode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectDogovorList, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.Fill(dt);
            }
            return dt;
        }

        static public int GetDogovorCreater(string dgcode)
        {
            int creater = 0;
            string comStr = String.Format(@"SELECT [DL_CREATOR] FROM [dbo].[tbl_DogovorList] where DL_DGCOD = '{0}'", dgcode);
            using (SqlCommand com = new SqlCommand(comStr, Connection))
            {
                creater = (int)com.ExecuteScalar();
            }
            return creater;
        }

        public static DateTime GetMaxDateDogovor()
        {
            DateTime date;
            using (SqlCommand com = new SqlCommand("Select max(dg_turdate) from tbl_dogovor",Connection))
            {
                date = (DateTime) com.ExecuteScalar();
            }
            return date;
        } 
        static public string GenerateBlock1ForCruise(int dlKey)
        {
            DataTable _turists = new DataTable(), _servises = new DataTable(), _servisescrin = new DataTable(), _cruiseinfo = new DataTable();
            String str = string.Empty;
            int nmen = 0;


           
          
           //Загрузка сервисов
           string servises = string.Empty;
           SqlCommand serviseCommand = new SqlCommand(@"SELECT [DL_key]
      ,[actions_id]
      ,[Text]
      ,CDP_NAME
      ,CDP_ORDER
      ,CDP_BLOCK
  FROM [dbo].[mk_actions_options] 
  inner join dbo.MK_Cruise_DOPServise on actions_id=CDS_ID
  where actions_id <0 and
  DL_key = @dlkey and
  CDP_BLOCK = 2 
  order by CDP_ORDER  ", Connection);
           serviseCommand.Parameters.AddWithValue("@dlkey", dlKey);
           SqlDataAdapter serviceAdapter = new SqlDataAdapter(serviseCommand);

           serviceAdapter.Fill(_servises);
           servises = "\n\nСервис";
           foreach (DataRow row in _servises.Rows)
           {
               servises += "\n" + row.Field<string>("CDP_NAME") + ": " + row.Field<string>("Text");
           }
           //Загрузка туристов

           string turists = string.Empty;
            
           SqlCommand selTurist = new SqlCommand(@"SELECT 
	        TU_NAMELAT
	        ,TU_FNAMELAT
	        ,TU_BIRTHDAY
	        ,TU_BIRTHCITY
            ,TU_BIRTHCOUNTRY
            ,TU_PASPORTTYPE
            ,TU_PASPORTNUM
            ,TU_PASPORTDATE
            ,TU_PASPORTDATEEND
            ,TU_PASPORTBYWHOM
            ,TU_CITIZEN
            FROM [dbo].[TuristService] 
            inner join dbo.tbl_Turist on TU_KEY = tu_tukey
            where tu_dlkey=@dlkey", Connection);
           selTurist.Parameters.AddWithValue("@dlkey", dlKey);
           SqlDataAdapter turistadapter = new SqlDataAdapter(selTurist);
           turistadapter.Fill(_turists);
 
           int i = 0;
           foreach (DataRow row in _turists.Rows)
            {
                ++i;
                string turist = string.Empty;
                turist += "\n\n" + "Турист " + i.ToString();
                turist += Convert.ToChar(13) + "фамилия: " + row.Field<string>("TU_NAMELAT");
                turist += Convert.ToChar(13) + "имя: " + row.Field<string>("TU_FNAMELAT");
                DateTime birrthday = row.Field<DateTime>("TU_BIRTHDAY");
                
                turist += Convert.ToChar(13) + "дата: " + birrthday.Date.ToString("dd.MM") + " год рождения " + birrthday.Date.Year.ToString();
                if ((row.Field<string>("TU_CITIZEN") != string.Empty) && (row.Field<string>("TU_CITIZEN") != null))
                {
                    turist += Convert.ToChar(13) + "национальность: " + row.Field<string>("TU_CITIZEN");
                }
                if ((row.Field<string>("TU_BIRTHCITY") != string.Empty) && (row.Field<string>("TU_BIRTHCITY") != null))
                {
                    turist += Convert.ToChar(13) + "место рождения: " + row.Field<string>("TU_BIRTHCITY") + "(" + row.Field<string>("TU_BIRTHCOUNTRY") + ")";
                }
                
                turist += Convert.ToChar(13) + "загранпаспорт: " + row.Field<string>("TU_PASPORTTYPE") + "№" + row.Field<string>("TU_PASPORTNUM");
                if (row.Field<DateTime?>("TU_PASPORTDATE")!=null)
                {
                    turist += Convert.ToChar(13) + "дата выдачи з\\паспорта: " + row.Field<DateTime>("TU_PASPORTDATE").Date.ToString("dd.MM.yyyy");
                }
                else
                {
                    turist += Convert.ToChar(13) + "дата выдачи з\\паспорта: ";
                }
                if (row.Field<DateTime?>("TU_PASPORTDATEEND") != null)
                {
                    turist += Convert.ToChar(13) + "дата окончания действия паспорта: " +
                              row.Field<DateTime>("TU_PASPORTDATEEND").Date.ToString("dd.MM.yyyy");
                }
                else
                {
                    turist += Convert.ToChar(13) + "дата окончания действия паспорта: ";
                }
                turists += turist;
   

            }
           
           //загрузка инфо по круизу
           string cruiseInfo = string.Empty;
           SqlCommand selcruiseinfo = new SqlCommand(@"select * from mk_dogovorlistadd where tbl_dogovor_list_key=@dlKey", Connection);
           selcruiseinfo.Parameters.AddWithValue("@dlKey", dlKey);
           SqlDataAdapter cruiseinfoadapter = new SqlDataAdapter(selcruiseinfo);
           DateTime turdate;
            string dg_code;
           using (SqlCommand com = new SqlCommand(@"SELECT 
        [DL_DGCOD]
      ,[DL_TURDATE]
      ,[DL_KEY]
      ,[DL_DAY]
      ,[DL_NMEN]
  FROM [dbo].[tbl_DogovorList]
  where  DL_KEY = @dlkey", Connection))
           {
               com.Parameters.AddWithValue("@dlkey", dlKey);
               DataTable dt = new DataTable();
               SqlDataAdapter ad = new SqlDataAdapter(com);
               ad.Fill(dt);
               turdate =
                   dt.Rows[0].Field<DateTime>("DL_TURDATE").AddDays(dt.Rows[0].Field<Int16>("DL_DAY") - 1);
               nmen = dt.Rows[0].Field<Int16>("DL_NMEN");
               dg_code = dt.Rows[0].Field<string>("DL_DGCOD");


           }
            cruiseinfoadapter.Fill(_cruiseinfo);
            DataTable _servisesmas = new DataTable();
            _servisesmas.Clear();
            
            
            using (SqlCommand com = new SqlCommand("MK_lk_servises_putevka", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dg_code", dg_code);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(_servisesmas);
                
            }
            
           
           if (_cruiseinfo.Rows.Count < 1) return "";
           string ship=string.Empty, crline =string.Empty,brandCode = string.Empty;
            if (_cruiseinfo.Rows[0].Field<string>("brandcode") != null)
            {
                using (
                    SqlCommand com = new SqlCommand(@"select name_en,mnemo from  CruiseLines where mnemo = @crline ",
                                                    ConnectionTS))
                {
                    com.Parameters.AddWithValue("@crline", _cruiseinfo.Rows[0].Field<string>("brandcode"));
                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    ad.Fill(dt);
                    crline = dt.Rows[0].Field<string>("name_en");
                    brandCode = dt.Rows[0].Field<string>("mnemo");
                }
            }
            int id_ship = 0;
            DataTable _SystemDate = new DataTable();
            //if(brandCode!=string.Empty)
            SqlCommand systemCommand = new SqlCommand(@"select * from dbo.CruiseLines_Sys where brandCode = @crl order by parent ", ConnectionTS);
            systemCommand.Parameters.AddWithValue("@crl", brandCode);
            SqlDataAdapter sysadapter = new SqlDataAdapter(systemCommand);
            sysadapter.Fill(_SystemDate);
            foreach (DataRow row in _SystemDate.Rows)
            {
                cruiseInfo += "\n" + row.Field<string>("Parametr_name") + " : " + row.Field<string>("Parametr_value");
            }
            if (_cruiseinfo.Rows[0].Field<int?>("cl_id") != null &&
                _cruiseinfo.Rows[0].Field<string>("shipcode") != null)
            {
                using (
                    SqlCommand com =
                        new SqlCommand(
                            @"select id,name_en from  Ships where code = @shipcode and cruise_line_id = @crline ",
                            ConnectionTS))
                {
                    com.Parameters.AddWithValue("@crline", _cruiseinfo.Rows[0].Field<int>("cl_id"));
                    com.Parameters.AddWithValue("@shipcode", _cruiseinfo.Rows[0].Field<string>("shipcode"));
                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    ad.Fill(dt);
                    ship = dt.Rows[0].Field<string>("name_en");
                    id_ship = dt.Rows[0].Field<int>("id");
                }
            }
            string cabinNomber = string.Empty, cabinCategory = string.Empty,optionNumer =string.Empty,cabinDef=string.Empty;
            DateTime? optionDate = null;
            bool isBook = false;

           using (SqlCommand com = new SqlCommand(@"SELECT top 1
		[OP_ID]
      ,[OP_DLKEY]
      ,[OP_Descript]
      ,[OP_number]
      ,[OP_N_cabin]
      ,[OP_date_end]
      ,[OP_WHO]
      ,[OP_LastUpdate]
      ,[OP_category]
      ,[OP_IsBook]
      ,[OP_LEVEL_CABIN]
  FROM [dbo].[mk_options] where  OP_DLKEY = @dlkey
  order by OP_ID desc", Connection))
           {
               com.Parameters.AddWithValue("@dlkey", dlKey);
               DataTable dt = new DataTable();
               SqlDataAdapter ad = new SqlDataAdapter(com);
               ad.Fill(dt);
               
               if (dt.Rows.Count > 0)
               {
                   cabinNomber = dt.Rows[0].Field<string>("OP_N_cabin");
                   cabinCategory = dt.Rows[0].Field<string>("OP_category");
                   optionNumer = dt.Rows[0].Field<string>("OP_number");
                   optionDate = dt.Rows[0].Field<DateTime>("OP_date_end");
                   isBook = dt.Rows[0].Field<bool>("OP_IsBook");
                   cabinDef = dt.Rows[0].Field<string>("OP_LEVEL_CABIN");

               }
           }
            string cabinClass = string.Empty;
            if (cabinCategory != string.Empty && cabinCategory != null)
            {
                using (SqlCommand com = new SqlCommand(@"SELECT  name
  FROM [dbo].[CabinCategories]
  inner join dbo.CabinClasses as cc on class_id = cc.id where ship_id = @id and code =@code", ConnectionTS))
                {
                    com.Parameters.AddWithValue("@id", id_ship);
                    com.Parameters.AddWithValue("@code", cabinCategory);
                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    ad.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cabinClass = dt.Rows[0].Field<string>("name");
                    }
                }
            }
            cruiseInfo += "\n\nКруизная компания: " + crline;
           cruiseInfo += "\nЛайнер: " + ship + " код лайнера " + _cruiseinfo.Rows[0].Field<string>("shipcode");
           cruiseInfo += "\nДата круиза: " + turdate.ToString("dd.MM.yyyy");
           cruiseInfo += "\nкатегория каюты: " + cabinCategory + " с размещением " + nmen.ToString() + " чел.";
            cruiseInfo += "\nкласс каюты:" + cabinClass;

 
            if (optionNumer != null && optionNumer != string.Empty)
           {
               cruiseInfo += "\n№ каюты: " + cabinNomber;
               
               cruiseInfo += "\nНомер опции:" + optionNumer;
               cruiseInfo += "\nУровень дефицита кают :" + cabinDef;
               if (!isBook)
               {
                   cruiseInfo += "\nОпция до:" + optionDate.ToString();
               }
               else
               {
                   cruiseInfo += "\nОпция до:" + "Опция подтверждена";
               }
           }
            float totalSum = 0;
            foreach (DataRow row in _servisesmas.Rows)
            {
                if (row.Field<int>("tipe") == 1)
                {
                    if (row.Field<int>("order") == 1)
                    {
                        cruiseInfo += "\n" +  "круизный тариф : " +
                                      row.Field<float>("DL_BRUTTO");
                        totalSum += row.Field<float>("DL_BRUTTO");
                    }
                    else
                    {
                        cruiseInfo += "\n" + row.Field<string>("DL_NAME") + " : " +
                                      row.Field<float>("DL_BRUTTO");
                        totalSum += row.Field<float>("DL_BRUTTO");
                    }
                }
            }
            cruiseInfo += "\nОбщая сумма: " + totalSum.ToString();
            SqlCommand servisecrinCommand = new SqlCommand(@"SELECT [DL_key]
      ,[actions_id]
      ,[Text]
      ,CDP_NAME
      ,CDP_ORDER
      ,CDP_BLOCK
  FROM [dbo].[mk_actions_options] 
  inner join dbo.MK_Cruise_DOPServise on actions_id=CDS_ID
  where actions_id <0 and
  DL_key = @dlkey and
  CDP_BLOCK = 1
  order by CDP_ORDER  ", Connection);
           servisecrinCommand.Parameters.AddWithValue("@dlkey", dlKey);
           SqlDataAdapter serviccrineAdapter = new SqlDataAdapter(servisecrinCommand);
           serviccrineAdapter.Fill(_servisescrin);
           foreach (DataRow row in _servisescrin.Rows)
           {
               cruiseInfo += "\n"+row.Field<string>("CDP_NAME") + " " + row.Field<string>("Text");
           }
           
           //Акции
            string actions = string.Empty;
            using (SqlCommand com = new SqlCommand(selAction, Connection))
           {
               com.Parameters.AddWithValue("@dlkey", dlKey);
               com.Parameters.AddWithValue("@bon", false);
               DataTable dt = new DataTable();
               SqlDataAdapter ad = new SqlDataAdapter(com);
               ad.Fill(dt);
               if (dt.Rows.Count>0)
               {
                   actions += "\n\nАкции";
                   int ii = 1;
                   foreach (DataRow row in dt.Rows)
                   {
                       actions += "\n" + ii.ToString() + "." + row.Field<string>("Text").Replace("\\", "");
                       ii++;
                   }
               }


           }
           //бонусы
            string bonus = string.Empty;
            using (SqlCommand com = new SqlCommand(selAction, Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                com.Parameters.AddWithValue("@bon", true);
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    bonus += "\n\nБонусы";
                    int ii = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        bonus += "\n" + ii.ToString() + "." + row.Field<string>("Text").Replace("\\","");
                        ii++;
                    }
                }


            }
           //сборка 
        //   str += "\n1.Создание брони в круиз.компаниях";
           str += cruiseInfo;
           str += actions;
           str += bonus;
           str += turists;
           str += servises;
           return str;
        }
        
        
        private static SqlConnection _connection,_connectionTS;
       
        


        public static bool InitConnection(string user,string pass)
        {
            bool state, stateTS;
#if DEBUG
            _connection = LantaSQLConnection.Open_LantaSQLConnection("test", user,pass,out state);
            _connectionTS = LantaSQLConnection.Open_LantaSQLConnection("total_services_test", user, pass, out stateTS);
#else
             _connection = LantaSQLConnection.Open_LantaSQLConnection("mk", user,pass,out state);
            _connectionTS= LantaSQLConnection.Open_LantaSQLConnection("total_services", user,pass,out stateTS);

#endif
            

            return state && stateTS;
        }
        
        static public SqlConnection Connection
        {
            get
            {
                
                if (_connection == null)
                    throw new Exception("Connection must be initialized");
                if (_connection.State != ConnectionState.Open)
                {
                   
                    _connection.Open();
                }
                return _connection;
            }
        }
        static public SqlConnection ConnectionTS
        {
            get
            {
                
                if (_connectionTS == null)
                    throw new Exception("ConnectionTS must be initialized");
                if (_connectionTS.State != ConnectionState.Open)
                {
                    _connectionTS.Open();
                }
                return _connectionTS;
            }
        }


        public static string GetAviaBron(int idBron)
        {
            string rezult=string.Empty;
            using (
                SqlCommand com = new SqlCommand("select top 1 n_bron_seller from mk_avia_bron where id = @id",
                                                Connection))
            {
                com.Parameters.AddWithValue("@id", idBron);
                rezult = (string) com.ExecuteScalar();
            }
            return rezult;

        }
        public static DataTable GetAviaTurist(int idBron)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectaviaturist,Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@idBron", idBron);
                adapter.Fill(dt);
            }

            return dt;
        }
        public static byte[] GetPetition(int an_key)
        {
            byte[] data;
            using (SqlCommand com = new SqlCommand("Select top 1 AN_PETITION from mk_Anulate where AN_KEY =@ankey",Connection))
            {
                com.Parameters.AddWithValue("@ankey", an_key);
                data = (byte[]) com.ExecuteScalar();
            }
            return data;
        }

        internal static void UpdateDogovorRegion(string dgcode, int regionId)
        {
            using (SqlCommand com = new SqlCommand("update mk_DogovorAdd set DA_region = @region where DA_DGCODE =@dgcod  ", Connection))
            {
                com.Parameters.AddWithValue("@region", regionId);
                com.Parameters.AddWithValue("@dgcod", dgcode);
                com.ExecuteNonQuery();
            }
        }
        public static void UpdateDogovorSetting(string dgcode,bool prepaydtype,decimal prepayd,object ppaymentdate,object paymentdate)
        {
            using (SqlCommand com = new SqlCommand(updateDogovorSettings,Connection))
            {

                com.Parameters.AddWithValue("@PrePaydType", prepaydtype?1:0);
                com.Parameters.AddWithValue("@ppaymentdate", ppaymentdate);
                com.Parameters.AddWithValue("@paymentdate", paymentdate);
                com.Parameters.AddWithValue("@prerayd", prepayd);
                com.Parameters.AddWithValue("@dgcode", dgcode);
                com.ExecuteNonQuery();
            }
        }
        public static DataRow GetAnnulationById(int annulId)
        {
            DataTable _dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectAnnulationByKey, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@ankey", annulId);
                adapter.Fill(_dt);

            }
            return _dt.Rows[0];
        }
        public static void UpdateStatusDogovor(string dgcode, int status)
        {
            using (SqlCommand com = new SqlCommand("update mk_DogovorAdd set DA_Status =@status where DA_DGCODE =@dgcode exec dbo.mk_dogovor_recalc @dg_code=@dgcode",Connection))
            {
                com.Parameters.AddWithValue("@status", status);
                com.Parameters.AddWithValue("@dgcode", dgcode);
                com.ExecuteNonQuery();
            }
        }
        public static void UpdateDiscountDogovor(string dgcode, decimal discount)
        {
            using (SqlCommand com = new SqlCommand(@"update tbl_dogovor set DG_DISCOUNT =@discount,DG_TYPECOUNT=1 where dg_code = @dgcode   exec dbo.mk_dogovor_recalc @dg_code=@dgcode",Connection))
            {
                com.Parameters.AddWithValue("@discount", discount);
                com.Parameters.AddWithValue("@dgcode", dgcode);
                com.ExecuteNonQuery();
            }
        }
    }
}