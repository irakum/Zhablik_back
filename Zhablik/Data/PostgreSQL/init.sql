CREATE TABLE Users
(
    UserID UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Authenticated BOOLEAN,
    Coins INT,
    CONSTRAINT UQ_Username UNIQUE (Username),
    CONSTRAINT UQ_Email UNIQUE (Email)
);

CREATE TABLE IF NOT EXISTS Assignments (
    TaskID UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserID UUID REFERENCES Users(UserID),
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(1000),
    DueDate TIMESTAMP NOT NULL,
    Level INT CHECK (Level >= 1 AND Level <= 3),
    IsComplete BOOLEAN
    );

CREATE TABLE FrogInfo
(
    FrogInfoID UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(1000) NOT NULL,
    Price INT NOT NULL,
    UpgradePrice INT NOT NULL
);


CREATE TABLE UserFrogs
(
    UserFrogID UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserID UUID REFERENCES Users(UserID),
    FrogInfoID UUID REFERENCES FrogInfo(FrogInfoID),
    Level INT,
    CONSTRAINT FK_UserFrog_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_UserFrog_FrogInfo FOREIGN KEY (FrogInfoID) REFERENCES FrogInfo(FrogInfoID),
    CONSTRAINT CK_UserFrog_Level CHECK (Level >= 1 AND Level <= 5)
);



