CREATE TABLE [dbo].[Users]
(
	[id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[login] NVARCHAR(30),
	[password] NVARCHAR(30),
	[type] INT

)
