USE [master]
GO
/****** Object:  Database [Invoice]    Script Date: 4/10/2563 16:08:30 ******/
CREATE DATABASE [Invoice]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Invoice', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Invoice.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Invoice_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Invoice_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Invoice] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Invoice].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Invoice] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Invoice] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Invoice] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Invoice] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Invoice] SET ARITHABORT OFF 
GO
ALTER DATABASE [Invoice] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Invoice] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Invoice] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Invoice] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Invoice] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Invoice] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Invoice] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Invoice] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Invoice] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Invoice] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Invoice] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Invoice] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Invoice] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Invoice] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Invoice] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Invoice] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Invoice] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Invoice] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Invoice] SET  MULTI_USER 
GO
ALTER DATABASE [Invoice] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Invoice] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Invoice] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Invoice] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Invoice] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Invoice] SET QUERY_STORE = OFF
GO
USE [Invoice]
GO
/****** Object:  Table [dbo].[TransactionUpload]    Script Date: 4/10/2563 16:08:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionUpload](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](50) NULL,
	[Amount] [numeric](18, 0) NOT NULL,
	[Currency] [nchar](3) NULL,
	[TransactionDate] [datetime2](7) NULL,
	[Status] [nvarchar](50) NULL,
	[FromFile] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_TransactionUpload] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Invoice] SET  READ_WRITE 
GO
