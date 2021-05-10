use RCTTC;

insert into Theater ([Name],[Address],[Phone],Email)
    select distinct theater, theater_address, theater_phone, theater_email from [dbo].[rcttc-data];

begin TRAN
insert into Customer(FirstName,LastName,Email,Phone,[Address])
select distinct customer_first, customer_last, customer_email, customer_phone, customer_address from [dbo].[rcttc-data];
Commit

begin tran
insert into Performance(ShowTitle,ShowDate,TheaterId)
select distinct d.show, d.[date], t.TheaterId  from [dbo].[rcttc-data] d
left join Theater t on t.[Name] = d.theater;
COMMIT

begin TRAN
insert into Ticket(CustomerId,Price,Seat,ShowTitle,ShowDate,TheaterId)
select c.CustomerId, d.ticket_price, d. seat, d.show, d.[date], t.TheaterId  from [dbo].[rcttc-data] d
left join Theater t on t.[Name] = d.theater
left join Customer c on c.LastName = d.customer_last;
commit

begin tran
update Ticket set
 Price = 22.25
where ShowTitle = 'The Sky Lit Up' AND ShowDate = '2021-03-01'
commit

begin tran
update Ticket SET
    CustomerId = 87
where TicketId = 91
commit

update Ticket set
    CustomerId = 131
where TicketId = 95

update Ticket set
    CustomerId = 89
where TicketId = 97

begin TRAN
update Customer SET
    Phone = '1-801-EAT-CAKE'
where LastName = 'Swindles'
commit


SELECT CustomerId, Count(Seat) TicketsPurchased,ShowTitle,ShowDate
From Ticket t1
Where TheaterId = 1
GROUP by ShowTitle, ShowDate, CustomerId
Having Count(Seat) =1;

begin tran
delete from Ticket where (CustomerId in (SELECT CustomerId
From Ticket t1
Where TheaterId = 1
GROUP by CustomerId
Having Count(Seat) =1) AND ShowTitle = 'Send in the Clowns')

delete from Ticket where (CustomerId in (SELECT CustomerId
From Ticket t1
Where TheaterId = 1
GROUP by CustomerId
Having Count(Seat) =1) AND ShowTitle = 'Tell Me What To Think')

delete from Ticket where (CustomerId in (SELECT CustomerId
From Ticket t1
Where TheaterId = 1
GROUP by CustomerId
Having Count(Seat) =1) AND ShowTitle = 'The Dress')
commit

begin tran
delete from Ticket Where CustomerId = (SELECT CustomerID FROM Customer Where LastName = 'Egle of Germany');
delete from Customer where LastName = 'Egle of Germany';
commit