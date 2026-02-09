USE MotorinDB
GO

CREATE SCHEMA CoreSchema
GO

CREATE TABLE CoreSchema.Users
(
    UserId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Active BIT
)
GO

CREATE Table CoreSchema.Collections
(
    CollectionId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    CollectionType NVARCHAR(50) NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(255) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedAt DATETIME2 NULL,
    FOREIGN KEY (UserId) REFERENCES CoreSchema.Users(UserId) ON DELETE CASCADE,
    INDEX IX_Collections_UserId (UserId),
    INDEX IX_Collections_CollectionType (CollectionType),
    CHECK (CollectionType IN ('HotWheels', 'ThomasTheTankEngine'))
    --Add new Types Here
)
GO

-- ============================================
-- HOT WHEELS CARS
-- ============================================
CREATE TABLE HotWheelsCars
(
    HotWheelsCarId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CollectionId UNIQUEIDENTIFIER NOT NULL,

    -- Product Line Info
    ProductLine NVARCHAR(50) NOT NULL,
    -- 'Mainline', 'BlackLabel', 'Premium', 'RedLineClub'

    -- Car Details
    ModelName NVARCHAR(200) NOT NULL,
    ToyNumber NVARCHAR(100) NULL,
    -- e.g., 'JJJ46'
    SeriesName NVARCHAR(200) NULL,
    -- e.g., 'HW City 2024'
    SeriesNumber Char(20) NULL,
    -- e.g., '5/10'
    CastingName NVARCHAR(200) NULL,
    -- e.g., 'Bone Shaker'
    ColorVariant NVARCHAR(100) NULL,
    TampoDesign NVARCHAR(500) NULL,
    -- Graphics/livery description
    TreasureHunt BIT NOT NULL DEFAULT 0,
    SuperTreasureHunt BIT NOT NULL DEFAULT 0,

    -- Manufacturing Info
    ManufactureYear INT NULL,
    CountryOfOrigin NVARCHAR(100) NULL,

    -- Condition & Packaging
    Condition NVARCHAR(50) NULL,
    -- 'Mint', 'Good', 'Fair', 'Poor'
    IsInPackage BIT NOT NULL DEFAULT 1,
    PackageCondition NVARCHAR(50) NULL,
    -- For carded collectors

    -- Collector Info
    AcquisitionDate DATE NULL,
    PurchasePrice DECIMAL(10,2) NULL,
    EstimatedValue DECIMAL(10,2) NULL,
    Notes NVARCHAR(500) NULL,

    -- Media
    ImageUrl NVARCHAR(500) NULL,

    -- Metadata
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedAt DATETIME2 NULL,

    FOREIGN KEY (CollectionId) REFERENCES CoreSchema.Collections(CollectionId) ON DELETE CASCADE,
    INDEX IX_HotWheelsCars_CollectionId (CollectionId),
    INDEX IX_HotWheelsCars_ProductLine (ProductLine),
    INDEX IX_HotWheelsCars_ManufactureYear (ManufactureYear),
    CHECK (ProductLine IN ('Mainline', 'BlackLabel', 'Premium', 'RedLineClub', 'Treasure Hunt'))
);
GO