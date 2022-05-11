CREATE TABLE [dbo].[Vehicles]
(
	[id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[make] NVARCHAR(50) NOT NULL,
	[model] NVARCHAR(50) NOT NULL,
	[type_id] INT NOT NULL,
	[registration] NVARCHAR(50) NOT NULL,
	[avg_fuel_consumption] DECIMAL(18, 1) NOT NULL
	CONSTRAINT FK_type_id FOREIGN KEY (type_id)
        REFERENCES Vehicle_types (id)
)
