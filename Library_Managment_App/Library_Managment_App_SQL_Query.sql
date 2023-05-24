create database Library_Managment_App

use Library_Managment_App

create table login
(
	id int identity primary key,
	username varchar(20),
	password varchar(20)
)
select * from login
insert into login values ('harish','harish@18')
insert into login values ('xyz','xyz@123')

create table students
(
	Roll_Number int identity primary key,
	Name varchar(20),
	Department Varchar(20)
)
--drop table students
create table books
(
	Book_ID int identity primary key,
	Title varchar(20),
	Author varchar(20),
	Publication varchar(50),
	Quantity int
)
--drop table books
create table issuebook
(
	Roll_Number int primary key references students(Roll_Number),
	Book_ID int references books(Book_ID),
	Issue_Date date
)
--drop table issuebook
select * from students
select * from books
select * from issuebook