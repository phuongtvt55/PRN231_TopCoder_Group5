USE [master]
GO
/****** Object:  Database [jobApplicationService]    Script Date: 10/12/2023 11:24:19 CH ******/
CREATE DATABASE [jobApplicationService]

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
ALTER DATABASE [jobApplicationService] SET QUERY_STORE = OFF
GO
USE [jobApplicationService]
GO
/****** Object:  Table [dbo].[JobApplication]    Script Date: 10/12/2023 11:24:19 CH ******/
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
USE [master]
GO
ALTER DATABASE [jobApplicationService] SET  READ_WRITE 
GO
