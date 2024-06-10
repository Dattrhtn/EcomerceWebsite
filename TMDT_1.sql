USE [master]
GO
/****** Object:  Database [DBBanHangTMDT]    Script Date: 06/06/2024 7:18:11 CH ******/
CREATE DATABASE [DBBanHangTMDT]

GO
ALTER DATABASE [DBBanHangTMDT] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBBanHangTMDT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBBanHangTMDT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DBBanHangTMDT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBBanHangTMDT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBBanHangTMDT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBBanHangTMDT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBBanHangTMDT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBBanHangTMDT] SET  MULTI_USER 
GO
ALTER DATABASE [DBBanHangTMDT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBBanHangTMDT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBBanHangTMDT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBBanHangTMDT] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBBanHangTMDT] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBBanHangTMDT] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DBBanHangTMDT] SET QUERY_STORE = OFF
GO
USE [DBBanHangTMDT]
GO
/****** Object:  Table [dbo].[account]    Script Date: 06/06/2024 7:18:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[account](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](100) NULL,
	[last_name] [nvarchar](100) NULL,
	[email] [varchar](100) NULL,
	[password] [varchar](100) NULL,
	[address] [nvarchar](100) NULL,
	[role] [int] NULL,
	[phone_number] [varchar](100) NULL,
	[ngayTao] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__account__46A222CDFF4E00BA] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 06/06/2024 7:18:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[cart_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[account_account_id] [int] NOT NULL,
	[product_product_id] [int] NULL,
	[ngayTao] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__cart__D1365B6D071BDB9A] PRIMARY KEY CLUSTERED 
(
	[cart_id] ASC,
	[account_account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 06/06/2024 7:18:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[ngayTao] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Category__D54EE9B4945B45BA] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 06/06/2024 7:18:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[total_price] [decimal](10, 2) NULL,
	[account_account_id] [int] NULL,
	[Payment_payment_id] [int] NULL,
	[Shipment_shipment_id] [int] NULL,
	[ngayTao] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Order__4659622970897440] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_item]    Script Date: 06/06/2024 7:18:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_item](
	[order_item_id] [int] IDENTITY(1,1) NOT NULL,
	[quantity] [int] NULL,
	[price] [decimal](10, 2) NULL,
	[product_product_id] [int] NULL,
	[order_order_id] [int] NOT NULL,
	[ngayTao] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__order_it__A9DF22CDDB1BC28E] PRIMARY KEY CLUSTERED 
(
	[order_item_id] ASC,
	[order_order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 06/06/2024 7:18:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[payment_method] [nvarchar](100) NULL,
	[ngayTao] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Payment__ED1FC9EA439D3D5C] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 06/06/2024 7:18:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[productCode] [varchar](100) NULL,
	[SKU] [varchar](100) NULL,
	[description] [nvarchar](max) NULL,
	[price] [decimal](10, 2) NULL,
	[name] [nvarchar](200) NULL,
	[size] [varchar](10) NULL,
	[color] [int] NULL,
	[quantity] [int] NULL,
	[stock] [int] NULL,
	[Category_category_id] [int] NULL,
	[Image] [varchar](500) NULL,
	[ngayTao] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Product__47027DF5E1A5B9CF] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Product__E96EB9A1AE113945] UNIQUE NONCLUSTERED 
(
	[productCode] ASC,
	[size] ASC,
	[color] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shipment]    Script Date: 06/06/2024 7:18:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shipment](
	[shipment_id] [int] IDENTITY(1,1) NOT NULL,
	[shipment_date] [datetime2](7) NULL,
	[address] [nvarchar](255) NULL,
	[city] [nvarchar](100) NULL,
	[state] [nvarchar](255) NULL,
	[country] [nvarchar](50) NULL,
	[zip_code] [varchar](10) NULL,
	[account_account_id] [int] NULL,
	[ngayTao] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__shipment__41466E59DE04214D] PRIMARY KEY CLUSTERED 
(
	[shipment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[wishlist]    Script Date: 06/06/2024 7:18:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wishlist](
	[wishlist_id] [int] IDENTITY(1,1) NOT NULL,
	[account_account_id] [int] NOT NULL,
	[product_product_id] [int] NULL,
	[ngayTao] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__wishlist__9E9220044B50615E] PRIMARY KEY CLUSTERED 
(
	[wishlist_id] ASC,
	[account_account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[account] ADD  CONSTRAINT [DF__account__ngayTao__24927208]  DEFAULT (getdate()) FOR [ngayTao]
GO
ALTER TABLE [dbo].[cart] ADD  CONSTRAINT [DF__cart__ngayTao__412EB0B6]  DEFAULT (getdate()) FOR [ngayTao]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF__Category__ngayTa__276EDEB3]  DEFAULT (getdate()) FOR [ngayTao]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF__Order__ngayTao__36B12243]  DEFAULT (getdate()) FOR [ngayTao]
GO
ALTER TABLE [dbo].[order_item] ADD  CONSTRAINT [DF__order_ite__ngayT__3C69FB99]  DEFAULT (getdate()) FOR [ngayTao]
GO
ALTER TABLE [dbo].[Payment] ADD  CONSTRAINT [DF__Payment__ngayTao__2F10007B]  DEFAULT (getdate()) FOR [ngayTao]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__ngayTao__2B3F6F97]  DEFAULT (getdate()) FOR [ngayTao]
GO
ALTER TABLE [dbo].[shipment] ADD  CONSTRAINT [DF__shipment__ngayTa__32E0915F]  DEFAULT (getdate()) FOR [ngayTao]
GO
ALTER TABLE [dbo].[wishlist] ADD  CONSTRAINT [DF__wishlist__ngayTa__45F365D3]  DEFAULT (getdate()) FOR [ngayTao]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [cart_account] FOREIGN KEY([account_account_id])
REFERENCES [dbo].[account] ([account_id])
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [cart_account]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [cart_product] FOREIGN KEY([product_product_id])
REFERENCES [dbo].[Product] ([product_id])
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [cart_product]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [order_account] FOREIGN KEY([account_account_id])
REFERENCES [dbo].[account] ([account_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [order_account]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [order_payment] FOREIGN KEY([Payment_payment_id])
REFERENCES [dbo].[Payment] ([payment_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [order_payment]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [order_shipment] FOREIGN KEY([Shipment_shipment_id])
REFERENCES [dbo].[shipment] ([shipment_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [order_shipment]
GO
ALTER TABLE [dbo].[order_item]  WITH CHECK ADD  CONSTRAINT [order_item_order] FOREIGN KEY([order_order_id])
REFERENCES [dbo].[Order] ([order_id])
GO
ALTER TABLE [dbo].[order_item] CHECK CONSTRAINT [order_item_order]
GO
ALTER TABLE [dbo].[order_item]  WITH CHECK ADD  CONSTRAINT [order_item_product] FOREIGN KEY([product_product_id])
REFERENCES [dbo].[Product] ([product_id])
GO
ALTER TABLE [dbo].[order_item] CHECK CONSTRAINT [order_item_product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_Category] FOREIGN KEY([Category_category_id])
REFERENCES [dbo].[Category] ([category_id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_Category]
GO
ALTER TABLE [dbo].[shipment]  WITH CHECK ADD  CONSTRAINT [shipment_account] FOREIGN KEY([account_account_id])
REFERENCES [dbo].[account] ([account_id])
GO
ALTER TABLE [dbo].[shipment] CHECK CONSTRAINT [shipment_account]
GO
ALTER TABLE [dbo].[wishlist]  WITH CHECK ADD  CONSTRAINT [wishlist_account] FOREIGN KEY([account_account_id])
REFERENCES [dbo].[account] ([account_id])
GO
ALTER TABLE [dbo].[wishlist] CHECK CONSTRAINT [wishlist_account]
GO
ALTER TABLE [dbo].[wishlist]  WITH CHECK ADD  CONSTRAINT [wishlist_product] FOREIGN KEY([product_product_id])
REFERENCES [dbo].[Product] ([product_id])
GO
ALTER TABLE [dbo].[wishlist] CHECK CONSTRAINT [wishlist_product]
GO
USE [master]
GO
ALTER DATABASE [DBBanHangTMDT] SET  READ_WRITE 
GO
alter table cart
drop constraint [PK__cart__D1365B6D071BDB9A]

alter table cart
add constraint [PK_cart]  primary Key (cart_id)
USE [DBBanHangTMDT]
GO

-- Insert sample data into account table
INSERT INTO [dbo].[account] (first_name, last_name, email, [password], [address], [role], phone_number, ngayTao)
VALUES 
('John', 'Doe', 'john.doe@example.com', 'password123', '123 Main St', 1, '555-1234', GETDATE()),
('Jane', 'Smith', 'jane.smith@example.com', 'password123', '456 Elm St', 2, '555-5678', GETDATE());

-- Insert sample data into Category table
INSERT INTO [dbo].[Category] ([name], ngayTao)
VALUES 
('Electronics', GETDATE()),
('Books', GETDATE());

-- Insert sample data into Product table
INSERT INTO [dbo].[Product] (productCode, SKU, description, price, [name], [size], color, quantity, stock, Category_category_id, Image, ngayTao)
VALUES 
('ELEC123', 'SKU123', 'Smartphone with 5G', 699.99, 'Smartphone', 'M', 1, 100, 50, 1, 'image1.jpg', GETDATE()),
('BOOK456', 'SKU456', 'Science Fiction Novel', 19.99, 'Sci-Fi Novel', 'N/A', 2, 200, 100, 2, 'image2.jpg', GETDATE());

-- Insert sample data into Payment table
INSERT INTO [dbo].[Payment] (payment_method, ngayTao)
VALUES 
('Credit Card', GETDATE()),
('PayPal', GETDATE());

-- Insert sample data into shipment table
INSERT INTO [dbo].[shipment] (shipment_date, [address], city, [state], country, zip_code, account_account_id, ngayTao)
VALUES 
(GETDATE(), '123 Main St', 'New York', 'NY', 'USA', '10001', 1, GETDATE()),
(GETDATE(), '456 Elm St', 'Los Angeles', 'CA', 'USA', '90001', 2, GETDATE());

-- Insert sample data into [Order] table
INSERT INTO [dbo].[Order] (total_price, account_account_id, Payment_payment_id, Shipment_shipment_id, ngayTao)
VALUES 
(719.98, 1, 1, 1, GETDATE()),
(39.98, 2, 2, 2, GETDATE());

-- Insert sample data into order_item table
INSERT INTO [dbo].[order_item] (quantity, price, product_product_id, order_order_id, ngayTao)
VALUES 
(1, 699.99, 1, 1, GETDATE()),
(2, 19.99, 2, 2, GETDATE());

-- Insert sample data into cart table
INSERT INTO [dbo].[cart] (cart_id, quantity, account_account_id, product_product_id, ngayTao)
VALUES 
(1, 2, 1, 1, GETDATE()),
(2, 1, 2, 2, GETDATE());

-- Insert sample data into wishlist table
INSERT INTO [dbo].[wishlist] (account_account_id, product_product_id, ngayTao)
VALUES 
(1, 1, GETDATE()),
(2, 2, GETDATE());

GO
