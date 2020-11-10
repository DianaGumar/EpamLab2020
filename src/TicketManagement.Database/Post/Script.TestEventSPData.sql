--- Venue
insert into dbo.Venue
values ('TestEvent venue', 'TestEvent venue address', '123 45 678 90 10', 5, 5)

--- Layout
insert into dbo.TMLayout
values (1, 'First TestEvent layout'),
(1, 'Second TestEvent layout')

--- Area
insert into dbo.Area
values (1, 'First area of first layout', 0, 0),
(1, 'Second area of first layout', 0, 1),
(2, 'First area of second layout', 0, 0),
(2, 'Second area of second layout', 4, 0)

--- Seat
insert into dbo.Seat
values 
(1, 1, 1),
(1, 1, 2),
(1, 1, 3),
(1, 1, 4),
(1, 1, 5),

(2, 1, 1),
(2, 1, 2),
(2, 1, 3),
(2, 1, 4),
(2, 1, 5),

(3, 1, 1),
(3, 1, 2),
(3, 2, 1),
(3, 2, 2),
(3, 3, 1),
(3, 3, 2),

(4, 1, 1),
(4, 1, 2),
(4, 2, 1),
(4, 2, 2),
(4, 3, 1),
(4, 3, 2)


-- event

insert into TMEvent
values 
('Sunny party', 'hot' , 1, '2020-11-07 01:25:00.000', '2020-11-08 01:25:00.000', 'https://as1.ftcdn.net/jpg/02/38/75/90/500_F_238759091_FroUBmssuq7M9IpuJ8vnIXOeEKXCwaA1.jpg'),
('Big music event', 'Every summer, when it isnt having a "fallow year" to give locals a break, the Big Daddy of UK festivals commandeers a chunk of Somerset for music, mischief, hippies, healing and a whole lot of cider.',1,'2020-11-10 01:25:00.000', '2020-11-11 01:25:00.000', 'https://as1.ftcdn.net/jpg/02/22/61/82/500_F_222618245_U3An26F7USS1uioX7PKUPQ6KwgFbSRfn.jpg'),
( 'Open cinema','Long ago, in the fantasy world of Kumandra, humans and dragons lived together in harmony. But when an evil force threatened the land, the dragons sacrificed themselves to save humanity. Now, 500 years later, that same evil has returned and its up to a lone warrior, Raya, to track down the legendary last dragon to restore the fractured land and its divided people.', 1,'2020-11-08 01:30:00.000', '2020-11-08 02:00:00.000', 'https://as1.ftcdn.net/jpg/03/66/78/60/500_F_366786068_Xtr7VygkxtlbWmtUIp5qYxIg9dfS9EhW.jpg')

insert into dbo.TMEventArea
values (1, 'First area of first layout', 0, 0, 0),
(2, 'First area of first layout', 0, 0, 0),
(3, 'First area of first layout', 0, 0, 0)

insert into dbo.TMEventSeat
values 
(1, 1, 1, 0),
(1, 1, 2, 0),
(1, 1, 3, 0),
(1, 1, 4, 0),
(1, 1, 5, 0),

(2, 1, 1, 0),
(2, 1, 2, 0),
(2, 1, 3, 0),
(2, 1, 4, 0),
(2, 1, 5, 0),

(3, 1, 1, 0),
(3, 1, 2, 0),
(3, 1, 3, 0),
(3, 1, 4, 0),
(3, 1, 5, 0)