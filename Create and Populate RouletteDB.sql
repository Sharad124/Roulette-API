Create Database RouletteDB

Create table Users
(
	UserID INT PRIMARY KEY,
	FirstName VARCHAR(50),
	LastName VARCHAR(50)
)

Create table BetTypes
(
	BetTypeID INT PRIMARY KEY,
	[Name] VARCHAR(50)
)

Create table Bets
(
	BetID INT PRIMARY KEY,
	BetAmount DECIMAL,
	BetItem VARCHAR(50),
	BetTypeID INT FOREIGN KEY REFERENCES BetTypes(BetTypeID),
	UserID INT FOREIGN KEY REFERENCES Users(UserID),
	PotentialPayout DECIMAL,
	isActive BIT
)

--Inserts for users
INSERT INTO Users Values 
(1, 'Sharad', 'Ramsundar')

INSERT INTO Users Values 
(2, 'Bob', 'Hope')

INSERT INTO Users Values 
(3, 'Harry', 'Potter')

--Inserts for BetTypes
INSERT INTO BetTypes Values
(1, 'Number')

INSERT INTO BetTypes Values
(2, 'Colour')

INSERT INTO BetTypes Values
(3, 'Even')

INSERT INTO BetTypes Values
(4, 'Odd')

INSERT INTO BetTypes Values
(5, 'Number Range')

--Inserts for Bets
INSERT INTO Bets Values
(1, 500, 'Number', 1, 1, 1000, 1)

INSERT INTO Bets Values
(2, 500, 'Colour', 2, 1, 1500, 1)

INSERT INTO Bets Values
(3, 900, 'Even', 3, 3, 3600, 1)

INSERT INTO Bets Values
(4, 500, 'Odd', 4, 2, 2000, 1)

INSERT INTO Bets Values
(5, 500, 'Number Range', 5, 3, 2500, 1)