USE Foodbank;
GO

-- Limpiar datos de prueba previos (si ya corriste este script antes)
DELETE FROM FoodItems WHERE Barcode IN ('BAR001', 'BAR002', 'BAR003', 'BAR004', 'BAR005');
GO

INSERT INTO FoodItems (Name, Category, Brand, Price, QuantityInStock, IsPerishable, CaloriesPerServing, IsActive, Barcode)
VALUES
('Manzana Fuji',   'Fruit',  'Del Valle',  2.50,  50, 1,  80, 1, 'BAR001'),
('Banano',         'Fruit',  'Tropical',   1.80,  30, 1,  90, 1, 'BAR002'),
('Queso Brie',     'Dairy',  'Lacteos CR', 8.00,   5, 1, 350, 1, 'BAR003'),
('Ribeye Premium', 'Meat',   'Carnes SA', 35.00,  15, 1, 450, 1, 'BAR004'),
('Arroz',          'Grains', 'Gallo',      3.00, 100, 0, 200, 1, 'BAR005');
GO

-- Limpiar usuarios de prueba previos
DELETE FROM UserRoles WHERE UserId IN (SELECT UserId FROM Users WHERE Username IN ('admin', 'jperez', 'mgarcia', 'inactive_user'));
GO
DELETE FROM Users WHERE Username IN ('admin', 'jperez', 'mgarcia', 'inactive_user');
GO

INSERT INTO Users (Username, Email, FullName, IsActive, LastLogin)
VALUES
('admin',         'admin@foodbank.cr',        'Administrador Sistema', 1, NULL),
('jperez',        'juan.perez@foodbank.cr',   'Juan Perez',            1, '2026-06-01'),
('mgarcia',       'maria.garcia@foodbank.cr', 'Maria Garcia',          1, NULL),
('inactive_user', 'inactive@foodbank.cr',     'Usuario Inactivo',      0, NULL);
GO
