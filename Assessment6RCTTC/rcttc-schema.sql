use master;
go
drop database if exists RCTTC;
GO
create database RCTTC;
go
use RCTTC;

--ALTER DATABASE RCTTC Set SINGLE_USER With ROLLBACK IMMEDIATE
create TABLE Customer (
  CustomerId int PRIMARY key IDENTITY(1,1),
  FirstName VARCHAR(50) not null,
  LastName VARCHAR(50) not null,
  Email VARCHAR(50) not null,
  Phone VARCHAR(20) null,
  Address VARCHAR(100) null
);

CREATE table Theater(
  TheaterId int PRIMARY key IDENTITY(1,1),
  [Name] VARCHAR(50) not null,
  Address VARCHAR(100) not null,
  Phone CHAR(14) not null DEFAULT '(651) 555-5555',
  Email VARCHAR(50) not null
);

create TABLE Performance(
  ShowTitle VARCHAR(100) not null,
  ShowDate DATE not null,
  TheaterId int not null,
  CONSTRAINT pk_ShowDateTheater
    PRIMARY KEY(ShowTitle,ShowDate,TheaterId),
  CONSTRAINT fk_Theater_Performance_TheaterId
    FOREIGN key (TheaterId)
    REFERENCES Theater(TheaterId)
);

create TABLE Ticket(
  TicketId int PRIMARY key IDENTITY(1,1),
  Price DECIMAL not null,
  ShowTitle VARCHAR(100) not null,
  ShowDate date not null,
  TheaterId int not null,
  Seat char(3) not null,
  CustomerId int not null,
  CONSTRAINT fk_Performance_Ticket_PK
    FOREIGN key (ShowTitle, ShowDate, TheaterId)
    REFERENCES Performance(ShowTitle, ShowDate, TheaterId),
  CONSTRAINT fk_Customer_Ticket_CustomerId
    FOREIGN key (CustomerId)
    REFERENCES Customer(CustomerId)
);