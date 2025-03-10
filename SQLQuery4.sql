create database nickgirl
use nickgirl

create TABLE USERS
(
	id int primary key identity (1,1),
	email varchar(max) null,
	username varchar(max) null,
	password varchar(max) null,
)
select * from USERS

create table ADMIN
(
	id int primary key identity (1,1),
	adname varchar(max) null,
	adpassword varchar(max) null,
)
select * from ADMIN
INSERT INTO ADMIN (adname, adpassword) VALUES ('buibach', '123456');
INSERT INTO ADMIN (adname, adpassword) VALUES ('a', 'a');

CREATE TABLE Category
(
    id INT PRIMARY KEY IDENTITY(1,1),
    category_title VARCHAR(255) NOT NULL,
    image VARCHAR(500) NULL
);
select * from Category

create table BOOKS
(
	id int primary key identity (1,1),
	book_title varchar(max) null,
	author varchar(max) null,
	published_date date null,
	status varchar(max) null,
	date_insert date null,
	date_update date null,
	date_delete date null,
	category_id INT null,
)
ALTER TABLE BOOKS ADD CONSTRAINT FK_Books_Category FOREIGN KEY (category_id) REFERENCES Category(id);
alter table BOOKS
add images varchar(max) null
CREATE TABLE Borrow_Return
(
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    book_id INT NOT NULL,
    borrow_date DATE NOT NULL DEFAULT GETDATE(),
    due_date DATE NOT NULL,
    return_date DATE NULL,
    status VARCHAR(50) NOT NULL DEFAULT 'Lending',

    CONSTRAINT FK_Borrow_User FOREIGN KEY (user_id) REFERENCES USERS(id),
    CONSTRAINT FK_Borrow_Book FOREIGN KEY (book_id) REFERENCES BOOKS(id)
);
ALTER Table USERS add PhoneNumber NVARCHAR(20),
    Address NVARCHAR(255),
    Gender NVARCHAR(10),
    Status NVARCHAR(20)
ALTER Table USERS add CCCD NVARCHAR(20)
select * from USERS
INSERT INTO USERS (email, username, password, PhoneNumber, Address, Gender, Status, CCCD) VALUES ('c', 'c', 'c', 'c', 'c', 'Male', 'Active', '1122');
Alter Table BOOKS add Quantity int null
select * from BOOKS

insert into Borrow_Return (user_id, book_id, borrow_date, due_date, return_date, status) Values (1, 5, '2025-02-28','2025-03-05','2025-03-08','Lending');
select * from Borrow_Return
insert into Borrow_Return (user_id, book_id, borrow_date, due_date, return_date, status) 
Values (1, 5, '2025-02-28','2025-03-05', NULL, 'Lending');
