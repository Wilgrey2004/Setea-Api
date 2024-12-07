CREATE DATABASE SistemaGestion;
GO

USE SistemaGestion;
GO

-- Tabla de Roles
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    Contraseña NVARCHAR(200) NOT NULL,
    RolId INT NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RolId) REFERENCES Roles(Id) ON DELETE CASCADE
);

-- Tabla de Métodos de Pago
CREATE TABLE MetodosPago (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Metodo NVARCHAR(50) NOT NULL UNIQUE
);

-- Tabla de Clientes
CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) UNIQUE,
    Telefono NVARCHAR(20),
    Clasificacion NVARCHAR(50),
    FechaRegistro DATETIME DEFAULT GETDATE()
);

-- Tabla de Categorías
CREATE TABLE Categorias (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);

-- Tabla de Productos
CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Precio DECIMAL(18,2) NOT NULL CHECK (Precio >= 0),
    CategoriaId INT NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id) ON DELETE CASCADE
);

-- Tabla de Inventario
CREATE TABLE Inventario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductoId INT NOT NULL UNIQUE,
    Cantidad INT NOT NULL CHECK (Cantidad >= 0),
    StockMinimo INT NOT NULL CHECK (StockMinimo >= 0),
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id) ON DELETE CASCADE
);

-- Tabla de Ventas
CREATE TABLE Ventas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    MetodoPagoId INT NOT NULL,
    Total DECIMAL(18,2) NOT NULL CHECK (Total >= 0),
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id) ON DELETE SET NULL,
    FOREIGN KEY (MetodoPagoId) REFERENCES MetodosPago(Id)
);

-- Tabla de Detalles de Venta
CREATE TABLE DetallesVenta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    VentaId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario DECIMAL(18,2) NOT NULL CHECK (PrecioUnitario >= 0),
    FOREIGN KEY (VentaId) REFERENCES Ventas(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);

-- Tabla de Promociones
CREATE TABLE Promociones (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    DescuentoPorcentaje DECIMAL(5,2) CHECK (DescuentoPorcentaje >= 0 AND DescuentoPorcentaje <= 100),
    FechaInicio DATETIME NOT NULL,
    FechaFin DATETIME NOT NULL,
    Activa BIT DEFAULT 0 -- Agregamos la columna 'Activa' para gestionar su estado
);

-- Tabla de Promociones por Producto
CREATE TABLE PromocionesProductos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PromocionId INT NOT NULL,
    ProductoId INT NOT NULL,
    FOREIGN KEY (PromocionId) REFERENCES Promociones(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);

-- Tabla de Citas
CREATE TABLE Citas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT NOT NULL,
    Fecha DATETIME NOT NULL,
    Servicio NVARCHAR(100) NOT NULL,
    RecordatorioEnviado BIT DEFAULT 0,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

-- Tabla de Backups
CREATE TABLE Backups (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Tipo NVARCHAR(50),
    Ubicacion NVARCHAR(255) NOT NULL
);

-- Tabla de Auditoría
CREATE TABLE Auditoria (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    Accion NVARCHAR(255) NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

-- Índices para optimización
CREATE INDEX IX_Usuarios_Correo ON Usuarios(Correo);
CREATE INDEX IX_Clientes_Correo ON Clientes(Correo);
CREATE INDEX IX_Productos_Nombre ON Productos(Nombre);
CREATE INDEX IX_Ventas_Fecha ON Ventas(Fecha);
CREATE INDEX IX_Inventario_ProductoId ON Inventario(ProductoId);

-- Triggers
GO

-- Trigger para reducir inventario tras una venta
-- Trigger para reducir inventario tras una venta
-- Trigger para reducir inventario tras una venta
CREATE TRIGGER TRG_ReducirInventario
ON DetallesVenta
AFTER INSERT
AS
BEGIN
    UPDATE inv
    SET inv.Cantidad = inv.Cantidad - i.Cantidad
    FROM Inventario inv
    JOIN inserted i ON inv.ProductoId = i.ProductoId
    WHERE inv.Cantidad >= i.Cantidad;
END;
GO

-- Trigger para registrar promociones activadas
CREATE TRIGGER TRG_ActualizarPromociones
ON Promociones
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE Promociones
    SET Activa = 1
    WHERE GETDATE() BETWEEN FechaInicio AND FechaFin;
END;
GO

-- Trigger para alertar cuando el stock mínimo sea alcanzado
CREATE TRIGGER TRG_AlertaStockMinimo
ON Inventario
AFTER UPDATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Inventario WHERE Cantidad <= StockMinimo)
        PRINT '¡Alerta: El inventario está por debajo del stock mínimo!';
END;
GO




