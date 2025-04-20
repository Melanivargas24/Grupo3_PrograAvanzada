USE [Hospital]
GO

-- Reiniciar el contador de identidad (SOLO CUANDO ES NECESARIO)
DBCC CHECKIDENT ('dbo.Estados', RESEED, 0);
DBCC CHECKIDENT ('dbo.Especialidades', RESEED, 0);



INSERT INTO [dbo].[Estados]
           ([Nombre])
     VALUES
           ('Activo'),
		   ('Inactivo')
GO


INSERT INTO [dbo].[Especialidades] ([Nombre]) VALUES
('Cardiología'),
('Pediatría'),
('Dermatología'),
('Neurología'),
('Ginecología'),
('Oncología'),
('Oftalmología'),
('Ortopedia'),
('Psicología'),
('Urología'),
('Endocrinología'),
('Gastroenterología'),
('Neumología'),
('Reumatología'),
('Nefrología');
GO

INSERT INTO [Hospital].[dbo].[Usuarios] 
([nombre], [apellido], [Email], [Clave], [Role], [Direccion], [Telefono])
VALUES
('Juan', 'Perez', 'juan.perez@ccss.sa.cr', 'clave123', 'Medico', 'Calle 1, Casa 10', '87966588'),
('Maria', 'Lopez', 'maria.lopez@ccss.sa.cr', 'clave123', 'Medico', 'Calle 2, Casa 15', '72548963'),
('Carlos', 'Gomez', 'carlos.gomez@ccss.sa.cr', 'clave123', 'Medico', 'Calle 3, Casa 20', '42587963'),
('Ana', 'Martinez', 'ana.martinez@ccss.sa.cr', 'clave123', 'Medico', 'Calle 4, Casa 25', '65482369'),
('Luis', 'Rodriguez', 'luis.rodriguez@ccss.sa.cr', 'clave123', 'Medico', 'Calle 5, Casa 30', '32569875');

