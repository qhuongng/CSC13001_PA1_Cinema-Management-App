create database cinemaManagement
 use cinemaManagement

CREATE TABLE [User] (
  id INT NOT NULL IDENTITY(1,1),
  userName VARCHAR(50) NOT NULL primary key,
  password VARCHAR(60) NOT NULL,
  DOB DATE NOT NULL,
  gender VARCHAR(10) NOT NULL,
  isAdmin BIT NOT NULL
);

create table [Movie] (
	id int not null identity(1,1),
	name VARCHAR(50) NOT NULL,

);
