using System.Collections.Generic;
using System.Windows.Forms;
using WpfControlLibrary.Model.Voucher;
using lanta.SQLConfig;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using terms_prepaid.Properties;
using MessageBox = System.Windows.Forms.MessageBox;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;
using TextFormat = terms_prepaid.Helper_Classes.TextFormat;

namespace terms_prepaid.Helpers
{
    public class ChangeData
    {
        public DateTime Date { get; set; }
        public string Who { get; set; }
    }

    public class WorkWithData : IContractService, IVoucherService, IAviaCardsService, ICollectingLowcostAirlinesService, ICorrespondenceService, IRequestJournalService,
        IUsersService, IUnseenRequestMessageService, IUnansweredRequestsService, ICallRecordService
    {   
        #region consts

        public const string SearchDogovorProc = @"mk_search_dogovor";
        public const string GetProblemDogovorsProc = @"mk_GetProblemDogovors";

        private const string selectMessageMaster =
           "select HI_DGCOD,HI_DATE ,HI_WHO,HI_TEXT,HI_MOD from History where [HI_MOD]=@p1 and [HI_DGCOD]=@p2 order by HI_DATE ";

        private const string updteNew = "insert into dbo.mk_messageStatus(MS_HIID,MS_IsRead,MS_USKEY) " +
                                        "select HI_ID,1,@user from History where HI_MOD in ('MTM','WWW') and [HI_DGCOD]=@dgCode and HI_ID not in (select MS_HIID from dbo.mk_messageStatus where MS_USKEY=@user ) " +
                                        "update mk_messageStatus set MS_IsRead=1 where MS_HIID in(select HI_ID from History where [HI_DGCOD]=@dgCode ) ";

        private const string selectdogovorsetting = @"declare @temp table ([HI_DATE] datetime null,
	[HI_WHO] varchar(25),
	[HD_ID] [int] NOT NULL,
	[HD_HIID] [int] NOT NULL,
	[HD_Alias] [varchar](32) NULL,
	[HD_Text] [varchar](255) NULL,
	[HD_ValueOld] [varchar](255) NULL,
	[HD_ValueNew] [varchar](255) NULL,
	[HD_IntValueOld] [int] NULL,
	[HD_IntValueNew] [int] NULL,
	[HD_Invisible] [int] NULL,
	[HD_DateTimeValueOld] [datetime] NULL,
	[HD_DateTimeValueNew] [datetime] NULL,
	[HD_OAId] [int] NULL)

insert into @temp
select [HI_DATE]
	  ,[HI_WHO]
	  ,[HD_ID]
      ,[HD_HIID]
      ,[HD_Alias]
      ,[HD_Text]
      ,[HD_ValueOld]
      ,[HD_ValueNew]
      ,[HD_IntValueOld]
      ,[HD_IntValueNew]
      ,[HD_Invisible]
      ,[HD_DateTimeValueOld]
      ,[HD_DateTimeValueNew]
      ,[HD_OAId]
from (select [HI_DATE],
	[HI_WHO],
	[HD_ID],
	[HD_HIID],
	[HD_Alias],
	[HD_Text],
	[HD_ValueOld],
	[HD_ValueNew],
	[HD_IntValueOld],
	[HD_IntValueNew],
	[HD_Invisible],
	[HD_DateTimeValueOld],
	[HD_DateTimeValueNew],
	[HD_OAId]
from History as hi
inner join HistoryDetail as hd on HD_HIID = hi_id
where HI_DGCOD = @dgCode) as t
where [HD_Alias] = 'DG_PAYMENTDATE' or [HD_Alias] = 'DG_PPAYMENTDATE' or [HD_Alias] = 'DG_RazmerP' or 
	[HD_Alias] = 'DG_DISCOUNT' or [HD_Alias] = 'DG_SOR_CODE' or [HD_Alias] = 'DG_PRICE'
order by HI_DATE

select top 1
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
from @temp 
where HD_Alias ='DG_PPAYMENTDATE' order by HI_DATE desc) as PPAYMENTDATEWHO
,(select top 1 HI_DATE as [Date] 
from @temp 
where HD_Alias ='DG_PPAYMENTDATE' order by HI_DATE desc) as PPAYMENTDATEWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from @temp 
where HD_Alias ='DG_PAYMENTDATE' order by HI_DATE desc) as PAYMENTDATEWHO
,(select top 1 HI_DATE as [Date] 
from @temp 
where HD_Alias ='DG_PAYMENTDATE' order by HI_DATE desc) as PAYMENTDATEWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from @temp 
where HD_Alias ='DG_RazmerP' order by HI_DATE desc) as Razmer_PWHO
,(select top 1 HI_DATE as [Date] 
from @temp
where HD_Alias ='DG_RazmerP' order by HI_DATE desc) as Razmer_PWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from @temp
where HD_Alias ='DG_DISCOUNT' order by HI_DATE desc) as DISCOUNTWHO
,(select top 1 HI_DATE as [Date] 
from @temp
where HD_Alias ='DG_DISCOUNT' order by HI_DATE desc) as DISCOUNTWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from @temp 
where HD_Alias ='DG_SOR_CODE' order by HI_DATE desc) as StatusWHO
,(select top 1 HI_DATE as [Date] 
from @temp
where HD_Alias ='DG_SOR_CODE' order by HI_DATE desc) as StatusWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from @temp
where HD_Alias ='DG_PRICE' order by HI_DATE desc) as PriceWHO
,(select top 1 HI_DATE as [Date] 
from @temp
where HD_Alias ='DG_PRICE' order by HI_DATE desc) as PriceWHY
from tbl_Dogovor with (nolock)
left join mk_DogovorAdd with (nolock) on dg_code collate Cyrillic_General_CI_AS =DA_DGCODE collate Cyrillic_General_CI_AS
left join mk_NewStatuses with (nolock) on NS_ID =DA_STaTUs
where DG_code = @dgcode";

        private const string selectdogovorsetting_old = @"select top 1
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
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PPAYMENTDATE' order by HI_DATE desc) as PPAYMENTDATEWHO
,(select top 1 HI_DATE as [Date] 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PPAYMENTDATE' order by HI_DATE desc) as PPAYMENTDATEWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PAYMENTDATE' order by HI_DATE desc) as PAYMENTDATEWHO
,(select top 1 HI_DATE as [Date] 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PAYMENTDATE' order by HI_DATE desc) as PAYMENTDATEWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_RazmerP' order by HI_DATE desc) as Razmer_PWHO
,(select top 1 HI_DATE as [Date] 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_RazmerP' order by HI_DATE desc) as Razmer_PWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_DISCOUNT' order by HI_DATE desc) as DISCOUNTWHO
,(select top 1 HI_DATE as [Date] 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_DISCOUNT' order by HI_DATE desc) as DISCOUNTWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_SOR_CODE' order by HI_DATE desc) as StatusWHO
,(select top 1 HI_DATE as [Date] 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_SOR_CODE' order by HI_DATE desc) as StatusWHY
,(select top 1 replace(replace(Hi_who,'dbo','Система'),'WEB_LAPI','Система') as WHO 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PRICE' order by HI_DATE desc) as PriceWHO
,(select top 1 HI_DATE as [Date] 
from History with (nolock) inner join HistoryDetail with (nolock) on HD_HIID=hi_id 
where HI_DGCOD= @dgcode and HD_Alias ='DG_PRICE' order by HI_DATE desc) as PriceWHY
from tbl_Dogovor with (nolock)
left join mk_DogovorAdd with (nolock) on dg_code collate Cyrillic_General_CI_AS =DA_DGCODE collate Cyrillic_General_CI_AS
left join mk_NewStatuses with (nolock) on NS_ID =DA_STaTUs
where DG_code = @dgcode";

        private const string getShipName = @"select top 1 id, name_en as shipName
    from dbo.Ships
    where code = @shipCode and cruise_line_id = @cl_id"; //@cl_id int, @shipCode varchar(50)

        private const string getCruiseBrandName = @"select top 1 name_en as name_en
    from dbo.CruiseLines
    where mnemo = @brandcode"; //@brandcode varchar(2)

        private const string getShipInfo = @"select top 1 do.shipcode as shipcode,
    do.brandcode as brandcode,
	do.cl_id as clId,
	OP_N_cabin as cabinNumber,
	OP_category as category,
    [OP_ID]
    ,[OP_DLKEY]
    ,[OP_Descript]
    ,[OP_number]
    ,[OP_date_end]
    ,[OP_WHO]
    ,[OP_LastUpdate]
    ,[OP_IsBook]
    ,isnull(OP_DOCUMENT_QUERY,0) as OP_DOCUMENT_QUERY
    ,isnull(OP_DOCUMENT_GET,0) as OP_DOCUMENT_GET
    ,[OP_LEVEL_CABIN] as OP_LEVEL_CABIN
from dbo.mk_dogovorlistadd as do
inner join dbo.mk_options as op on op.OP_DLKEY = @dlkey
where tbl_dogovor_list_key=@dlKey
order by OP_ID desc"; // @dlKey int

        private const string getServicesForCruisesQuery = @"SELECT distinct [DL_key]
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
            order by CDP_ORDER";

        private const string getDopServicesForCruiseQuery = @"SELECT distinct [DL_key]
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
  order by CDP_ORDER";

        private const string getOpNumberForTransfer = @"SELECT TOP 1 [OP_number]
FROM [lanta].[dbo].[mk_options]
where OP_DLKEY = @dlKey";

        private const string getVoucherInfo = @"select DG_DISCOUNT, DG_DISCOUNTSUM, DG_TYPECOUNT, DG_PAYMENTDATE, DG_PPAYMENTDATE, DG_PRICE, DG_PROCENT, DG_RazmerP, DG_PRICE, DG_RATE, DG_PARTNERKEY, DD_CLКеу, US_KEY as LkUserId 
from tbl_Dogovor
inner join mk_DogovorAdd on DA_DGCODE collate Cyrillic_General_CS_AS = DG_CODE collate Cyrillic_General_CS_AS
left join DUP_USER on US_PRKEY = DG_PARTNERKEY
outer apply DogovorDeputat((select top 1 DG_Key from tbl_Dogovor where DG_CODE = @dgCode))
where DG_CODE = @dgCode"; // @dgCode varchar(10)

        private const string getVisaInfo = @"select DL_NAME, SL_NAME, DL_NDAYS, CN_NAME
from dbo.Dogovorlist
inline join ServiceList on DL_CODE = SL_KEY
left join Country on Country.CN_KEY = SL_CNKEY
where DL_KEY = @dlKey"; // @dlKey int

        private const string getTransferInfo = @"SELECT [dl_key]
      ,[code_from]
      ,[point_from]
      ,[time_from]
      ,[code_to]
      ,[point_to]
      ,[time_to]
      ,[transport]
      ,[id_transfer]
      ,[guide]
	  ,[guide_phone]
      ,da.Date_of_create
	  ,op.OP_number
      ,PPaymentdaDate
FROM [mk_transfer] 
inner join mk_DogovorListAdd as da on da.tbl_dogovor_list_key = dl_key
left join mk_options as op on op.OP_DLKEY = dl_key
where dl_key = @dlKey"; // @dlKey int

        private const string setTransferInfo = @"update [mk_transfer] set [guide] = @guide, [guide_phone] = @guidePhone where dl_key = @dlKey;
update [mk_options] set OP_number = @opNumber where OP_DLKEY = @dlKey;
update [mk_DogovorListAdd] set PPaymentdaDate = @timeLimit where tbl_dogovor_list_key = @dlKey";

        private const string getTransferTypeInfo = @"SELECT [direction]
FROM [Transfers] where id = @transferId";

        private const string getRate = @"select DG_RATE
from tbl_Dogovor
where DG_CODE = @dgCode"; // @dgCode varchar(10)

        private const string updateDogovorListSettings = @"update mk_DogovorListAdd set 
	   PPaimentTipe =@PrePaydType
	   ,PaymantDate=@ppaymentdate
	   ,PPaymentdaDate=@paymentdate
	   ,PaymantValue=@prerayd
where tbl_dogovor_list_key = @dlKey";

        private const string updateDogovorSettings = @"update  tbl_Dogovor set 
	   DG_PROCENT =@PrePaydType
	   ,DG_PPAYMENTDATE=@ppaymentdate
	   ,DG_PAYMENTDATE=@paymentdate
	   ,DG_RazmerP=@prerayd
where DG_code = @dgcode
exec dbo.mk_dogovor_recalc @dg_code=@dgcode";

        private const string selectAviaBron = @"SELECT  
      distinct 
	  ar.[DL_key]
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
	  ,ab.id as id_bron
	  ,apFrom.AP_NAME as airPortFrom
	  ,apTo.AP_name as airPortTo
	  ,cdFrom.CT_NAME as CityFrom
	  ,cdTo.CT_NAME as CityTo
	  ,cFrom.CN_NAME as CountryFrom
	  ,cTo.CN_NAME as CountryTo
      ,cl.class as className
      ,cl.class_cod as classCode
      ,cl.baggage as baggage
      ,ar.id as reisId
      ,ab.text_detail as textDetail
      ,ab.is_ok
	  ,ab.error_code
      ,ab.date_of
      ,ab.payment_state
      ,dl.DL_CONTROL
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
  left join mk_avia_reis_class as cl on cl.id_reis = ar.id
  left join tbl_DogovorList as dl on dl.DL_KEY = ar.DL_key
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
        
        private const string insertRequestMessageHistory = @"INSERT INTO [dbo].[History]
           ([HI_DATE]
           ,[HI_WHO]
           ,[HI_TEXT]
           ,[HI_MOD]
           ,[HI_TYPE]
           ,[HI_TYPECODE]
)
     VALUES
           (GetDate(),
           @who,
           @text,
           @mod,
           @hiType,
           @hiTypeCode)";

        private const string selectItinerary = @"select distinct rank() OVER (ORDER BY convert(datetime, convert(varchar(10),activitydate,101)+ ' ' + case when charindex('уточн',arrival)>0 then REPLACE(arrival,'уточн','00:00')
                                                         when charindex('24',arrival)=1 then ''
                                                          when arrival='-' then ''
                                                          else  arrival end)) as rank, activitydate, locname_ru, case when arrival = '00:00:00' then '-' else substring (arrival,1,5) end as arrival,  case when departure ='00:00:00' then'-' else substring (departure,1,5) end as departure,
  convert(datetime, convert(varchar(10),activitydate,101)+ ' ' + case when charindex('уточн',arrival)>0 then REPLACE(arrival,'уточн','00:00')
                                                         when charindex('24',arrival)=1 then ''
                                                          when arrival='-' then ''
                                                          else  arrival end ) as tt
