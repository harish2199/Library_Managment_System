create database Library_Management_System

use Library_Management_System

create table Login_User
(
	Username  varchar(20),
	Password varchar(20)
)

insert into Login_User values('xyz','xyz@123');
--drop table Login_User
select * from Login_User


create table Books
(
	ID int identity primary key,
	Title varchar(50),
	Author varchar(50),
	Publication varchar(50),
	Available varchar(5),
	Student_ID int,
	foreign key(Student_ID) references Students(Student_ID)
)

--drop table Books
select * from Books

insert into Books(Title,Author,Publication,Available) values('Computer Networks','Kumar Verma','Sun India Publications','YES')
insert into Books(Title,Author,Publication,Available) values('.Net Framework','Kumar Verma','Sun India Publications','YES')
insert into Books(Title,Author,Publication,Available) values('Data Structures','Lalit Kumar','Thakur Publications','YES')

create table Students
(
	Student_ID int primary key,
	Student_Name varchar(20),
	Book_Issued varchar(5)
)

--drop table Students
select * from Students

update Books set Available = 'YES', Student_ID = NULL where ID = 8
update Students set Book_Issued = 'NO' where Student_ID = 124

select * from Login_User
select * from Books
select * from Students

select Books.Title,Books.Author,Books.Publication,Students.Student_ID,Students.Student_Name
from Books
join Students on Books.Student_ID = Students.Student_ID
