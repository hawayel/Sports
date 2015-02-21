CREATE TABLE [dbo].[Total]
(
	MatchId		bigint		NOT NULL, 
	SportId		bigint		NOT NULL, 
	LeagueId	bigint		NOT NULL, 
	Points		decimal		NOT NULL, 	
	OverPrice	decimal		NOT NULL, 
	UnderPrice	decimal		NOT NULL,
	IsAlt		bit			NOT NULL
)
