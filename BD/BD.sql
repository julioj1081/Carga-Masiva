create database JFernandezTP

use JFernandezTP

create table Usuarios(
	RFC varchar(50) unique,
	NumUsuario int unique,
	Nombre varchar(50),
	ApellidoPaterno varchar(50),
	ApellidoMaterno varchar(50),
	FechaControl date,
	Salario money
)



alter procedure GetAllEmpleados
as
select RFC, NumEmpleado, Nombre, ApellidoPaterno, ApellidoMaterno, COnvert(varchar, FechaControl, 131) as 'FechaControl', 
Salario from Usuarios

select * from Usuarios

truncate table Usuarios
/*
select RFC, NumUsuario, Nombre, ApellidoPaterno, ApellidoMaterno, COnvert(varchar, FechaControl, 131) as [MMM DD YYYY hh:mm:ss:mmm(AM/PM)] , 
Salario from Usuarios

alter procedure AddUsuario
	@RFC varchar(50),
	@NumUsuario tinyint,
	@Nombre varchar(50),
	@ApellidoPaterno varchar(50),
	@ApellidoMaterno varchar(50),
	@FechaControl date,
	@Salario money
	as
	insert into Usuarios values(@RFC, @NumUsuario, @Nombre, @ApellidoPaterno, @ApellidoMaterno, Convert(varchar, @FechaControl, 131) , @Salario)

	*/
	alter procedure AddUsuario
	@RFC varchar(50),
	@NumUsuario int,
	@Nombre varchar(50),
	@ApellidoPaterno varchar(50),
	@ApellidoMaterno varchar(50),
	@FechaControl date,
	@Salario money
	as
	insert into Usuarios values(@RFC, @NumUsuario, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaControl , @Salario)


	create procedure DeleteUsuario
	@RFC varchar(50)
	as
	delete from Usuarios where RFC = @RFC

	create procedure UpdateUsuario
	@RFC varchar(50),
	@NumUsuario int,
	@Nombre varchar(50),
	@ApellidoPaterno varchar(50),
	@ApellidoMaterno varchar(50),
	@FechaControl date,
	@Salario money
	as
	update Usuarios set NumUsuario = @NumUsuario, Nombre = @Nombre,
						ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno,
						FechaControl = @FechaControl, Salario = @Salario 
						WHERE @RFC = @RFC