CREATE TABLE [dbo].[Transports]
(
	[id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[user_id] INT NOT NULL,
	[driver_id] INT NOT NULL,
	[vehicle_id] INT NOT NULL,
	[customer_id] INT NOT NULL,
	[origin] NVARCHAR(50) NOT NULL,
	[destination] NVARCHAR(50) NOT NULL,
	[distance] INT NOT NULL,
	[status_id] INT NOT NULL,
	[start_date] DATETIME NOT NULL,
	[end_date] DATETIME NOT NULL,
	[income] DECIMAL(18, 2) NOT NULL,
	[cost] DECIMAL(18, 2) NOT NULL,
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