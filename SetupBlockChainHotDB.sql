--Last updated on 9th Jan 2017

--CREATE DATABASE [BlockChainHot]
--GO

Use BlockChainHot
GO

select * from Batch
select * from BatchOwnershipHistory
select * from StabilityRange
select * from TemparatureTelemetry
select * from OwnerManager

if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Batch')
BEGIN
	Drop table Batch
END
GO

if not exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Batch')
BEGIN
	Create table Batch
	(
		Id bigint identity(1,1) not null,
		BatchCode varchar(100) not null,
		TempLoggerCode varchar(100) not null,
		[Description] varchar(500) null,
		ProducerCode varchar(500) null,
		ExpiryStatus bit null
	)
END
GO

if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'StabilityRange')
BEGIN
	Drop table StabilityRange
END
GO

if not exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'StabilityRange')
BEGIN
	Create table StabilityRange
	(
		Id bigint identity(1,1) not null,
		BatchCode varchar(100) not null,
		RangeId int not null,
		MinTemp int not null,
		MaxTemp int not null
		,ExpireTickCount int not null
	)
END
GO

--select * from Batch
if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'BatchOwnershipHistory')
BEGIN
	Drop table BatchOwnershipHistory
END
GO

if not exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'BatchOwnershipHistory')
BEGIN
	Create table BatchOwnershipHistory
	(
		Id bigint identity(1,1) not null,
		BatchCode varchar(100) not null,
		OwnerCode varchar(100) not null,
		StartTime datetime not null,
		EndTime datetime null
	)
END
GO

--select * from Batch
if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'OwnerManager')
BEGIN
	Drop table OwnerManager
END
GO


--drop table OwnerManager
if not exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'OwnerManager')
BEGIN
	Create table OwnerManager
	(
		Id bigint identity(1,1) not null,
		OwnerCode varchar(100) not null,
		OwnerDesc varchar(500) not null,
		IsTempLogger bit not null
	)
END
GO

if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'TemparatureTelemetry')
BEGIN
	Drop table TemparatureTelemetry
END
GO

if not exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'TemparatureTelemetry')
BEGIN
	Create table TemparatureTelemetry
	(
		Id bigint identity(1,1) not null,
		BatchCode varchar(100) not null,
		Temperature int not null,
		LogTime Datetime not null
	)
END
GO

--setup OWNER table with existing records
--Owner Account Addresses:
--0x11e0e21d9cc5408fb5d311f69c36dbd165946369 => Owner1 - NN
--0x613648ee5e21e260a9610ec425a1fb9e04245c3b => Owner2 - FedEx
--0xc021325abaadb14641c2aea88ad1434b8bd647cc => Owner3 - CareLab
--
--Temperature Logger Account Addresses:
--0x6918cddfd625829e62b808f32b847cd4363ee9bb => Temperature Logger 1
--0x31215f4f2305453c83401a543a4c04bf70d17adb => Temperature Logger 2

if not exists(select * from OwnerManager where OwnerCode = '0x11e0e21d9cc5408fb5d311f69c36dbd165946369')
begin
	Insert into OwnerManager 
		(OwnerCode
		, OwnerDesc
		,IsTempLogger
		)
	values
		('0x11e0e21d9cc5408fb5d311f69c36dbd165946369'
		,'Owner1 - NN'
		,0
		)		
end

if not exists(select * from OwnerManager where OwnerCode = '0x613648ee5e21e260a9610ec425a1fb9e04245c3b')
begin
	Insert into OwnerManager 
		(OwnerCode
		, OwnerDesc
		,IsTempLogger
		)
	values
		('0x613648ee5e21e260a9610ec425a1fb9e04245c3b'
		,'Owner2 - Fedex'
		,0
		)		
end

--setup OWNER table with existing records
if not exists(select * from OwnerManager where OwnerCode = '0xc021325abaadb14641c2aea88ad1434b8bd647cc')
begin
	Insert into OwnerManager 
		(OwnerCode
		, OwnerDesc
		,IsTempLogger
		)
	values
		('0xc021325abaadb14641c2aea88ad1434b8bd647cc'
		,'Owner3 - CareLab'
		,0
		)		
end


--setup OWNER table with existing records
if not exists(select * from OwnerManager where OwnerCode = '0x6918cddfd625829e62b808f32b847cd4363ee9bb')
begin
	Insert into OwnerManager 
		(OwnerCode
		, OwnerDesc
		,IsTempLogger
		)
	values
		('0x6918cddfd625829e62b808f32b847cd4363ee9bb'
		,'Temperature Logger 1'
		,1
		)		
end


--setup OWNER table with existing records
if not exists(select * from OwnerManager where OwnerCode = '0x31215f4f2305453c83401a543a4c04bf70d17adb')
begin
	Insert into OwnerManager 
		(OwnerCode
		, OwnerDesc
		,IsTempLogger
		)
	values
		('0x31215f4f2305453c83401a543a4c04bf70d17adb'
		,'Temperature Logger 2'
		,1
		)		
end