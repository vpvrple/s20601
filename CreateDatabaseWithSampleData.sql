-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2025-02-12 20:49:18.431

-- tables
-- Table: ActivityType
CREATE TABLE ActivityType (
    Id int  NOT NULL,
    Name int  NOT NULL,
    CONSTRAINT ActivityType_pk PRIMARY KEY  (Id)
);

-- Table: Comment
CREATE TABLE Comment (
    Id int  NOT NULL,
    IdPost int  NOT NULL,
    IdUser int  NOT NULL,
    IdComment int  NULL,
    CreatedAt datetime2(2)  NOT NULL,
    LastModifiedAt datetime2(2)  NULL,
    Content varchar(500)  NOT NULL,
    CONSTRAINT Comment_pk PRIMARY KEY  (Id)
);

-- Table: Crew
CREATE TABLE Crew (
    Id int  NOT NULL,
    FirstName varchar(100)  NOT NULL,
    LastName varchar(100)  NOT NULL,
    BirthYear int  NOT NULL,
    DeathYear int  NULL,
    CONSTRAINT Crew_pk PRIMARY KEY  (Id)
);

-- Table: Genre
CREATE TABLE Genre (
    Id int  NOT NULL,
    Name varchar(50)  NOT NULL,
    CONSTRAINT Genre_pk PRIMARY KEY  (Id)
);

-- Table: Group
CREATE TABLE "Group" (
    Id int  NOT NULL,
    Name int  NOT NULL,
    Description varchar(100)  NULL,
    Image varbinary(100)  NOT NULL,
    CONSTRAINT Group_pk PRIMARY KEY  (Id)
);

-- Table: GroupMembership
CREATE TABLE GroupMembership (
    IdUser int  NOT NULL,
    IdGroup int  NOT NULL,
    JoinedAt datetime2(2)  NOT NULL,
    CONSTRAINT GroupMembership_pk PRIMARY KEY  (IdUser,IdGroup)
);

-- Table: GroupOwnership
CREATE TABLE GroupOwnership (
    IdOwner int  NOT NULL,
    IdGroup int  NOT NULL,
    CONSTRAINT GroupOwnership_pk PRIMARY KEY  (IdOwner,IdGroup)
);

-- Table: Message
CREATE TABLE Message (
    Id int  NOT NULL,
    IdSender int  NOT NULL,
    IdRecipient int  NOT NULL,
    Created int  NOT NULL,
    Content varchar(2500)  NOT NULL,
    IdStatus int  NOT NULL,
    DeliverTime datetime2(2)  NULL,
    CONSTRAINT Message_pk PRIMARY KEY  (Id)
);

-- Table: Movie
CREATE TABLE Movie (
    Id varchar(10)  NOT NULL,
    Title varchar(100)  NOT NULL,
    OriginalTitle varchar(100)  NOT NULL,
    StartYear int  NOT NULL,
    EndYear int  NULL,
    RuntimeMinutes int  NOT NULL,
    TitleType varchar(50)  NOT NULL,
    CONSTRAINT Movie_pk PRIMARY KEY  (Id)
);

-- Table: MovieCollection
CREATE TABLE MovieCollection (
    Id int  NOT NULL,
    Name varchar(100)  NOT NULL,
    Description varchar(250)  NOT NULL,
    CreatedAt datetime2(2)  NOT NULL,
    CONSTRAINT MovieCollection_pk PRIMARY KEY  (Id)
);

-- Table: MovieCollectionMovie
CREATE TABLE MovieCollectionMovie (
    IdMovieCollection int  NOT NULL,
    AddedAt datetime2(2)  NOT NULL,
    Movie_Id varchar(10)  NOT NULL,
    CONSTRAINT MovieCollectionMovie_pk PRIMARY KEY  (IdMovieCollection)
);

-- Table: MovieCollectionUser
CREATE TABLE MovieCollectionUser (
    Id int  NOT NULL,
    IdUser int  NOT NULL,
    IdMovieCollection int  NOT NULL,
    IdRole int  NOT NULL,
    CONSTRAINT MovieCollectionUser_ak_1 UNIQUE (IdUser, IdMovieCollection, IdRole),
    CONSTRAINT MovieCollectionUser_pk PRIMARY KEY  (Id)
);

-- Table: MovieCrew
CREATE TABLE MovieCrew (
    Id int  NOT NULL,
    IdCrew int  NOT NULL,
    Job varchar(100)  NOT NULL,
    CharacterName varchar(100)  NULL,
    Movie_Id varchar(10)  NOT NULL,
    CONSTRAINT MovieCrew_pk PRIMARY KEY  (Id)
);

