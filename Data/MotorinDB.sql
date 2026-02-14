USE MotorinDB
GO

CREATE SCHEMA CoreSchema
GO

CREATE TABLE CoreSchema.Users
(
    UserId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserName NVARCHAR(255) NOT NULL,
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
-- MASTER CATALOG (Shared reference data)
-- ============================================
CREATE TABLE CoreSchema.HotWheelsMasterCatalog
(
    CatalogId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),

    -- Product Line Info
    ProductLine NVARCHAR(50) NOT NULL,

    -- Car Details (what makes this car unique)
    ModelName NVARCHAR(200) NOT NULL,
    -- e.g., 'Bone Shaker'
    ToyNumber NVARCHAR(100) NULL,
    -- e.g., 'JJJ46-N9COB'
    SeriesName NVARCHAR(200) NULL,
    -- e.g., 'HW City 2024'
    SeriesNumber Char(20) NULL,
    -- e.g., '5/10'
    CastingName NVARCHAR(200) NULL,
    -- e.g., 'physical mold used: Bone Shaker'
    ColorVariant NVARCHAR(100) NULL,
    TampoDesign NVARCHAR(500) NULL,
    -- Graphics/livery description
    TreasureHunt BIT NOT NULL DEFAULT 0,
    SuperTreasureHunt BIT NOT NULL DEFAULT 0,

    -- Manufacturing Info
    ManufactureYear INT NULL,

    -- Reference Info
    ProductNumber NVARCHAR(100) NULL,
    -- SKU/UPC
    OfficialImageUrl NVARCHAR(500) NULL,
    Description NVARCHAR(MAX) NULL,

    -- Metadata
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    INDEX IX_HotWheelsMasterCatalog_ProductLine (ProductLine),
    INDEX IX_HotWheelsMasterCatalog_ModelName (ModelName),
    INDEX IX_HotWheelsMasterCatalog_ManufactureYear (ManufactureYear),
    INDEX IX_HotWheelsMasterCatalog_TreasureHunt (TreasureHunt),
    INDEX IX_HotWheelsMasterCatalog_SuperTreasureHunt (SuperTreasureHunt)
);
GO

SELECT
    c.name AS ColumnName,
    dc.name AS DefaultConstraintName,
    dc.definition AS DefaultValue
FROM sys.default_constraints dc
    INNER JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
WHERE dc.parent_object_id = OBJECT_ID('CoreSchema.HotWheelsMasterCatalog')
    AND c.name IN ('CatalogId', 'CreatedAt', 'UpdatedAt');
    GO
-- ============================================
-- USER'S COLLECTION ITEMS (Many-to-Many join)
-- ============================================
CREATE TABLE CoreSchema.CollectionItems
(
    CollectionItemId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    CollectionId UNIQUEIDENTIFIER NOT NULL,
    CatalogId UNIQUEIDENTIFIER NOT NULL,
    -- References the master catalog

    -- USER-SPECIFIC data (condition, price, notes)
    Condition NVARCHAR(50) NULL,
    IsInPackage BIT NOT NULL DEFAULT 1,
    PackageCondition NVARCHAR(50) NULL,

    AcquisitionDate DATE NULL,
    PurchasePrice DECIMAL(10,2) NULL,
    EstimatedValue DECIMAL(10,2) NULL,
    UserNotes NVARCHAR(500) NULL,
    UserImageUrl NVARCHAR(500) NULL,
    -- User's personal photo

    Quantity INT NOT NULL DEFAULT 1,
    -- If they own multiple of the same car

    -- Metadata
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedAt DATETIME2 NULL,

    FOREIGN KEY (CollectionId) REFERENCES CoreSchema.Collections(CollectionId) ON DELETE CASCADE,
    FOREIGN KEY (CatalogId) REFERENCES CoreSchema.HotWheelsMasterCatalog(CatalogId),

    INDEX IX_CollectionItems_CollectionId (CollectionId),
    INDEX IX_CollectionItems_CatalogId (CatalogId),

    -- Prevent duplicate entries in same collection
    UNIQUE (CollectionId, CatalogId)
);
GO