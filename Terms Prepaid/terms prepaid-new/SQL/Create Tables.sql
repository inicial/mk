CREATE TABLE dbo.mk_option_files (
  OPF_Id int IDENTITY,
  OPF_file_date varbinary(max) NULL,
  OPF_file_name varchar(150) NULL,
  OPF_option_key int NULL,
  CONSTRAINT PK_mk_option_files PRIMARY KEY (OPF_Id),
  CONSTRAINT FK_mk_option_files_mk_options_OP_ID FOREIGN KEY (OPF_option_key) REFERENCES dbo.mk_options (OP_ID)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE dbo.mk_options (
  OP_ID int IDENTITY,
  OP_DLKEY int NULL,
  OP_Descript varchar(250) NULL,
  CONSTRAINT PK_mk_options PRIMARY KEY (OP_ID),
  CONSTRAINT FK_mk_options_tbl_DogovorList_DL_KEY FOREIGN KEY (OP_DLKEY) REFERENCES dbo.tbl_DogovorList (DL_KEY)
) ON [PRIMARY]

CREATE TABLE dbo.mk_DogovorListAdd (
  tbl_dogovor_list_key int NOT NULL,
  PaymantDate datetime NULL,
  PaymantValue decimal(18, 2) NULL,
  CabinNumber varchar(10) NULL,
  HandWho varchar(4000) NULL,
  PPaymentdaDate datetime NULL,
  Komission int NULL,
  Pacage varchar(50) NULL,
  Optsia datetime NULL,
  shipcode varchar(5) NULL,
  CONSTRAINT PK_mk_pamand_period PRIMARY KEY (tbl_dogovor_list_key)
) ON [PRIMARY]