USE [Product Management]
GO
/****** Object:  StoredProcedure [dbo].[sp_UserLogin]    Script Date: 19-04-2024 10:31:11 PM ******/
DROP PROCEDURE [dbo].[sp_UserLogin]
GO
/****** Object:  StoredProcedure [dbo].[sp_UserDetails]    Script Date: 19-04-2024 10:31:11 PM ******/
DROP PROCEDURE [dbo].[sp_UserDetails]
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Details]    Script Date: 19-04-2024 10:31:11 PM ******/
DROP PROCEDURE [dbo].[sp_Product_Details]
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Completed]    Script Date: 19-04-2024 10:31:11 PM ******/
DROP PROCEDURE [dbo].[sp_Order_Completed]
GO
/****** Object:  StoredProcedure [dbo].[sp_Get_User_Order_Details]    Script Date: 19-04-2024 10:31:11 PM ******/
DROP PROCEDURE [dbo].[sp_Get_User_Order_Details]
GO
/****** Object:  StoredProcedure [dbo].[sp_Get_Order_Details]    Script Date: 19-04-2024 10:31:11 PM ******/
DROP PROCEDURE [dbo].[sp_Get_Order_Details]
GO
/****** Object:  StoredProcedure [dbo].[sp_Cart_Details]    Script Date: 19-04-2024 10:31:11 PM ******/
DROP PROCEDURE [dbo].[sp_Cart_Details]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19-04-2024 10:31:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 19-04-2024 10:31:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 19-04-2024 10:31:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cart]') AND type in (N'U'))
DROP TABLE [dbo].[Cart]
GO
USE [master]
GO
/****** Object:  Database [Product Management]    Script Date: 19-04-2024 10:31:11 PM ******/
DROP DATABASE [Product Management]
GO
/****** Object:  Database [Product Management]    Script Date: 19-04-2024 10:31:11 PM ******/
CREATE DATABASE [Product Management]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Product Management', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Product Management.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Product Management_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Product Management_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Product Management] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Product Management].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Product Management] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Product Management] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Product Management] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Product Management] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Product Management] SET ARITHABORT OFF 
GO
ALTER DATABASE [Product Management] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Product Management] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Product Management] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Product Management] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Product Management] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Product Management] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Product Management] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Product Management] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Product Management] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Product Management] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Product Management] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Product Management] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Product Management] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Product Management] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Product Management] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Product Management] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Product Management] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Product Management] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Product Management] SET  MULTI_USER 
GO
ALTER DATABASE [Product Management] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Product Management] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Product Management] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Product Management] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Product Management] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Product Management] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Product Management] SET QUERY_STORE = OFF
GO
USE [Product Management]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 19-04-2024 10:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[Cart_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [int] NULL,
	[Product_Id] [int] NULL,
	[Quantity] [int] NULL,
	[Is_Completed] [int] NULL,
	[Is_Deleted] [int] NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[Cart_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 19-04-2024 10:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Product_Id] [int] IDENTITY(1,1) NOT NULL,
	[Product_Name] [nvarchar](100) NULL,
	[Product_Description] [nvarchar](1000) NULL,
	[Product_Price] [decimal](18, 0) NULL,
	[Product_Cover_Image] [nvarchar](100) NULL,
	[Product_Images] [nvarchar](500) NULL,
	[Product_Quantity] [int] NULL,
	[Product_Status] [int] NULL,
	[Is_Deleted] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Product_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19-04-2024 10:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Birthdate] [datetime] NULL,
	[Gender] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Phonenumber] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[Is_Deleted] [int] NULL,
	[User_Type] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_Cart_Details]    Script Date: 19-04-2024 10:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
        
CREATE PROCEDURE [dbo].[sp_Cart_Details]        
(  
 @Cart_Id INT = NULL,  
 @User_Id INT = NULL,      
 @Product_Id  INT = NULL,            
 @Quantity INT = NULL,        
 @sp_operation  NVARCHAR(50)          
)          
AS        
BEGIN          
        
IF @sp_operation = 'INSERT'        
 BEGIN        
  INSERT INTO dbo.[Cart] ([User_Id],Product_Id, Quantity,Is_Deleted)          
  VALUES (@User_Id,@Product_Id,@Quantity,0)          
 END         

ELSE IF @sp_operation = 'UPDATE'        
 BEGIN        
  UPDATE dbo.[Cart] SET Quantity=@Quantity WHERE Cart_Id = @Cart_Id
 END  

ELSE IF @sp_operation = 'SELECT'        
 BEGIN        
  SELECT C.Cart_Id, C.Quantity,P.Product_Name,P.Product_Price AS Price,P.Product_Cover_Image,U.User_Id FROM dbo.[Cart] C    
  INNER JOIN [dbo].[Product] AS P ON P.Product_Id = C.Product_Id    
  INNER JOIN [dbo].[User] AS U ON U.[User_Id] = C.[User_Id]      
  WHERE C.[User_Id] = @User_Id AND C.Is_Deleted = 0 AND C.Is_Completed IS NULL  
 END    
   
ELSE IF @sp_operation = 'DELETE'        
 BEGIN        
  UPDATE dbo.[Cart] SET Is_Deleted = 1 WHERE [Cart_Id] = @Cart_Id      
 END    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Get_Order_Details]    Script Date: 19-04-2024 10:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Get_Order_Details]
         
AS          
BEGIN 

	SELECT U.Firstname + ' ' + U.Lastname AS [User_Name],U.Email,P.Product_Name,P.Product_Price,C.Quantity, P.Product_Price * C.Quantity AS Total_Price FROM Cart AS C
	INNER JOIN [dbo].[Product] AS P ON P.Product_Id = C.Product_Id
	INNER JOIN [dbo].[User] AS U ON U.[User_Id] = C.[User_Id]

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Get_User_Order_Details]    Script Date: 19-04-2024 10:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Get_User_Order_Details]  
      
