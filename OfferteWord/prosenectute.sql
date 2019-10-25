
IF OBJECT_ID ('dbo.tabqqtr') IS NOT NULL
	DROP TABLE dbo.tabqqtr
GO

CREATE TABLE dbo.tabqqtr
	(
	tb_codqqtr VARCHAR (1) DEFAULT (' ') NOT NULL,
	tb_desqqtr VARCHAR (60) DEFAULT (' ') NOT NULL,
	CONSTRAINT tabqqtr_PrimaryKey PRIMARY KEY (tb_codqqtr)
	)
GO



INSERT INTO dbo.tabqqtr (tb_codqqtr, tb_desqqtr)
VALUES ('M', 'Mittente')
GO

INSERT INTO dbo.tabqqtr (tb_codqqtr, tb_desqqtr)
VALUES ('D', 'Destinatario')
GO

INSERT INTO dbo.tabqqtr (tb_codqqtr, tb_desqqtr)
VALUES ('V', 'Vettore')
GO

ALTER TABLE movord ADD mo_hhoffalt VARCHAR(1) DEFAULT 'N' NULL
GO
 
UPDATE movord SET mo_hhoffalt = 'N' 
GO
