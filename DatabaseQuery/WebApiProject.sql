   
/****** Object:  Database [WebApiProject]    Script Date: 12/21/2021 11:16:12 PM ******/
CREATE DATABASE [WebApiProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebApiProject', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\WebApiProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WebApiProject_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\WebApiProject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WebApiProject] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebApiProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebApiProject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebApiProject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebApiProject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebApiProject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebApiProject] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebApiProject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WebApiProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebApiProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebApiProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebApiProject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebApiProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebApiProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebApiProject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebApiProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebApiProject] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WebApiProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebApiProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebApiProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebApiProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebApiProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebApiProject] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [WebApiProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebApiProject] SET RECOVERY FULL 
GO
ALTER DATABASE [WebApiProject] SET  MULTI_USER 
GO
ALTER DATABASE [WebApiProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebApiProject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebApiProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebApiProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WebApiProject] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'WebApiProject', N'ON'
GO
ALTER DATABASE [WebApiProject] SET QUERY_STORE = OFF
GO
USE [WebApiProject]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/21/2021 11:16:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/21/2021 11:16:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](255) NULL,
	[Password] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 12/21/2021 11:16:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](max) NULL,
	[TokenExp] [datetime2](7) NOT NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[RefreshTokenExp] [datetime2](7) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211221184252_Init', N'5.0.13')
GO
INSERT [dbo].[Users] ([Id], [UserName], [Password], [IsActive]) VALUES (N'448d6b80-cbad-4803-bd69-39db8acbc7aa', N'Arash', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [IsActive]) VALUES (N'458d6b80-cbad-4803-bd69-39db8acbc7aa', N'Ali ', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [IsActive]) VALUES (N'468d6b80-cbad-4803-bd69-39db8acbc7aa', N'Abolfazl', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', 1)
GO
/****** Object:  Index [IX_UserTokens_UserId]    Script Date: 12/21/2021 11:16:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserTokens_UserId] ON [dbo].[UserTokens]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users]
GO
/****** Object:  StoredProcedure [dbo].[AddUserToken]    Script Date: 12/21/2021 11:16:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUserToken](@userId uniqueidentifier,@token nvarchar(MAX),
@tokenExp datetime2(7),
@refreshToken nvarchar(MAX),
@refreshTokenExp datetime2(7))
AS
	
	INSERT INTO dbo.UserTokens(UserId,Token,TokenExp, RefreshToken,RefreshTokenExp)
		   VALUES (@userId,@token, @tokenExp, @refreshToken, @refreshTokenExp);
 	
GO
/****** Object:  StoredProcedure [dbo].[ExistsUserLogin]    Script Date: 12/21/2021 11:16:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ExistsUserLogin] (@userName nvarchar(255),@password nvarchar(300))
AS
	SELECT TOP 1 * FROM Users WHERE UserName = @userName AND Password = @password 
GO
/****** Object:  StoredProcedure [dbo].[GetAllUser]    Script Date: 12/21/2021 11:16:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
 --GetAllUser
Create PROCEDURE [dbo].[GetAllUser]
AS
	SELECT * FROM Users	
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 12/21/2021 11:16:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--GetUser
CREATE PROCEDURE [dbo].[GetUser](@userId uniqueidentifier)
AS
	SELECT TOP 1 * FROM dbo.UserTokens WHERE UserId =@userId
GO
/****** Object:  StoredProcedure [dbo].[GetUserToken]    Script Date: 12/21/2021 11:16:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--GetUserToken
CREATE PROCEDURE [dbo].[GetUserToken](@userToken NVARCHAR(MAX))
AS
	
	SELECT TOP 1 * FROM dbo.UserTokens WHERE Token =@userToken
GO
USE [master]
GO
ALTER DATABASE [WebApiProject] SET  READ_WRITE 
GO
