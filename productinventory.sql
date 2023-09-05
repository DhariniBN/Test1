/*********CONNECTED DATA MODEL FOR PRODUCT INVENTORY************/
create table vendor(
vndr_id int primary key identity(100,1),
vndr_name nvarchar(50) NOT NULL)

select * from vendor

insert into  vendor values('ram')
insert into  vendor values('sham')
insert into  vendor values('raju')
insert into  vendor values('jaggu')

create table product (
prdt_id int primary key identity(1,1),
prdt_name nvarchar(50) NOT NULL,
vndr_id int foreign key references vendor(vndr_id))

select * from product

create table purchase(
pur_id int primary key identity(1,1),
vndr_id int foreign key references vendor(vndr_id))

create table purchaseitems(
puritemid int primary key identity(1,1),
pur_id int foreign key references purchase(pur_id))


