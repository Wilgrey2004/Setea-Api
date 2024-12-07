-- Aqui voy a poner los procesos almacenados aqui 

--CREATE TABLE Roles (
--    Id INT PRIMARY KEY IDENTITY(1,1),
--    Nombre NVARCHAR(50) NOT NULL UNIQUE
--);

--create procedure Sp_Insert_In_Roles
--@Nombre Nvarchar(50)
--as
--begin 
--	insert into Roles(Nombre) values(@Nombre)
--end 


--create procedure Sp_Update_In_Roles
--@id int,
--@Nombre Nvarchar(50)
--as
--begin 
--	update Roles 
--	set Nombre = @Nombre
--	where Id = @id
--end 

--create procedure Sp_Delete_In_Roles
--@id int
--as
--begin
--	delete Roles
--	where Id = @id
--end 

--create procedure Sp_Get_In_Roles
--as 

--begin
--	select * from Roles
--end 

--create procedure Sp_Get_un_Rol
--@id int

--as
--begin 
--	select * from Roles 
--	where  id = @id
--end 

--CREATE TABLE Usuarios (
--    Id INT PRIMARY KEY IDENTITY(1,1),
--    Nombre NVARCHAR(100) NOT NULL,
--    Correo NVARCHAR(100) NOT NULL UNIQUE,
--    Contraseña NVARCHAR(200) NOT NULL,
--    RolId INT NOT NULL,
--    FechaCreacion DATETIME DEFAULT GETDATE(),
--    FOREIGN KEY (RolId) REFERENCES Roles(Id) ON DELETE CASCADE
--);

--CREATE PROCEDURE Sp_Insert_In_Usuarios
--    @Nombre NVARCHAR(100),
--    @Correo NVARCHAR(100),
--    @Contraseña NVARCHAR(200),
--    @RolId INT
--AS
--BEGIN 
--    INSERT INTO Usuarios (Nombre, Correo, Contraseña, RolId) 
--    VALUES (@Nombre, @Correo, @Contraseña, @RolId)
--END 

--CREATE PROCEDURE Sp_Update_In_Usuarios
--    @Id INT,
--    @Nombre NVARCHAR(100),
--    @Correo NVARCHAR(100),
--    @Contraseña NVARCHAR(200),
--    @RolId INT
--AS
--BEGIN 
--    UPDATE Usuarios 
--    SET Nombre = @Nombre,
--        Correo = @Correo,
--        Contraseña = @Contraseña,
--        RolId = @RolId
--    WHERE Id = @Id
--END 

--CREATE PROCEDURE Sp_Delete_In_Usuarios
--    @Id INT
--AS
--BEGIN
--    DELETE FROM Usuarios
--    WHERE Id = @Id
--END 

--CREATE PROCEDURE Sp_Get_In_Usuarios
--AS 
--BEGIN
--    SELECT * FROM Usuarios
--END 

--CREATE PROCEDURE Sp_Get_un_Usuario
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM Usuarios 
--    WHERE Id = @Id
--END 

---- Tabla de Métodos de Pago
--CREATE TABLE MetodosPago (
--    Id INT PRIMARY KEY IDENTITY(1,1),
--    Metodo NVARCHAR(50) NOT NULL UNIQUE
--);


--CREATE TABLE MetodosPago (
--    Id INT PRIMARY KEY IDENTITY(1,1),
--    Metodo NVARCHAR(50) NOT NULL UNIQUE
--);

--CREATE PROCEDURE Sp_Update_In_MetodosPago
--    @Id INT,
--    @Metodo NVARCHAR(50)
--AS
--BEGIN 
--    UPDATE MetodosPago 
--    SET Metodo = @Metodo
--    WHERE Id = @Id
--END 

--CREATE PROCEDURE Sp_Delete_In_MetodosPago
--    @Id INT
--AS
--BEGIN
--    DELETE FROM MetodosPago
--    WHERE Id = @Id
--END 

--CREATE PROCEDURE Sp_Get_In_MetodosPago
--AS 
--BEGIN
--    SELECT * FROM MetodosPago
--END 

