USE [master]
GO
/****** Object:  Database [demo]    Script Date: 04/11/2019 11:33:06 ******/
IF EXISTS (SELECT name FROM sysdatabases WHERE name = 'demo')
	DROP DATABASE [demo]
GO
CREATE DATABASE [demo]
GO
USE [demo]
GO
CREATE TABLE [dbo].[tbl_Slider](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[meta_name] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Publisher]    Script Date: 04/11/2019 11:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Publisher](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[meta_name] [varchar](100) NULL,
	[amount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Customer]    Script Date: 04/11/2019 11:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[pass] [varchar](200) NOT NULL,
	[name] [nvarchar](100) NULL,
	[email] [varchar](100) NULL,
	[phone] [varchar](20) NULL,
	[cus_address] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Admin]    Script Date: 04/11/2019 11:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Admin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](20) NOT NULL,
	[pass] [varchar](200) NOT NULL,
	[name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Bill]    Script Date: 04/11/2019 11:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[level_status] [tinyint] NOT NULL,
	[cus_id] [int] NOT NULL,
	[created_at] [datetime] NOT NULL,
	[total_price] [int] NOT NULL,
	[cus_name] [nvarchar](100) NULL,
	[cus_address] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Category]    Script Date: 04/11/2019 11:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pub_id] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[meta_name] [varchar](100) NULL,
	[amount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Product]    Script Date: 04/11/2019 11:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cate_id] [int] NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[meta_name] [varchar](100) NULL,
	[img] [varchar](100) NOT NULL,
	[rate] [tinyint] NOT NULL,
	[amount] [int] NOT NULL,
	[price] [int] NOT NULL,
	[discount] [int] NOT NULL,
	[get_at] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Cart]    Script Date: 04/11/2019 11:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cus_id] [int] NOT NULL,
	[pro_id] [int] NOT NULL,
	[amount] [int] NOT NULL,
	[total_price] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_BillDetail]    Script Date: 04/11/2019 11:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BillDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bill_id] [int] NOT NULL,
	[pro_id] [int] NOT NULL,
	[amount] [int] NOT NULL,
	[price] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF__tbl_Publi__amoun__08EA5793]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Publisher] ADD  DEFAULT ((0)) FOR [amount]
GO
/****** Object:  Default [DF__tbl_Bill__level___1BFD2C07]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Bill] ADD  DEFAULT ((-1)) FOR [level_status]
GO
/****** Object:  Default [DF__tbl_Bill__create__1CF15040]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Bill] ADD  DEFAULT (getdate()) FOR [created_at]
GO
/****** Object:  Default [DF__tbl_Bill__total___1DE57479]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Bill] ADD  DEFAULT ((0)) FOR [total_price]
GO
/****** Object:  Default [DF__tbl_Categ__amoun__0DAF0CB0]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Category] ADD  DEFAULT ((0)) FOR [amount]
GO
/****** Object:  Default [DF__tbl_Produc__rate__1367E606]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Product] ADD  DEFAULT ((0)) FOR [rate]
GO
/****** Object:  Default [DF__tbl_Produ__amoun__145C0A3F]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Product] ADD  DEFAULT ((0)) FOR [amount]
GO
/****** Object:  Default [DF__tbl_Produ__price__15502E78]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Product] ADD  DEFAULT ((0)) FOR [price]
GO
/****** Object:  Default [DF__tbl_Produ__disco__164452B1]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Product] ADD  DEFAULT ((0)) FOR [discount]
GO
/****** Object:  Default [DF__tbl_Cart__amount__2B3F6F97]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Cart] ADD  DEFAULT ((0)) FOR [amount]
GO
/****** Object:  Default [DF__tbl_Cart__total___2C3393D0]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Cart] ADD  DEFAULT ((0)) FOR [total_price]
GO
/****** Object:  Default [DF__tbl_BillD__amoun__5FB337D6]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_BillDetail] ADD  DEFAULT ((1)) FOR [amount]
GO
/****** Object:  Default [DF__tbl_BillD__price__60A75C0F]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_BillDetail] ADD  DEFAULT ((0)) FOR [price]
GO
/****** Object:  ForeignKey [fk_customer_bill]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Bill]  WITH CHECK ADD  CONSTRAINT [fk_customer_bill] FOREIGN KEY([cus_id])
REFERENCES [dbo].[tbl_Customer] ([id])
GO
ALTER TABLE [dbo].[tbl_Bill] CHECK CONSTRAINT [fk_customer_bill]
GO
/****** Object:  ForeignKey [fk_pub_cate]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Category]  WITH CHECK ADD  CONSTRAINT [fk_pub_cate] FOREIGN KEY([pub_id])
REFERENCES [dbo].[tbl_Publisher] ([id])
GO
ALTER TABLE [dbo].[tbl_Category] CHECK CONSTRAINT [fk_pub_cate]
GO
/****** Object:  ForeignKey [fk_cate_pro]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Product]  WITH CHECK ADD  CONSTRAINT [fk_cate_pro] FOREIGN KEY([cate_id])
REFERENCES [dbo].[tbl_Category] ([id])
GO
ALTER TABLE [dbo].[tbl_Product] CHECK CONSTRAINT [fk_cate_pro]
GO
/****** Object:  ForeignKey [fk_customer_cart]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Cart]  WITH CHECK ADD  CONSTRAINT [fk_customer_cart] FOREIGN KEY([cus_id])
REFERENCES [dbo].[tbl_Customer] ([id])
GO
ALTER TABLE [dbo].[tbl_Cart] CHECK CONSTRAINT [fk_customer_cart]
GO
/****** Object:  ForeignKey [fk_product_cart]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_Cart]  WITH CHECK ADD  CONSTRAINT [fk_product_cart] FOREIGN KEY([pro_id])
REFERENCES [dbo].[tbl_Product] ([id])
GO
ALTER TABLE [dbo].[tbl_Cart] CHECK CONSTRAINT [fk_product_cart]
GO
/****** Object:  ForeignKey [fk_bill_detail]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_BillDetail]  WITH CHECK ADD  CONSTRAINT [fk_bill_detail] FOREIGN KEY([bill_id])
REFERENCES [dbo].[tbl_Bill] ([id])
GO
ALTER TABLE [dbo].[tbl_BillDetail] CHECK CONSTRAINT [fk_bill_detail]
GO
/****** Object:  ForeignKey [fk_product_detail]    Script Date: 04/11/2019 11:33:06 ******/
ALTER TABLE [dbo].[tbl_BillDetail]  WITH CHECK ADD  CONSTRAINT [fk_product_detail] FOREIGN KEY([pro_id])
REFERENCES [dbo].[tbl_Product] ([id])
GO
ALTER TABLE [dbo].[tbl_BillDetail] CHECK CONSTRAINT [fk_product_detail]
GO
INSERT INTO [dbo].[tbl_Admin] (username,pass,name) VALUES
	('hien1997','hien123456789','Nguyen Thanh Hien');
