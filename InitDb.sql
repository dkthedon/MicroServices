USE microservices
CREATE TABLE IF NOT EXISTS Users (
	UserId INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Name NVARCHAR(32) NOT NULL UNIQUE,
	Age INT NOT NULL,
	Email NVARCHAR(200) NOT NULL
)