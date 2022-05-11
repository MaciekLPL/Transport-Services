CREATE TABLE [dbo].[Vehicle_types]
(
	[id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[name] NVARCHAR(50) NOT NULL,
	[max_load] INT NOT NULL
)