@User_Id INT
	   
AS            
BEGIN   
  
 SELECT P.Product_Name,P.Product_Price AS Price,C.Quantity, P.Product_Cover_Image FROM Cart AS C  
 INNER JOIN [dbo].[Product] AS P ON P.Product_Id = C.Product_Id  
 INNER JOIN [dbo].[User] AS U ON U.[User_Id] = C.[User_Id]
 WHERE C.[User_Id] = @User_Id AND C.Is_Completed = 1
  
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Completed]    Script Date: 19-04-2024 10:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Order_Completed]
(  
 @Cart_Ids NVARCHAR(100) = NULL     
)          
AS        
BEGIN          
        UPDATE dbo.[Cart] SET Is_Completed = 1 WHERE Cart_Id IN (SELECT value FROM string_split(@Cart_Ids, ','))
 END 
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Details]    Script Date: 19-04-2024 10:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Product_Details]
(  
 @Product_Id  INT = NULL,  
 @Product_Name  NVARCHAR(100) = NULL, 
 @Product_Description   NVARCHAR(1000) = NULL,  
 @Product_Price decimal = NULL,  
 @Product_Cover_Image  NVARCHAR(500) = NULL,
 @Product_Images  NVARCHAR(500) = NULL,
 @Product_Quantity INT = NULL,
 @Product_Status INT = NULL,
 @sp_operation  NVARCHAR(50)  
)  
AS
BEGIN  

IF @sp_operation = 'INSERT'
	BEGIN
	 INSERT INTO dbo.[Product] (Product_Name, Product_Description, Product_Price,Product_Cover_Image, Product_Images,Product_Quantity,Product_Status, Is_Deleted)  
	 VALUES (@Product_Name,@Product_Description,@Product_Price,@Product_Cover_Image,@Product_Images,@Product_Quantity,@Product_Status, 0)  
	END
	
ELSE IF @sp_operation = 'UPDATE'
	BEGIN
	 UPDATE dbo.[Product] SET Product_Name = @Product_Name, Product_Description = @Product_Description, Product_Price = @Product_Price, 
	 Product_Cover_Image = @Product_Cover_Image, Product_Images = @Product_Images,Product_Quantity = @Product_Quantity,Product_Status = @Product_Status   
	 WHERE Product_Id = @Product_Id
	END

ELSE IF @sp_operation = 'DELETE'
	BEGIN
	   UPDATE dbo.[Product] SET Is_Deleted = 1 WHERE Product_Id = @Product_Id 
	END

ELSE IF @sp_operation = 'SELECT'
	BEGIN
	 SELECT * FROM dbo.[Product] WHERE Is_Deleted = 0
	END

ELSE IF @sp_operation = 'GET'
	BEGIN
	 SELECT * FROM dbo.[Product] WHERE Product_Id = @Product_Id 
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserDetails]    Script Date: 19-04-2024 10:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UserDetails]  
(  
 @User_Id int = NULL,
 @Firstname  NVARCHAR(50) = NULL,    
 @Lastname  NVARCHAR(50) = NUll,    
 @Birthdate  datetime  = NUll,    
 @Gender   tinyint  = NUll,    
 @Email   NVARCHAR(50) = NUll,    
 @Phonenumber NVARCHAR(15) = NUll,    
 @Password  NVARCHAR(50) = NUll,    
 @sp_operation  NVARCHAR(50)    
)    
AS  
BEGIN    
  
IF @sp_operation = 'INSERT'  
	 BEGIN  
	  INSERT INTO dbo.[User] (Firstname, Lastname, Birthdate, Gender, Email, Phonenumber, [Password], Is_Deleted,User_Type)    
	  VALUES (@Firstname,@Lastname,@Birthdate,@Gender,@Email,@Phonenumber,@Password, 0,0)    
	 END  
   
ELSE IF @sp_operation = 'UPDATE'  
	 BEGIN  
	  UPDATE dbo.[User] SET Firstname = @Firstname, Lastname = @Lastname, Birthdate = @Birthdate, Gender = @Gender, Email = @Email, Phonenumber = @Phonenumber, [Password] = @Password  WHERE [User_Id] = @User_Id  
	 END  
  
ELSE IF @sp_operation = 'DELETE'  
	 BEGIN  
		UPDATE dbo.[User] SET Is_Deleted = 1 WHERE [User_Id] = @User_Id
	 END  
  
ELSE IF @sp_operation = 'SELECT'  
	 BEGIN  
	  SELECT * FROM dbo.[User]  WHERE Is_Deleted = 0
	 END 

ELSE IF @sp_operation = 'GET'  
	 BEGIN  
	  SELECT * FROM dbo.[User]  WHERE [User_Id] = @User_Id AND Is_Deleted = 0
	 END  

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserLogin]    Script Date: 19-04-2024 10:31:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UserLogin]
(  
 @Email   varchar(50),  
 @Password  varchar(50)
)  
AS
BEGIN  

	 SELECT * FROM dbo.[User] WHERE Email = @Email AND [Password] = @Password AND Is_Deleted = 0
END
GO
USE [master]
GO
ALTER DATABASE [Product Management] SET  READ_WRITE 
GO


INSERT INTO dbo.[User] (Firstname, Lastname, Birthdate, Gender, Email, Phonenumber, [Password], Is_Deleted,User_Type)    
	  VALUES ('kevin','gadhiya','2024-02-07 00:00:00.000',1,'kevin.gadhiya@gmail.com','9865896545','MTIzNDU2', 0,1) 