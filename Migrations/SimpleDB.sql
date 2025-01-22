IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Users] (
    [RowId] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [PhoneCode] nvarchar(max) NOT NULL,
    [LastLogin] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([RowId])
);

CREATE TABLE [Jobs] (
    [RowId] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [Completed] datetime2 NOT NULL,
    [Cost] time NOT NULL,
    [User] bigint NOT NULL,
    CONSTRAINT [PK_Jobs] PRIMARY KEY ([RowId]),
    CONSTRAINT [FK_Jobs_Users] FOREIGN KEY ([User]) REFERENCES [Users] ([RowId])
);

CREATE INDEX [IX_Jobs_User] ON [Jobs] ([User]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250121220149_Init', N'9.0.1');

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Name', N'Phone', N'PhoneCode') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Name], [Phone], [PhoneCode])
VALUES (N'TestUser', N'+1234567', N'1111');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Name', N'Phone', N'PhoneCode') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250122085604_testuser', N'9.0.1');

COMMIT;
GO

