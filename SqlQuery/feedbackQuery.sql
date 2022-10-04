use BookStoreDB

create table Feedbacks(
FeedbackId int primary key identity,
Rating float not null,
Comment varchar(max) not null,
BookId int not null foreign key (BookId) references BookInfo(BookId),
UserId int not null foreign key (UserId) references UserInfo(UserId)
)