--CREATE PROCEDURE Sp_Get_un_MetodoPago
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM MetodosPago 
--    WHERE Id = @Id
--END 

---- Tabla de Clientes
--CREATE TABLE Clientes (
--    Id INT PRIMARY KEY IDENTITY(1,1),
--    Nombre NVARCHAR(100) NOT NULL,
--    Correo NVARCHAR(100) UNIQUE,
--    Telefono NVARCHAR(20),
--    Clasificacion NVARCHAR(50),
--    FechaRegistro DATETIME DEFAULT GETDATE()
--);

-- Tabla de Clientes
--CREATE TABLE Clientes (
--    Id INT PRIMARY KEY IDENTITY(1,1),
--    Nombre NVARCHAR(100) NOT NULL,
--    Correo NVARCHAR(100) UNIQUE,
--    Telefono NVARCHAR(20),
--    Clasificacion NVARCHAR(50),
--    FechaRegistro DATETIME DEFAULT GETDATE()
--);

--CREATE PROCEDURE Sp_Update_In_Clientes
--    @Id INT,
--    @Nombre NVARCHAR(100),
--    @Correo NVARCHAR(100),
--    @Telefono NVARCHAR(20),
--    @Clasificacion NVARCHAR(50)
--AS
--BEGIN 
--    UPDATE Clientes 
--    SET Nombre = @Nombre,
--        Correo = @Correo,
--        Telefono = @Telefono,
--        Clasificacion = @Clasificacion
--    WHERE Id = @Id
--END 

--CREATE PROCEDURE Sp_Delete_In_Clientes
--    @Id INT
--AS
--BEGIN
--    DELETE FROM Clientes
--    WHERE Id = @Id
--END 

--CREATE PROCEDURE Sp_Get_In_Clientes
--AS 
--BEGIN
--    SELECT * FROM Clientes
--END 

--CREATE PROCEDURE Sp_Get_un_Cliente
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM Clientes 
--    WHERE Id = @Id
--END 

---- Tabla de Categorías
--CREATE TABLE Categorias (
--    Id INT PRIMARY KEY IDENTITY(1,1),
--    Nombre NVARCHAR(50) NOT NULL UNIQUE
--);

--CREATE PROCEDURE Sp_Insert_In_Categorias
--    @Nombre NVARCHAR(50)
--AS
--BEGIN 
--    INSERT INTO Categorias (Nombre) 
--    VALUES (@Nombre)
--END 

--CREATE PROCEDURE Sp_Update_In_Categorias
--    @Id INT,
--    @Nombre NVARCHAR(50)
--AS
--BEGIN 
--    UPDATE Categorias 
--    SET Nombre = @Nombre
--    WHERE Id = @Id
--END 

--CREATE PROCEDURE Sp_Delete_In_Categorias
--    @Id INT
--AS
--BEGIN
--    DELETE FROM Categorias
--    WHERE Id = @Id
--END 

--CREATE PROCEDURE Sp_Get_In_Categorias
--AS 
--BEGIN
--    SELECT * FROM Categorias
--END 

--CREATE PROCEDURE Sp_Get_un_Categoria
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM Categorias 
--    WHERE Id = @Id
--END 


---- Tabla de Usuarios
--CREATE TABLE Usuarios (
--    Id INT PRIMARY KEY IDENTITY(1,1),
--    Nombre NVARCHAR(100) NOT NULL,
--    Correo NVARCHAR(100) NOT NULL UNIQUE,
--    Contraseña NVARCHAR(200) NOT NULL,
--    RolId INT NOT NULL,
--    FechaCreacion DATETIME DEFAULT GETDATE(),
--    FOREIGN KEY (RolId) REFERENCES Roles(Id) ON DELETE CASCADE
--);


---- Procedimiento para Insertar un Usuario


---- Procedimiento para Obtener un Usuario por Correo
--CREATE PROCEDURE Sp_Get_Usuario_By_Correo
--    @Correo NVARCHAR(100)
--AS
--BEGIN 
--    SELECT * FROM Usuarios 
--    WHERE Correo = @Correo
--END 


