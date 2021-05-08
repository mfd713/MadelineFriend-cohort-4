use RCTTC;

SELECT* From Performance Where ShowDate BETWEEN '2021-10-01' AND '2021-12-31';

SELECT * from Customer;

SELECT * from Customer where Email LIKE '%.com';

SELECT distinct TOP(3) Price, ShowTitle,ShowDate from Ticket ORDER by Price asc;

Select distinct c.FirstName, c.LastName, c.CustomerId, t.ShowTitle, t.ShowDate from Customer c 
left join Ticket t on c.CustomerId = t.CustomerId;

Select th.Name as Theater, t.ShowTitle, t.Seat, c.FirstName, c.LastName from Ticket t
    inner join Customer c on c.CustomerId = t.CustomerId
    inner join Theater th on th.TheaterId = t.TheaterId;

SELECT * from Customer Where [Address] is null;

SELECT c.FirstName customer_first, c.LastName customer_last, c.Email customer_email,
c.Email customer_email, c.Address customer_address, t.Seat, t.ShowTitle show,
t.Price ticket_price, t.ShowDate date, th.Name theater, th.Address theater_address,
th.Phone theater_phone, th.Email theater_email from Customer c
    inner join Ticket t on t.CustomerId = c.CustomerId
    inner join Theater th on t.TheaterId = th.TheaterId;

Select t.CustomerId, c.FirstName, c.LastName, Count(Seat) TicketsPurchased From Ticket t
    left join Customer c on c.CustomerId = t.CustomerId
GROUP BY t.CustomerId, c.FirstName, c.LastName;

Select ShowTitle, Sum(Revenue) TotalRevenue From ( select ShowTitle, Count(Seat) TicketsSold, Price*Count(Seat) Revenue
from Ticket GROUP by ShowTitle, Price) sales
GROUP By ShowTitle;

Select TheaterName, Sum(Revenue) from(Select ShowTitle, th.Name as TheaterName, Count(t.Seat)*t.Price Revenue from Ticket t 
    left join Theater th on th.TheaterId =t.TheaterId
    GROUP By ShowTitle, th.Name, t.Price) sales
    GROUP By TheaterName;


Select top(1) ID, First, Last, Sum(TotalSpent) TotalSpent From(Select t.CustomerId ID, c.FirstName First, c.LastName Last, Count(Seat) TicketsPurchased,
t.Price*Count(Seat) as TotalSpent From Ticket t
    left join Customer c on c.CustomerId = t.CustomerId
GROUP BY t.CustomerId, c.FirstName, c.LastName, t.Price) spent
Group By ID, First, Last
Order By TotalSpent desc;