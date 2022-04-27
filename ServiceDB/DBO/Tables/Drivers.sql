﻿CREATE TABLE [dbo].[Drivers]
(
	[id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[name] NVARCHAR(50) NOT NULL,
	[surname] NVARCHAR(50) NOT NULL,
	[age] INT NOT NULL,
	[hourly_rate] DECIMAL(18, 2) NOT NULL,
)
