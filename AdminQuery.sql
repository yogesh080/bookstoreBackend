Use BookStoreDB;

Create Table AdminInfo(
AdminId int Identity (1,1) Primary Key,
AdminName varchar(200) Not Null,
AdminEmail varchar(100) Not Null,
AdminPassword varchar(100) Not Null
);

select * from AdminInfo

Create Procedure spAdminRegister
(
@AdminName varchar(200),
@AdminEmail varchar(100),
@AdminPassword varchar(100)
)
As
Begin
	Insert AdminInfo
	Values (@AdminName, @AdminEmail, @AdminPassword)
End;

-- spAdminLogIn--

Create Procedure spAdminLogIn
(
@AdminEmail varchar(100),
@AdminPassword varchar(100)
)
As
Begin
	Select * From AdminInfo Where AdminEmail = @AdminEmail And AdminPassword = @AdminPassword
End
