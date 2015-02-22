CREATE TABLE [dbo].[Total]
(
	MatchId		bigint			NOT NULL, 
	SportId		bigint			NOT NULL, 
	LeagueId	bigint			NOT NULL, 
	Points		decimal(3,2)	NOT NULL, 	
	OverPrice	decimal(3,2)	NOT NULL, 
	UnderPrice	decimal(3,2)	NOT NULL,
	IsAlt		bit				NOT NULL
)
