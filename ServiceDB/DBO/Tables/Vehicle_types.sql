CREATE TABLE [dbo].[Vehicle_types]
(
	[id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[name] NVARCHAR(50),
	[max_load] INT
)
