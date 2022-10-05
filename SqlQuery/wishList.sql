use BookStoreDB

	Create Table WishList(
	WishListId int Identity Primary Key,
	UserId int Not Null Foreign Key(UserId) References UserInfo(UserId),
	BookId int Not Null Foreign Key(BookId) References BookInfo(BookId),
	)

select * from WishList
--------- Stored Procedure For Adding Book To WishList -----------
Create Procedure spAddToWishlList
(
@UserId int,
@BookId int
)
As
Begin
	Insert Into WishList(UserId, BookId)
	Values(@UserId, @BookId)
End

-- delete sp ----
Create Procedure spDeleteFromWishList
(
@UserId int,
@WishListId int
)
As
Begin
	Delete From WishList Where UserId = @UserId and WishListId = @WishListId
End

------------ Stored Procedure For Get All WishListed Items --------
Create Procedure spGetWishList
(
@UserId int
)
As
Begin
	Select WishList.WishListId, WishList.BookId, WishList.UserId,
	BookInfo.BookName, BookInfo.Author, BookInfo.Description, BookInfo.Quantity, BookInfo.Price, BookInfo.DiscountedPrice,
	BookInfo.Rating, BookInfo.RatingCount, BookInfo.BookImage
	From WishList Inner Join BookInfo ON WishList.BookId = BookInfo.BookId
	Where WishList.UserId = @UserId
End