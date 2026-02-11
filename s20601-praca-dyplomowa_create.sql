/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 11-Feb-26 14:36:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [varchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[CreatedAt] [datetime2](2) NOT NULL,
	[LastLogin] [datetime2](2) NULL,
	[ProfileDescription] [varchar](500) NULL,
	[ReputationPoints] [int] NOT NULL,
	[LastDailyLogin] [datetime2](2) NULL,
	[Avatar] [nvarchar](max) NULL,
 CONSTRAINT [User_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Crew]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Crew](
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[BirthYear] [int] NOT NULL,
	[DeathYear] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IMDBId] [nvarchar](max) NOT NULL,
 CONSTRAINT [Crew_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[Name] [varchar](50) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [Genre_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[IdSender] [nvarchar](450) NOT NULL,
	[IdRecipient] [nvarchar](450) NOT NULL,
	[Content] [varchar](2500) NOT NULL,
	[MessageStatus] [int] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [Message_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](1000) NOT NULL,
	[OriginalTitle] [varchar](1000) NOT NULL,
	[StartYear] [int] NOT NULL,
	[EndYear] [int] NULL,
	[RuntimeMinutes] [int] NOT NULL,
	[TitleType] [varchar](100) NOT NULL,
	[PosterPath] [nvarchar](max) NULL,
	[IMDBId] [nvarchar](max) NULL,
	[Overview] [nvarchar](max) NULL,
 CONSTRAINT [Movie_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieCollection]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieCollection](
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[CreatedAt] [datetime2](2) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Visibility] [int] NOT NULL,
 CONSTRAINT [MovieCollection_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieCollectionMovie]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieCollectionMovie](
	[AddedAt] [datetime2](2) NOT NULL,
	[Movie_Id] [int] NOT NULL,
	[IdMovieCollection] [int] NOT NULL,
 CONSTRAINT [MovieCollectionMovie_pk] PRIMARY KEY CLUSTERED 
(
	[IdMovieCollection] ASC,
	[Movie_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieCollectionUser]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieCollectionUser](
	[IdUser] [nvarchar](450) NOT NULL,
	[IdMovieCollection] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [int] NOT NULL,
 CONSTRAINT [MovieCollectionUser_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieCrew]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieCrew](
	[IdCrew] [int] NOT NULL,
	[Job] [varchar](100) NOT NULL,
	[CharacterName] [varchar](100) NULL,
	[Movie_Id] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [MovieCrew_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieGenre]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieGenre](
	[Movie_Id] [int] NOT NULL,
	[Genre_Id] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [MovieGenre_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieOfTheDay]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieOfTheDay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Movie_Id] [int] NOT NULL,
	[Date] [datetime2](2) NOT NULL,
 CONSTRAINT [MovieOfTheDay_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieRate]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieRate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [nvarchar](450) NOT NULL,
	[Rating] [int] NOT NULL,
	[RatedAt] [datetime2](2) NOT NULL,
	[Movie_Id] [int] NOT NULL,
 CONSTRAINT [MovieRate_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieUpdateRequest]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieUpdateRequest](
	[IdUser] [nvarchar](450) NOT NULL,
	[Description] [varchar](2500) NULL,
	[CreatedAt] [datetime2](2) NOT NULL,
	[Movie_Id] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NewEndYear] [int] NULL,
	[NewOriginalTitle] [nvarchar](max) NULL,
	[NewRuntimeMinutes] [int] NULL,
	[NewStartYear] [int] NULL,
	[NewTitle] [nvarchar](max) NULL,
	[NewTitleType] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[NewOverview] [nvarchar](max) NULL,
	[NewPosterPath] [nvarchar](max) NULL,
 CONSTRAINT [MovieUpdateRequest_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieUpdateRequestCrew]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieUpdateRequestCrew](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MovieUpdateRequestId] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[BirthYear] [int] NULL,
	[DeathYear] [int] NULL,
	[Job] [varchar](100) NOT NULL,
	[CharacterName] [varchar](100) NULL,
	[CrewId] [int] NULL,
 CONSTRAINT [MovieUpdateRequestCrew_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieUpdateRequestGenre]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieUpdateRequestGenre](
	[MovieUpdateRequestId] [int] NOT NULL,
	[GenreId] [int] NOT NULL,
 CONSTRAINT [MovieUpdateRequestGenre_pk] PRIMARY KEY CLUSTERED 
(
	[MovieUpdateRequestId] ASC,
	[GenreId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdAuthor] [nvarchar](450) NOT NULL,
	[Content] [varchar](2500) NOT NULL,
	[CreatedAt] [datetime2](2) NOT NULL,
	[LastModifiedAt] [datetime2](2) NULL,
	[Movie_Id] [int] NOT NULL,
 CONSTRAINT [Review_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewRate]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewRate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [nvarchar](450) NOT NULL,
	[Review_Id] [int] NOT NULL,
	[ReviewRateType] [int] NOT NULL,
	[RatedAt] [datetime2](3) NOT NULL,
 CONSTRAINT [ReviewRate_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRelationship]    Script Date: 11-Feb-26 14:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRelationship](
	[IdUser] [nvarchar](450) NOT NULL,
	[IdRelatedUser] [nvarchar](450) NOT NULL,
	[Type] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
 CONSTRAINT [UserRelationship_pk] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC,
	[IdRelatedUser] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Crew] ADD  DEFAULT (N'') FOR [IMDBId]
GO
ALTER TABLE [dbo].[Message] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[MovieCollection] ADD  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[MovieCollection] ADD  DEFAULT ((0)) FOR [Visibility]
GO
ALTER TABLE [dbo].[MovieCollectionMovie] ADD  DEFAULT ((0)) FOR [IdMovieCollection]
GO
ALTER TABLE [dbo].[MovieCollectionUser] ADD  DEFAULT ((0)) FOR [IdMovieCollection]
GO
ALTER TABLE [dbo].[MovieCollectionUser] ADD  DEFAULT ((0)) FOR [Role]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [Message_Recipient] FOREIGN KEY([IdRecipient])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [Message_Recipient]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [Message_Sender] FOREIGN KEY([IdSender])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [Message_Sender]
GO
ALTER TABLE [dbo].[MovieCollectionMovie]  WITH CHECK ADD  CONSTRAINT [MovieCollectionMovie_Movie] FOREIGN KEY([Movie_Id])
REFERENCES [dbo].[Movie] ([Id])
GO
ALTER TABLE [dbo].[MovieCollectionMovie] CHECK CONSTRAINT [MovieCollectionMovie_Movie]
GO
ALTER TABLE [dbo].[MovieCollectionMovie]  WITH CHECK ADD  CONSTRAINT [MovieCollectionMovie_MovieCollection] FOREIGN KEY([IdMovieCollection])
REFERENCES [dbo].[MovieCollection] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MovieCollectionMovie] CHECK CONSTRAINT [MovieCollectionMovie_MovieCollection]
GO
ALTER TABLE [dbo].[MovieCollectionUser]  WITH CHECK ADD  CONSTRAINT [MovieCollectionUsers_MovieCollection] FOREIGN KEY([IdMovieCollection])
REFERENCES [dbo].[MovieCollection] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MovieCollectionUser] CHECK CONSTRAINT [MovieCollectionUsers_MovieCollection]
GO
ALTER TABLE [dbo].[MovieCollectionUser]  WITH CHECK ADD  CONSTRAINT [MovieCollectionUsers_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[MovieCollectionUser] CHECK CONSTRAINT [MovieCollectionUsers_User]
GO
ALTER TABLE [dbo].[MovieCrew]  WITH CHECK ADD  CONSTRAINT [MovieCrew_Crew] FOREIGN KEY([IdCrew])
REFERENCES [dbo].[Crew] ([Id])
GO
ALTER TABLE [dbo].[MovieCrew] CHECK CONSTRAINT [MovieCrew_Crew]
GO
ALTER TABLE [dbo].[MovieCrew]  WITH CHECK ADD  CONSTRAINT [MovieCrew_Movie] FOREIGN KEY([Movie_Id])
REFERENCES [dbo].[Movie] ([Id])
GO
ALTER TABLE [dbo].[MovieCrew] CHECK CONSTRAINT [MovieCrew_Movie]
GO
ALTER TABLE [dbo].[MovieGenre]  WITH CHECK ADD  CONSTRAINT [MovieGenre_Genre] FOREIGN KEY([Genre_Id])
REFERENCES [dbo].[Genre] ([Id])
GO
ALTER TABLE [dbo].[MovieGenre] CHECK CONSTRAINT [MovieGenre_Genre]
GO
ALTER TABLE [dbo].[MovieGenre]  WITH CHECK ADD  CONSTRAINT [MovieGenre_Movie] FOREIGN KEY([Movie_Id])
REFERENCES [dbo].[Movie] ([Id])
GO
ALTER TABLE [dbo].[MovieGenre] CHECK CONSTRAINT [MovieGenre_Movie]
GO
ALTER TABLE [dbo].[MovieOfTheDay]  WITH CHECK ADD  CONSTRAINT [MovieOfTheDay_Movie] FOREIGN KEY([Movie_Id])
REFERENCES [dbo].[Movie] ([Id])
GO
ALTER TABLE [dbo].[MovieOfTheDay] CHECK CONSTRAINT [MovieOfTheDay_Movie]
GO
ALTER TABLE [dbo].[MovieRate]  WITH CHECK ADD  CONSTRAINT [MovieRate_Movie] FOREIGN KEY([Movie_Id])
REFERENCES [dbo].[Movie] ([Id])
GO
ALTER TABLE [dbo].[MovieRate] CHECK CONSTRAINT [MovieRate_Movie]
GO
ALTER TABLE [dbo].[MovieRate]  WITH CHECK ADD  CONSTRAINT [MovieRate_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[MovieRate] CHECK CONSTRAINT [MovieRate_User]
GO
ALTER TABLE [dbo].[MovieUpdateRequest]  WITH CHECK ADD  CONSTRAINT [MovieUpdateRequest_Movie] FOREIGN KEY([Movie_Id])
REFERENCES [dbo].[Movie] ([Id])
GO
ALTER TABLE [dbo].[MovieUpdateRequest] CHECK CONSTRAINT [MovieUpdateRequest_Movie]
GO
ALTER TABLE [dbo].[MovieUpdateRequest]  WITH CHECK ADD  CONSTRAINT [MovieUpdateRequest_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[MovieUpdateRequest] CHECK CONSTRAINT [MovieUpdateRequest_User]
GO
ALTER TABLE [dbo].[MovieUpdateRequestCrew]  WITH CHECK ADD  CONSTRAINT [MovieUpdateRequestCrew_MovieUpdateRequest] FOREIGN KEY([MovieUpdateRequestId])
REFERENCES [dbo].[MovieUpdateRequest] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MovieUpdateRequestCrew] CHECK CONSTRAINT [MovieUpdateRequestCrew_MovieUpdateRequest]
GO
ALTER TABLE [dbo].[MovieUpdateRequestGenre]  WITH CHECK ADD  CONSTRAINT [MovieUpdateRequestGenre_Genre] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([Id])
GO
ALTER TABLE [dbo].[MovieUpdateRequestGenre] CHECK CONSTRAINT [MovieUpdateRequestGenre_Genre]
GO
ALTER TABLE [dbo].[MovieUpdateRequestGenre]  WITH CHECK ADD  CONSTRAINT [MovieUpdateRequestGenre_MovieUpdateRequest] FOREIGN KEY([MovieUpdateRequestId])
REFERENCES [dbo].[MovieUpdateRequest] ([Id])
GO
ALTER TABLE [dbo].[MovieUpdateRequestGenre] CHECK CONSTRAINT [MovieUpdateRequestGenre_MovieUpdateRequest]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [Review_Movie] FOREIGN KEY([Movie_Id])
REFERENCES [dbo].[Movie] ([Id])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [Review_Movie]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [Review_User] FOREIGN KEY([IdAuthor])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [Review_User]
GO
ALTER TABLE [dbo].[ReviewRate]  WITH CHECK ADD  CONSTRAINT [ReviewRate_Review] FOREIGN KEY([Review_Id])
REFERENCES [dbo].[Review] ([Id])
GO
ALTER TABLE [dbo].[ReviewRate] CHECK CONSTRAINT [ReviewRate_Review]
GO
ALTER TABLE [dbo].[ReviewRate]  WITH CHECK ADD  CONSTRAINT [ReviewRate_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ReviewRate] CHECK CONSTRAINT [ReviewRate_User]
GO
ALTER TABLE [dbo].[UserRelationship]  WITH CHECK ADD  CONSTRAINT [UserRelationship_User1] FOREIGN KEY([IdUser])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserRelationship] CHECK CONSTRAINT [UserRelationship_User1]
GO
ALTER TABLE [dbo].[UserRelationship]  WITH CHECK ADD  CONSTRAINT [UserRelationship_User2] FOREIGN KEY([IdRelatedUser])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserRelationship] CHECK CONSTRAINT [UserRelationship_User2]
GO

-- set up initial admin role
INSERT INTO [dbo].[AspNetRoles]
VALUES (1, 'Admin', 'ADMIN', NULL)
GO