INSERT INTO [dbo].[tbl_Publisher] (name, meta_name,amount) VALUES
	('Apple','apple',3),
	('SamSung','samsung',5),
	('Huawei','huawei',3),
	('Oppo','oppo',2),
	('Xiaomi','xiaomi',6);
INSERT INTO [dbo].[tbl_Category] (pub_id,name,meta_name,amount) VALUES
	('1','Iphone X','iphone-x',1),
	('1','Iphone 8','iphone-8',2),
	('2','Galaxy A5','samsung-galaxy-a5',0),
	('2','Galaxy A6','samsung-galaxy-a6',0),
	('2','Galaxy A7','samsung-galaxy-a7',1),
	('4','Oppo F7','oppo-f7',0),
	('3','Huawei Honor 7A','huawei-honor-7a',1),
	('3','Huawei Honor 7C','huawei-honor-7c',0),
	('5','Xiaomi Mi A2','xiaomi-mi-a2',0),
	('5','Xiaomi Redmi 6','xiaomi-redmi-6',0),
	('5','Xiaomi Redmi 6 Pro','xiaomi-redmi-6-pro',0),
	('5','Xiaomi Redmi Note 5','xiaomi-redmi-note-5',3),
	('4','Oppo F3 Lite','oppo-f3-lite',2),
	('3','Huawei Honor P20 Pro','huawei-honor-p20-pro',2),
	('5','Xiaomi Redmi Note 5 Pro','xiaomi-redmi-note-5-pro',4),
	('1','Iphone 7 Plus','iphone-7-plus',2),
	('2','Galaxy S7','samsung-galaxy-s7',1),
	('2','Galaxy S7 Edge','samsung-galaxy-s7',1),
	('5','Xiaomi Mi Max','xiaomi-mi-max',1);
