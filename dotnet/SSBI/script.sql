USE [master]
GO
/****** Object:  Database [CustomerDataWareHouse]    Script Date: Sun, Oct 11 1:01:23 AM ******/
CREATE DATABASE [CustomerDataWareHouse]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CustomerDataWareHouse', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CustomerDataWareHouse.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CustomerDataWareHouse_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CustomerDataWareHouse_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CustomerDataWareHouse] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CustomerDataWareHouse].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CustomerDataWareHouse] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET ARITHABORT OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CustomerDataWareHouse] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CustomerDataWareHouse] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CustomerDataWareHouse] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CustomerDataWareHouse] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET RECOVERY FULL 
GO
ALTER DATABASE [CustomerDataWareHouse] SET  MULTI_USER 
GO
ALTER DATABASE [CustomerDataWareHouse] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CustomerDataWareHouse] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CustomerDataWareHouse] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CustomerDataWareHouse] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CustomerDataWareHouse] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CustomerDataWareHouse', N'ON'
GO
ALTER DATABASE [CustomerDataWareHouse] SET QUERY_STORE = OFF
GO
USE [CustomerDataWareHouse]
GO
/****** Object:  Table [dbo].[DimCountry]    Script Date: Sun, Oct 11 1:01:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimCountry](
	[CountryId] [int] NOT NULL,
	[CountryName] [nvarchar](50) NULL,
	[IsNew] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DimProduct]    Script Date: Sun, Oct 11 1:01:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimProduct](
	[ProductId] [int] NOT NULL,
	[ProductName] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ExpiredDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DimSalesPerson]    Script Date: Sun, Oct 11 1:01:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimSalesPerson](
	[PersonId] [int] NOT NULL,
	[PersonName] [nvarchar](50) NULL,
 CONSTRAINT [PK_DimSalesPerson] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DimStates]    Script Date: Sun, Oct 11 1:01:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimStates](
	[StatesId] [int] NOT NULL,
	[StatesName] [nvarchar](50) NULL,
 CONSTRAINT [PK_DimStates] PRIMARY KEY CLUSTERED 
(
	[StatesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FactCustomer]    Script Date: Sun, Oct 11 1:01:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FactCustomer](
	[CustomerCode] [nvarchar](50) NOT NULL,
	[CustomerName] [nvarchar](50) NULL,
	[CustomerAmount] [money] NULL,
	[SalesDate] [datetime] NULL,
	[CountryId] [int] NULL,
	[StatesId] [int] NULL,
	[ProductId] [int] NULL,
	[SalesPersonId] [int] NULL,
 CONSTRAINT [PK_tblCustomer] PRIMARY KEY CLUSTERED 
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [CustomerDataWareHouse] SET  READ_WRITE 
GO
