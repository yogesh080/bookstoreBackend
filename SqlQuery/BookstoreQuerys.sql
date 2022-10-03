use BookStoreDB

DROP TABLE BookInfo;

create Table BookInfo(
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
)




select * from BookInfo


-----sp for create book-----
alter Procedure spCreateBook
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


--sp get all book---
Create Procedure SPGetAllBook
As
Begin
	Select * From BookInfo
End

---- sp UpdateBook  -----
alter Procedure spUpdateBook
(
@BookId int,
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
	Update BookInfo Set BookName=@BookName,Author=@Author,Description=@Description,Quantity=@Quantity,Price=@Price,
	DiscountedPrice=@DiscountedPrice,Rating=@Rating,RatingCount=@RatingCount,@BookImage=@BookImage
	Where BookId=@BookId
	Select * From BookInfo Where BookId=@BookId
End


--sp delete book---
Create Procedure spDeleteBook
(
@BookId int
)
As
Begin
	Delete From BookInfo Where BookId=@BookId
End


--- sp by bookid--
Create Procedure spGetBookByID
(
@BookId int
)
As
Begin
	Select * From BookInfo Where BookId = @BookId
End