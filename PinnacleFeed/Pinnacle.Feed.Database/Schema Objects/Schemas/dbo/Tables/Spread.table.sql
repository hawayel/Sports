CREATE TABLE [dbo].[Spread]
(
	MatchId		bigint			NOT NULL, 
	SportId		bigint			NOT NULL, 
	LeagueId	bigint			NOT NULL, 
	HomeSpread  decimal(3,2)	NOT NULL, 
	AwaySpread  decimal(3,2)	NOT NULL,
	HomePrice	decimal(3,2)	NOT NULL, 
	AwayPrice	decimal(3,2)	NOT NULL,
	IsAlt		bit				NOT NULL
)
