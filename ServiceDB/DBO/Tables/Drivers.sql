CREATE TABLE [dbo].[Drivers]
(
	[id] INT NOT NULL PRIMARY KEY,
	[name] NVARCHAR(50) NOT NULL,
	[surname] NVARCHAR(50) NOT NULL,
	[age] INT NOT NULL,
	[hourly_rate] DECIMAL NOT NULL,
)
