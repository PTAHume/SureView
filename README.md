The Following SQL will create two tables in its MS SQL Database:  
● “Device” which contains a list of security devices and their connection details, with columns:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; “DeviceID” (primary key), “IPAddress”, “Name”  
● “DeviceCamera” which contains a list of cameras connected to each device, with columns:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; “DeviceID” (foreign key), “CameraNumber”, “Name”   
For example, there may be a Device with two cameras on it, number 1 called “Front Door” and number 2 called “Back door”.  

Then run the following queries to achieve the following:
1. Get the camera details of all the cameras on device ID 1
2. Get the device details of all devices which have a camera (exclude any that don’t)
3. Get the device details of all devices which have a camera name starting with “Front”
4. Change the name of the 2nd Camera of the device with a DeviceID of 1984 to “Front Door Camera”


```
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Device')
BEGIN
    -- Create Device table
    CREATE TABLE Device (
        [DeviceID] INT NOT NULL,
        [IPAddress] VARCHAR(255),
        [Name] VARCHAR(255)
		CONSTRAINT [PK_Device_Id] PRIMARY KEY (DeviceID)
    );
END;

-- Check if DeviceCamera table exists
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'DeviceCamera')
BEGIN
    -- Create DeviceCamera table
    CREATE TABLE DeviceCamera (
        [DeviceID] INT,
        [CameraNumber] INT,
        [Name] VARCHAR(255),
		CONSTRAINT FK_DeviceID FOREIGN KEY (DeviceID)
		REFERENCES Device(DeviceID)
    );
END;

-- Insert dummy data into the Device table
INSERT INTO Device (DeviceID, IPAddress, [Name])
VALUES
    (1984, '192.168.1.10', 'Device 1984'),
    (1, '192.168.1.20', 'Device 1'),
    (2, '192.168.1.30', 'Device 2'),
    (3, '192.168.1.40', 'Device 3'),
	(4, '192.168.1.50', 'Device 4');

-- Insert dummy data into the DeviceCamera table
INSERT INTO DeviceCamera (DeviceID, CameraNumber, Name)
VALUES
    (1984, 2, 'Front Door'),
    (1, 3, 'Back Door'),
    (2, 4, 'Camera 1'),
    (3, 5, 'Camera 2'),
    (4, 7, 'Front Camera');

--SELECT * FROM Device WITH (NOLOCK) 

-- Query 1: Get camera details of all cameras on Device ID 1
SELECT * FROM DeviceCamera WITH (NOLOCK) WHERE DeviceID = 1;

-- Query 2: Get device details of all devices which have a camera
SELECT Device.* FROM Device WITH (NOLOCK)
INNER JOIN DeviceCamera WITH (NOLOCK) ON Device.DeviceID = DeviceCamera.DeviceID;

-- Query 3: Get device details of all devices which have a camera name starting with "Front"
SELECT Device.* FROM Device WITH (NOLOCK)
INNER JOIN DeviceCamera  WITH (NOLOCK) ON Device.DeviceID = DeviceCamera.DeviceID
WHERE DeviceCamera.[Name] LIKE 'Front%';

-- Query 4: Change the name of the 2nd Camera of the device with a DeviceID of 1984 to "Front Door Camera"
UPDATE DeviceCamera SET [Name] = 'Front Door Camera' WHERE DeviceID = 1984 AND CameraNumber = 2;
SELECT * FROM DeviceCamera WITH (NOLOCK) WHERE DeviceID = 1984 AND CameraNumber = 2;

DROP TABLE DeviceCamera
DROP TABLE  Device
```
