CREATE procedure usp_mz_kfhzxx_getdata
@ghxh u5_xh12,
@patid u5_xh12
as
/*
康复患者信息集成
exec usp_mz_kfhzxx_getdata 1481,491
*/
begin
 set nocount on 
 if exists(select 1 from tempdb..sysobjects where name ='#cdsspatinfo') drop table #kfhz 
 create table #kfhz
 (
  patientid	    varchar(12), 
  outpationid		varchar(12),
  [type]	    varchar(8),
  patientnum	    varchar(16), 
  cardno	        varchar(32),
  inCode	        varchar(32),
  inType	    varchar(16),
  [name]		    varchar(16),
  sex	        varchar(16),
  age	    varchar(8),
  [national] varchar(8),
  marriage	    varchar(8),
  birthday	        varchar(8),
  vocation	        varchar(8),
  personcard	    varchar(32),
  number	    varchar(8),
  [address]      varchar(8),
  diseasename      varchar(32),
  diseasecode      varchar(256),
  main      varchar(256),
  present      varchar(256),
  past      varchar(256),
  allergic      varchar(256),
  inspect      varchar(1024),
  [sign]      varchar(16),
  temperature      varchar(16),
  heartRate      varchar(16),
  pulse      varchar(16),
  patheight      varchar(16),
  patweight      varchar(16),
  systolic      varchar(16),
  diastolic      varchar(16),
  stageGoal      varchar(16),
  longGoal      varchar(16),
  remarks      varchar(2000),
  doctorName      varchar(16),
  doctorId      varchar(16),
  deptName      varchar(16),
  deptCode      varchar(16),
  executeName      varchar(16),
  executeCode      varchar(16),
  seeDate      varchar(16),
  leaveDate      varchar(16),
  hospitalid      varchar(16),
  hospitalname      varchar(16)
  )

  insert into #kfhz(patientid,type,patientnum,cardno,inCode,name,sex,birthday,age)
  select PATID,1,GHXH,CARDNO,CARDNO,HZXM,SEX,BIRTH,dbo.ufn_his5_CalcAge(BIRTH, CONVERT(varchar(8),getdate(),112) ,0,1,1,1)
  from OUTP_JZJLK with(nolock) where GHXH=@ghxh


  declare @zy varchar(16),@mz varchar(16)
  select @zy=ZYDM,@mz=MZDM from PUB_MZBRXXK where PATID=@patid


  update #kfhz set vocation =@zy,[national] =@mz
  
  select * from #kfhz
end