from  dbo.MK_iternary1() where dg_code = @dogovor
order by  convert(datetime, convert(varchar(10),activitydate,101)+ ' ' + case when charindex('уточн',arrival)>0 then REPLACE(arrival,'уточн','00:00')
                                                         when charindex('24',arrival)=1 then ''
                                                          when arrival='-' then ''
                                                          else  arrival end)";
       
        
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
      ,[mp_visastatus] as VisaStatus
      ,[mp_ppaymentDate] as DG_PPAYMENTDATE
  FROM [dbo].[mk_ProblemsBrons] as mpb
    left join mk_VisaStatuses  with (nolock) on mp_visastatus = VS_Key
  where mp_problemCode = @ProblemCode
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
    mp_visastatuscode,
    mp_ppaymentDate";

        private const string selectProblemDogovorsOld2 = @"SELECT [mp_dgcod] as DG_CODE
      ,[mp_Turdate] as DG_TURDATE
      ,[mp_MainMan] as DG_MAINMEN
      ,[mp_NDAY] as DG_NDAY
      ,[mp_NMEN] as DG_NMEN
      ,[mp_crdate] as DG_CRDATE
      ,(convert(varchar(10),convert(int,[mp_price])) + ' ' + DG_RATE) as DG_PRICE
      ,[mp_bronir] as bronir
      ,[mp_bronKey] as bron_key
      ,[mp_managKey] as manag_key
      ,[mp_manag] as manag
      ,(convert(varchar(10),convert(int,mp_payd)) + ' ' + DG_RATE) as DG_PAYED
      ,[mp_optionDateEnd] as optionDateEnd
      ,[mp_dhCreateDate] as DH_CREATEDATE
      ,(select count(mp_problemCode) from mk_ProblemsBrons where mp_dgcod =mpb.mp_dgcod )  as countProblem
      ,(select count(distinct MCD_CHANGE_CODE) from dbo.mk_ChangesDogovor where isnull(MCD_Accept,0)=0 and MCD_DGCOD= mp_dgcod ) as countchanges
      ,[mp_paymentDate] as DG_PAYMENTDATE
      ,[mp_visastatuscode] as da_visaStatus
      ,mp_visastatus as VisaStatus
      ,DG_PPAYMENTDATE
  FROM [dbo].[mk_ProblemsBrons] as mpb
    left join mk_VisaStatuses  with (nolock) on mp_visastatus = VS_Key
    left join tbl_Dogovor on DG_CODE collate Cyrillic_General_CS_AS = mp_dgcod collate Cyrillic_General_CS_AS
  where mp_problemCode = @ProblemCode
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
    mp_visastatuscode,
    DG_RATE,
    DG_PPAYMENTDATE";

        private const string selectProblemDogovorsOld = @"SELECT [mp_dgcod] as DG_CODE
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
  where mp_problemCode = @ProblemCode
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

        private const string selectServiceSettingLastChange = @"select top 1 HI_DATE as date, HI_WHO as who
from [dbo].History
inner join [dbo].HistoryDetail on HD_HIID = HI_ID
where HI_TypeCode=@dlKey and HI_MOD='UPD' and HD_Alias=@alias
order by HI_DATE desc";

        private const string getChangeDataSlow = @"select HI_DATE as date, HI_WHO as who, HD_Alias as alias
from [dbo].History
inner join [dbo].HistoryDetail on HD_HIID = HI_ID
where HI_TypeCode=@dlKey and HI_MOD='UPD' and HD_Alias IN ('DL_PAYMENTDATE', 'DL_PPAYMENTDATE', 'DL_PPAYMENTVALUE', 'DL_DISCOUNT', 'DL_CONTROL')
order by HI_DATE desc";

        private const string getChangeData = @"declare @tbl table (HI_ID int, HI_DATE datetime, HI_WHO varchar(25), HI_MOD varchar(3))
insert into @tbl
select HI_ID, HI_DATE, HI_WHO, HI_MOD
from [dbo].[History]
where HI_DGCOD=@dgCode and HI_TypeCode=@dlKey

declare @tbl2 table (HI_DATE datetime, HI_WHO varchar(25), HD_Alias varchar(255), rn int)
insert into @tbl2
select HI_DATE as date, HI_WHO as who, HD_Alias as alias, ROW_NUMBER() OVER(PARTITION BY HD_Alias ORDER BY HI_DATE DESC) as rn 
from @tbl
inner join [dbo].[HistoryDetail] on HD_HIID = HI_ID
where HI_MOD='UPD' and HD_Alias IN ('DL_PAYMENTDATE', 'DL_PPAYMENTDATE', 'DL_PPAYMENTVALUE', 'DL_DISCOUNT', 'DL_Control')
order by HI_DATE desc

select HI_DATE as date, HI_WHO as who, HD_Alias as alias
from @tbl2
where rn = 1";

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
      ,[PPaimentTipe]
      ,[DL_COST]
      ,[DL_BRUTTO]
      ,[DL_CONTROL]
      ,[DL_DGCOD]
      ,[CR_NAME]
      ,dateadd(day,DL_DAY-1,DL_TURDATE) as date
  FROM [dbo].[mk_DogovorListAdd]
inner join tbl_dogovorList on tbl_dogovor_list_key=Dl_key
left join dbo.Controls on [DL_CONTROL] =[CR_KEY]
  where tbl_dogovor_list_key = @dlkey";
        private const string selectDogovorList = @"SELECT distinct [DL_DGCOD]
      ,DATEADD(day,DL_DAY-1,[DL_TURDATE]) as DL_TURDATE
      ,dl.DL_KEY
      ,dl.DL_CONTROL
      ,[DL_PAKETKEY]
      ,[DL_TRKEY]
      ,[DL_SVKEY]
      ,dbo.MK_name_SERVISES_lk(DL_SVKEY, DL_CODE, DL_SUBCODE1,DL_SUBCODE2,DL_NAME,dl.DL_KEY) as DL_NAME
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
	  ,at.id_bron as bronId
  FROM [dbo].[tbl_DogovorList] as dl
 left join dbo.Controls on [DL_CONTROL] = [CR_KEY]
 left join  dbo.mk_avia_reis as ar on ar.dl_key = dl.DL_KEY
 left join dbo.mk_avia_reis_class as arc on ar.id = arc.id_reis
 left join dbo.mk_avia_clases_by_ticket as acbt on acbt.id_class = arc.id 
 left join dbo.mk_avia_ticket as at on at.id = acbt.id_ticket
 left join dbo.mk_avia_bron as ab on ab.id = at.id_bron