---- Procedimiento para Insertar un Producto
--CREATE PROCEDURE Sp_Insert_In_Productos
--    @Nombre NVARCHAR(100),
--    @Descripcion NVARCHAR(255),
--    @Precio DECIMAL(18,2),
--    @CategoriaId INT
--AS
--BEGIN 
--    INSERT INTO Productos (Nombre, Descripcion, Precio, CategoriaId) 
--    VALUES (@Nombre, @Descripcion, @Precio, @CategoriaId)
--END 

---- Procedimiento para Actualizar un Producto
--CREATE PROCEDURE Sp_Update_In_Productos
--    @Id INT,
--    @Nombre NVARCHAR(100),
--    @Descripcion NVARCHAR(255),
--    @Precio DECIMAL(18,2),
--    @CategoriaId INT
--AS
--BEGIN 
--    UPDATE Productos 
--    SET Nombre = @Nombre,
--        Descripcion = @Descripcion,
--        Precio = @Precio,
--        CategoriaId = @CategoriaId
--    WHERE Id = @Id
--END 

---- Procedimiento para Eliminar un Producto
--CREATE PROCEDURE Sp_Delete_In_Productos
--    @Id INT
--AS
--BEGIN
--    DELETE FROM Productos
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Todos los Productos
--CREATE PROCEDURE Sp_Get_In_Productos
--AS 
--BEGIN
--    SELECT * FROM Productos
--END 

---- Procedimiento para Obtener un Producto por ID
--CREATE PROCEDURE Sp_Get_un_Producto
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM Productos 
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Productos por Categoría
--CREATE PROCEDURE Sp_Get_Productos_By_Categoria
--    @CategoriaId INT
--AS
--BEGIN 
--    SELECT * FROM Productos 
--    WHERE CategoriaId = @CategoriaId
--END 


---- Procedimiento para Insertar un Registro en Inventario
--CREATE PROCEDURE Sp_Insert_In_Inventario
--    @ProductoId INT,
--    @Cantidad INT,
--    @StockMinimo INT
--AS
--BEGIN 
--    INSERT INTO Inventario (ProductoId, Cantidad, StockMinimo) 
--    VALUES (@ProductoId, @Cantidad, @StockMinimo)
--END 

---- Procedimiento para Actualizar un Registro en Inventario
--CREATE PROCEDURE Sp_Update_In_Inventario
--    @Id INT,
--    @ProductoId INT,
--    @Cantidad INT,
--    @StockMinimo INT
--AS
--BEGIN 
--    UPDATE Inventario 
--    SET ProductoId = @ProductoId,
--        Cantidad = @Cantidad,
--        StockMinimo = @StockMinimo
--    WHERE Id = @Id
--END 

---- Procedimiento para Eliminar un Registro en Inventario
--CREATE PROCEDURE Sp_Delete_In_Inventario
--    @Id INT
--AS
--BEGIN
--    DELETE FROM Inventario
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Todos los Registros de Inventario
--CREATE PROCEDURE Sp_Get_In_Inventario
--AS 
--BEGIN
--    SELECT * FROM Inventario
--END 

---- Procedimiento para Obtener un Registro de Inventario por ID
--CREATE PROCEDURE Sp_Get_un_Inventario
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM Inventario 
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener el Inventario por Producto
--CREATE PROCEDURE Sp_Get_Inventario_By_Producto
--    @ProductoId INT
--AS
--BEGIN 
--    SELECT * FROM Inventario 
--    WHERE ProductoId = @ProductoId
--END 



-- Procedimiento para Insertar una Venta
--CREATE PROCEDURE Sp_Insert_In_Ventas
--    @ClienteId INT,
--    @MetodoPagoId INT,
--    @Total DECIMAL(18,2)
--AS
--BEGIN 
--    INSERT INTO Ventas (ClienteId, Fecha, MetodoPagoId, Total) 
--    VALUES (@ClienteId, GETDATE(), @MetodoPagoId, @Total)
--END 

