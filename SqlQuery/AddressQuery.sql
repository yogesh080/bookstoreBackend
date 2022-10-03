use BookStoreDB

DROP table Address
Drop table AddressTypetable

create table Address(
AddressId int Identity (1,1) Primary Key,
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


---- sp for get all address---

DROP PROCEDURE GetAllAddressSP
create procedure GetAllAddressSP(
@UserId int
)
As
Begin 
select 
a.AddressId,a.AddressType,a.FullAddress,a.City,a.State,u.UserId,u.FullName,u.MobileNumber
from Address a INNER JOIN UserInfo u ON a.UserId = u.UserId
end


--sp delete---

DROP PROCEDURE spDeleteByAddressId
create procedure spDeleteByAddressId(
@AddressId int,
@UserId int
)
As
Begin 
delete from Address where AddressId = @AddressId AND UserId = @UserId
end 


--sp update---
DROP PROCEDURE UpdateAddressbyIdSP

create procedure spUpdateAddressbyId (
@AddressId int,
@UserId int,
@AddressType int, 
@FullAddress varchar(255),
@City varchar(255),
@State varchar(255)
)
As
Begin 
update Address set AddressType=@AddressType,FullAddress=@FullAddress,City=@City,State=@State where UserId=@UserId
end 

---- sp get addressbyid---
create procedure spGetAddressById(
@AddressId int,
@UserId int
)
As
Begin 
select 
a.AddressId,a.AddressType,a.FullAddress,a.City,a.State,u.UserId,u.FullName,u.MobileNumber
from Address a INNER JOIN UserInfo u ON a.UserId = u.UserId where AddressId=@AddressId
end
