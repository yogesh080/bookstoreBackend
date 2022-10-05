use BookStoreDB

create table Feedbackinfo(
FeedbackId int primary key identity,
TotalRating float,
Comment varchar(max),
BookId int  foreign key (BookId) references BookInfo(BookId),
UserId int  foreign key (UserId) references UserInfo(UserId)
)

select * from Feedbackinfo

create procedure spAddFeedbackpro
(
	@Comment varchar(max),
	@TotalRating decimal,
	@BookId int,
	@UserId int
)
as
Declare @AverageRating int;
begin
	if (exists(SELECT * FROM Feedbackinfo where BookId = @BookId and UserId=@UserId))
		select 1;
	Else
	Begin
		if (exists(SELECT * FROM BookInfo WHERE BookId = @BookId))
		Begin  select * from Feedbackinfo
			Begin try
				Begin transaction
					Insert into Feedbackinfo(Comment, TotalRating, BookId, UserId) values(@Comment, @TotalRating, @BookId, @UserId);		
					set @AverageRating = (Select AVG(TotalRating) from Feedbackinfo where BookId = @BookId);
					Update BookInfo set @TotalRating=@AverageRating where  BookId = @BookId;
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



-----get all feedback sp--------

alter procedure spGetFeedback(
@BookId int
)  
As  
Begin   
select Feedbackinfo.Comment,Feedbackinfo.FeedbackId,Feedbackinfo.TotalRating,Feedbackinfo.BookId,
UserInfo.FullName
from Feedbackinfo inner join UserInfo on Feedbackinfo.UserId=UserInfo.UserId
where Feedbackinfo.BookId=@BookId


end 

--- delete by book id----
create procedure SPDeleteFeedbackById(
@FeedbackId int
)
As
Begin
delete from Feedbackinfo where FeedbackId = @FeedbackId
end 