---- Procedimiento para Actualizar una Venta
--CREATE PROCEDURE Sp_Update_In_Ventas
--    @Id INT,
--    @ClienteId INT,
--    @MetodoPagoId INT,
--    @Total DECIMAL(18,2)
--AS
--BEGIN 
--    UPDATE Ventas 
--    SET ClienteId = @ClienteId,
--        MetodoPagoId = @MetodoPagoId,
--        Total = @Total
--    WHERE Id = @Id
--END 

---- Procedimiento para Eliminar una Venta
--CREATE PROCEDURE Sp_Delete_In_Ventas
--    @Id INT
--AS
--BEGIN
--    DELETE FROM Ventas
--    WHERE Id = @Id
--END;

---- Procedimiento para Obtener Todas las Ventas
--CREATE PROCEDURE Sp_Get_In_Ventas
--AS 
--BEGIN
--    SELECT * FROM Ventas
--END ;

---- Procedimiento para Obtener una Venta por ID
--CREATE PROCEDURE Sp_Get_un_Venta
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM Ventas 
--    WHERE Id = @Id
--END ;

---- Procedimiento para Obtener Ventas por Cliente
--CREATE PROCEDURE Sp_Get_Ventas_By_Cliente
--    @ClienteId INT
--AS
--BEGIN 
--    SELECT * FROM Ventas 
--    WHERE ClienteId = @ClienteId
--END ;

-- Procedimiento para Insertar un Detalle de Venta
CREATE PROCEDURE Sp_Insert_In_DetallesVenta
    @VentaId INT,
    @ProductoId INT,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(18,2)
AS
BEGIN 
    INSERT INTO DetallesVenta (VentaId, ProductoId, Cantidad, PrecioUnitario) 
    VALUES (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario)
END 

---- Procedimiento para Actualizar un Detalle de Venta
--CREATE PROCEDURE Sp_Update_In_DetallesVenta
--    @Id INT,
--    @VentaId INT,
--    @ProductoId INT,
--    @Cantidad INT,
--    @PrecioUnitario DECIMAL(18,2)
--AS
--BEGIN 
--    UPDATE DetallesVenta 
--    SET VentaId = @VentaId,
--        ProductoId = @ProductoId,
--        Cantidad = @Cantidad,
--        PrecioUnitario = @PrecioUnitario
--    WHERE Id = @Id
--END 

---- Procedimiento para Eliminar un Detalle de Venta
--CREATE PROCEDURE Sp_Delete_In_DetallesVenta
--    @Id INT
--AS
--BEGIN
--    DELETE FROM DetallesVenta
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Todos los Detalles de Venta
--CREATE PROCEDURE Sp_Get_In_DetallesVenta
--AS 
--BEGIN
--    SELECT * FROM DetallesVenta
--END 

---- Procedimiento para Obtener un Detalle de Venta por ID
--CREATE PROCEDURE Sp_Get_un_DetalleVenta
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM DetallesVenta 
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Detalles de Venta por Venta
--CREATE PROCEDURE Sp_Get_DetallesVenta_By_Venta
--    @VentaId INT
--AS
--BEGIN 
--    SELECT * FROM DetallesVenta 
--    WHERE VentaId = @VentaId
--END 


---- Procedimiento para Insertar una Promoción
--CREATE PROCEDURE Sp_Insert_In_Promociones
--    @Nombre NVARCHAR(100),
--    @DescuentoPorcentaje DECIMAL(5,2),
--    @FechaInicio DATETIME,
--    @FechaFin DATETIME,
--    @Activa BIT = 0
--AS
--BEGIN 
--    INSERT INTO Promociones (Nombre, DescuentoPorcentaje, FechaInicio, FechaFin, Activa) 
--    VALUES (@Nombre, @DescuentoPorcentaje, @FechaInicio, @FechaFin, @Activa)
--END 

