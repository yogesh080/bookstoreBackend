use BookStoreDB

Create Table Orderinfo(
OrderId int Identity Primary Key,
UserId int Foreign key References UserInfo(UserId),
BookId int Foreign key References BookInfo(BookId),
AddressId int Foreign key References Address(AddressId),
BookQuantity int,
OrderDate Date Not Null,
TotalPrice float,	

)

select * FROM Orderinfo

drop table Orderinfo

----- sp create procedure---

alter procedure spAddOrder(
	@UserId int,
	@BookId int,
	@AddressId int,
	@BookQuantity int

) as
Declare @TotalPrice int
begin
		set @TotalPrice = (select DiscountedPrice from BookInfo where BookId = @BookId);
		If(Exists(Select * from BookInfo where BookId = @BookId))
			begin
			If(Exists (Select * from UserInfo where UserId = @UserId))
				BEGIN
					Begin try
						Begin Transaction
						Insert Into Orderinfo(Totalprice, BookQuantity, OrderDate, UserId, BookId, AddressId)
						Values(@TotalPrice*@BookQuantity, @BookQuantity, GETDATE(), @UserId, @BookId, @AddressId);
						Update BookInfo set Quantity=Quantity-@BookQuantity where BookId = @BookId;
						Delete from CartInfo where BookId = @BookId and UserId = @UserId;
						select * from Orderinfo;
commit Transaction
					End try
					Begin Catch
							rollback; 
					End Catch
				end
			Else
				Begin
					Select 3;
				End
		End
	Else
		Begin
			Select 2;
		End
end;

select * from Orderinfo



---- get sp order----
create procedure spGetOrders
(
	@UserId int
)
as
begin
		Select 
		O.OrderId,O.UserId, O.AddressId, b.bookId,
		O.TotalPrice, O.BookQuantity, O.OrderDate,
		b.BookName, b.Author, b.BookImage
		FROM BookInfo b
		inner join Orderinfo O on O.BookId = b.BookId 
		where 
			O.UserId = @UserId;
end;