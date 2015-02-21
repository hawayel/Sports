CREATE TABLE [dbo].[Spread]
(
	MatchId		bigint		NOT NULL, 
	SportId		bigint		NOT NULL, 
	LeagueId	bigint		NOT NULL, 
	HomeSpread  decimal		NOT NULL, 
	AwaySpread  decimal		NOT NULL,
	HomePrice	decimal		NOT NULL, 
	AwayPrice	decimal		NOT NULL,
	IsAlt		bit			NOT NULL
)
