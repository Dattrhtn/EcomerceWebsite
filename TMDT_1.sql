--use master
--drop database DBBanHangTMDT
--go
create database DBBanHangTMDT
go
use DbBanHangTMDT
go
create table customer(
 customer_id int primary key,
 first_name varchar(100),
 last_name varchar(100),
 email varchar(100),
 password varchar(100),
 address varchar(100),
 phone_number varchar(100),
)
go
alter table customer
add [role] int
go
EXEC sp_rename 'customer', 'Account'
go

create table Category (
 category_id int primary key,
 name varchar(100),
)
go
create table Product (
 product_id int primary key,
 SKU varchar(100),
 description varchar(100),
 price decimal(10,2),
 stock int,
 Category_category_id int,
 Image varchar(500)
 constraint Product_Category foreign key (Category_category_id) references Category(category_id)
)

go
create table Payment(
 payment_id int primary key,
 payment_date datetime,
 payment_method varchar(100),
 amount decimal(10,2),
 customer_customer_id int,
 constraint payment_customer foreign key (customer_customer_id) references customer(customer_id)
)
create table shipment (
 shipment_id int primary key,
 shipment_date datetime,
 address varchar(100),
 city varchar(100),
 state varchar(20),
 country varchar(50),
 zip_code varchar(10),
 customer_customer_id int,
 constraint shipment_customer foreign key (customer_customer_id) references customer(customer_id)
)
go
create table [Order](
 order_id int primary key,
 order_date datetime,
 total_price decimal(10,2),
 Customer_customer_id int,
 Payment_payment_id int,
 Shipment_shipment_id int,
 constraint order_customer foreign key (Customer_customer_id) references customer(customer_id),
 constraint order_payment foreign key (payment_payment_id) references Payment(payment_id),
 constraint order_shipment foreign key (Shipment_shipment_id) references shipment(shipment_id)
)
go
create table order_item(
 order_item_id int,
 quantity int,
 price decimal(10,2),
 product_product_id int,
 order_order_id int,
 primary key (order_item_id, order_order_id),
 constraint order_item_product foreign key (product_product_id) references product(product_id),
 constraint order_item_order foreign key (order_order_id) references [order](order_id),
)
go
create table cart (
 cart_id int,
 quantity int,
 customer_customer_id int,
 product_product_id int,
 primary key (cart_id, customer_customer_id),
 constraint cart_customer foreign key (Customer_customer_id) references customer(customer_id),
 constraint cart_product foreign key (product_product_id) references product(product_id),
)
go
create table wishlist(
 wishlist_id int,
 Customer_customer_id int,
 product_product_id int,
 primary key (wishlist_id, Customer_customer_id),
  constraint wishlist_customer foreign key (Customer_customer_id) references customer(customer_id),
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