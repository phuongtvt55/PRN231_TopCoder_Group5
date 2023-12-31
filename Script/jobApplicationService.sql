USE [master]
GO
/****** Object:  Database [jobApplicationService]    Script Date: 11/6/2023 8:36:35 PM ******/
CREATE DATABASE [jobApplicationService]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'jobApplicationService', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\jobApplicationService.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'jobApplicationService_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\jobApplicationService_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [jobApplicationService] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [jobApplicationService].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [jobApplicationService] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [jobApplicationService] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [jobApplicationService] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [jobApplicationService] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [jobApplicationService] SET ARITHABORT OFF 
GO
ALTER DATABASE [jobApplicationService] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [jobApplicationService] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [jobApplicationService] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [jobApplicationService] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [jobApplicationService] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [jobApplicationService] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [jobApplicationService] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [jobApplicationService] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [jobApplicationService] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [jobApplicationService] SET  ENABLE_BROKER 
GO
ALTER DATABASE [jobApplicationService] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [jobApplicationService] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [jobApplicationService] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [jobApplicationService] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [jobApplicationService] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [jobApplicationService] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [jobApplicationService] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [jobApplicationService] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [jobApplicationService] SET  MULTI_USER 
GO
ALTER DATABASE [jobApplicationService] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [jobApplicationService] SET DB_CHAINING OFF 
GO
ALTER DATABASE [jobApplicationService] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [jobApplicationService] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [jobApplicationService] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [jobApplicationService] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'jobApplicationService', N'ON'
GO
ALTER DATABASE [jobApplicationService] SET QUERY_STORE = OFF
GO
USE [jobApplicationService]
GO
/****** Object:  Table [dbo].[JobApplication]    Script Date: 11/6/2023 8:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobApplication](
	[ApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[JobID] [int] NULL,
	[ApplyDate] [datetime] NULL,
	[IsDelete] [int] NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_JobApplication] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[JobApplication] ON 

INSERT [dbo].[JobApplication] ([ApplicationID], [UserID], [JobID], [ApplyDate], [IsDelete], [Status]) VALUES (1, 2, 1, CAST(N'2023-11-03T00:00:00.000' AS DateTime), 1, N'Waiting')
INSERT [dbo].[JobApplication] ([ApplicationID], [UserID], [JobID], [ApplyDate], [IsDelete], [Status]) VALUES (2, 3, 4, CAST(N'2023-11-03T00:00:00.000' AS DateTime), 1, N'Waiting')
INSERT [dbo].[JobApplication] ([ApplicationID], [UserID], [JobID], [ApplyDate], [IsDelete], [Status]) VALUES (3, 2, 2, CAST(N'2023-11-03T00:00:00.000' AS DateTime), 1, N'Waiting')
INSERT [dbo].[JobApplication] ([ApplicationID], [UserID], [JobID], [ApplyDate], [IsDelete], [Status]) VALUES (7, 10, 1, CAST(N'2023-11-06T15:49:23.377' AS DateTime), 1, N'Waiting')
SET IDENTITY_INSERT [dbo].[JobApplication] OFF
GO
USE [master]
GO
ALTER DATABASE [jobApplicationService] SET  READ_WRITE 
GO
