USE [PokerTracker]
GO

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'vw_TotalHourlyRate')
DROP VIEW [dbo].[vw_TotalHourlyRate]
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'vw_Statistics')
EXEC sp_executesql N'CREATE VIEW [dbo].[vw_Statistics]
AS
WITH PerSessionRates AS
(
	SELECT SUM(te.StackDifferential / s.BigBlind) / s.HoursActive AS HourlyRate
	FROM [dbo].[Sessions] s
	INNER JOIN [dbo].[TimeEntries] te ON s.Id = te.SessionId
	WHERE s.HoursActive > 0
	AND te.StackDifferential IS NOT NULL
	GROUP BY s.HoursActive
)
SELECT
	CAST(COALESCE(AVG(HourlyRate), 0) AS DECIMAL(8,2)) As AvgHourlyRatePerSession,
	CAST(COALESCE(VAR(HourlyRate), 0) AS DECIMAL(8,2)) As HourlyRateVariance,
	CAST(COALESCE(STDEV(HourlyRate), 0) AS DECIMAL(8,2)) AS HourlyRateStdDev,
	(
		SELECT CAST(COALESCE(SUM(te.StackDifferential / s.BigBlind) / SUM(s.HoursActive), 0) AS DECIMAL(8,2))
		FROM [dbo].[Sessions] s
		INNER JOIN [dbo].[TimeEntries] te ON s.Id = te.SessionId
		WHERE s.HoursActive > 0
		AND te.StackDifferential IS NOT NULL
	) As TotalHourlyRate,
	(SELECT CAST(COALESCE(SUM(HoursActive), 0) AS DECIMAL(8,2)) FROM [dbo].[Sessions]) AS TotalHoursPlayed
FROM
	PerSessionRates'
GO

INSERT INTO [dbo].[SchemaLastUpdated]([Id], [LastUpdateDate], [LastScriptFile])
VALUES(NEWID(), CURRENT_TIMESTAMP, '2016-05-15 04 Add Statistics View.sql')
GO