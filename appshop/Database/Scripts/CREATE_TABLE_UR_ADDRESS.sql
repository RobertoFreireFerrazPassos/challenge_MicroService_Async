-- =============================================
-- Author:      Passos, Roberto
-- Create Date: 16/12/2021
-- Description: Create Table Address
-- =============================================

CREATE TABLE UR_ADDRESS (
	ADDRESS_ID uniqueidentifier NOT NULL PRIMARY KEY,
	STREET VARCHAR(20) NOT NULL,
	NUMBER SMALLINT NOT NULL,
	ADDITIONALINFO VARCHAR(20) NOT NULL,
	CITY VARCHAR(20) NOT NULL,
	STATE VARCHAR(20) NOT NULL,
	COUNTRY VARCHAR(20) NOT NULL,
)
GO

INSERT INTO UR_ADDRESS VALUES(NEWID(),'Woodmoss Dr',4514,'','Fairfield','Ohio','US')
INSERT INTO UR_ADDRESS VALUES(NEWID(),'Ottawa River Cir',8717,'','Fountain Valley','CA','US')
INSERT INTO UR_ADDRESS VALUES(NEWID(),'Meadowbrook Dr',278,'','Bloomington','Indiana','US')
INSERT INTO UR_ADDRESS VALUES(NEWID(),'Bent Pine Dr',636,'','Allen','Texas','US')

SELECT * FROM UR_ADDRESS
GO