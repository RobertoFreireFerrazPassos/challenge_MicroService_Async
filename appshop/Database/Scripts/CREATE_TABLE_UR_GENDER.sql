-- =============================================
-- Author:      Passos, Roberto
-- Create Date: 16/12/2021
-- Description: Create Table Gender
-- =============================================

CREATE TABLE UR_GENDER (
	GENDER_ID uniqueidentifier NOT NULL PRIMARY KEY,
	DESCRIPTION VARCHAR(15) NOT NULL,
)
GO

INSERT INTO UR_GENDER VALUES(NEWID(),'Feminino')
INSERT INTO UR_GENDER VALUES(NEWID(),'Masculino')

SELECT * FROM UR_GENDER
GO