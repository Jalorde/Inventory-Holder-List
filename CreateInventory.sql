USE [BISInventoryCOMP235]

Create TABLE Condition(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
[Description] varchar(255))

CREATE TABLE Inventory(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
SerialNo varchar(255) NULL,
ItemName varchar(255) NULL,
Purchased Date NULL,
ImagePath varchar(255) NULL,
Condition int REFERENCES Condition(Id))

INSERT INTO Condition ([Description]) VALUES ('New');
INSERT INTO Condition ([Description]) VALUES ('Good');
INSERT INTO Condition ([Description]) VALUES ('Fair');
INSERT INTO Condition ([Description]) VALUES ('Needs Replacement');

INSERT INTO Inventory (SerialNo, ItemName, Purchased, ImagePath, Condition) VALUES ('8GF2D4B6A9C2', 'Meta Quest 2', '2020-11-30', '/Images/metaquest2.jpg', 2);
INSERT INTO Inventory (SerialNo, ItemName, Purchased, ImagePath, Condition) VALUES ('2E7A1FD2B1D7', 'Microsoft Hololense', '2021-06-30', '/Images/hololense2.jpg', 1);