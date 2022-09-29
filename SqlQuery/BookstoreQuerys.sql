use BookStoreDB

Create Table BookInfo(
BookId int Primary Key identity,
BookName varchar(100) Unique Not Null,
Author varchar(200) Unique Not Null,
Description varchar(max) Not Null,
Quantity int Not Null,
Price money Not Null,
DiscountedPrice money Not Null,
Rating float,
RatingCount int,
BookImage varchar(255)
);


select * from BookInfo

Create Procedure spBookCreate
(
@BookName varchar(100),
@Author varchar(200),
@Description varchar(max),
@Quantity int,
@Price money,
@DiscountedPrice money,
@Rating float,
@RatingCount int,
@BookImage varchar
)
As
Begin
	Insert Into BookInfo(BookName, Author, Description, Quantity, Price, DiscountedPrice, Rating, RatingCount, BookImage)
	Values(@BookName, @Author, @Description, @Quantity, @Price, @DiscountedPrice, @Rating, @RatingCount, @BookImage)
End