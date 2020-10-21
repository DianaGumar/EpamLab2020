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

