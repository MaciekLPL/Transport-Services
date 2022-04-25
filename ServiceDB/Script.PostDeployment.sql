IF NOT EXISTS (SELECT 1 FROM dbo.[Users])
BEGIN
	INSERT INTO dbo.[Users] ([login], [password], [type]) VALUES ('admin', 'admin', 1), ('user', 'user', 0);
END