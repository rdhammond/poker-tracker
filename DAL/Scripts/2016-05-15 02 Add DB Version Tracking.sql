USE [PokerTracker]
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SchemaLastUpdated')
CREATE TABLE [dbo].[SchemaLastUpdated]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[LastUpdateDate] DATETIME NOT NULL,
	[LastScriptFile] VARCHAR(256) NOT NULL
)
GO

IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name = 'IX_LastUpdateDate' AND object_id = OBJECT_ID('[dbo].[SchemaLastUpdated]'))
CREATE INDEX [IX_LastUpdateDate]
ON [dbo].[SchemaLastUpdated]([LastUpdateDate] DESC)
GO

IF NOT EXISTS(SELECT 1 FROM sys.views WHERE name = 'vw_LatestUpdate')
EXEC sp_executesql N'CREATE VIEW [dbo].[vw_LatestUpdate]
AS
SELECT TOP 1 * FROM [dbo].[SchemaLastUpdated] ORDER BY [LastUpdateDate] DESC'
GO

INSERT INTO [dbo].[SchemaLastUpdated](Id, LastUpdateDate, LastScriptFile)
VALUES(NEWID(), CURRENT_TIMESTAMP, '2016-05-15 02 Add DB Version Tracking.sql')
GO