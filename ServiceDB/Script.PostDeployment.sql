IF NOT EXISTS (SELECT 1 FROM dbo.[Users])
BEGIN
	INSERT INTO dbo.[Users] ([login], [sha256], [salt], [type]) VALUES ('admin', 'af1aaacae482a4155fa69122e50a7fb8c27f18a8c782c7037733af2c20721914','digO7bgwzaEQl4v8WX0A8Y4q7a1sByXbvD2HtDD1l7Q=', 1);
	INSERT INTO Status ([name]) VALUES ('active'), ('finished'), ('canceled');
END