CREATE DATABASE scheduler_db;

CREATE TABLE processes(
	id SERIAL,
	name VARCHAR(25) NOT NULL,
	description VARCHAR NOT NULL,
	isActive BOOLEAN NOT NULL DEFAULT FALSE,
	creationTime TIMESTAMP NOT NULL DEFAULT NOW(),
	userName VARCHAR(256) NOT NULL DEFAULT current_user,
	CONSTRAINT pk_processes_id PRIMARY KEY (id)
);

CREATE TABLE tickets(
	id SERIAL,
	processName VARCHAR(25) NOT NULL,
	productType VARCHAR(25) NOT NULL,
	caseNumber VARCHAR(10) NOT NULL,
	subscriberNumber VARCHAR(25) NOT NULL,
	currentQueue VARCHAR(50) NOT NULL,
	destinationQueue VARCHAR(50),
	uac VARCHAR(15) NOT NULL,
	clientName VARCHAR (100) NOT NULL,
	clientContactPhone VARCHAR(25) NOT NULL,
	status VARCHAR(250) NOT NULL,
	creationTime TIMESTAMP NOT NULL DEFAULT NOW(),
	userName VARCHAR(256) NOT NULL DEFAULT current_user,
	CONSTRAINT pk_tickets_id PRIMARY KEY (id),
	CONSTRAINT uq_tickets_caseNumber UNIQUE (caseNumber),
	CONSTRAINT uq_tickets_subscriberNumber UNIQUE (subscriberNumber),
	CONSTRAINT uq_tickets_uac UNIQUE (uac)
);

CREATE TABLE facilities(
	id SERIAL,
	subscriberNumber VARCHAR(25) NOT NULL,
	nodeAddress VARCHAR(50) NOT NULL,
	ipAddress VARCHAR(20) NOT NULL,
	nodeName VARCHAR(50) NOT NULL,
	creationTime TIMESTAMP NOT NULL DEFAULT NOW(),
	userName VARCHAR(256) NOT NULL DEFAULT current_user,
	CONSTRAINT pk_facilities_id PRIMARY KEY (id),
	CONSTRAINT fk_facilities_subscriberNumber FOREIGN KEY (subscriberNumber) REFERENCES tickets(subscriberNumber)
);

CREATE TABLE diagnostics(
	id SERIAL,
	facilityId INT NOT NULL,
	isConfigured BOOLEAN NOT NULL,
	oltAdminState BOOLEAN NOT NULL,
	oltOperState BOOLEAN NOT NULL,
	ontAdminState BOOLEAN NOT NULL,
	ontOperState BOOLEAN NOT NULL,
	ontRxPower BOOLEAN NOT NULL,
	ontTxPower BOOLEAN NOT NULL,
	ontVoltage BOOLEAN NOT NULL,
	creationTime TIMESTAMP NOT NULL DEFAULT NOW(),
	userName VARCHAR(256) NOT NULL DEFAULT current_user,
	CONSTRAINT pk_diagnostics_id PRIMARY KEY (id),
	CONSTRAINT fk_diagnostics_facilityId FOREIGN KEY (facilityId) REFERENCES facilities(id)
);

INSERT INTO processes (name,description,isActive) VALUES 
('DIAG-ADSL','Check configurations and params for tickets that have ADSL as a product in the diagnostic stage','1'),
('CONF-IPTV','Check configurations and params for tickets that have IPTV as a product in the confirmation stage','1');

INSERT INTO tickets (processName,productType,caseNumber,subscriberNumber,currentQueue,uac,clientName,clientContactPhone,status) VALUES 
('DIAG-ADSL','ADSL','356241','3545558888','ADSL FAULT DIAGNOSIS','243','Bailey_Davidson','4607295853','PENDING'),
('CONF-IPTV','IPTV','142653','3546661515','IPTV FAULT CONFIRMATION','789','Remi_Holmes','16967598718','PENDING');