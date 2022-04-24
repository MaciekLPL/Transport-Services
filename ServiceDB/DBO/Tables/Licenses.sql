CREATE TABLE [dbo].[Licenses]
(
	[id] INT NOT NULL PRIMARY KEY,
	[driver_id] INT NOT NULL, 
	[vehicle_type_id] INT NOT NULL,

	CONSTRAINT FK_driver_licences_id FOREIGN KEY (driver_id)
        REFERENCES Drivers (id),
	CONSTRAINT FK_vehicle_type_id FOREIGN KEY (vehicle_type_id)
		REFERENCES Vehicle_types (id)
)
