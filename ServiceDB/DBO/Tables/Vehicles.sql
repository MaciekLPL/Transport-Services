CREATE TABLE [dbo].[Vehicles]
(
	[id] INT NOT NULL PRIMARY KEY,
	[make] NVARCHAR(50) NOT NULL,
	[model] NVARCHAR(50) NOT NULL,
	[type_id] INT NOT NULL,
	[reistration] NVARCHAR(50) NOT NULL,
	[avg_fuel_consumption] FLOAT NOT NULL
	CONSTRAINT FK_type_id FOREIGN KEY (type_id)
        REFERENCES Vehicle_types (id)
)
