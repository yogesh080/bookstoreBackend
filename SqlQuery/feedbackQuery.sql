use BookStoreDB

create table Feedbacks(
FeedbackId int primary key identity,
TotalRating float not null,
Comment varchar(max) not null,
BookId int not null foreign key (BookId) references BookInfo(BookId),
UserId int not null foreign key (UserId) references UserInfo(UserId)
)

--- create sp -----

DROP TABLE Feedbacks;


Select * From Feedbacks
-----sp feedback query----

alter  Proc spAddFeedback
(
	@Comment varchar(max),
	@TotalRating decimal,
	@BookId int,
	@UserId int
)
as
Declare @AverageRating int;
begin
	if (exists(SELECT * FROM Feedbacks where BookId = @BookId and UserId=@UserId))
		select 1;
	Else
	Begin
		if (exists(SELECT * FROM BookInfo WHERE BookId = @BookId))
		Begin  select * from Feedbacks
			Begin try
				Begin transaction
					Insert into Feedbacks(Comment, TotalRating, BookId, UserId) values(@Comment, @TotalRating, @BookId, @UserId);		
					set @AverageRating = (Select AVG(TotalRating) from Feedbacks where BookId = @BookId);
					Update BookInfo set Rating=@AverageRating where  BookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
end;


----- get sp feed back----

alter procedure spGetFeedbackks(
@BookId int
)  
As  
Begin try  
select Feedbacks.Comment,Feedbacks.FeedbackId,Feedbacks.TotalRating,Feedbacks.BookId,
UserInfo.FullName
from Feedbacks inner join UserInfo on Feedbacks.UserId=UserInfo.UserId	
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

--- delete feed back----

alter procedure SPDeleteFeedbackById(
@FeedbackId int
)
As
Begin
delete from Feedbackinfo where FeedbackId = @FeedbackId
end 
