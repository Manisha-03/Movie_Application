CREATE TABLE Country (
    CountryId INT IDENTITY(1,1) PRIMARY KEY,
    Country VARCHAR(50) NULL,
    LastUpdate DATETIME NULL
);
CREATE TABLE City (
    CityId INT IDENTITY(1,1) PRIMARY KEY,
    City VARCHAR(50) NULL,
    CountryId INT NULL,
    LastUpdate DATETIME NULL,
    FOREIGN KEY (CountryId) REFERENCES Country(CountryId)
);
CREATE TABLE Address (
    AddressId INT IDENTITY(1,1) PRIMARY KEY,
    Address VARCHAR(50) NULL,
    Address2 VARCHAR(50) NULL,
    District VARCHAR(20) NULL,
    CityId INT NULL,
    PostalCode VARCHAR(10) NULL,
    Phone VARCHAR(20) NULL,
    LastUpdate DATETIME NULL,
    FOREIGN KEY (CityId) REFERENCES City(CityId)
);
CREATE TABLE Customer (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    StoreId INT NULL,
    FirstName VARCHAR(45) NULL,
    LastName VARCHAR(45) NULL,
    Email VARCHAR(50) NULL,
    AddressId INT NULL,
    Active BIT NULL,
    CreateDate DATETIME NULL,
    LastUpdate DATETIME NULL,
    FOREIGN KEY (AddressId) REFERENCES Address(AddressId)
);
CREATE TABLE Store (
    StoreId INT IDENTITY(1,1) PRIMARY KEY,
    ManagerStaffId INT NULL,
    AddressId INT NULL,
    LastUpdate DATETIME NULL,
);
CREATE TABLE Staff (
    StaffId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(45) NULL,
    LastName VARCHAR(45) NULL,
    AddressId INT NULL,
    Picture VARCHAR(MAX) NULL,
    Email VARCHAR(50) NULL,
    StoreId INT NULL,
    Active BIT NULL,
    UserName VARCHAR(16) NULL,
    Password VARCHAR(40) NULL,
    LastUpdate DATETIME NULL,
);

CREATE TABLE Rental (
    RentalId INT IDENTITY(1,1) PRIMARY KEY,
    RentalDate DATETIME NULL,
    InventoryId INT NULL,
    CustomerId INT NULL,
    ReturnDate DATETIME NULL,
    StaffId INT NULL,
    LastUpdate DATETIME NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
    FOREIGN KEY (StaffId) REFERENCES Staff(StaffId)
);
CREATE TABLE Payment (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NULL,
    StaffId INT NULL,
    RentalId INT NULL,
    Amount DECIMAL(5,2) NULL,
    PaymentDate DATETIME NULL,
    LastUpdate DATETIME NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
    FOREIGN KEY (StaffId) REFERENCES Staff(StaffId),
    FOREIGN KEY (RentalId) REFERENCES Rental(RentalId)
);

CREATE TABLE Language (
    LanguageId INT PRIMARY KEY,  
    Name VARCHAR(50) NULL,
    LastUpdate DATETIME NULL
);
CREATE TABLE Film (
    FilmId INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(255) NULL,
    Description VARCHAR(MAX) NULL,
    ReleaseYear INT NULL,
    LanguageId INT NULL,
    RentalDuration INT NULL,
    RentalRate DECIMAL(5,2) NULL,
    Length INT NULL,
    ReplacementCost DECIMAL(5,2) NULL,
    Rating VARCHAR(10) NULL,
    SpecialFeatures VARCHAR(50) NULL,
    LastUpdate DATETIME NULL,
    FOREIGN KEY (LanguageId) REFERENCES Language(LanguageId)
);
CREATE TABLE Actor (
    ActorId INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(45) NULL,
    LastName VARCHAR(45) NULL,
    LastUpdate DATETIME NULL
);
CREATE TABLE FilmActor (
    FilmId INT NOT NULL,
    ActorId INT NOT NULL,
    LastUpdate DATETIME NULL,
    PRIMARY KEY (FilmId, ActorId),
    FOREIGN KEY (FilmId) REFERENCES Film(FilmId),
    FOREIGN KEY (ActorId) REFERENCES Actor(ActorId)
);
CREATE TABLE Inventory (
    InventoryId INT IDENTITY(1,1) PRIMARY KEY,
    FilmId INT NULL,
    StoreId INT NULL,
    LastUpdate DATETIME NULL,
    FOREIGN KEY (FilmId) REFERENCES Film(FilmId),
    FOREIGN KEY (StoreId) REFERENCES Store(StoreId)
);
CREATE TABLE Category (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(25) NULL,
    LastUpdate DATETIME NULL
);
CREATE TABLE FilmCategory (
    FilmId INT NULL,
    CategoryId INT NULL,
    LastUpdate DATETIME NULL,
    FOREIGN KEY (FilmId) REFERENCES Film(FilmId),
    FOREIGN KEY (FilmId) REFERENCES Film(FilmId),
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);


-- Add foreign key from Staff to Store
ALTER TABLE Staff
ADD FOREIGN KEY (StoreId) REFERENCES Store(StoreId);

-- Add foreign key from Store to Staff (for ManagerStaffId)
ALTER TABLE Store
ADD FOREIGN KEY (ManagerStaffId) REFERENCES Staff(StaffId);

-- Add foreign key from Staff to Address
ALTER TABLE Staff
ADD FOREIGN KEY (AddressId) REFERENCES Address(AddressId);

-- Add foreign key from Store to Address
ALTER TABLE Store
ADD FOREIGN KEY (AddressId) REFERENCES Address(AddressId);


SELECT * FROM Country;
SELECT * FROM City;
SELECT * FROM Address;
SELECT * FROM Customer;
SELECT * FROM Staff;
SELECT * FROM Store;
SELECT * FROM Payment;
SELECT * FROM Rental;
SELECT * FROM Film;
SELECT * FROM FilmActor;
SELECT * FROM FilmCategory;
SELECT * FROM Category;
SELECT * FROM Actor;
SELECT * FROM Language;
SELECT * FROM Inventory;