INSERT INTO [dbo].[tbl_Product] (cate_id,name,meta_name,img,rate,amount,price,discount,get_at) VALUES
	(1,'Iphone X Adidas','iphone-x-adidas','iphone-x-adidas-510x510.png',0,98,200000,0,'2018-11-21 00:00:00.000'),
	(2,'Iphone 8 Love','iphone-8-love','iphone-8-love-510x510.png',0,1,200000,150000,'2019-03-28 00:00:00.000'),
	(2,'Iphone 8 Vintage Bus','iphone-8-vintage-bus','iphone-8-vintage-bus-510x510.png',1,9,500000,420000,'2019-03-28 00:00:00.000'),
	(5,'Samsung Galaxy A7 Digital Abstract','samsung-gal-a7-digital-abstract','samsung-gal-a7-digital-abstract-510x510.png',0,5,200000,0,'2019-03-28 00:00:00.000'),
	(7,'Huawei Honor 7A Owl','huawei-honor-7a-owl','huawei-honor-7a-owl-510x510.png',0,0,200000,0,'2019-03-28 00:00:00.000'),
	(12,'Xiaomi Redmi Note 5 Pebbles','xiaomi-redmi-note5-pebbles','xiaomi-redmi-note5-pebbles-510x510.png',0,0,370000,0,'2019-03-28 00:00:00.000'),
	(12,'Xiaomi Redmi Note 5 Flowers','xiaomi-redmi-note5-flowers','xiaomi-redmi-note5-flowers-510x510.png',1,8,430000,300000,'2019-03-28 00:00:00.000'),
	(12,'Xiaomi Redmi Note 5 Roses','xiaomi-redmi-note5-roses','xiaomi-redmi-note5-roses-510x510.png',0,0,500000,0,'2019-03-28 00:00:00.000'),
	(13,'Oppo F3 Lite Deer','oppo-f3-lite-deer','oppo-f3-lite-deer-510x510.png',1,5,500000,340000,'2019-03-28 00:00:00.000'),
	(13,'Oppo F3 Lite Wolf','oppo-f3-lite-wolf','oppo-f3-lite-wolf-510x510.png',0,4,400000,210000,'2019-03-28 00:00:00.000'),
	(14,'Huawei Honor P20 Pro Digital','huawei-honor-p20-pro-digital','huawei-honor-p20-pro-digital-510x510.png',1,1,700000,550000,'2019-03-28 00:00:00.000'),
	(14,'Huawei Honor P20 Pro Color','huawei-honor-p20-pro-coulor','huawei-honor-p20-pro-coulor-510x510.png',0,0,320000,0,'2019-03-28 00:00:00.000'),
	(15,'Xiaomi Redmi Note 5 Pro Painted','xiaomi-redmi-note5-pro-painted-abstract','xiaomi-redmi-note5-pro-painted-abstract-510x510.png',1,9,720000,600000,'2019-03-28 00:00:00.000'),
	(15,'Xiaomi Redmi Note 5 Pro','xiaomi-redmi-note5-pro-spring','xiaomi-redmi-note5-pro-spring-510x510.png',1,100,320000,0,'2019-03-28 00:00:00.000'),
	(15,'Xiaomi Redmi Note 5 Pro Green Vintage','xiaomi-redmi-note5-pro-green-vintage-floral','xiaomi-redmi-note5-pro-green-vintage-floral-510x510.png',1,2,400000,0,'2019-03-28 00:00:00.000'),
	(15,'Xiaomi Redmi Note 5 Pro Chris','xiaomi-redmi-note5-pro-chris','xiaomi-redmi-note5-pro-chris-510x510.png',0,5,250000,0,'2019-03-28 00:00:00.000'),
	(16,'Iphone 7 Plus Floral','iphone-7-plus-floral','iphone-7-plus-floral-510x510.png',1,120,450000,400000,'2019-03-28 00:00:00.000'),
	(16,'Iphone 7 Plus Polygon','iphone-7-plus-coulor-polygon','iphone-7-plus-coulor-polygon-510x510.png',0,7,300000,0,'2019-03-28 00:00:00.000'),
	(17,'Samsung Galaxy S7 Triangles','samsung-gal-s7-triangles','samsung-gal-s7-triangles-510x510.png',0,0,300000,0,'2019-03-28 00:00:00.000'),
	(18,'Samsung Galaxy S7 Edge Waves','samsung-gal-s7-edge-line-waves','samsung-gal-s7-edge-line-waves-510x510.png',1,6,450000,400000,'2019-03-28 00:00:00.000'),
	(18,'Samsung Galaxy S7 Edge Heart','samsung-gal-s7-edge-red-heart','samsung-gal-s7-edge-red-heart-510x510.png',0,0,300000,0,'2019-03-28 00:00:00.000'),
	(19,'Xiaomi Mi Max Color','xiaomi-mi-max-coulor','xiaomi-mi-max-coulor-510x510.png',0,0,260000,0,'2019-03-28 00:00:00.000');
INSERT INTO [dbo].[tbl_Slider] (name) VALUES
	('slide-1.jpg'),
	('slide-2.jpg'),
	('slide-3.jpg'),
	('slide-4.jpg');

