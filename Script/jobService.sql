USE [master]
GO
/****** Object:  Database [jobService]    Script Date: 10/12/2023 11:23:21 CH ******/
CREATE DATABASE [jobService]

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
/****** Object:  Table [dbo].[Category]    Script Date: 10/12/2023 11:23:21 CH ******/
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
/****** Object:  Table [dbo].[Job]    Script Date: 10/12/2023 11:23:21 CH ******/
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
	[RankID] [int] NULL,
	[ContractType] [nvarchar](50) NULL,
	[IsDelete] [int] NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobCategory]    Script Date: 10/12/2023 11:23:21 CH ******/
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
/****** Object:  Table [dbo].[JobRank]    Script Date: 10/12/2023 11:23:21 CH ******/
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
/****** Object:  Table [dbo].[Rank]    Script Date: 10/12/2023 11:23:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rank](
	[RankID] [int] IDENTITY(1,1) NOT NULL,
	[RankName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Rank] PRIMARY KEY CLUSTERED 
(
	[RankID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wishlists]    Script Date: 10/12/2023 11:23:21 CH ******/
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
