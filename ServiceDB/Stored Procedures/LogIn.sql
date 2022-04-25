CREATE PROCEDURE [dbo].[LogIn]
	@Login NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [dbo].[Users].[id], [dbo].[Users].[login] FROM [dbo].[Users] WHERE [dbo].[Users].[login] = @Login AND [dbo].[Users].[password] = @Password
END