where DL_DGCOD =@dgcode  and DL_SVKEY <> 3161
order by DL_TURDATE";
        private const string selectDogovorListOld = @"SELECT [DL_DGCOD]
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
 left join dbo.Controls on [DL_CONTROL] = [CR_KEY]
 inner join  dbo.mk_avia_reis as ar on ar.dl_key = DL_KEY
 inner join dbo.mk_avia_reis_class as arc on ar.id = arc.id_reis  
 inner join dbo.mk_avia_clases_by_ticket as acbt on arc.id = acbt.id_class
 inner join dbo.mk_avia_ticket as at on at.id = acbt.id_ticket
 inner join dbo.mk_avia_bron as ab on ab.id = at.id_bron   
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
        
        public const string selectTurists = @"SELECT 
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

        private const string updateAnnulateOk =
              @"Update mk_Anulate set  DATE_OF_OK =Getdate(),MANAGE_OK =isnull((select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()),0) where AN_KEY = @key";
        private const string updateAnnulateClose =
             @"Update mk_Anulate set  CLOSETASK_DATE =Getdate(),MANAG_CLOSETASK =isnull((select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()),0) where AN_KEY = @key";
        private const string updateAnnulateBronOk =
             @"Update mk_Anulate set  BRONIR_OK_DATE =Getdate(),BRONIR_OK =isnull((select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()),0) where AN_KEY = @key";
        private const string updateAnnulateCalc =
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

        private string addCruiseOptionQuery = @"INSERT INTO [dbo].[mk_options]
                                         ([OP_DLKEY]
                                         ,[OP_Descript]
                                         ,[OP_number]
                                         ,[OP_N_cabin]
                                         ,[OP_date_end]
                                         ,[OP_WHO]
                                         ,[OP_LastUpdate]
                                         ,[OP_category]
                                         ,OP_LEVEL_CABIN
                                         ,OP_IsBook
                                         ,OP_DOCUMENT_QUERY
                                         ,OP_DOCUMENT_GET)
                             VALUES
                                         (@OP_DLKEY 
                                         ,@OP_Descript
                                         ,@OP_number
                                         ,@OP_N_cabin
                                         ,@OP_date_end
                                         ,(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())
                                         ,getdate()
                                         ,@OP_category
                                         ,@OP_LEVEL_CABIN
                                         ,@OP_IsBook
                                         ,@OP_DOCUMENT_QUERY
                                         ,@OP_DOCUMENT_GET)";
        #endregion

        private static WorkWithData _instance;
        private static object _sync = new object();

        private WorkWithData()
        {
            
        }

        public static WorkWithData GetInstance()
        {
            lock (_sync)
            {
                return _instance ?? (_instance = new WorkWithData());
            }
        }

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

        public static string GetAviaErrorMessage(string code)
        {
            string rezult = string.Empty;
            using (SqlCommand com = new SqlCommand("SELECT TOP 1 [Message] FROM [total_services].[dbo].[ErrorsAvia] where Code = @code", _connectionTS))
            {
                com.Parameters.AddWithValue("@code", code);
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
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
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

        public static void UpdateProblemOk(int codeProblem, int okCode,string okMessage,string dgcode)
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

        public DataRow GetDogovorSettings(string dgcode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectdogovorsetting, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.Fill(dt);
            }
            return dt.Rows[0];
        }

        public string GetOptionNumber(int dlKey)
        {
            string optionNumber;
            using (SqlCommand cmd = new SqlCommand(getOpNumberForTransfer, Connection))
            {
                cmd.Parameters.AddWithValue("@dlKey", dlKey);
                optionNumber = (string) cmd.ExecuteScalar();
            }
            return optionNumber;
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

        public void SetAnnulateDogovor(string dgcode, int reason)
        {
            using (SqlCommand com = new SqlCommand("mk_putevka_anulate",Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dg_code", dgcode);
                com.Parameters.AddWithValue("@reason", reason);
                com.ExecuteNonQuery();
            }
        }

        public static DataTable GetAccounts(string dgCode, int type)
        {
            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter("GetAccounts",Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dgCode", dgCode);
                adapter.SelectCommand.Parameters.AddWithValue("@type", type);
                adapter.Fill(dt);
            }
            return dt;
        }

        public static DataSet GetAllProblems(int? selectedUsKey = null)
        {
            int myId = GetUserID();
            int mgrId = selectedUsKey ?? myId;

            DataSet problemsSet = new DataSet();
            DataTable problemCodes = GetProblemCodes();
            foreach (DataRow row in problemCodes.Rows)
            {
                string tableName = row.Field<string>("mpc_TableName");
                problemsSet.Tables.Add(new DataTable(tableName));
                using (SqlDataAdapter adapter = new SqlDataAdapter(GetProblemDogovorsProc, Connection))
                {
                    adapter.SelectCommand.CommandType=CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@ProblemCode", row.Field<int>("mpc_id"));
                    adapter.Fill(problemsSet.Tables[tableName]);
                }
            }

            DataTable allTable = problemsSet.Tables[1].Clone();
            allTable.TableName = "ALL";
            problemsSet.Tables.Add(allTable);

            DataTable allTableMY = problemsSet.Tables[1].Clone();
            allTableMY.TableName = "ALLMY";
            problemsSet.Tables.Add(allTableMY);

            DataTable managerTable = problemsSet.Tables[1].Clone();
            managerTable.TableName = "ALLMANAGER";
            problemsSet.Tables.Add(managerTable);

            List<DataTable> myTables = new List<DataTable>();
            List<DataTable> managerTables = new List<DataTable>();

            foreach (DataTable table in problemsSet.Tables)
            {

                if (table.TableName == "ALL" || table.TableName == "ALLMY" || table.TableName == "ALLMANAGER") continue;
                
                DataTable tempTable = table.Clone();
                tempTable.TableName = table.TableName + "MY";
                myTables.Add(tempTable);

                DataTable tempMgrTable = null;
                tempMgrTable = table.Clone();
                tempMgrTable.TableName = table.TableName + "MANAGER";
                managerTables.Add(tempMgrTable);
                
                foreach (DataRow row in table.Rows)
                {
                    if (!(problemsSet.Tables["ALL"].Select("DG_CODE='" + row.Field<string>("DG_CODE")+"'").Length > 0))
                    {
                       problemsSet.Tables["ALL"].ImportRow(row);
                       
                       if (row.Field<int?>("bron_key") == myId || row.Field<int?>("manag_key") == myId)
                       {
                           problemsSet.Tables["ALLMY"].ImportRow(row);
                       }

                       if (row.Field<int?>("bron_key") == mgrId || row.Field<int?>("manag_key") == mgrId)
                       {
                           problemsSet.Tables["ALLMANAGER"].ImportRow(row);
                       }
                    }

                    if (row.Field<int?>("bron_key") == myId || row.Field<int?>("manag_key") == myId)
                    {
                        tempTable.ImportRow(row);
                    }

                    if (row.Field<int?>("bron_key") == mgrId || row.Field<int?>("manag_key") == mgrId)
                    {
                        tempMgrTable.ImportRow(row);
                    }
                }
            }
            problemsSet.Tables.AddRange(myTables.ToArray());
            problemsSet.Tables.AddRange(managerTables.ToArray());
            return problemsSet;
        }

        public static DataSet GetAllProblemsOld()
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

       private static Dictionary<string, ChangeData> GetChangeDataDictionary(string dgCode, int dlKey, List<string> aliasArray)
        {
            DataTable dt;
            var changeDataDictionary = new Dictionary<string, ChangeData>();
            
            using (SqlDataAdapter adapter = new SqlDataAdapter(getChangeData, _connection))
            {
                dt = new DataTable();
                adapter.SelectCommand.Parameters.AddWithValue("@dgCode", dgCode);
                adapter.SelectCommand.Parameters.AddWithValue("@dlKey", dlKey);
                adapter.Fill(dt);

                if (dt.Rows != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string alias = row.Field<string>("alias");

                        if (aliasArray.Exists(a => a.Equals(alias)))
                        {
                            var changeData = new ChangeData() { Date = row.Field<DateTime>("date"), Who = row.Field<string>("who") };
                            try
                            {
                                changeDataDictionary.Add(alias, changeData);
                            }
                            catch (ArgumentException e)
                            {
                                TpLogger.Error("GetHistory", "GetHistoryError", e);
                            }
                        }
                    }
                }
            }

            return changeDataDictionary;
        }

        private static readonly string[] _serviceHistoryStringArray = {"DL_PAYMENTDATE", "DL_PPAYMENTDATE", "DL_PPAYMENTVALUE",
                        "DL_DISCOUNT", "DL_Control"};

        //private static readonly string[] _serviceHistoryStringArray = {"DL_DISCOUNT", "DL_Control"};

        public static DataTable GetItinerary(string dgcode)
        {
            DataTable rezultable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectItinerary,Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Dogovor", dgcode);
                adapter.Fill(rezultable);
            }
            return rezultable;

        }

        public DataTable GetItinerary2(string dgcode)
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectItinerary, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Dogovor", dgcode);
                adapter.Fill(rezultable);
            }
            return rezultable;
        }


        public static DataTable GetChanges(string dgcode)
        {
            if(dgcode == null || dgcode.Equals(string.Empty))
                throw new Exception("dgCode is null");

            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select * from GetChanges('{0}')", dgcode), Connection))
            {
                adapter.Fill(dt);
            }

            dt.Columns.Add("view", typeof(Image));
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
            using (SqlCommand com = new SqlCommand(insert_history, Connection))
            {
                com.Parameters.AddWithValue("@dgcode", dgcode);
                com.Parameters.AddWithValue("@who", WorkWithData.GetUserName());
                com.Parameters.AddWithValue("@text", text);
                com.Parameters.AddWithValue("@mod", mod);
                com.Parameters.AddWithValue("@remark", remark);
                com.ExecuteNonQuery();
            }
        }

        public void InsertHistory2(string dgcode, string text, string mod, string remark)
        {
            using (SqlCommand com = new SqlCommand(insert_history, Connection))
            {
                com.Parameters.AddWithValue("@dgcode", dgcode);
                com.Parameters.AddWithValue("@who", GetUserName());
                com.Parameters.AddWithValue("@text", text);
                com.Parameters.AddWithValue("@mod", mod);
                com.Parameters.AddWithValue("@remark", remark);
                com.ExecuteNonQuery();
            }
        }

        public void WathMessages(int requestId, string mod)
        {
            string query = @"Update mk_RequestMessages set Seen = 1, ReadDate = @Date where RequestId = @RequestId and [Mod] = @Mod and IsIncoming = 0";

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@RequestId", requestId);
                com.Parameters.AddWithValue("@Mod", mod);
                com.Parameters.AddWithValue("@Date", DateTime.Now);
                com.ExecuteNonQuery();
            }
        }

        public void WathMessage(int messageId)
        {
            string query = @"Update mk_RequestMessages set Seen = 1, ReadDate = @Date where Id = @MessageId";

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@MessageId", messageId);
                com.Parameters.AddWithValue("@Date", DateTime.Now);
                com.ExecuteNonQuery();
            }
        }

        public void InsertRequestMessageToHistory(string text, string mod, int requestId)
        {
            using (SqlCommand com = new SqlCommand(insertRequestMessageHistory, Connection))
            {
                com.Parameters.AddWithValue("@who", GetUserName());
                com.Parameters.AddWithValue("@text", text);
                com.Parameters.AddWithValue("@mod", mod);
                com.Parameters.AddWithValue("@hiType", "REQUESTMSG");
                com.Parameters.AddWithValue("@hiTypeCode", requestId);
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
        
        public static string GetUserName()
        {
            string user = "";
            using (SqlCommand com = new SqlCommand(@"(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())", Connection))
            {
               user= (string)com.ExecuteScalar();
            }
            return user;
        }

        public string GetUserName2()
        {
            string user = "";
            using (SqlCommand com = new SqlCommand(@"(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())", Connection))
            {
                user = (string)com.ExecuteScalar();
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

        public int GetUsKey()
        {
            int key = 0;
            using (SqlCommand com = new SqlCommand(@"select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()", Connection))
            {
                key = (int)com.ExecuteScalar();
            }
            return key;
        }

        public string GetUserId()
        {
            string id;
            using (SqlCommand com = new SqlCommand(@"select top 1 isnull(US_USERID,0 ) from UserList where US_USERID = SUSER_SNAME()", Connection))
            {
                id = (string)com.ExecuteScalar();
            }
            return id;
        }

        public DataRow GetUserSignature(int usKey)
        {
            DataTable dt = new DataTable();
            
            string query = @"SELECT TOP 1 [UsKey]
                          ,[USERNAME_RU]
                          ,[USERNAME_EN]
                          ,[EMAIL]
                          ,[POSITION]
                          ,[DEPARTAMENT]
                          ,[PHONE]
                          ,[DEPARTAMENT_EMAIL]
                      FROM [lanta].[dbo].[UserSignatures] where UsKey = @usKey";

            string signature;
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@usKey", usKey);
                adapter.Fill(dt);
            }

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public static string[] rezervedText = {"бронь оплачена, но не подтвеждена у партнера;","бронь не оплачена, а опция скоро закончится;","бронь оплачена, но не заказана у партнера"};

        public static string GetSuperProblem()
        {
            string rezult = string.Empty;
            int i = 1;
            using (SqlDataAdapter adapter = new SqlDataAdapter(SearchDogovorProc, Connection))
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(SearchDogovorProc, Connection))
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(SearchDogovorProc, Connection))
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

        public static DataTable GetUnhandledCorrespondence(out int count, int? usKey = null)
        {
            var dt = new DataTable();

            using (SqlCommand com = new SqlCommand(SearchDogovorProc, Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@newmessage", true);
                com.Parameters.AddWithValue("@count", 0);
                AddParam(com, "@usKey", usKey);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                dt = new DataTable();
                adapter.Fill(dt);
                count = (int)com.Parameters["@count"].Value;
            }

            return dt;
        }

        public static DataTable GetUnhandledCorrespondence2(int? usKey)
        {
            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter("GetUnhandledCorrespondence", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                AddParam(adapter, "@usKey", usKey);
                adapter.Fill(dt);
            }

            return dt;
        }

        public static DataTable GetManagerWithUnhandledCorrespondence(string[] dgCodes)
        {
            var dt = new DataTable();

            var dgCodesTable = ArrayToDataTable(dgCodes);

            using (var adapter = new SqlDataAdapter("GetManagerWithUnhandledCorrespondence", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                AddParam(adapter, "@dgCodes", dgCodesTable);
                adapter.Fill(dt);
            }

            return dt;
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
        private static void chekValue(DataRow real, DataRow history, DataTable rezult, String column ,String value)
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
        public static DataTable GetAllProblems(string dgcode)
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

        public DataTable GetDogovorList(string dgcode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("GetDogovorListNew", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.Fill(dt);
            }
            return dt;
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

        /*
        public static string GenerateBlock1ForCruise(int dlKey)
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
        */

        private static SqlConnection _connection;
        private static SqlConnection _connectionTS;
        private static MySqlConnection _connectionAsterisk;

        public static bool InitConnection(string user,string pass)
        {
            bool state;
            bool stateTS;
            bool stateAsterisk;

            string astLogin = "call_stat";      //conf.Get_Value("appSettings", "geckoCaching");
            string astPassword = "se0Baht8";    //conf.Get_Value("appSettings", "astPassword");
            string connectionStringFull =
                "Driver={MySQL ODBC 5.3 ANSI Driver};server=198.168.8.1;uid=call_stat;pwd=se0Baht8;database=asteriskcdrdb;option=3";

            string connectionString =
                "Server=198.168.8.1;Port=3306;Database=asteriskcdrdb;Uid=call_stat;Pwd=se0Baht8;";

#if DEBUG
            _connection = LantaSQLConnection.Open_LantaSQLConnection("test", user, pass, out state);
            _connectionTS = LantaSQLConnection.Open_LantaSQLConnection("total_services_test", user, pass, out stateTS);
            //_connectionAsterisk = LantaSQLConnection.Open_LantaSQLConnection("asterisk", astLogin, astPassword, out stateAsterisk);
#else
            _connection = LantaSQLConnection.Open_LantaSQLConnection("mk", user,pass,out state);
            _connectionTS = LantaSQLConnection.Open_LantaSQLConnection("total_services", user,pass,out stateTS);
            //_connectionAsterisk = LantaSQLConnection.Open_LantaSQLConnection("asterisk", astLogin, astPassword, out stateAsterisk);
#endif
            /*_connectionAsterisk = new MySqlConnection(connectionString);

            var t = new Task<bool>(() => OpenConnection(_connectionAsterisk));
            t.Start();
            t.Wait();

            stateAsterisk = t.Result;*/

            return state && stateTS;    // && stateAsterisk;
        }

        private static bool OpenConnection(SqlConnection connection)
        {
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                connection.Close(); 
                throw;
            };
            return true;
        }

        private static bool OpenConnection(MySqlConnection connection)
        {
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                connection.Close(); 
                throw;
            };
            return true;
        }

        private static SqlConnection GetConnection(SqlConnection connection, string connectionName)
        {
            if (connection == null)
                throw new Exception(string.Format("{0} must be initialized", connectionName));
            if (connection.State != ConnectionState.Open)
            {

                connection.Open();
            }
            return connection;
        }

        private static MySqlConnection GetConnection(MySqlConnection connection, string connectionName)
        {
            if (connection == null)
                throw new Exception(string.Format("{0} must be initialized", connectionName));
            if (connection.State != ConnectionState.Open)
            {

                connection.Open();
            }
            return connection;
        }
        
        public static SqlConnection Connection
        {
            get { return GetConnection(_connection, "Connection"); }
        }

        public static SqlConnection ConnectionTS
        {
            get { return GetConnection(_connectionTS, "ConnectionTS"); }
        }

        public static MySqlConnection ConnectionAsterisk
        {
            get { return GetConnection(_connectionAsterisk, "ConnectionAsterisk"); }
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

        public string GetAviaBron2(int idBron)
        {
            string rezult = string.Empty;
            using (
                SqlCommand com = new SqlCommand("select top 1 n_bron_seller from mk_avia_bron where id = @id",
                                                Connection))
            {
                com.Parameters.AddWithValue("@id", idBron);
                rezult = (string)com.ExecuteScalar();
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

        public static byte[] GetPetition(int anKey)
        {
            byte[] data;

            using (SqlCommand com = new SqlCommand("select top 1 IsNull(AN_SCAN, AN_PETITION) from mk_Anulate where AN_KEY = @ankey", Connection))
            {
                com.Parameters.AddWithValue("@ankey", anKey);
                data = (byte[]) com.ExecuteScalar();
            }

            return data;
        }

        public static DataRow GetPetition2(int anKey)
        {
            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter("select top 1 IsNull(AN_SCAN, AN_PETITION) as data, IsNull(AN_EXT, 'pdf') as ext from mk_Anulate where AN_KEY = @ankey", Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@ankey", anKey);
                adapter.Fill(dt);
            }
            
            return dt.Select().FirstOrDefault();
        }

        internal static void UpdateDogovorRegion(string dgcode, int regionId)
        {
            using (SqlCommand com = new SqlCommand("update mk_DogovorAdd set DA_region = @region where DA_DGCODE = @dgcod  ", Connection))
            {
                com.Parameters.AddWithValue("@region", regionId);
                com.Parameters.AddWithValue("@dgcod", dgcode);
                com.ExecuteNonQuery();
            }
        }

        public static void UpdateDogovorListSetting(string dlKey, bool prepaydtype, decimal prepayd, object ppaymentdate, object paymentdate)
        {
            using (SqlCommand com = new SqlCommand(updateDogovorListSettings, Connection))
            {
                com.Parameters.AddWithValue("@PrePaydType", prepaydtype ? 1 : 0);
                com.Parameters.AddWithValue("@ppaymentdate", ppaymentdate);
                com.Parameters.AddWithValue("@paymentdate", paymentdate);
                com.Parameters.AddWithValue("@prerayd", prepayd);
                com.Parameters.AddWithValue("@dgcode", dlKey);
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

        public DataTable GetStatusesForService(int svKey)
        {
            DataTable rez = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(
                string.Format("select CR_KEY as 'key', CR_NAME as 'name' from dbo.GetControls({0})", svKey), Connection))
            {
                adapter.Fill(rez);
            }
            return rez;
        }

        public DataTable GetStatuses()
        {
            DataTable rez = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("select * from dbo.mk_GetStatusesDogovorWithFilter()", Connection))
            {
                adapter.Fill(rez);
            }
            return rez;
        }

        public DataRow GetShipName(int clId, string shipCode)
        {
            DataRow row;
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(getShipName, ConnectionTS))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@cl_id", clId);
                adapter.SelectCommand.Parameters.AddWithValue("@shipCode", shipCode);
                adapter.Fill(dt);
                row = dt.Rows[0];
            }
            return row;
        }

        public string GetCruiseBrandName2(string brandCode)
        {
            if (brandCode == null)
                return null;

            using (SqlCommand com = new SqlCommand(getCruiseBrandName, ConnectionTS))
            {
                com.Parameters.AddWithValue("@brandcode", brandCode);
                var tmp = (string)com.ExecuteScalar();
                if (tmp != null && !tmp.Equals(DBNull.Value))
                {
                    return (string)tmp;
                }
            }

            return null;
        }

        public string GetRate2(string dgCode)
        {
            using (SqlCommand com = new SqlCommand(getRate, Connection))
            {
                com.Parameters.AddWithValue("@dgCode", dgCode);
                var tmp = (string)com.ExecuteScalar();
                if (tmp != null && !tmp.Equals(DBNull.Value))
                {
                    return (string)tmp;
                }
            }

            return null;
        }

        public DataTable GetServicesSettings(string dgCode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("GetServicesSettings3", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgCode);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetTransactions(string dgCode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("MK_payment_dogovor_history", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dg_code", dgCode);
                adapter.Fill(dt);
            }
            return dt;
        }

        public ServiceSetting GetServiceSetting(string dgCode, int dlKey)
        {
            ServiceSetting serviceSetting = null;

            using (SqlDataAdapter adapter = new SqlDataAdapter(selectDogovorListAdd, _connection))
            {
                DataTable dt = new DataTable();
                adapter.SelectCommand.Parameters.AddWithValue("@dlkey", dlKey);
                adapter.Fill(dt);

                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];

                    var changeDatas = GetChangeDataDictionary(dgCode, dlKey, new List<string>(_serviceHistoryStringArray));

                    ChangeData pdate = null;
                    ChangeData ppdate = null;
                    ChangeData ppvalue = null;
                    ChangeData disc = null;
                    ChangeData stat = null;

                    if (!changeDatas.TryGetValue(_serviceHistoryStringArray[0], out pdate)) pdate = null;
                    if (!changeDatas.TryGetValue(_serviceHistoryStringArray[1], out ppdate)) ppdate = null;
                    if (!changeDatas.TryGetValue(_serviceHistoryStringArray[2], out ppvalue)) ppvalue = null;
                    if (!changeDatas.TryGetValue(_serviceHistoryStringArray[3], out disc)) disc = null;
                    if (!changeDatas.TryGetValue(_serviceHistoryStringArray[4], out stat)) stat = null;

                    //decimal cost = row.Field<decimal?>("DL_COST") ?? 0;
                    decimal cost = row.Field<decimal?>("DL_BRUTTO") ?? 0;

                    string handWho = row.Field<string>("HandWho");
                    //string rate = row.Field<string>("DG_RATE");

                    var payment = new PaymentSetting
                    {
                        PaymentDate = new RowSettingDate()
                        {
                            Name = "Оплата до",
                            DateValue = row.Field<DateTime?>("PaymantDate"),
                            DateChange = TextFormat.GetDate(pdate),
                            Manager = pdate != null ? pdate.Who : ""
                        },
                        PrePaymentDate = new RowSettingDate()
                        {
                            Name = "Предоплата до",
                            DateValue = row.Field<DateTime?>("PPaymentdaDate"),
                            DateChange = TextFormat.GetDate(ppdate),
                            Manager = ppdate != null ? ppdate.Who : ""
                        },
                        PaymentValue = new RowSettingValue()
                        {
                            Name = "Сумма оплаты",
                            Value = cost,
                            ValueType = 0,
                            Cost = cost,
                            DateChange = "",
                            Manager = handWho
                        },
                        PrePaymentValue = new RowSettingValue()
                        {
                            Name = "Сумма предоплаты",
                            Value = row.Field<decimal?>("PaymantValue") ?? 0,
                            ValueType = row.Field<int?>("PPaimentTipe") ?? 0,
                            Cost = cost,
                            DateChange = TextFormat.GetDate(ppvalue),
                            Manager = ppvalue != null ? ppvalue.Who : ""
                        }
                    };

                    //payment.Rate = rate;

                    var discount = new DiscountSetting
                    {
                        DiscountValue = new RowSettingValue()
                        {
                            Name = "Скидка/комиссия",
                            Value = (decimal)(row.Field<float?>("Komission") ?? 0),
                            ValueType = row.Field<int?>("Tipe_of_Komission") ?? 0,
                            Cost = cost,
                            DateChange = TextFormat.GetDate(disc),
                            Manager = disc != null ? disc.Who : ""
                        }
                    };

                    var status = new StatusSetting
                    {
                        Name = "Статус",
                        DateChange = TextFormat.GetDate(stat),
                        Manager = stat != null ? stat.Who : "",
                        StatusValue = row.Field<int>("DL_CONTROL"),
                        StatusName = row.Field<string>("CR_NAME")
                    };

                    //discount.Rate = rate;

                    serviceSetting = new ServiceSetting
                    {
                        Payment = payment,
                        Discount = discount,
                        Status = status,
                        DlKey = dlKey,
                        DgCode = row.Field<string>("DL_DGCOD")
                    };
                }
            }

            return serviceSetting;
        }

        public decimal GetCourse2(string rate)
        {
            if (rate.Equals("рб"))
                return 1;
            using (SqlCommand com = new SqlCommand(string.Format("select dbo.course_by_date('{0}', GetDate())", rate), Connection))
            {
                decimal val;
                if (decimal.TryParse(com.ExecuteScalar().ToString(), out val))
                    return val;
                else
                    throw new Exception("Ошибка запроса курса валюты");
            }
        }

        public DataRow GetShipInfo2(int dlKey)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(getShipInfo, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dlKey", dlKey);
                adapter.Fill(dt);
            }
            if (dt.Rows != null && dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public DataRow GetVisaInfo2(int dlKey)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(getVisaInfo, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dlKey", dlKey);
                adapter.Fill(dt);
            }

            if (dt.Rows != null && dt.Rows.Count > 0)
                return dt.Rows[0];

            return null;
        }

        public DataRow GetTransferInfo(int dlKey)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(getTransferInfo, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dlKey", dlKey);
                adapter.Fill(dt);
            }
            
            if (dt.Rows != null && dt.Rows.Count > 0)
                return dt.Rows[0];
            
            return null;
        }

        public void SetTransferInto(int dlKey, bool? guide, string guidePhone, string opNumber, DateTime? timeLimit)
        {
            using (SqlCommand cmd = new SqlCommand(setTransferInfo, Connection))
            {
                AddParam(cmd, "@dlKey", dlKey);
                AddParam(cmd, "@guide", guide);
                AddParam(cmd, "@guidePhone", guidePhone);
                AddParam(cmd, "@opNumber", opNumber);
                AddParam(cmd, "@timeLimit", timeLimit);
                cmd.ExecuteNonQuery();
            }
        }

        public DataRow GetTransferTypeInfo(int transferId)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(getTransferTypeInfo, ConnectionTS))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@transferId", transferId);
                adapter.Fill(dt);
            }

            if (dt.Rows != null && dt.Rows.Count > 0)
                return dt.Rows[0];

            return null;
        }

        public DataRow GetVoucherInfo(string dgCode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(getVoucherInfo, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgCode", dgCode);
                adapter.Fill(dt);
            }
            if (dt.Rows != null && dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public DataTable GetAviaTable2(int aviaBronID)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectAviaBron, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@idAvBron", aviaBronID);
                try
                {
                    adapter.Fill(dt);
                }
                catch (Exception e)
                {
                    TpLogger.ErrorWithMessage("WorkWithData.GetAviaTable2", string.Format("database={0}, aviaBronID={1}", Connection.Database, aviaBronID), e);
                }
            }
            return dt;
        }

        public DataTable GetAviaTurist2(int idBron)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectaviaturist, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@idBron", idBron);
                adapter.Fill(dt);
            }

            return dt;
        }

        public void UpdateDogovorLinePaymentSetting(string dgCode, int dlKey, bool prepaydType, decimal ppaymentValue, object ppaymentdate,
                                                    object paymentdate, string handWhy)
        {
            using (SqlCommand com = new SqlCommand("mk_DogovorListAddPaymentUpdate_new", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dgCode", dgCode);
                com.Parameters.AddWithValue("@dlKey", dlKey);
                com.Parameters.AddWithValue("@PaymantDate", paymentdate);
                com.Parameters.AddWithValue("@PPaymentdaDate", ppaymentdate);
                com.Parameters.AddWithValue("@PaymantValue", ppaymentValue);
                com.Parameters.AddWithValue("@PPaimentTipe", prepaydType ? 1 : 0);
                com.Parameters.AddWithValue("@HandWho", GetUserName());
                com.ExecuteNonQuery();
            }
        }

        public void UpdateDogovorLineDiscountSetting(string dgCode, int dlKey, bool discountType, decimal discount)
        {
            using (SqlCommand com = new SqlCommand("mk_DogovorListAddDiscountUpdate", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dgCode", dgCode);
                com.Parameters.AddWithValue("@dlKey", dlKey);
                com.Parameters.AddWithValue("@Komission", (float)discount);
                com.Parameters.AddWithValue("@Tipe_of_Komission", discountType ? 1 : 0);
                com.Parameters.AddWithValue("@HandWho", GetUserName());
                
                com.ExecuteNonQuery();
            }
        }

        public decimal CalculateDogovorLineDiscountSetting(string dgCode, int dlKey, decimal discountProcent)
        {
            if(dgCode == null || string.Equals(dgCode, string.Empty) || dlKey <= 0 || discountProcent < 0)
                throw new ArgumentException(string.Format("CalculateDogovorLineDiscountSetting: dgCode={0}, dlKey={1}, discountProcent={2}", dgCode, dlKey, discountProcent));

            decimal discountNumeric;
            using (SqlCommand com = new SqlCommand("mk_DogovorListAddDiscountCalc", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dgCode", dgCode);
                com.Parameters.AddWithValue("@dlKey", dlKey);
                com.Parameters.AddWithValue("@Komission", (float)discountProcent);
                com.Parameters.AddWithValue("@Tipe_of_Komission", 1);

                discountNumeric = Convert.ToDecimal(com.ExecuteScalar());
            }
            return discountNumeric;
        }

        public void DogovorRecalc(string dgCode)
        {
            using (SqlCommand com = new SqlCommand("mk_dogovor_recalc", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dg_code", dgCode);
                com.ExecuteNonQuery();
            }
        }

        public void UpdateDogovorLineStatusSetting(string dgCode, int dlKey, int status, string handWhy)
        {
            using (SqlCommand com = new SqlCommand("mk_DogovorListAddStatusUpdate", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dgCode", dgCode);
                com.Parameters.AddWithValue("@dlKey", dlKey);
                com.Parameters.AddWithValue("@Status", status);
                com.Parameters.AddWithValue("@HandWho", GetUserName());
                com.ExecuteNonQuery();
            }
        }

        public void UpdateDogovorSetting(string dgcode,bool prepaydtype,decimal prepayd,object ppaymentdate,object paymentdate)
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

        public void UpdateStatusDogovor(string dgcode, int status)
        {
            using (SqlCommand com = new SqlCommand("update mk_DogovorAdd set DA_Status =@status where DA_DGCODE =@dgcode exec dbo.mk_dogovor_recalc @dg_code=@dgcode",Connection))
            {
                com.Parameters.AddWithValue("@status", status);
                com.Parameters.AddWithValue("@dgcode", dgcode);
                com.ExecuteNonQuery();
            }
        }

        public void UpdateDiscountDogovor(string dgcode, decimal discount)
        {
            using (SqlCommand com = new SqlCommand(@"update tbl_dogovor set DG_DISCOUNT =@discount,DG_TYPECOUNT=1 where dg_code = @dgcode   exec dbo.mk_dogovor_recalc @dg_code=@dgcode",Connection))
            {
                com.Parameters.AddWithValue("@discount", discount);
                com.Parameters.AddWithValue("@dgcode", dgcode);
                com.ExecuteNonQuery();
            }
        }

        public DataTable GetSkyTeam()
        {
            DataTable _dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"select Company from [dbo].[AviaCardsSkyTeam]", ConnectionTS))
            {
                adapter.Fill(_dt);
            }
            return _dt;
        }

        public DataTable GetOneWorld()
        {
            DataTable _dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"select Company, Subsidiary from [dbo].[AviaCardsOneWorld]", ConnectionTS))
            {
                adapter.Fill(_dt);
            }
            return _dt;
        }

        public DataTable GetStarAliance()
        {
            DataTable _dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"select Company from [dbo].[AviaCardsStarAlliance]", ConnectionTS))
            {
                adapter.Fill(_dt);
            }
            return _dt;
        }

        public DataTable GetProblemServicesForDogovor(string dgcode = null)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("GetProblemServicesForDogovor", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DateTime GetDateForStatus(string dgcode, string query)
        {
            DateTime date = new DateTime();
            try
            {
                using (SqlCommand com = new SqlCommand(query, Connection))
                {
                    com.Parameters.AddWithValue("@dg_code", dgcode);
                    date = (DateTime)com.ExecuteScalar();
                }
            }
            catch (Exception)
            {

                date = new DateTime();
            }
            return date;
        }

        public DateTime GetDateForStatus(string dgcode, int status)
        {
            string query = string.Empty;
            using (SqlCommand com = new SqlCommand("select NS_QUERYFORDATE from mk_NewStatuses where NS_ID = @idstatus", Connection))
            {
                com.Parameters.AddWithValue("@idstatus", status);
                query = (string)com.ExecuteScalar();
            }
            return GetDateForStatus(dgcode, query);
        }

        public void HotelOk2(int dlKey, bool isOk)
        {
            using (SqlCommand com = new SqlCommand("insert into mk_options(OP_DLKEY," +
                                                    "OP_IsBook," +
                                                    "OP_WHO," +
                                                    "OP_LastUpdate) " +
                                                    "values " +
                                                    "(@dl_key" +
                                                    ",@p2" +
                                                    ",(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())" +
                                                    ",GetDate() ) ",
                                                    Connection))
            {
                com.Parameters.AddWithValue("@dl_key", dlKey);
                com.Parameters.AddWithValue("@p2", isOk);
                com.ExecuteNonQuery();
            }
        }

        public void InshurOk(string dgCode, bool isOk)
        {
            using (SqlCommand com = new SqlCommand("insert into mk_options(OP_DLKEY," +
                                                "OP_IsBook," +
                                                "OP_WHO," +
                                                "OP_LastUpdate) " +
                                                "select dl_key" +
                                                ",@p2" +
                                                ",(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())" +
                                                ",GetDate() from tbl_dogovorlist where dl_svkey=6 and dl_dgcod =@p1 ",
                                                Connection))
            {
                com.Parameters.AddWithValue("@p1", dgCode);
                com.Parameters.AddWithValue("@p2", isOk);
                com.ExecuteNonQuery();
            }
        }

        public DataTable GetUralsibInshurs(string dgCode)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("Select distinct INS_Numder,INS_Status from URS_Insurance where INS_DGCode=@p1 ", Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@p1", dgCode);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetInshurs(string dgCode)
        {
            DataTable dt = new DataTable();
            using (
                SqlDataAdapter adapter =
                    new SqlDataAdapter(
                        @"select distinct ui.INS_Numder,tl.TU_NAMELAT,tl.TU_FNAMELAT  from TuristService as ts 
                                                                inner join tbl_DogovorList as dl on ts.TU_DLKEY=dl.DL_KEY
                                                                inner join tbl_Turist as tl on tl.TU_KEY =ts.TU_TUKEY
                                                                left join URS_Insurance as ui on ts.TU_TUKEY=ui.INS_tukey
                                                                where ((ui.INS_Status=1)or(ui.INS_Status is null)) and dl.DL_SVKEY=6 and dl.DL_DGCOD=@p1",
                        Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@p1", dgCode);
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool GetInshurCreatedStatus(int dlKey)
        {
            bool inshurCreated = false;
            using ( SqlDataAdapter adapter = new SqlDataAdapter("select top 1 op_Isbook from mk_options where op_dlkey=@p1 order by op_id desc",
                                       WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@p1", dlKey);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    inshurCreated = dt.Rows[0].Field<bool>("op_Isbook");
                }

            }
            return inshurCreated;
        }

        public DataTable GetCollectingLowcostAirlinesTable()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"select * from [dbo].[CollectingLowcostAirlines]", ConnectionTS))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetCollectingLowcostAirlinesNotes()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"select * from [dbo].[CollectingLowcostAirlinesNotes]", ConnectionTS))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetCruiseLinesByBrandCode(string brandCode)
        {
            if (brandCode == null)
                return null;
            var dt = new DataTable();
            using (SqlCommand systemCommand = new SqlCommand(@"select Parametr_name, Parametr_value 
                from dbo.CruiseLines_Sys where brandCode = @crl order by parent", ConnectionTS))
            {
                systemCommand.Parameters.AddWithValue("@crl", brandCode);
                SqlDataAdapter sysadapter = new SqlDataAdapter(systemCommand);
                sysadapter.Fill(dt);
            }
            return dt;
        }

        public string GetCabinClasses(int shipId, string cabinCategory)
        {
            string rezult = string.Empty;
            using (SqlCommand com = new SqlCommand(@"SELECT top 1 name FROM [dbo].[CabinCategories]
                inner join dbo.CabinClasses as cc on class_id = cc.id where ship_id = @id and code =@code", _connectionTS))
            {
                com.Parameters.AddWithValue("@id", shipId);
                    com.Parameters.AddWithValue("@code", cabinCategory);
                rezult = (string)com.ExecuteScalar();
            }
            return rezult;
        }


        public string GetOpCategory(int shipId, string cabinCategory)
        {
            string rezult = string.Empty;
            using (SqlCommand com = new SqlCommand(@"SELECT top 1 name FROM [dbo].[CabinCategories]
                inner join dbo.CabinClasses as cc on class_id = cc.id where ship_id = @id and code =@code", _connectionTS))
            {
                com.Parameters.AddWithValue("@id", shipId);
                    com.Parameters.AddWithValue("@code", cabinCategory);
                rezult = (string)com.ExecuteScalar();
            }
            return rezult;
        }

        public DataTable GetSpecialsForCruise(int dlKey)
        {
            DataTable dt = new DataTable();
            using (SqlCommand com = new SqlCommand(selAction, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                com.Parameters.AddWithValue("@bon", false);
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
            }
            return dt;
        }

        public DataTable GetBonusesForCruise(int dlKey)
        {
            DataTable dt = new DataTable();
            using (SqlCommand com = new SqlCommand(selAction, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                com.Parameters.AddWithValue("@bon", true);
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
            }
            return dt;
        }

        public DataTable GetServicesForCruise(int dlKey)
        {
            DataTable dt = new DataTable();
            using (SqlCommand com = new SqlCommand(getServicesForCruisesQuery, Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetDopServicesForCruise(int dlKey)
        {
            DataTable dt = new DataTable();
            using (SqlCommand com = new SqlCommand(getDopServicesForCruiseQuery, Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
            }
            return dt;
        }
        
        public DataTable GetSelTourists(int dlKey)
        {
            DataTable dt = new DataTable();
            using (SqlCommand com = new SqlCommand(@"SELECT tbl_Turist.tu_key
                FROM [dbo].[TuristService] 
                inner join dbo.tbl_Turist on TU_KEY = tu_tukey
                where tu_dlkey=@dlkey", Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetBonusesAndServices(int dlKey)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT actions_id, isNull(isRight, 0) as isRight, CDP_NAME, Text
                FROM [dbo].[mk_actions_options] left join dbo.MK_Cruise_DOPServise on actions_id = CDS_ID where dl_key = @dlKey", Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dlKey", dlKey);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetTouristsByDlKey(int dlKey)
        {
            var query = @"select distinct tl.TU_KEY, tl.TU_NAMELAT, tl.TU_FNAMELAT, isnull(convert(varchar(10),[TU_BIRTHDAY],104),'') as TU_BIRTHDAY
                from TuristService as ts 
                inner join tbl_Turist as tl on tl.TU_KEY = ts.TU_TUKEY
                where ts.TU_DLKEY = @dlKey";

            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dlKey", dlKey);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetCorrespondence(string dgcode, string mod)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("GetMessages3", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", dgcode);
                adapter.SelectCommand.Parameters.AddWithValue("@mod", mod);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetRequestCorrespondence(int requestId)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("GetRequestMessages", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@requestId", requestId);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetUnreadMessages(string dgCode)
        {
            DataTable dt = new DataTable();
            using (
               SqlDataAdapter adapter =
                   new SqlDataAdapter(
                       "Select HI_ID,HI_MOD from History where HI_MOD in ('MTM','WWW') and HI_ID not in (select MS_HIID from dbo.mk_messageStatus where MS_isRead=1  and ms_uskey=@user) and HI_DGCOD =@dgCode ",
                       Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgCode", dgCode);
                adapter.SelectCommand.Parameters.AddWithValue("@user", GetUserID());
                adapter.Fill(dt);
            }
            return dt;
        }

        public void CheckUnreadMessages(string dgCode)
        {
            using (SqlCommand com = new SqlCommand(updteNew, Connection))
            {
                com.Parameters.AddWithValue("@dgCode", dgCode);
                com.Parameters.AddWithValue("@user", GetUserID());
                com.ExecuteNonQuery();
            }
        }

        private DateTime GetTrueDateTime(DateTime dt)
        {
            DateTime minValue = (DateTime) System.Data.SqlTypes.SqlDateTime.MinValue;
            return dt < minValue ? minValue : dt;
        }

        public void AddCruiseOption(int dlKey, string description, string optionNumber, string cabinNumber, string cabinDef, string category, DateTime dt,
            bool isBook, bool documentQuery, bool documentGet)
        {
            SqlCommand com = new SqlCommand(addCruiseOptionQuery, Connection);
            com.Parameters.AddWithValue("@OP_DLKEY", dlKey);

            AddParam(com, "@OP_Descript", description);
            AddParam(com, "@OP_number", optionNumber);
            AddParam(com, "@OP_N_cabin", cabinNumber);
            AddParam(com, "@OP_LEVEL_CABIN", cabinDef);
            AddParam(com, "@OP_category", category);

            com.Parameters.AddWithValue("@OP_date_end", GetTrueDateTime(dt));
            com.Parameters.AddWithValue("@OP_IsBook", isBook);
            com.Parameters.AddWithValue("@OP_DOCUMENT_QUERY", documentQuery);
            com.Parameters.AddWithValue("@OP_DOCUMENT_GET", documentGet);
            com.ExecuteNonQuery();
        }

        public void CruiseBonusesAndServicesSet(int dlKey, int[] id)
        {
            if (id == null || id.Length == 0)
                return;

            var query = @"update mk_actions_options set
                WhoRight =(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME()),
                [isRight]=1 where DL_key = @dlKey and actions_id in (" + string.Join(",", id) +
                           ") and (isRight = 0 or isRight is null)";

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@dlKey", dlKey);
                com.ExecuteNonQuery();
            }
        }

        public void CruiseBonusesAndServicesReset(int dlKey, int[] id)
        {
            if (id == null || id.Length == 0)
                return;

            var query = @"update mk_actions_options set
                WhoRight =(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME()),
                [isRight]=0 where DL_key = @dlKey and actions_id in (" + string.Join(",", id) + ") and isRight = 1";

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@dlKey", dlKey);
                com.ExecuteNonQuery();
            }
        }

        public void CruiseBonusesAndServicesChangeText(int dlKey, int id, string text)
        {
            var query = @"update mk_actions_options set Text = @text,
                WhoChangeText =(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())
                 where DL_key = @dlKey and actions_id = @action_id";

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@dlKey", dlKey);
                com.Parameters.AddWithValue("@action_id", id);
                com.Parameters.AddWithValue("@text", text);
                com.ExecuteNonQuery();
            }
        }

        protected static void AddParam(SqlCommand com, string name, Object value)
        {
            com.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }

        protected static void AddParam(SqlDataAdapter adapter, string name, Object value)
        {
            adapter.SelectCommand.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public int GetRequestByMessageId(int messageId)
        {
            var query = @"select RequestId from dbo.mk_RequestMessages
	            where Id = @MessageId";

            int rezult;
            using (SqlCommand com = new SqlCommand(query, _connection))
            {
                com.Parameters.AddWithValue("@MessageId", messageId);
                rezult = (int)(com.ExecuteScalar() ?? -1);
            }
            return rezult;
        }

        private int GetMinRequestMessageId()
        {
            int minRequestMessageId;
            var query = @"select MinSentMessageId from mk_RequestJournalSettings";

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                minRequestMessageId = (int)com.ExecuteScalar();
            }
            return minRequestMessageId;
        }
        
        public int AddMessage(int messageId, int? requestId, string text, DateTime date, string senderAddress, string reseiverAddress, bool seen, DateTime? readDate, 
            string theme, bool isIncoming, int inReplyToId, bool reply, string html, string mod, int? usKey)
        {
            if (!isIncoming)
            {
                var minOutputId = GetMinRequestMessageId();
                int maxId = GetMaxMessageId();
                messageId = maxId < minOutputId ? minOutputId : maxId + 1;
            }

            int realRequestId;
            using (SqlCommand com = new SqlCommand("AddRequestMessage", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@MessageId", messageId);
                com.Parameters.AddWithValue("@RequestId", requestId);
                com.Parameters.AddWithValue("@Date", date);
                com.Parameters.AddWithValue("@Text", text ?? "");
                com.Parameters.AddWithValue("@SenderAddress", senderAddress);
                com.Parameters.AddWithValue("@DestinationAddress", reseiverAddress);
                com.Parameters.AddWithValue("@Seen", seen);
                AddParam(com, "@ReadDate", readDate);
                AddParam(com, "@Theme", theme);
                com.Parameters.AddWithValue("@IsIncoming", isIncoming);
                AddParam(com, "@InReplyToId", inReplyToId);
                com.Parameters.AddWithValue("@Reply", reply);
                AddParam(com, "@Html", html);
                AddParam(com, "@Mod", mod);
                AddParam(com, "@UsKey", usKey);
                
                realRequestId = (int)com.ExecuteScalar();
            }
            return messageId;
            //return reply ? messageId : realRequestId;
        }

        public void AddAttachmentOld(int requestMessageId, int requestAttachmentTypeId, string contentTypeStr, string name, byte[] data)
        {
            var query = @"insert into mk_RequestAttachments values(@RequestMessageId, @RequestAttachmentTypeId, @Name, @Data)";
            
            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@RequestMessageId", requestMessageId);
                com.Parameters.AddWithValue("@RequestAttachmentTypeId", requestAttachmentTypeId);
                com.Parameters.AddWithValue("@Name", name);
                com.Parameters.AddWithValue("@Data", data);
                com.ExecuteNonQuery();
            }
        }

        public void AddAttachment(int requestMessageId, string contentType, string name, byte[] data)
        {
            using (SqlCommand com = new SqlCommand("mk_AddRequestMessageAttachment", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@RequestMessageId", requestMessageId);
                com.Parameters.AddWithValue("@ContentType", contentType);
                com.Parameters.AddWithValue("@Name", name);
                com.Parameters.AddWithValue("@Data", data);
                com.ExecuteScalar();
            }
        }

        public DataTable GetAttachments(int requestMessageId)
        {
            var query = @"select a.[Id], a.[RequestMessageId], a.[RequestAttachmentTypeId], a.[Name], a.[Data], at.ContentType 
                        from mk_RequestAttachments as a
                        inner join mk_RequestAttachmentType as at on at.Id = a.RequestAttachmentTypeId
                        where a.RequestMessageId = @RequestMessageId";
            
            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@RequestMessageId", requestMessageId);
                adapter.Fill(dt);
            }
            return dt;
        }

        public int GetMaxMessageId()
        {
            var query = @"select top 1 Id from [mk_RequestMessages] order by Id desc";

            int maxId = -1;
            using(SqlCommand com = new SqlCommand(query, Connection))
            {
                maxId = (int)com.ExecuteScalar();
            }
            return maxId;
        }

        public void AddRequestStatus(int requestId, int statusId, int? userId, DateTime dateTime)
        {
            using (SqlCommand com = new SqlCommand("mk_SetRequestStatus", Connection))
            {
                com.CommandType = CommandType.StoredProcedure; 
                com.Parameters.AddWithValue("@requestId", requestId);
                com.Parameters.AddWithValue("@statusId", statusId);
                com.Parameters.AddWithValue("@date", dateTime);
                AddParam(com, "@userId", userId);
                com.ExecuteNonQuery();
            }
        }

        public void SetRequestSubStatus(int requestId, int subStatusId, int? userId, DateTime dateTime)
        {
            using(var com = new SqlCommand("mk_SetRequestSubStatus", Connection))
            {
                com.CommandType = CommandType.StoredProcedure; 
                com.Parameters.AddWithValue("@requestId", requestId);
                com.Parameters.AddWithValue("@subStatusId", subStatusId);
                com.Parameters.AddWithValue("@date", dateTime);
                AddParam(com, "@userId", userId);
                com.ExecuteNonQuery();
            }
        }

        public void MakeReservation(int requestId, string dgCode, int? userId, DateTime dateTime)
        {
            var statusId = 3;
            using (SqlCommand com = new SqlCommand("mk_SetRequestStatus", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@requestId", requestId);
                com.Parameters.AddWithValue("@statusId", statusId);
                com.Parameters.AddWithValue("@date", dateTime);
                AddParam(com, "@userId", userId);
                com.Parameters.AddWithValue("@dgCode", dgCode);
                com.ExecuteNonQuery();
            }
        }

        public void ChangeSenderAddress(int messageId, string newAddress)
        {
            using (var com = new SqlCommand("mk_ChangeSenderAddress", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Id", messageId);
                com.Parameters.AddWithValue("@SenderAddress", newAddress);
                com.ExecuteNonQuery();
            }
        }

        public void AnnulateRequest(int requestId, int userId, int subStatusId)
        {
            using (SqlCommand com = new SqlCommand("mk_AnnulateRequest", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@requestId", requestId);
                com.Parameters.AddWithValue("@userId", userId != -1 ? (object) userId : DBNull.Value);
                com.Parameters.AddWithValue("@subStatusId", subStatusId);
                com.ExecuteNonQuery();
            }
        }

        public int? GetAnnulateStatusId()
        {
            var query =
                @"select top 1 [AnnulateStatus] from mk_RequestJournalSettings";

            int? anulateId;

            using (var cmd = new SqlCommand(query, Connection))
            {
                anulateId = cmd.ExecuteScalar() as int?;
            }

            return anulateId;
        }

        public DataRow GetAnnulateStatus()
        {
            var query =
                @"select * from mk_RequestStatuses where Id = (select top 1 [AnnulateStatus] from mk_RequestJournalSettings)";

            DataRow row = null;

            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows != null && dt.Rows.Count > 0)
                    row = dt.Rows[0];
            }

            return row;
        }

        public bool RequestIdIsExists(int id)
        {
            var query = @"select case when exists(select * from [mk_Requests] where Id = @Id) then 1 else 0 end";

            bool result;
            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@Id", id);
                result = (int)com.ExecuteScalar() == 1;
            }
            return result;
        }

        public DataTable GetAllRequests_old()
        {
            var query = @"select	rq.Id, 
		[Date], 
		[RequestTypeId], 
		rq.RequestStatusId, 
		rq.RequestSubStatusId, 
		rq.US_KEY, 
        rq.DgCode, 
        rq.SuperviserChecked,
        rq.IsClosed,
		rt.Name as RequestTypeStr, 
		rs.Name as RequestStatusStr,
		ss.Name as RequestSubStatusStr,  
		ul.US_FullName as UserName
from dbo.mk_Requests as rq
left join dbo.mk_RequestTypes as rt on rt.Id = [RequestTypeId]
left join dbo.mk_RequestStatuses as rs on rs.Id =  rq.[RequestStatusId]
left join dbo.mk_RequestSubStatuses as ss on ss.Id = rq.[RequestSubStatusId]
left join dbo.UserList as ul on ul.US_KEY = rq.US_KEY";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetAllRequests(bool cancellate, bool reserved, ulong timeStamp)
        {
            var timeStampData = BitConverter.GetBytes(timeStamp);
            Array.Reverse(timeStampData);

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("mk_GetRequests", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@cancellate", cancellate);
                adapter.SelectCommand.Parameters.AddWithValue("@reserved", reserved);
                //adapter.SelectCommand.Parameters.AddWithValue("@minTimestamp", timeStampData);
                adapter.Fill(dt);
            }
            return dt;
        }

        private static DataTable ArrayToDataTable<T>(IEnumerable<T> data)
        {
            var dt = new DataTable();
            dt.Columns.Add();

            foreach (var value in data)
            {
                var row = dt.NewRow();
                row[0] = value;
                dt.Rows.Add(row);
            }

            return dt;
        }

        private static DataTable ArrayToDataTable(IEnumerable<int> data)
        {
            var dt = new DataTable();
            dt.Columns.Add();

            foreach (var value in data)
            {
                var row = dt.NewRow();
                row[0] = value;
                dt.Rows.Add(row);
            }

            return dt;
        }

        public DataTable GetRequests(DateTime? dateBegin, DateTime? dateEnd, string senderAddress, int? usKey, int[] problems, int[] statuses, int[] subStatuses, bool senderAddressContains)
        {
            var problemsTable = ArrayToDataTable(problems);
            var statusesTable = ArrayToDataTable(statuses);
            var subStatusesTable = ArrayToDataTable(subStatuses);

            var procName = senderAddressContains ? "mk_GetRequests_like" : "mk_GetRequests";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(procName, Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                AddParam(adapter, "@DateBegin", dateBegin);
                AddParam(adapter, "@DateEnd", dateEnd);
                AddParam(adapter, "@SenderAddress", senderAddress);
                AddParam(adapter, "@UsKey", usKey);
                AddParam(adapter, "@Problems", problemsTable);
                AddParam(adapter, "@Statuses", statusesTable);
                AddParam(adapter, "@SubStatuses", subStatusesTable);
                adapter.Fill(dt);
            }
            return dt;
        }

        public int GetRequestCount(DateTime? dateBegin, DateTime? dateEnd, int? usKey, bool showReserved, bool showAnnulate)
        {
            var count = 0;

            using (SqlCommand cmd = new SqlCommand("mk_GetRequestCount", Connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                AddParam(cmd, "@DateBegin", dateBegin);
                AddParam(cmd, "@DateEnd", dateEnd);
                AddParam(cmd, "@UsKey", usKey);
                cmd.Parameters.AddWithValue("@ShowReserved", showReserved);
                cmd.Parameters.AddWithValue("@ShowAnnulate", showAnnulate);
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return count;
        }

        public int GetLockingCompanionsCount()
        {
            var count = 0;
            var query = @"select Count(*) from mk_Requests where RequestSubStatusId = (select top 1 LockingCompanionsSubstatusId from mk_RequestJournalSettings) and RequestStatusId < 3";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return count;
        }

        public int GetExistsCompanionsCount()
        {
            var count = 0;
            var query = @"select Count(*) from mk_Requests where RequestSubStatusId = (select top 1 ExistsCompanionsSubstatusId from mk_RequestJournalSettings) and RequestStatusId < 3";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return count;
        }

        public DataTable GetAllSenders(bool cancellate, bool reserved)
        {
            var query = @"select distinct SenderAddress from (select SenderAddress, RequestId, ROW_NUMBER() over (partition by RequestId order by [Date]) as RowNumber 
                        from mk_RequestMessages) as ms
                        inner join mk_Requests as rq on rq.Id = ms.RequestId
                        where RowNumber = 1 and 
                        rq.RequestStatusId != case when @cancellate = 0 then 4 else -1 end and 
                        rq.RequestStatusId != case when @reserved = 0 then 3 else -1 end";
            
            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@cancellate", cancellate);
                adapter.SelectCommand.Parameters.AddWithValue("@reserved", reserved);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetCancelationReasons()
        {
            var query = @"select * from mk_RequestSubStatuses where RequestStatusId = 3";
            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }

            return dt;
        }

        public DataTable GetRequestsIdForReservation()
        {
            var query = @"select Id
                from dbo.mk_Requests
                where RequestStatusId In (0,1,2)";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetRequestsIdForReservation(int usKey)
        {
            var query = @"select Id
                from dbo.mk_Requests
                where US_KEY = @usKey and RequestStatusId In (0,1,2)";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("usKey", usKey);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetAllStatuses()
        {
            var query = @"select * from mk_RequestStatuses";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetProblemSubStatuses()
        {
            var query = @"select * from mk_RequestSubStatuses";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetRequestProblems()
        {
            var query = @"select * from mk_RequestProblems";

            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetProblemRequests()
        {
            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter("mk_GetProblemRequests", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetProblemRequestGroups(int? uskey)
        {
            var query = @"declare @t table (RequestId int, ProblemId int, ProblemName varchar(100), usKey int null)
                insert into @t
                exec mk_GetProblemRequests @usKey = @usKey
                select ProblemId, ProblemName, Count(*) as [Count] from @t group by ProblemId, ProblemName order by ProblemId ";

            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                AddParam(adapter, "@usKey", uskey);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetManagerWithProblemRequests()
        {
            var query = @"declare @t table (RequestId int, ProblemId int, ProblemName varchar(100), usKey int null)
                insert into @t
                exec mk_GetProblemRequests @usKey = null
                select distinct usKey as [key], US_NAME + ' ' + US_FNAME as [name]
                from @t 
                inner join UserList on US_KEY = usKey
                where not usKey is null
                order by name";

            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public int GetProblemRequestCount(int? uskey)
        {
            var query = @"declare @t table (RequestId int, ProblemId int, ProblemName varchar(100), usKey int null)
                insert into @t
                exec mk_GetProblemRequests @usKey = @usKey
                select count(*) from (select distinct RequestId from @t) as t";

            int count = 0;
            using (var command = new SqlCommand(query, Connection))
            {
                AddParam(command, "@usKey", uskey);
                count = Convert.ToInt32(command.ExecuteScalar());
            }
            return count;
        }
        
        public DataTable AnnulateRequest(int requestId)
        {
            var query = @"select * from mk_RequestStatuses";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetMessages(int requestId)
        {
            var query = @"SELECT * from [dbo].[mk_RequestMessages] where RequestId = @RequestId order by Date";

            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@RequestId", requestId);
                adapter.Fill(dt);
            }
            return dt;
        }

        /*public DataTable GetRequestStatusHistory(int ruquestId)
        {
            var query = @"SELECT h.[Id], h.[RequestId], h.[RequestStatusId], h.[Date], s.Name
                FROM [dbo].[mk_RequestStatusHistory] as h
                left join [dbo].mk_RequestStatuses as s on s.Id = h.RequestStatusId
                where h.[RequestId] = @RequestId";

            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@RequestId", ruquestId);
                adapter.Fill(dt);
            }
            return dt;
        }*/

        public DataTable GetRequestStatusHistory(int requestId)
        {
            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter("mk_GetRequestStatusHistory", Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@RequestId", requestId);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetRequestStatusHistory2(int requestId)
        {
            var query = @"select RequestStatusId, Name, [Date], UserId,  isNull(ul.US_FullName, 'Система') as UsName 
from mk_RequestStatusHistory
inner join mk_RequestStatuses as rs on rs.Id = RequestStatusId
left join UserList as ul on ul.US_KEY = UserId
where RequestId = @RequestId order by [Date]";

            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@RequestId", requestId);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetRequestSubStatusHistory2(int requestId)
        {
            var query = @"select * from mk_RequestSubStatusHistory
inner join mk_RequestSubStatuses as rs on rs.Id = RequestSubStatusId
where RequestId = @RequestId order by [Date]";

            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@RequestId", requestId);
                adapter.Fill(dt);
            }
            return dt;
        }
        
        public DataTable GetManagerListOld()
        {
            var query = @"select us_key, US_FullNameLat from UserList left join mk_user_rule on UR_USKEY = us_key where isnull([is_realiz],0) = 1 order by US_FullNameLat";

            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }

            return dt;
        }

        public DataTable GetManagersWithProblemVouchers()
        {
            var query = @"select distinct mp_bronKey, mp_bronir from mk_ProblemsBrons where not mp_bronKey is null
                union
                select distinct mp_managKey, mp_manag from mk_ProblemsBrons where not mp_managKey is null";

            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }

            return dt;
        }

        public DataTable GetManagerList()
        {
            var query = @"select ul.us_key, ul.US_FullNameLat, ul.US_NAME, ul.US_FNAME, ul.US_SNAME, ul.US_JOB, ul.US_MAILBOX, pn.Phone from UserList as ul 
left join [mk_roles] on USERID = ul.US_USERID
left join mk_UserPhones as pn on pn.UsKey = ul.US_KEY 
inner join mk_user_rule on UR_USKEY=ul.US_KEY  
where isSuperViser = 1 or isProductManagers = 1 or isSalesManagers = 1 and isnull([is_realiz],0)=1 order by US_FullNameLat";

            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }

            return dt;
        }
        
        public IEnumerable<int?> GetUnseenMessages()
        {
            var query = @"select rq.RequestStatusId, rq.US_KEY from mk_Requests as rq
                        left join mk_RequestMessages as ms on ms.RequestId = rq.Id
                        where ms.Seen = 0";

            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }

            return dt.Select().Select(r => r.Field<int?>("US_KEY"));
        }

        public DataTable GetUnseenMessages2()
        {
            var query = @"select rq.RequestStatusId, rq.US_KEY, ms.Mod from mk_Requests as rq
                        left join mk_RequestMessages as ms on ms.RequestId = rq.Id
                        where ms.Seen = 0";

            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }

            return dt;
        }

        public int GetUnansweredRequestsCount(string mod, int? usKey)
        {
            int val;

            using (var com = new SqlCommand("mk_GetUnansweredRequestsCount", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@mod", mod);
                AddParam(com, "@usKey", usKey);
                val = Convert.ToInt32(com.ExecuteScalar());
            }

            return val;
        }
        
        public ulong GetMaxTimeStamp()
        {
            var query = @"declare @requestTs timestamp,
		                @messageTs timestamp
                        SELECT @requestTs = Max(ts) FROM mk_Requests
                        select @messageTs = Max(ts) FROM mk_RequestMessages
                        select case when(@requestTs > @messageTs) then @requestTs else @messageTs end";

            ulong ts;

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                var data = (byte[]) com.ExecuteScalar();
                Array.Reverse(data);
                ts = data.Length > 0 ? BitConverter.ToUInt64(data, 0) : 0;
            }

            return ts;
        }

        public void SetTracking(int id, DateTime date)
        {
            var query = @"Update mk_RequestMessages Set Tracking = @trackingDate where Id = @id";

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@id", id);
                AddParam(com, "@trackingDate", date);
                com.ExecuteNonQuery();
            }
        }

        public void SetSent(int id, DateTime date)
        {
            var query = @"Update mk_RequestMessages Set Sent = @sentDate where Id = @id
                        declare @dt datetime = GetDate()
	                    insert into mk_RequestHistory ([Date], [UsKey], [ActionId], [ValueInt], [ValueDatetime]) values (@dt, null, 4, @id, @sentDate)";

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@id", id);
                AddParam(com, "@sentDate", date);
                com.ExecuteNonQuery();
            }
        }

        public void SuperviserChecked(int id, int usKey)
        {
            var query = @"declare @Date datetime = GetDate()
                        Update mk_Requests Set SuperviserChecked = @Date where Id = @id
                        insert into mk_RequestHistory ([Date], [UsKey], [ActionId], [ValueInt]) values (@Date, @usKey, 5, @id)";

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@id", id);
                com.Parameters.AddWithValue("@usKey", id);
                com.ExecuteNonQuery();
            }
        }

        public void CloseCorrespondence(int id, int usKey)
        {
            using (SqlCommand com = new SqlCommand("mk_RequestCloseCorresp", Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@RequestId", id);
                com.Parameters.AddWithValue("@UsKey", usKey);
                com.ExecuteNonQuery();
            }
        }

        public void OpenCorrespondence(int id, int usKey)
        {
            var query = @"declare @Date datetime = GetDate()
                        Update mk_Requests Set IsClosed = null where Id = @id
                        insert into mk_RequestHistory ([Date], [UsKey], [ActionId], [ValueInt]) values (@Date, @usKey, 6, @id)";

            using (SqlCommand com = new SqlCommand(query, Connection))
            {
                com.Parameters.AddWithValue("@id", id);
                com.Parameters.AddWithValue("@usKey", id);
                com.ExecuteNonQuery();
            }
        }

        public DataTable GetCallRecords(DateTime? dateBegin, DateTime? dateEnd, int? status)
        {
            var query = @"SELECT * FROM [Mk_ring_from_site] as r
                left join mk_status_ring as s on s.id = r.status
                where [Time_for_ring] >= isnull(@dateBegin, [Time_for_ring]) and 
                [Time_for_ring] <= isnull(@dateEnd, [Time_for_ring]) and 
                [status] = isnull(@status, [status])";

            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                AddParam(adapter, "@dateBegin", dateBegin);
                AddParam(adapter, "@dateEnd", dateEnd);
                AddParam(adapter, "@status", status);
                adapter.Fill(dt);
            }

            return dt;
        }

        public DataTable GetCallRecordStatuses()
        {
            var query = @"SELECT * FROM mk_status_ring";

            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter(query, Connection))
            {
                adapter.Fill(dt);
            }

            return dt;
        }
    }
}