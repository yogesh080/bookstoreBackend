use BookStoreDB

DROP table Address
Drop table AddressType

create table Address(
AddressId int Identity(1,1)Primary key,
UserId int  not null FOREIGN KEY (UserId) REFERENCES UserInfo(UserId),
AddressType int not null FOREIGN KEY (AddressType) REFERENCES AddressTypetable(AddressTypeId),
FullAddress varchar(255) unique not null,
City varchar(255) not null,
State varchar(255) not null,
)



create table AddressTypetable(
AddressTypeId INT IDENTITY(1,1) PRIMARY KEY,
AddressType varchar(20) not null
)

insert into AddressTypeTable values('Home'),('Office'),('Other');

select * from AddressTypetable


select * from Address

DROP PROCEDURE spAddAddress

create procedure spAddAddress(
@UserId int,
@AddressType int, 
@FullAddress varchar(255),
@City varchar(255),
@State varchar(255)
)
As
Begin
   insert into Address(UserId,AddressType,FullAddress,City,State) values(@UserId,@AddressType,@FullAddress,@City,@State)
end
