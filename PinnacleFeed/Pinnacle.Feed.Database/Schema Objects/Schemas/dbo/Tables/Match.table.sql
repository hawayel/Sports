CREATE TABLE [dbo].[Match]
(
	Id			bigint			NOT NULL, 
	SportId		bigint			NOT NULL, 
	LeagueId	bigint			NOT NULL, 
	StartTime	DateTime		NOT NULL, 
	HomeTeam	nvarchar(32)	NOT NULL, 
	AwayTeam	nvarchar(32)	NOT NULL, 
)
