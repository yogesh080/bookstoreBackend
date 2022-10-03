use BookStoreDB

Create Table CartInfo(
CartId int Identity Primary Key,
UserId int Not Null Foreign Key(UserId) References UserInfo(UserId),
BookId int Not Null Foreign Key(BookId) References BookInfo(BookId),
BookQuantity int
)

Select * From CartInfo

----stored procedure for add cart----
Create Procedure spAddToCart
(
@BookId int,
@UserId int,
@BookQuantity int
)
As
Begin
	Insert Into CartInfo(BookId, UserId, BookQuantity)
	Values(@BookId, @UserId, @BookQuantity)
End

---Recieve data sp-----
Create Procedure spRetrieveCart
(
@UserId int
)
As
Begin
	Select CartInfo.CartId, CartInfo.BookId, CartInfo.BookQuantity, CartInfo.UserId,
	BookInfo.BookName, BookInfo.Author, BookInfo.Description, BookInfo.Price, BookInfo.DiscountedPrice,
    BookInfo.BookImage
	From CartInfo Inner Join BookInfo ON CartInfo.BookId = BookInfo.BookId
	Where CartInfo.UserId = @UserId
End

----------- Stored Procedure For Deleting From Cart ----------
Create Procedure spDeleteFromCart
(
@CartId int,
@UserId int
)
As
Begin
	Delete From CartInfo Where CartId = @CartId and UserId = @UserId
End

---------- Stored Procedure For Upadating The Cart ----------
Create Procedure spUpdateCart
(
@CartId int,
@UserId int,
@BookQuantity int
)
As
Begin
	Update CartInfo Set BookQuantity = @BookQuantity Where CartId=@CartId and UserId=@UserId
End