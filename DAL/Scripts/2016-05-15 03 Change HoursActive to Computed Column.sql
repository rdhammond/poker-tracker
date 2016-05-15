USE [PokerTracker]
GO

IF EXISTS(SELECT 1 FROM sys.columns WHERE name = 'HoursActive' AND object_id = OBJECT_ID('[dbo].[Sessions]') AND is_computed = 0)
BEGIN
DECLARE @Msg NVARCHAR(MAX) = 'HoursActive already exists as a non-computed column. (You probably missed an update script somewhere';
RAISERROR(16, 1, @Msg)
END
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'HoursActive' AND TABLE_NAME = 'Sessions')
ALTER TABLE [dbo].[Sessions]
ADD [HoursActive] AS (DATEDIFF(minute, [StartTime], [EndTime]) / 60.0 * [PercentOfTimePlayed])
GO

INSERT INTO [dbo].[SchemaLastUpdated](Id, LastUpdateDate, LastScriptFile)
VALUES(NEWID(), CURRENT_TIMESTAMP, '2016-05-15 03 Change HoursActive to Computed Column')
GO