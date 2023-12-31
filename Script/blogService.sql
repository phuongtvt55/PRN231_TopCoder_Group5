USE [master]
GO
/****** Object:  Database [blogService]    Script Date: 11/6/2023 8:34:40 PM ******/
CREATE DATABASE [blogService]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'blogService', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\blogService.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'blogService_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\blogService_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [blogService] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [blogService].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [blogService] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [blogService] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [blogService] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [blogService] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [blogService] SET ARITHABORT OFF 
GO
ALTER DATABASE [blogService] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [blogService] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [blogService] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [blogService] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [blogService] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [blogService] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [blogService] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [blogService] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [blogService] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [blogService] SET  ENABLE_BROKER 
GO
ALTER DATABASE [blogService] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [blogService] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [blogService] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [blogService] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [blogService] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [blogService] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [blogService] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [blogService] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [blogService] SET  MULTI_USER 
GO
ALTER DATABASE [blogService] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [blogService] SET DB_CHAINING OFF 
GO
ALTER DATABASE [blogService] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [blogService] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [blogService] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [blogService] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'blogService', N'ON'
GO
ALTER DATABASE [blogService] SET QUERY_STORE = OFF
GO
USE [blogService]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 11/6/2023 8:34:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[BlogID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[BlogTitle] [nvarchar](50) NULL,
	[BlogDetail] [nvarchar](max) NULL,
	[Image] [varchar](max) NULL,
	[IsDelete] [int] NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[BlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 11/6/2023 8:34:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [int] NULL,
	[UserID] [int] NULL,
	[CommentMsg] [nvarchar](max) NULL,
	[CommentDate] [datetime] NULL,
	[Rate] [int] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 

INSERT [dbo].[Blog] ([BlogID], [UserID], [BlogTitle], [BlogDetail], [Image], [IsDelete], [Status]) VALUES (2, 19, N'Everything Is Possible in Finding a Job', N'<p style=''border: 0px solid rgb(217, 217, 227); box-sizing: border-box; --tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-pan-x: ; --tw-pan-y: ; --tw-pinch-zoom: ; --tw-scroll-snap-strictness: proximity; --tw-gradient-from-position: ; --tw-gradient-via-position: ; --tw-gradient-to-position: ; --tw-ordinal: ; --tw-slashed-zero: ; --tw-numeric-figure: ; --tw-numeric-spacing: ; --tw-numeric-fraction: ; --tw-ring-inset: ; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(69,89,164,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; --tw-blur: ; --tw-brightness: ; --tw-contrast: ; --tw-grayscale: ; --tw-hue-rotate: ; --tw-invert: ; --tw-saturate: ; --tw-sepia: ; --tw-drop-shadow: ; --tw-backdrop-blur: ; --tw-backdrop-brightness: ; --tw-backdrop-contrast: ; --tw-backdrop-grayscale: ; --tw-backdrop-hue-rotate: ; --tw-backdrop-invert: ; --tw-backdrop-opacity: ; --tw-backdrop-saturate: ; --tw-backdrop-sepia: ; margin: 1.25em 0px; color: rgb(55, 65, 81); font-family: Söhne, ui-sans-serif, system-ui, -apple-system, "Segoe UI", Roboto, Ubuntu, Cantarell, "Noto Sans", sans-serif, "Helvetica Neue", Arial, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; white-space: pre-wrap; background-color: rgb(247, 247, 248); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;'' id="isPasted">In today&#39;s dynamic and ever-evolving job market, the saying &quot;everything is possible&quot; takes on a whole new meaning. The quest for a fulfilling career has become a journey of endless possibilities, where job seekers navigate a complex web of opportunities and challenges. While the path to employment might seem daunting, it is crucial to embrace the belief that with determination, adaptability, and perseverance, everything is indeed possible in finding a job.</p><p style=''border: 0px solid rgb(217, 217, 227); box-sizing: border-box; --tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-pan-x: ; --tw-pan-y: ; --tw-pinch-zoom: ; --tw-scroll-snap-strictness: proximity; --tw-gradient-from-position: ; --tw-gradient-via-position: ; --tw-gradient-to-position: ; --tw-ordinal: ; --tw-slashed-zero: ; --tw-numeric-figure: ; --tw-numeric-spacing: ; --tw-numeric-fraction: ; --tw-ring-inset: ; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(69,89,164,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; --tw-blur: ; --tw-brightness: ; --tw-contrast: ; --tw-grayscale: ; --tw-hue-rotate: ; --tw-invert: ; --tw-saturate: ; --tw-sepia: ; --tw-drop-shadow: ; --tw-backdrop-blur: ; --tw-backdrop-brightness: ; --tw-backdrop-contrast: ; --tw-backdrop-grayscale: ; --tw-backdrop-hue-rotate: ; --tw-backdrop-invert: ; --tw-backdrop-opacity: ; --tw-backdrop-saturate: ; --tw-backdrop-sepia: ; margin: 1.25em 0px; color: rgb(55, 65, 81); font-family: Söhne, ui-sans-serif, system-ui, -apple-system, "Segoe UI", Roboto, Ubuntu, Cantarell, "Noto Sans", sans-serif, "Helvetica Neue", Arial, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; white-space: pre-wrap; background-color: rgb(247, 247, 248); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;''>One of the key aspects of this perspective is the adaptability to changing job trends. The job landscape has witnessed a transformation over the years, with traditional career paths giving way to innovative roles and industries. The rise of technology, automation, and remote work has opened doors to a multitude of opportunities that were unimaginable a few decades ago. To harness these possibilities, job seekers must remain open to learning new skills and embracing change. Adapting to the evolving job market is a testament to the belief that every individual can find a job that suits their aspirations and talents.</p><p style=''border: 0px solid rgb(217, 217, 227); box-sizing: border-box; --tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-pan-x: ; --tw-pan-y: ; --tw-pinch-zoom: ; --tw-scroll-snap-strictness: proximity; --tw-gradient-from-position: ; --tw-gradient-via-position: ; --tw-gradient-to-position: ; --tw-ordinal: ; --tw-slashed-zero: ; --tw-numeric-figure: ; --tw-numeric-spacing: ; --tw-numeric-fraction: ; --tw-ring-inset: ; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(69,89,164,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; --tw-blur: ; --tw-brightness: ; --tw-contrast: ; --tw-grayscale: ; --tw-hue-rotate: ; --tw-invert: ; --tw-saturate: ; --tw-sepia: ; --tw-drop-shadow: ; --tw-backdrop-blur: ; --tw-backdrop-brightness: ; --tw-backdrop-contrast: ; --tw-backdrop-grayscale: ; --tw-backdrop-hue-rotate: ; --tw-backdrop-invert: ; --tw-backdrop-opacity: ; --tw-backdrop-saturate: ; --tw-backdrop-sepia: ; margin: 1.25em 0px; color: rgb(55, 65, 81); font-family: Söhne, ui-sans-serif, system-ui, -apple-system, "Segoe UI", Roboto, Ubuntu, Cantarell, "Noto Sans", sans-serif, "Helvetica Neue", Arial, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; white-space: pre-wrap; background-color: rgb(247, 247, 248); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;''>Networking and relationship-building also play a pivotal role in job searches. The power of connections cannot be overstated, and one of the remarkable aspects of job hunting is the way opportunities can arise from the most unexpected sources. Through networking, individuals can access the hidden job market, where positions are filled through referrals and recommendations. Building and nurturing relationships with peers, mentors, and industry professionals can create a domino effect that opens doors to possibilities beyond one&#39;s imagination.</p><p style=''border: 0px solid rgb(217, 217, 227); box-sizing: border-box; --tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; --tw-rotate: 0; --tw-skew-x: 0; --tw-skew-y: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-pan-x: ; --tw-pan-y: ; --tw-pinch-zoom: ; --tw-scroll-snap-strictness: proximity; --tw-gradient-from-position: ; --tw-gradient-via-position: ; --tw-gradient-to-position: ; --tw-ordinal: ; --tw-slashed-zero: ; --tw-numeric-figure: ; --tw-numeric-spacing: ; --tw-numeric-fraction: ; --tw-ring-inset: ; --tw-ring-offset-width: 0px; --tw-ring-offset-color: #fff; --tw-ring-color: rgba(69,89,164,.5); --tw-ring-offset-shadow: 0 0 transparent; --tw-ring-shadow: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-shadow-colored: 0 0 transparent; --tw-blur: ; --tw-brightness: ; --tw-contrast: ; --tw-grayscale: ; --tw-hue-rotate: ; --tw-invert: ; --tw-saturate: ; --tw-sepia: ; --tw-drop-shadow: ; --tw-backdrop-blur: ; --tw-backdrop-brightness: ; --tw-backdrop-contrast: ; --tw-backdrop-grayscale: ; --tw-backdrop-hue-rotate: ; --tw-backdrop-invert: ; --tw-backdrop-opacity: ; --tw-backdrop-saturate: ; --tw-backdrop-sepia: ; margin: 1.25em 0px; color: rgb(55, 65, 81); font-family: Söhne, ui-sans-serif, system-ui, -apple-system, "Segoe UI", Roboto, Ubuntu, Cantarell, "Noto Sans", sans-serif, "Helvetica Neue", Arial, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; white-space: pre-wrap; background-color: rgb(247, 247, 248); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;''>In the pursuit of a dream job, patience and persistence are virtues that pay rich dividends. It&#39;s not uncommon to face setbacks and rejections, and these moments can be disheartening. However, the belief that everything is possible means viewing each obstacle as a stepping stone, not a roadblock. The perseverance to keep applying, refining one&#39;s skills, and honing interview techniques can lead to eventual success. Often, the job that once seemed unattainable becomes a reality because of one&#39;s unwavering determination.</p>', N'blogUploads/ed0eb4d7-6aed-4957-859e-4a2a7d6e201b.jpg', 1, N'Waiting')
SET IDENTITY_INSERT [dbo].[Blog] OFF
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Blog] FOREIGN KEY([BlogID])
REFERENCES [dbo].[Blog] ([BlogID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Blog]
GO
USE [master]
GO
ALTER DATABASE [blogService] SET  READ_WRITE 
GO
