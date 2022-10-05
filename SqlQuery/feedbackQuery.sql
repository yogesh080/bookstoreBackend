use BookStoreDB

create table Feedbacks(
FeedbackId int primary key identity,
Rating float not null,
Comment varchar(max) not null,
BookId int not null foreign key (BookId) references BookInfo(BookId),
UserId int not null foreign key (UserId) references UserInfo(UserId)
)


-----sp feedback query----

create procedure spGetFeedbackks(
@BookId int
)  
As  
Begin try  
select Feedbacks.Comment,Feedbacks.FeedbackId,Feedbacks.TotalRating,Feedbacks.BookId,
Users.FullName
from Feedbacks inner join UserInfo on Feedbacks.UserId=Users.UserId
where Feedbacks.BookId=@BookId


end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH
