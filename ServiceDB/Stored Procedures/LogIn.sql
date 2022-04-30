CREATE PROCEDURE [dbo].[LogIn]
	@Login NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [dbo].[Users].[id], [dbo].[Users].[login], [dbo].[Users].[salt] FROM [dbo].[Users] WHERE [dbo].[Users].[login] = @Login 
END
