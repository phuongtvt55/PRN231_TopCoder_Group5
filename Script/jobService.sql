USE [master]
GO
/****** Object:  Database [jobService]    Script Date: 11/06/2023 10:52:07 CH ******/
CREATE DATABASE [jobService]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'jobService', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\jobService.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'jobService_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\jobService_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [jobService] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [jobService].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [jobService] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [jobService] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [jobService] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [jobService] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [jobService] SET ARITHABORT OFF 
GO
ALTER DATABASE [jobService] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [jobService] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [jobService] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [jobService] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [jobService] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [jobService] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [jobService] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [jobService] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [jobService] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [jobService] SET  ENABLE_BROKER 
GO
ALTER DATABASE [jobService] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [jobService] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [jobService] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [jobService] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [jobService] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [jobService] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [jobService] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [jobService] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [jobService] SET  MULTI_USER 
GO
ALTER DATABASE [jobService] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [jobService] SET DB_CHAINING OFF 
GO
ALTER DATABASE [jobService] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [jobService] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [jobService] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [jobService] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [jobService] SET QUERY_STORE = OFF
GO
USE [jobService]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/06/2023 10:52:07 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 11/06/2023 10:52:07 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[JobID] [int] IDENTITY(1,1) NOT NULL,
	[BusinessID] [int] NULL,
	[PostDate] [datetime] NULL,
	[JobTitle] [nvarchar](100) NULL,
	[JobDetail] [nvarchar](max) NULL,
	[Salary] [nvarchar](100) NULL,
	[Address] [nvarchar](100) NULL,
	[JobRequirement] [nvarchar](max) NULL,
	[Skills] [nvarchar](max) NULL,
	[Website] [nvarchar](100) NULL,
	[Nationality] [nvarchar](50) NULL,
	[YearExperience] [int] NULL,
	[ContractType] [nvarchar](50) NULL,
	[IsDelete] [int] NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobCategory]    Script Date: 11/06/2023 10:52:07 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobCategory](
	[JobID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
 CONSTRAINT [PK_JobCategory_1] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC,
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobRank]    Script Date: 11/06/2023 10:52:07 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobRank](
	[JobID] [int] NOT NULL,
	[RankID] [int] NOT NULL,
 CONSTRAINT [PK_JobRank] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC,
	[RankID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rank]    Script Date: 11/06/2023 10:52:07 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rank](
	[RankID] [int] IDENTITY(1,1) NOT NULL,
	[RankName] [nvarchar](50) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Rank] PRIMARY KEY CLUSTERED 
