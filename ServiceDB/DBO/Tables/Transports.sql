CREATE TABLE [dbo].[Transports]
(
	[id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[user_id] INT,
	[driver_id] INT,
	[vehicle_id] INT,
	[customer_id] INT,
	[origin] NVARCHAR(50),
	[destination] NVARCHAR(50),
	[distance] INT,
	[status_id] INT,
	[start_date] DATETIME,
	[end_date] DATETIME,
	[income] DECIMAL,
	[cost] DECIMAL,
	CONSTRAINT FK_user_id FOREIGN KEY (user_id)
        REFERENCES Users (id),
	CONSTRAINT FK_driver_id FOREIGN KEY (driver_id)
        REFERENCES Drivers (id),
	CONSTRAINT FK_vehicle_id FOREIGN KEY (vehicle_id)
        REFERENCES Vehicles (id),
	CONSTRAINT FK_customer_id FOREIGN KEY (customer_id)
        REFERENCES Customers (id),
	CONSTRAINT FK_status_id FOREIGN KEY (status_id)
        REFERENCES Status (id)
)