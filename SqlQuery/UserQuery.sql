
Create DataBase BookStoreDB;

Use BookStoreDB;
Create Table UserInfo(
UserId int Identity (1,1) Primary Key,
FullName varchar(200) Not Null,
Email varchar(100) Not Null,
Password varchar(100) Not Null,
MobileNumber bigint
);

Select * From UserInfo
-- Stored Procedure For User Registration --
Create Procedure spUserRegister
(
@FullName varchar(200),
@Email varchar(100),
@Password varchar(100),
@MobileNumber bigint
)
As
Begin
	Insert UserInfo
	Values (@FullName, @Email, @Password, @MobileNumber)
End;

--SP for User Login--
Create Procedure spLogIn
(
@Email varchar(100),
@Password varchar(100)
)
As
Begin
	Select * From UserInfo Where Email = @Email And Password = @Password
End

-- SP for forgetpassword-- 
Create Procedure spForgetPassword
(
@Email varchar(100)
)
As
Begin
	Update UserInfo Set Password = 'Null' Where  Email = @Email;
	Select * From UserInfo Where Email = @Email;
End 

--set new Password--
Create Procedure spResetPassword
(
@Email varchar(100),
@Password varchar(100)
)
As
Begin
	Update UserInfo Set Password = @Password Where Email = @Email;
End