-- Table: MovieGenre
CREATE TABLE MovieGenre (
    IdMovieGenre int  NOT NULL,
    Movie_Id varchar(10)  NOT NULL,
    Genre_Id int  NOT NULL,
    CONSTRAINT MovieGenre_pk PRIMARY KEY  (IdMovieGenre)
);

-- Table: MovieOfTheDay
CREATE TABLE MovieOfTheDay (
    Movie_Id varchar(10)  NOT NULL,
    Date datetime2(2)  NOT NULL,
    CONSTRAINT MovieOfTheDay_pk PRIMARY KEY  (Movie_Id)
);

-- Table: MovieRate
CREATE TABLE MovieRate (
    Id int  NOT NULL,
    IdUser int  NOT NULL,
    Rating tinyint  NOT NULL,
    RatedAt datetime2(2)  NOT NULL,
    Movie_Id varchar(10)  NOT NULL,
    CONSTRAINT UniqueRating UNIQUE (IdUser, Movie_Id),
    CONSTRAINT MovieRate_pk PRIMARY KEY  (Id)
);

-- Table: MovieUpdateRequest
CREATE TABLE MovieUpdateRequest (
    Id int  NOT NULL,
    IdUser int  NOT NULL,
    Description varchar(2500)  NOT NULL,
    IsAccepted bit  NULL,
    CreatedAt datetime2(2)  NOT NULL,
    Movie_Id varchar(10)  NOT NULL,
    CONSTRAINT MovieUpdateRequest_pk PRIMARY KEY  (Id)
);

-- Table: Permission
CREATE TABLE Permission (
    Id int  NOT NULL,
    Type varchar(25)  NOT NULL,
    CONSTRAINT Permission_pk PRIMARY KEY  (Id)
);

-- Table: PermissionRole
CREATE TABLE PermissionRole (
    Permission_Id int  NOT NULL,
    Role_Id int  NOT NULL,
    CONSTRAINT PermissionRole_pk PRIMARY KEY  (Permission_Id,Role_Id)
);

-- Table: Post
CREATE TABLE Post (
    Id int  NOT NULL,
    Title varchar(150)  NOT NULL,
    Content varchar(5000)  NOT NULL,
    IdGroup int  NOT NULL,
    IdUser int  NOT NULL,
    CreatedAt datetime2(2)  NOT NULL,
    LastModifiedAt int  NULL,
    CONSTRAINT Post_pk PRIMARY KEY  (Id)
);

-- Table: Review
CREATE TABLE Review (
    IdAuthor int  NOT NULL,
    Content varchar(2500)  NOT NULL,
    CreatedAt datetime2(2)  NOT NULL,
    LastModifiedAt datetime2(2)  NULL,
    Movie_Id varchar(10)  NOT NULL,
    Id int  NOT NULL,
    CONSTRAINT Review_pk PRIMARY KEY  (Id)
);

-- Table: ReviewRate
CREATE TABLE ReviewRate (
    Id int  NOT NULL,
    IdUser int  NOT NULL,
    Rating int  NOT NULL,
    RatedAt datetime2(2)  NOT NULL,
    Review_Id int  NOT NULL,
    CONSTRAINT ReviewRate_pk PRIMARY KEY  (Id)
);

-- Table: Role
CREATE TABLE Role (
    Id int  NOT NULL,
    Name varchar(25)  NOT NULL,
    CONSTRAINT Role_pk PRIMARY KEY  (Id)
);

-- Table: SocialActivityLog
CREATE TABLE SocialActivityLog (
    Id int  NOT NULL,
    IdUser int  NOT NULL,
    IdActivityType int  NOT NULL,
    ActivityAt datetime2(2)  NOT NULL,
    CONSTRAINT SocialActivityLog_pk PRIMARY KEY  (Id)
);

-- Table: Status
CREATE TABLE Status (
    Id int  NOT NULL,
    Type varchar(50)  NOT NULL,
    CONSTRAINT Status_pk PRIMARY KEY  (Id)
);

-- Table: User
CREATE TABLE "User" (
    Id int  NOT NULL,
    UserName varchar(50)  NOT NULL,
    Email varchar(150)  NOT NULL,
    PasswordHash varchar(100)  NOT NULL,
    CreatedAt datetime2(2)  NOT NULL,
    ReputationPoints int  NOT NULL,
    LastLogin datetime2(2)  NULL,
    ProfileDescription varchar(500)  NULL,
    ProfileImage varbinary(100)  NULL,
    CONSTRAINT User_pk PRIMARY KEY  (Id)
);

