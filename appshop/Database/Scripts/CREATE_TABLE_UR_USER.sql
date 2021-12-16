-- =============================================
-- Author:      Passos, Roberto
-- Create Date: 16/12/2021
-- Description: Create Table User 
-- =============================================

DROP TABLE UR_USER

CREATE TABLE UR_USER (
	USER_ID uniqueidentifier NOT NULL PRIMARY KEY,
	NAME VARCHAR(200) NOT NULL,
	CPF CHAR(11) NOT NULL,
	BIRTHDATE DATE NOT NULL,
	GENDERID uniqueidentifier NOT NULL,
	PASSWORD VARCHAR(20) NULL,
	LOGIN VARCHAR(30) NULL,
	TOKEN CHAR(64) NULL,
	ADDRESSID uniqueidentifier NULL,
	CREDITCARDID uniqueidentifier NULL,
)
GO

ALTER TABLE UR_USER ADD CONSTRAINT FK_USER_GENDER
FOREIGN KEY(GENDERID) REFERENCES UR_GENDER(GENDER_ID)
GO

ALTER TABLE UR_USER ADD CONSTRAINT FK_USER_ADDRESS
FOREIGN KEY(ADDRESSID) REFERENCES UR_ADDRESS(ADDRESS_ID)
GO

ALTER TABLE UR_USER ADD CONSTRAINT FK_USER_CREDITCARD
FOREIGN KEY(CREDITCARDID) REFERENCES UR_CREDITCARD(CREDITCARD_ID)
GO

DECLARE @Feminino AS uniqueidentifier
SELECT @Feminino = [GENDER_ID] FROM UR_GENDER WHERE DESCRIPTION = 'Feminino'
PRINT @Feminino

DECLARE @Masculino AS uniqueidentifier
SELECT @Masculino = [GENDER_ID] FROM UR_GENDER WHERE DESCRIPTION = 'Masculino'
PRINT @Masculino

SELECT * FROM UR_CREDITCARD
SELECT * FROM UR_ADDRESS

INSERT INTO UR_USER VALUES(NEWID(),'ANDRE','27724980004','1985/12/14',@Masculino,'123123','ANDRE@IG.COM',NULL,'C7908780-0D38-4F1D-A8BB-407100144AF4','464C6B52-780C-4B16-95BA-C1E7FA7765C4')
INSERT INTO UR_USER VALUES(NEWID(),'TEREZA','08395456069','1982/12/29',@Feminino,'123456','TEREZA@IG.COM',NULL,'1EF09944-387E-460F-A554-9FC6A3808ECF','FC325684-1832-416E-B42D-614B2B73E3DA')
INSERT INTO UR_USER VALUES(NEWID(),'VANIA','51688063099','1967/11/01',@Feminino,NULL,NULL,NULL,'57DD8DD8-30DC-432D-B2D4-B74BAE685828','79C39BED-936C-427F-B8EC-AD2A97E888CC')
INSERT INTO UR_USER VALUES(NEWID(),'VALDIR','43022221070','1990/10/19',@Masculino,NULL,NULL,NULL,'2C8FBF0F-EF34-4D80-8377-D2E903524B59','274A1D31-7D64-4159-821F-B632243D1965')
GO

SELECT * 
FROM UR_USER U
JOIN UR_GENDER G ON G.GENDER_ID = U.GENDERID
JOIN UR_ADDRESS A ON A.ADDRESS_ID = U.ADDRESSID
JOIN UR_CREDITCARD C ON C.CREDITCARD_ID = U.CREDITCARDID
GO