---- Procedimiento para Actualizar una Promoción
--CREATE PROCEDURE Sp_Update_In_Promociones
--    @Id INT,
--    @Nombre NVARCHAR(100),
--    @DescuentoPorcentaje DECIMAL(5,2),
--    @FechaInicio DATETIME,
--    @FechaFin DATETIME,
--    @Activa BIT
--AS
--BEGIN 
--    UPDATE Promociones 
--    SET Nombre = @Nombre,
--        DescuentoPorcentaje = @DescuentoPorcentaje,
--        FechaInicio = @FechaInicio,
--        FechaFin = @FechaFin,
--        Activa = @Activa
--    WHERE Id = @Id
--END 

---- Procedimiento para Eliminar una Promoción
--CREATE PROCEDURE Sp_Delete_In_Promociones
--    @Id INT
--AS
--BEGIN
--    DELETE FROM Promociones
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Todas las Promociones
--CREATE PROCEDURE Sp_Get_In_Promociones
--AS 
--BEGIN
--    SELECT * FROM Promociones
--END 

---- Procedimiento para Obtener una Promoción por ID
--CREATE PROCEDURE Sp_Get_un_Promocion
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM Promociones 
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Promociones Activas
--CREATE PROCEDURE Sp_Get_Promociones_Activas
--AS
--BEGIN 
--    SELECT * FROM Promociones 
--    WHERE Activa = 1 AND FechaInicio <= GETDATE() AND FechaFin >= GETDATE()
--END 


---- Procedimiento para Insertar una Relación entre Promoción y Producto
--CREATE PROCEDURE Sp_Insert_In_PromocionesProductos
--    @PromocionId INT,
--    @ProductoId INT
--AS
--BEGIN 
--    INSERT INTO PromocionesProductos (PromocionId, ProductoId) 
--    VALUES (@PromocionId, @ProductoId)
--END 

---- Procedimiento para Actualizar una Relación entre Promoción y Producto
--CREATE PROCEDURE Sp_Update_In_PromocionesProductos
--    @Id INT,
--    @PromocionId INT,
--    @ProductoId INT
--AS
--BEGIN 
--    UPDATE PromocionesProductos 
--    SET PromocionId = @PromocionId,
--        ProductoId = @ProductoId
--    WHERE Id = @Id
--END 

---- Procedimiento para Eliminar una Relación entre Promoción y Producto
--CREATE PROCEDURE Sp_Delete_In_PromocionesProductos
--    @Id INT
--AS
--BEGIN
--    DELETE FROM PromocionesProductos
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Todas las Relaciones entre Promociones y Productos
--CREATE PROCEDURE Sp_Get_In_PromocionesProductos
--AS 
--BEGIN
--    SELECT * FROM PromocionesProductos
--END 

---- Procedimiento para Obtener una Relación por ID
--CREATE PROCEDURE Sp_Get_un_PromocionProducto
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM PromocionesProductos 
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Productos de una Promoción
--CREATE PROCEDURE Sp_Get_Productos_By_Promocion
--    @PromocionId INT
--AS
--BEGIN 
--    SELECT * FROM PromocionesProductos 
--    WHERE PromocionId = @PromocionId
--END 

-- Procedimiento para Insertar una Cita
--CREATE PROCEDURE Sp_Insert_In_Citas
--    @ClienteId INT,
--    @Fecha DATETIME,
--    @Servicio NVARCHAR(100),
--    @RecordatorioEnviado BIT = 0
--AS
--BEGIN 
--    INSERT INTO Citas (ClienteId, Fecha, Servicio, RecordatorioEnviado) 
--    VALUES (@ClienteId, @Fecha, @Servicio, @RecordatorioEnviado)
--END 

---- Procedimiento para Actualizar una Cita
--CREATE PROCEDURE Sp_Update_In_Citas
--    @Id INT,
--    @ClienteId INT,
--    @Fecha DATETIME,
--    @Servicio NVARCHAR(100),
--    @RecordatorioEnviado BIT
--AS
--BEGIN 
--    UPDATE Citas 
--    SET ClienteId = @ClienteId,
--        Fecha = @Fecha,
--        Servicio = @Servicio,
--        RecordatorioEnviado = @RecordatorioEnviado
--    WHERE Id = @Id
--END 