-- Table: UserRelationship
CREATE TABLE UserRelationship (
    IdUser int  NOT NULL,
    IdRelatedUser int  NOT NULL,
    Type varchar(50)  NOT NULL,
    CONSTRAINT UserRelationship_pk PRIMARY KEY  (IdUser,IdRelatedUser)
);

-- foreign keys
-- Reference: ActivityLog_ActivityType (table: SocialActivityLog)
ALTER TABLE SocialActivityLog ADD CONSTRAINT ActivityLog_ActivityType
    FOREIGN KEY (IdActivityType)
    REFERENCES ActivityType (Id);

-- Reference: ActivityLog_User (table: SocialActivityLog)
ALTER TABLE SocialActivityLog ADD CONSTRAINT ActivityLog_User
    FOREIGN KEY (IdUser)
    REFERENCES "User" (Id);

-- Reference: Comment_Comment (table: Comment)
ALTER TABLE Comment ADD CONSTRAINT Comment_Comment
    FOREIGN KEY (IdComment)
    REFERENCES Comment (Id);

-- Reference: Comment_Post (table: Comment)
ALTER TABLE Comment ADD CONSTRAINT Comment_Post
    FOREIGN KEY (IdPost)
    REFERENCES Post (Id);

-- Reference: Comment_User (table: Comment)
ALTER TABLE Comment ADD CONSTRAINT Comment_User
    FOREIGN KEY (IdUser)
    REFERENCES "User" (Id);

-- Reference: GroupMembership_Group (table: GroupMembership)
ALTER TABLE GroupMembership ADD CONSTRAINT GroupMembership_Group
    FOREIGN KEY (IdGroup)
    REFERENCES "Group" (Id);

-- Reference: GroupMembership_User (table: GroupMembership)
ALTER TABLE GroupMembership ADD CONSTRAINT GroupMembership_User
    FOREIGN KEY (IdUser)
    REFERENCES "User" (Id);

-- Reference: GroupOwnership_Group (table: GroupOwnership)
ALTER TABLE GroupOwnership ADD CONSTRAINT GroupOwnership_Group
    FOREIGN KEY (IdGroup)
    REFERENCES "Group" (Id);

-- Reference: GroupOwnership_User (table: GroupOwnership)
ALTER TABLE GroupOwnership ADD CONSTRAINT GroupOwnership_User
    FOREIGN KEY (IdOwner)
    REFERENCES "User" (Id);

-- Reference: Message_Recipient (table: Message)
ALTER TABLE Message ADD CONSTRAINT Message_Recipient
    FOREIGN KEY (IdRecipient)
    REFERENCES "User" (Id);

-- Reference: Message_Sender (table: Message)
ALTER TABLE Message ADD CONSTRAINT Message_Sender
    FOREIGN KEY (IdSender)
    REFERENCES "User" (Id);

-- Reference: Message_Status (table: Message)
ALTER TABLE Message ADD CONSTRAINT Message_Status
    FOREIGN KEY (IdStatus)
    REFERENCES Status (Id);

-- Reference: MovieCollectionMovie_Movie (table: MovieCollectionMovie)
ALTER TABLE MovieCollectionMovie ADD CONSTRAINT MovieCollectionMovie_Movie
    FOREIGN KEY (Movie_Id)
    REFERENCES Movie (Id);

-- Reference: MovieCollectionMovie_MovieCollection (table: MovieCollectionMovie)
ALTER TABLE MovieCollectionMovie ADD CONSTRAINT MovieCollectionMovie_MovieCollection
    FOREIGN KEY (IdMovieCollection)
    REFERENCES MovieCollection (Id);

-- Reference: MovieCollectionUsers_MovieCollection (table: MovieCollectionUser)
ALTER TABLE MovieCollectionUser ADD CONSTRAINT MovieCollectionUsers_MovieCollection
    FOREIGN KEY (IdMovieCollection)
    REFERENCES MovieCollection (Id);

-- Reference: MovieCollectionUsers_Role (table: MovieCollectionUser)
ALTER TABLE MovieCollectionUser ADD CONSTRAINT MovieCollectionUsers_Role
    FOREIGN KEY (IdRole)
    REFERENCES Role (Id);

-- Reference: MovieCollectionUsers_User (table: MovieCollectionUser)
ALTER TABLE MovieCollectionUser ADD CONSTRAINT MovieCollectionUsers_User
    FOREIGN KEY (IdUser)
    REFERENCES "User" (Id);