(
	[RankID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wishlists]    Script Date: 11/06/2023 10:52:07 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wishlists](
	[WishlistID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[JobID] [int] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Wishlists] PRIMARY KEY CLUSTERED 
(
	[WishlistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (1, N'C++
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (2, N'C#
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (3, N'Java
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (4, N'PHP
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (5, N'Ruby
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (6, N'Rust
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (7, N'React Native
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (8, N'Go
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (9, N'F#
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (10, N'Kotlin
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (11, N'AngularJS
', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [IsDelete]) VALUES (12, N'VueJS
', 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Job] ON 

INSERT [dbo].[Job] ([JobID], [BusinessID], [PostDate], [JobTitle], [JobDetail], [Salary], [Address], [JobRequirement], [Skills], [Website], [Nationality], [YearExperience], [ContractType], [IsDelete], [Status]) VALUES (1, 9, CAST(N'2023-11-03T00:00:00.000' AS DateTime), N'Senior Java Developer
', N'We are seeking an experienced Mid/Senior Java Developer.
', N'1000-2000', N'Cần Thơ
', N'Bachelor''s degree in Computer Science, Computer Engineering. 
', N'Fluent in English, both written and spoken.
', N'https://www.topcv.vn/cong-ty/fpt-software/3.html
', N'Vietnam', 3, N'Full Time', 1, N'Accept')
INSERT [dbo].[Job] ([JobID], [BusinessID], [PostDate], [JobTitle], [JobDetail], [Salary], [Address], [JobRequirement], [Skills], [Website], [Nationality], [YearExperience], [ContractType], [IsDelete], [Status]) VALUES (2, 9, CAST(N'2023-11-03T00:00:00.000' AS DateTime), N'DevOps
', N'We are looking for DevOps.
', N'1300-3000', N'Hồ Chí Minh
', N'Good knowledge of design patterns, system architecture, and best practices
', N'Bash, Python Scripting. Continuous Integration.
', N'https://www.topcv.vn/cong-ty/cong-ty-tnhh-mediastep-software-viet-nam/33294.html
', N'Vietnam', 2, N'Part Time', 1, N'Accept')
INSERT [dbo].[Job] ([JobID], [BusinessID], [PostDate], [JobTitle], [JobDetail], [Salary], [Address], [JobRequirement], [Skills], [Website], [Nationality], [YearExperience], [ContractType], [IsDelete], [Status]) VALUES (3, 11, CAST(N'2023-11-03T00:00:00.000' AS DateTime), N'Fullstack Developer
', N'Work with BA and stakeholders to clarify requirements.
', N'1000-2500', N'Đà Nẵng
', N'At least 5 years of programming experience using .NET, C#
', N'Experience in designing Microservices and Distributed systems.
', N'https://www.topcv.vn/cong-ty/cong-ty-co-phan-synodus/111658.html
', N'Vietnam', 5, N'Full Time', 1, N'Accept')
INSERT [dbo].[Job] ([JobID], [BusinessID], [PostDate], [JobTitle], [JobDetail], [Salary], [Address], [JobRequirement], [Skills], [Website], [Nationality], [YearExperience], [ContractType], [IsDelete], [Status]) VALUES (4, 10, CAST(N'2023-11-03T00:00:00.000' AS DateTime), N'VueJS Developer
', N'Commitment to collaboration.
', N'1200-3000', N'Hà Nội
', N'Strong analytical skills with the ability to learn quickly.
', N'In-depth knowledge of JavaScript, HTML, and CSS.
', N'https://www.topcv.vn/cong-ty/cong-ty-tnhh-phan-mem-fpt/31332.html
', N'Vietnam', 2, N'Part Time', 1, N'Accept')
INSERT [dbo].[Job] ([JobID], [BusinessID], [PostDate], [JobTitle], [JobDetail], [Salary], [Address], [JobRequirement], [Skills], [Website], [Nationality], [YearExperience], [ContractType], [IsDelete], [Status]) VALUES (14, 9, CAST(N'2023-11-03T00:00:00.000' AS DateTime), N'Front-End Developer
', N'Programming and optimization.
', N'500-1000', N'Hồ Chí Minh
', N'Graduated from university or college in information technology.
', N'Experience working with jquery/angularjs.
', N'https://www.topcv.vn/cong-ty/cong-ty-co-phan-hoc-vien-dao-tao-quoc-te-taki/72189.html
', N'Vietnam', 3, N'Full Time', 1, N'Accept')
INSERT [dbo].[Job] ([JobID], [BusinessID], [PostDate], [JobTitle], [JobDetail], [Salary], [Address], [JobRequirement], [Skills], [Website], [Nationality], [YearExperience], [ContractType], [IsDelete], [Status]) VALUES (16, 9, CAST(N'2023-11-03T00:00:00.000' AS DateTime), N'Lavarel', N'Develop Web App in low-code form using Laravel, Vuejs, API framework;', N'800-1000', N'Hà Nội
', N'Ability to analyze and solve problems independently as well as coordination skills in teamwork.', N'Gain knowledge of Javascript/HTML/CSS', N'https://www.topcv.vn/cong-ty/fpt-software/3.html
', N'Vietnam', 1, N'Full Time', 1, N'Accept')
INSERT [dbo].[Job] ([JobID], [BusinessID], [PostDate], [JobTitle], [JobDetail], [Salary], [Address], [JobRequirement], [Skills], [Website], [Nationality], [YearExperience], [ContractType], [IsDelete], [Status]) VALUES (17, 11, CAST(N'2023-11-06T00:00:00.000' AS DateTime), N'Java Dev', N'Working with active environment', N'900-2000', N'Đà Nẵng
', N'English is required', N'Java, SQL', N'https://www.topcv.vn/cong-ty/cong-ty-co-phan-synodus/111658.html
', N'Viet Nam', 2, N'Full Time', 1, N'Accept')
SET IDENTITY_INSERT [dbo].[Job] OFF
GO
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (1, 1)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (1, 2)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (2, 1)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (2, 3)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (3, 4)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (4, 1)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (4, 4)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (14, 1)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (14, 2)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (14, 3)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (16, 1)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (16, 4)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (17, 9)
INSERT [dbo].[JobCategory] ([JobID], [CategoryID]) VALUES (17, 10)
GO
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (1, 2)
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (1, 3)
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (2, 7)
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (3, 3)
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (4, 1)
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (4, 6)
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (14, 1)
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (14, 5)
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (16, 2)
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (17, 1)
INSERT [dbo].[JobRank] ([JobID], [RankID]) VALUES (17, 5)
GO
SET IDENTITY_INSERT [dbo].[Rank] ON 

INSERT [dbo].[Rank] ([RankID], [RankName], [IsDelete]) VALUES (1, N'Fresher
', 0)
INSERT [dbo].[Rank] ([RankID], [RankName], [IsDelete]) VALUES (2, N'Senior
', 0)
INSERT [dbo].[Rank] ([RankID], [RankName], [IsDelete]) VALUES (3, N'Mid-level
', 0)
INSERT [dbo].[Rank] ([RankID], [RankName], [IsDelete]) VALUES (4, N'Junior', 0)
INSERT [dbo].[Rank] ([RankID], [RankName], [IsDelete]) VALUES (5, N'Leader
', 0)
INSERT [dbo].[Rank] ([RankID], [RankName], [IsDelete]) VALUES (6, N'Tester', 0)
INSERT [dbo].[Rank] ([RankID], [RankName], [IsDelete]) VALUES (7, N'Trainee
', 0)
SET IDENTITY_INSERT [dbo].[Rank] OFF
GO
SET IDENTITY_INSERT [dbo].[Wishlists] ON 

INSERT [dbo].[Wishlists] ([WishlistID], [UserID], [JobID], [IsDelete]) VALUES (46, 4, 2, 0)
INSERT [dbo].[Wishlists] ([WishlistID], [UserID], [JobID], [IsDelete]) VALUES (47, 4, 3, 0)
INSERT [dbo].[Wishlists] ([WishlistID], [UserID], [JobID], [IsDelete]) VALUES (49, 4, 1, 0)
SET IDENTITY_INSERT [dbo].[Wishlists] OFF
GO
ALTER TABLE [dbo].[JobCategory]  WITH CHECK ADD  CONSTRAINT [FK_JobCategory_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[JobCategory] CHECK CONSTRAINT [FK_JobCategory_Category]
GO
ALTER TABLE [dbo].[JobCategory]  WITH CHECK ADD  CONSTRAINT [FK_JobCategory_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([JobID])
GO
ALTER TABLE [dbo].[JobCategory] CHECK CONSTRAINT [FK_JobCategory_Job]
GO
ALTER TABLE [dbo].[JobRank]  WITH CHECK ADD  CONSTRAINT [FK_JobRank_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([JobID])
GO
ALTER TABLE [dbo].[JobRank] CHECK CONSTRAINT [FK_JobRank_Job]
GO
ALTER TABLE [dbo].[JobRank]  WITH CHECK ADD  CONSTRAINT [FK_JobRank_Rank] FOREIGN KEY([RankID])
REFERENCES [dbo].[Rank] ([RankID])
GO
ALTER TABLE [dbo].[JobRank] CHECK CONSTRAINT [FK_JobRank_Rank]
GO
ALTER TABLE [dbo].[Wishlists]  WITH CHECK ADD  CONSTRAINT [FK_Wishlists_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([JobID])
GO
ALTER TABLE [dbo].[Wishlists] CHECK CONSTRAINT [FK_Wishlists_Job]
GO
USE [master]
GO
ALTER DATABASE [jobService] SET  READ_WRITE 
GO
