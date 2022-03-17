CREATE DATABASE [DemoTicketsDB]
GO
USE [DemoTicketsDB]
GO
CREATE TABLE [Processes](
	[Id] INT IDENTITY(1,1),
	[Name] VARCHAR(25) NOT NULL,
	[Description] VARCHAR(MAX) NOT NULL,
	[IsActive] BIT DEFAULT 0,
	[CreationTime] DATETIME2 DEFAULT GETDATE(),
	[HostName] NVARCHAR(128) DEFAULT HOST_NAME(),
	[UserName] NVARCHAR(256) DEFAULT SUSER_NAME(),
	CONSTRAINT PK_PROCESSES_ID PRIMARY KEY ([Id])
)
GO
CREATE TABLE [Tickets](
	[Id] INT IDENTITY(1,1),
	-- [ProcessId] INT NOT NULL,
	[ProcessName] VARCHAR(25) NOT NULL,
	[ProductType] VARCHAR(25) NOT NULL,
	[CaseNumber] VARCHAR(10) NOT NULL,
	[SubscriberNumber] VARCHAR(25) NOT NULL,
	[CurrentQueue] VARCHAR(50) NOT NULL,
	[DestinationQueue] VARCHAR(50),
	[UAC] VARCHAR(15) NOT NULL,
	[ClientName] VARCHAR (100) NOT NULL,
	[ClientContactPhone] VARCHAR(25) NOT NULL,
	[Status] VARCHAR(250) NOT NULL,
	[CreationTime] DATETIME2 DEFAULT GETDATE(),
	[HostName] NVARCHAR(128) DEFAULT HOST_NAME(),
	[UserName] NVARCHAR(256) DEFAULT SUSER_NAME(),
	CONSTRAINT PK_TICKETS_ID PRIMARY KEY ([Id]),
	-- CONSTRAINT FK_PROCESSID FOREIGN KEY ([ProcessId]) REFERENCES [Processes]([Id])
	CONSTRAINT UQ_TICKETS_CASENUMBER UNIQUE ([CaseNumber]),
	CONSTRAINT UQ_TICKETS_UAC UNIQUE ([UAC])
)
GO
CREATE TABLE [Facilities](
	[Id] INT IDENTITY(1,1),
	[NodeAddress] VARCHAR(50) NOT NULL,
	[IpAddress] VARCHAR(20) NOT NULL,
	[NodeName] VARCHAR(50) NOT NULL,
	[CreationTime] DATETIME2 DEFAULT GETDATE(),
	[HostName] NVARCHAR(128) DEFAULT HOST_NAME(),
	[UserName] NVARCHAR(256) DEFAULT SUSER_NAME(),
	CONSTRAINT PK_FACILITIES_ID PRIMARY KEY ([Id])
)
GO
CREATE TABLE [Diagnostics](
	[Id] INT IDENTITY(1,1),
	[TicketId] INT NOT NULL,
	[IsConfigured] BIT,
	[OLTAdminState] BIT,
	[OLTOperState] BIT,
	[ONTAdminState] BIT,
	[ONTOperState] BIT,
	[ONTRxPower] BIT,
	[ONTTxPower] BIT,
	[ONTVoltage] BIT,
	[CreationTime] DATETIME2 DEFAULT GETDATE(),
	[HostName] NVARCHAR(128) DEFAULT HOST_NAME(),
	[UserName] NVARCHAR(256) DEFAULT SUSER_NAME(),
	CONSTRAINT PK_DIAGNOSTICS_ID PRIMARY KEY ([Id])
)
GO
INSERT INTO [Processes] ([Name],[Description],[IsActive]) VALUES 
('DIAG-ADSL','Check configurations and params for tickets that have ADSL as a product in the diagnostic stage',1),
('CONF-IPTV','Check configurations and params for tickets that have IPTV as a product in the confirmation stage',1)
GO
INSERT INTO [Tickets] ([ProcessName],[ProductType],[CaseNumber],[SubscriberNumber],[CurrentQueue],[UAC],[ClientName],[ClientContactPhone],[Status]) VALUES 
('DIAG-ADSL','ADSL','356241','3545558888','ADSL FAULT DIAGNOSIS','243','Bailey Davidson','4607295853','PENDING'),
('CONF-IPTV','IPTV','142653','3546661515','IPTV FAULT CONFIRMATION','789','Remi Holmes','16967598718','PENDING')
GO