-- Reference: MovieCrew_Crew (table: MovieCrew)
ALTER TABLE MovieCrew ADD CONSTRAINT MovieCrew_Crew
    FOREIGN KEY (IdCrew)
    REFERENCES Crew (Id);

-- Reference: MovieCrew_Movie (table: MovieCrew)
ALTER TABLE MovieCrew ADD CONSTRAINT MovieCrew_Movie
    FOREIGN KEY (Movie_Id)
    REFERENCES Movie (Id);

-- Reference: MovieGenre_Genre (table: MovieGenre)
ALTER TABLE MovieGenre ADD CONSTRAINT MovieGenre_Genre
    FOREIGN KEY (Genre_Id)
    REFERENCES Genre (Id);

-- Reference: MovieGenre_Movie (table: MovieGenre)
ALTER TABLE MovieGenre ADD CONSTRAINT MovieGenre_Movie
    FOREIGN KEY (Movie_Id)
    REFERENCES Movie (Id);

-- Reference: MovieOfTheDay_Movie (table: MovieOfTheDay)
ALTER TABLE MovieOfTheDay ADD CONSTRAINT MovieOfTheDay_Movie
    FOREIGN KEY (Movie_Id)
    REFERENCES Movie (Id);

-- Reference: MovieRate_Movie (table: MovieRate)
ALTER TABLE MovieRate ADD CONSTRAINT MovieRate_Movie
    FOREIGN KEY (Movie_Id)
    REFERENCES Movie (Id);

-- Reference: MovieRate_User (table: MovieRate)
ALTER TABLE MovieRate ADD CONSTRAINT MovieRate_User
    FOREIGN KEY (IdUser)
    REFERENCES "User" (Id);

-- Reference: MovieUpdateRequest_Movie (table: MovieUpdateRequest)
ALTER TABLE MovieUpdateRequest ADD CONSTRAINT MovieUpdateRequest_Movie
    FOREIGN KEY (Movie_Id)
    REFERENCES Movie (Id);

-- Reference: MovieUpdateRequest_User (table: MovieUpdateRequest)
ALTER TABLE MovieUpdateRequest ADD CONSTRAINT MovieUpdateRequest_User
    FOREIGN KEY (IdUser)
    REFERENCES "User" (Id);

-- Reference: PermissionRole_Permission (table: PermissionRole)
ALTER TABLE PermissionRole ADD CONSTRAINT PermissionRole_Permission
    FOREIGN KEY (Permission_Id)
    REFERENCES Permission (Id);

-- Reference: PermissionRole_Role (table: PermissionRole)
ALTER TABLE PermissionRole ADD CONSTRAINT PermissionRole_Role
    FOREIGN KEY (Role_Id)
    REFERENCES Role (Id);

-- Reference: Post_Group (table: Post)
ALTER TABLE Post ADD CONSTRAINT Post_Group
    FOREIGN KEY (IdGroup)
    REFERENCES "Group" (Id);

-- Reference: Post_User (table: Post)
ALTER TABLE Post ADD CONSTRAINT Post_User
    FOREIGN KEY (IdUser)
    REFERENCES "User" (Id);

-- Reference: ReviewRate_Review (table: ReviewRate)
ALTER TABLE ReviewRate ADD CONSTRAINT ReviewRate_Review
    FOREIGN KEY (Review_Id)
    REFERENCES Review (Id);

-- Reference: ReviewRate_User (table: ReviewRate)
ALTER TABLE ReviewRate ADD CONSTRAINT ReviewRate_User
    FOREIGN KEY (IdUser)
    REFERENCES "User" (Id);

-- Reference: Review_Movie (table: Review)
ALTER TABLE Review ADD CONSTRAINT Review_Movie
    FOREIGN KEY (Movie_Id)
    REFERENCES Movie (Id);

-- Reference: Review_User (table: Review)
ALTER TABLE Review ADD CONSTRAINT Review_User
    FOREIGN KEY (IdAuthor)
    REFERENCES "User" (Id);

-- Reference: UserRelationship_User1 (table: UserRelationship)
ALTER TABLE UserRelationship ADD CONSTRAINT UserRelationship_User1
    FOREIGN KEY (IdUser)
    REFERENCES "User" (Id);

-- Reference: UserRelationship_User2 (table: UserRelationship)
ALTER TABLE UserRelationship ADD CONSTRAINT UserRelationship_User2
    FOREIGN KEY (IdRelatedUser)
    REFERENCES "User" (Id);

