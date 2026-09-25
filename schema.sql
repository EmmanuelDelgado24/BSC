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
CREATE TABLE [clientes] (
    [id_cliente] bigint NOT NULL IDENTITY,
    [nombre_cliente] nvarchar(50) NOT NULL,
    [activo] bit NOT NULL,
    CONSTRAINT [PK_clientes] PRIMARY KEY ([id_cliente])
);

CREATE TABLE [departamentos] (
    [id_departamento] bigint NOT NULL IDENTITY,
    [nombre_departamento] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_departamentos] PRIMARY KEY ([id_departamento])
);

CREATE TABLE [productos] (
    [id_producto] bigint NOT NULL IDENTITY,
    [clave_producto] varchar(50) NOT NULL,
    [nombre_producto] nvarchar(50) NOT NULL,
    [existencia] int NOT NULL,
    [activo] bit NOT NULL,
    CONSTRAINT [PK_productos] PRIMARY KEY ([id_producto])
);

CREATE TABLE [empleados] (
    [id_empleado] bigint NOT NULL IDENTITY,
    [nombre_empleado] nvarchar(150) NOT NULL,
    [activo] bit NOT NULL,
    [id_departamento] bigint NOT NULL,
    [application_user_id] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_empleados] PRIMARY KEY ([id_empleado]),
    CONSTRAINT [FK_empleados_departamentos_id_departamento] FOREIGN KEY ([id_departamento]) REFERENCES [departamentos] ([id_departamento]) ON DELETE CASCADE
);

CREATE TABLE [pedidos] (
    [id_pedido] bigint NOT NULL IDENTITY,
    [id_cliente] bigint NOT NULL,
    [id_empleado] bigint NOT NULL,
    [Fecha] datetime2 NOT NULL,
    [Estado] tinyint NOT NULL,
    CONSTRAINT [PK_pedidos] PRIMARY KEY ([id_pedido]),
    CONSTRAINT [FK_pedidos_clientes_id_cliente] FOREIGN KEY ([id_cliente]) REFERENCES [clientes] ([id_cliente]) ON DELETE NO ACTION,
    CONSTRAINT [FK_pedidos_empleados_id_empleado] FOREIGN KEY ([id_empleado]) REFERENCES [empleados] ([id_empleado]) ON DELETE NO ACTION
);

CREATE TABLE [pedidos_detalles] (
    [id_pedido_detalle] bigint NOT NULL IDENTITY,
    [IdPedido] bigint NOT NULL,
    [IdProducto] bigint NOT NULL,
    [cantidad] int NOT NULL,
    CONSTRAINT [PK_pedidos_detalles] PRIMARY KEY ([id_pedido_detalle]),
    CONSTRAINT [FK_pedidos_detalles_pedidos_IdPedido] FOREIGN KEY ([IdPedido]) REFERENCES [pedidos] ([id_pedido]) ON DELETE CASCADE,
    CONSTRAINT [FK_pedidos_detalles_productos_IdProducto] FOREIGN KEY ([IdProducto]) REFERENCES [productos] ([id_producto]) ON DELETE CASCADE
);

CREATE INDEX [IX_empleados_id_departamento] ON [empleados] ([id_departamento]);

CREATE INDEX [IX_pedidos_id_cliente] ON [pedidos] ([id_cliente]);

CREATE INDEX [IX_pedidos_id_empleado] ON [pedidos] ([id_empleado]);

CREATE INDEX [IX_pedidos_detalles_IdPedido] ON [pedidos_detalles] ([IdPedido]);

CREATE INDEX [IX_pedidos_detalles_IdProducto] ON [pedidos_detalles] ([IdProducto]);

CREATE UNIQUE INDEX [IX_productos_clave_producto] ON [productos] ([clave_producto]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260923185948_InitialCreate', N'10.0.12');

COMMIT;
GO

BEGIN TRANSACTION;
EXEC sp_rename N'[empleados].[application_user_id]', N'ApplicationUserId', 'COLUMN';

DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[empleados]') AND [c].[name] = N'ApplicationUserId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [empleados] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [empleados] ALTER COLUMN [ApplicationUserId] nvarchar(450) NULL;

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_empleados_ApplicationUserId] ON [empleados] ([ApplicationUserId]) WHERE [ApplicationUserId] IS NOT NULL;

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

ALTER TABLE [empleados] ADD CONSTRAINT [FK_empleados_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE SET NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260923195034_AddIdentity', N'10.0.12');

COMMIT;
GO

