USE [master]
GO
/****** Object:  Database [userService]    Script Date: 11/6/2023 3:03:10 PM ******/
CREATE DATABASE [userService]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'userService', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\userService.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'userService_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\userService_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [userService] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [userService].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [userService] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [userService] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [userService] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [userService] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [userService] SET ARITHABORT OFF 
GO
ALTER DATABASE [userService] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [userService] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [userService] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [userService] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [userService] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [userService] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [userService] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [userService] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [userService] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [userService] SET  ENABLE_BROKER 
GO
ALTER DATABASE [userService] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [userService] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [userService] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [userService] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [userService] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [userService] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [userService] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [userService] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [userService] SET  MULTI_USER 
GO
ALTER DATABASE [userService] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [userService] SET DB_CHAINING OFF 
GO
ALTER DATABASE [userService] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [userService] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [userService] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [userService] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'userService', N'ON'
GO
ALTER DATABASE [userService] SET QUERY_STORE = OFF
GO
USE [userService]
GO
/****** Object:  Table [dbo].[BusinessProfile]    Script Date: 11/6/2023 3:03:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessProfile](
	[BusinessID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[CompanyName] [nvarchar](50) NULL,
	[AboutCompany] [nvarchar](max) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_BusinessProfile] PRIMARY KEY CLUSTERED 
(
	[BusinessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/6/2023 3:03:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](20) NULL,
	[ImageProfile] [nvarchar](max) NULL,
	[CVProfile] [nvarchar](max) NULL,
	[UserType] [nvarchar](20) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BusinessProfile] ON 

INSERT [dbo].[BusinessProfile] ([BusinessID], [UserID], [CompanyName], [AboutCompany], [IsDelete]) VALUES (1, 2, N'FPT Software', N'FPT Software is a leading global IT services company, providing software outsourcing and digital transformation solutions. It specializes in software development, AI, IoT, and cybersecurity services.', 1)
INSERT [dbo].[BusinessProfile] ([BusinessID], [UserID], [CompanyName], [AboutCompany], [IsDelete]) VALUES (2, 3, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[BusinessProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [UserName], [Password], [Email], [Address], [Phone], [ImageProfile], [CVProfile], [UserType], [IsDelete]) VALUES (1, N'Admin', N'Admin@123', N'topcoder@gmail.com', N'Cần Thơ', N'091919191919', N'img/logo_title', NULL, N'Admin', 1)
INSERT [dbo].[User] ([UserID], [UserName], [Password], [Email], [Address], [Phone], [ImageProfile], [CVProfile], [UserType], [IsDelete]) VALUES (2, N'Phương', N'Phuong@123', N'phuonghhce161062@fpt.edu.vn', N'Cà Mau', N'0929292929', N'img/FPT_Logo.png', NULL, N'Employer', 1)
INSERT [dbo].[User] ([UserID], [UserName], [Password], [Email], [Address], [Phone], [ImageProfile], [CVProfile], [UserType], [IsDelete]) VALUES (3, N'Huy', N'Huy@123', N'huynqce161074@fpt.edu.vn', N'Vĩnh Long', N'0939393939', N'img/FPT_Logo.png', NULL, N'User', 1)
INSERT [dbo].[User] ([UserID], [UserName], [Password], [Email], [Address], [Phone], [ImageProfile], [CVProfile], [UserType], [IsDelete]) VALUES (4, N'Tài', N'Tai@123', N'taindce160859@fpt.edu.vn', N'Đồng Tháp', N'0949494949', NULL, NULL, N'User ', 1)
INSERT [dbo].[User] ([UserID], [UserName], [Password], [Email], [Address], [Phone], [ImageProfile], [CVProfile], [UserType], [IsDelete]) VALUES (5, N'Duy', N'Duy@123', N'duyntqce161080@fpt.edu.vn', N'Đồng Tháp', N'0959595959', NULL, NULL, N'User ', 1)
INSERT [dbo].[User] ([UserID], [UserName], [Password], [Email], [Address], [Phone], [ImageProfile], [CVProfile], [UserType], [IsDelete]) VALUES (6, N'Kiệt', N'Kiet@123', N'kiethace160496@fpt.edu.vn', N'Bạc Liêu', N'0969696969', NULL, NULL, N'User ', 1)
INSERT [dbo].[User] ([UserID], [UserName], [Password], [Email], [Address], [Phone], [ImageProfile], [CVProfile], [UserType], [IsDelete]) VALUES (7, N'Tân', N'Tan@123', N'tanhnce140420@fpt.edu.vn', N'Sóc Trăng', N'0979797979', NULL, NULL, N'User ', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
/****** Object:  Index [uq_userId]    Script Date: 11/6/2023 3:03:10 PM ******/
ALTER TABLE [dbo].[BusinessProfile] ADD  CONSTRAINT [uq_userId] UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BusinessProfile]  WITH CHECK ADD  CONSTRAINT [FK_BusinessProfile_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[BusinessProfile] CHECK CONSTRAINT [FK_BusinessProfile_User]
GO
USE [master]
GO
ALTER DATABASE [userService] SET  READ_WRITE 
GO
