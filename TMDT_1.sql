--use master
--drop database DBBanHangTMDT
--g


create database DBBanHangTMDT
go
use DbBanHangTMDT
go
create table account(
 account_id int primary key identity,
 first_name nvarchar(100),
 last_name nvarchar(100),
 email varchar(100),
 password varchar(100),
 address nvarchar(100),
 [role] int,
 phone_number varchar(100),
 ngayTao datetime2 not null default(getdate())
)
go

create table Category (
 category_id int primary key identity,
 name nvarchar(100),
  ngayTao  datetime2 not null default(getdate())
)
go
create table Product (
 product_id int primary key identity,
 productCode varchar(100),
 SKU varchar(100),
 description nvarchar(100),
 price decimal(10,2),
 name nvarchar(200),
 size varchar(10), --S M L XL XXL XXXL
 color int, --1.đỏ 2.cam 3.vàng 4.xanh nước biển   5.tím 6.trắng 7.đen
 quantity int,
 unique(productCode, size, color),
 stock int,
 Category_category_id int,
 Image varchar(500),
  ngayTao datetime2 not null default(getdate()),
 constraint Product_Category foreign key (Category_category_id) references Category(category_id)
)

go
create table Payment(
 payment_id int primary key identity,
 payment_method nvarchar(100),
 ngayTao datetime2 not null default(getdate()),
)

go

create table shipment (
 shipment_id int primary key identity,
 shipment_date datetime2,
 address nvarchar(100),
 city nvarchar(100),
 state nvarchar(20),
 country nvarchar(50),
 zip_code varchar(10),
 account_account_id int,
 ngayTao datetime2 not null default(getdate()),
 constraint shipment_account foreign key (account_account_id) references account(account_id)
)
go
create table [Order](
 order_id int primary key identity,
 total_price decimal(10,2),
 account_account_id int,
 Payment_payment_id int,
 Shipment_shipment_id int,
 ngayTao datetime2 not null default(getdate()),
 constraint order_account foreign key (account_account_id) references account(account_id),
 constraint order_payment foreign key (payment_payment_id) references Payment(payment_id),
 constraint order_shipment foreign key (Shipment_shipment_id) references shipment(shipment_id),
)
go
create table order_item(
 order_item_id int identity,
 quantity int,
 price decimal(10,2),
 product_product_id int,
 order_order_id int,
  ngayTao datetime2 not null default(getdate()),
 primary key (order_item_id, order_order_id),
 constraint order_item_product foreign key (product_product_id) references product(product_id),
 constraint order_item_order foreign key (order_order_id) references [order](order_id),
)
go
create table cart (
 cart_id int,
 quantity int,
 account_account_id int,
 product_product_id int,
  ngayTao datetime2 not null default(getdate()),
 primary key (cart_id, account_account_id),
 constraint cart_account foreign key (account_account_id) references account(account_id),
 constraint cart_product foreign key (product_product_id) references product(product_id),
)
go
create table wishlist(
 wishlist_id int identity,
 account_account_id int,
 product_product_id int,
  ngayTao datetime2 not null default(getdate()),
 primary key (wishlist_id, account_account_id),
  constraint wishlist_account foreign key (account_account_id) references account(account_id),
   constraint wishlist_product foreign key (product_product_id) references product(product_id),
)

--insert into cart
--values
--(1, null, 1	, 1),
--(2, null, 1	, 2),
--(3, null, 1	, 3),
--(4, null, 1	, 4),
--(5, null, 2	, 5),
--(6, null, 2 , 6),
--(7, null, 3	, 7)

--insert into [order]
--values
--(1, GETDATE(), 690000, 1, null, null),
--(2, GETDATE(), 200000, 2, null, null)

--in
--update cart 
--set quantity = 2
--where quantity is null

--select * from cart