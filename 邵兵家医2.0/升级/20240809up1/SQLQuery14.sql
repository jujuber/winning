CREATE procedure usp_mz_save_grtzdata
@ghxh decimal(12,0),
@patid decimal(12,0),
@personcard varchar(32),
@name varchar(32),
@gender varchar(32),
@birth varchar(32),
@measureType varchar(32),
@measureTime varchar(32),
@measureSourceId varchar(32),
@measureLocation varchar(32),
@measureOrgId varchar(32),
@measureMode varchar(32),
@deviceId varchar(32),
@deviceType varchar(32),
@measureDoc varchar(32),
@networkStatus varchar(32),
@measureData varchar(512)
as
/* 
*/
begin
 set nocount on 
 
 select 'T','³É¹¦'
 return;
end