-- End of file.



INSERT INTO Movie
VALUES 
('t001', 'TitleNumberOne', 'OriginalTitleNumberOne', 2025, NULL, 120, 'movie'),
('t002', 'TitleNumberTwo', 'OriginalTitleNumberTwo', 2020, NULL, 180, 'movie'),
('t003', 'TitleNumberThree', 'OriginalTitleNumberThree', 2026, NULL, 230, 'movie'),
('t004', 'TitleNumberFour', 'OriginalTitleNumberFour', 2027, NULL, 230, 'movie'),
('t005', 'TitleNumberFive', 'OriginalTitleNumberFive', 2028, NULL, 330, 'movie'),
('t006', 'TitleNumberSix', 'OriginalTitleNumberSix', 2029, NULL, 1110, 'movie'),
('t007', 'TitleNumberSeven', 'OriginalTitleNumberSeven', 2021, NULL, 999, 'movie'),
('t008', 'TitleNumberEight', 'OriginalTitleNumberEight', 2020, NULL, 190, 'movie'),
('t009', 'TitleNumberNine', 'OriginalTitleNumberNine', 2025, NULL, 200, 'movie'),
('t0010', 'TitleNumberTen', 'OriginalTitleNumberTen', 2025, NULL, 210, 'movie')

INSERT INTO MovieOfTheDay
VALUES 
('t003', GETDATE()),
('t002', GETDATE()-1),
('t001', GETDATE()-2),
('t005', GETDATE()-3),
('t006', GETDATE()-5),
('t007', GETDATE()-5),
('t008', GETDATE()-6),
('t009', GETDATE()-9)

INSERT INTO Genre
VALUES 
(1, 'Action'),
(2, 'Thriller'),
(3, 'Comedy')

INSERT INTO MovieGenre
VALUES 
(1, 't003', 1),
(2, 't003', 2),
(3, 't002', 1),
(4, 't002', 3),
(5, 't001', 2),
(6, 't001', 3)

INSERT INTO Crew
VALUES
(1, 'Jan', 'Kowalski', 1990, NULL),
(2, 'Roman', 'Testowy', 1989, NULL),
(3, 'Adam', 'Nowak', 1890, 1990)

INSERT INTO MovieCrew
VALUES
(1, 1, 'Actor', 'TestCharacterName', 't001'),
(2, 1, 'Writer', NULL, 't001'),
(3, 1, 'Director', NULL, 't001'),
(4, 2, 'Writer', NULL, 't001'),
(5, 2, 'Actor', 'TestCharacterName2', 't001'),
(6, 2, 'Actor', 'TestCharacterName3','t002'),
(7, 2, 'Writer', NULL, 't002'),
(8, 2, 'Director', NULL, 't002'),
(9, 3, 'Writer', NULL, 't002'),
(10, 3, 'Actor', 'TestCharacterName5', 't002')

INSERT INTO MovieCrew
VALUES
(11, 1, 'Actor', 'TestCharacterName', 't003'),
(12, 1, 'Writer', NULL, 't003'),
(13, 1, 'Director', NULL, 't003'),
(14, 2, 'Writer', NULL, 't003'),
(15, 2, 'Actor', 'TestCharacterName2', 't003')

INSERT INTO [dbo].[User]
VALUES
(1, 'admin', 'admin@admin.pl', '1G!@#^!', GETDATE(), 999, GETDATE(), 'admin description', 0),
(2, 'test', 'test@test.pl', '1G!@#^!', GETDATE(), 0, GETDATE(), 'test description', 0) 

INSERT INTO MovieRate
VALUES
(2, 2, 5, GETDATE(), 't001')

INSERT INTO Review
VALUES
(1, 'review content.....123', GETDATE()-1, GETDATE(),'t001', 1)

INSERT INTO ReviewRate
VALUES
(1, 1, 1, GETDATE(), 1)

INSERT INTO MovieCollection
VALUES
(1, 'CollectionNumberOne', 'Collection description...', GETDATE()),
(2, 'CollectionNumberTwo', 'Collection description...', GETDATE()),
(3, 'CollectionNumberThree', 'Collection description...', GETDATE()),
(5, 'CollectionNumberFive', 'Collection description...', GETDATE()),
(6, 'CollectionNumberSix', 'Collection description...', GETDATE()),
(7, 'CollectionNumberSeven', 'Collection description...', GETDATE()),
(8, 'CollectionNumberEight', 'Collection description...', GETDATE()),
(9, 'CollectionNumberNine', 'Collection description...', GETDATE())