---- Procedimiento para Eliminar una Cita
--CREATE PROCEDURE Sp_Delete_In_Citas
--    @Id INT
--AS
--BEGIN
--    DELETE FROM Citas
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Todas las Citas
--CREATE PROCEDURE Sp_Get_In_Citas
--AS 
--BEGIN
--    SELECT * FROM Citas
--END 

---- Procedimiento para Obtener una Cita por ID
--CREATE PROCEDURE Sp_Get_un_Cita
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM Citas 
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Citas por Cliente
--CREATE PROCEDURE Sp_Get_Citas_By_Cliente
--    @ClienteId INT
--AS
--BEGIN 
--    SELECT * FROM Citas 
--    WHERE ClienteId = @ClienteId
--END 


---- Procedimiento para Insertar un Backup
--CREATE PROCEDURE Sp_Insert_In_Backups
--    @Tipo NVARCHAR(50),
--    @Ubicacion NVARCHAR(255)
--AS
--BEGIN 
--    INSERT INTO Backups (Fecha, Tipo, Ubicacion) 
--    VALUES (GETDATE(), @Tipo, @Ubicacion)
--END 

---- Procedimiento para Actualizar un Backup
--CREATE PROCEDURE Sp_Update_In_Backups
--    @Id INT,
--    @Tipo NVARCHAR(50),
--    @Ubicacion NVARCHAR(255)
--AS
--BEGIN 
--    UPDATE Backups 
--    SET Tipo = @Tipo,
--        Ubicacion = @Ubicacion
--    WHERE Id = @Id
--END 

---- Procedimiento para Eliminar un Backup
--CREATE PROCEDURE Sp_Delete_In_Backups
--    @Id INT
--AS
--BEGIN
--    DELETE FROM Backups
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Todos los Backups
--CREATE PROCEDURE Sp_Get_In_Backups
--AS 
--BEGIN
--    SELECT * FROM Backups
--END 

---- Procedimiento para Obtener un Backup por ID
--CREATE PROCEDURE Sp_Get_un_Backup
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM Backups 
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Backups por Tipo
--CREATE PROCEDURE Sp_Get_Backups_By_Tipo
--    @Tipo NVARCHAR(50)
--AS
--BEGIN 
--    SELECT * FROM Backups 
--    WHERE Tipo = @Tipo
--END 

---- Procedimiento para Insertar un Registro de Auditoría
--CREATE PROCEDURE Sp_Insert_In_Auditoria
--    @UsuarioId INT,
--    @Accion NVARCHAR(255)
--AS
--BEGIN 
--    INSERT INTO Auditoria (UsuarioId, Accion, Fecha) 
--    VALUES (@UsuarioId, @Accion, GETDATE())
--END 

---- Procedimiento para Actualizar un Registro de Auditoría
--CREATE PROCEDURE Sp_Update_In_Auditoria
--    @Id INT,
--    @UsuarioId INT,
--    @Accion NVARCHAR(255)
--AS
--BEGIN 
--    UPDATE Auditoria 
--    SET UsuarioId = @UsuarioId,
--        Accion = @Accion,
--        Fecha = GETDATE()  -- Actualiza la fecha al momento de la modificación
--    WHERE Id = @Id
--END 

---- Procedimiento para Eliminar un Registro de Auditoría
--CREATE PROCEDURE Sp_Delete_In_Auditoria
--    @Id INT
--AS
--BEGIN
--    DELETE FROM Auditoria
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Todos los Registros de Auditoría
--CREATE PROCEDURE Sp_Get_In_Auditoria
--AS 
--BEGIN
--    SELECT * FROM Auditoria
--END 

---- Procedimiento para Obtener un Registro de Auditoría por ID
--CREATE PROCEDURE Sp_Get_un_Auditoria
--    @Id INT
--AS
--BEGIN 
--    SELECT * FROM Auditoria 
--    WHERE Id = @Id
--END 

---- Procedimiento para Obtener Registros de Auditoría por Usuario
--CREATE PROCEDURE Sp_Get_Auditoria_By_Usuario
--    @UsuarioId INT
--AS
--BEGIN 
--    SELECT * FROM Auditoria 
--    WHERE UsuarioId = @UsuarioId
--END 