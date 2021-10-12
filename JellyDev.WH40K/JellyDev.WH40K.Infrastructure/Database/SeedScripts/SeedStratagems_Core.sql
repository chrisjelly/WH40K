DECLARE @EmptyGuid UNIQUEIDENTIFIER = 0x0;
DECLARE @PhaseCommand INT = 1;
DECLARE @PhaseMovement INT = 2;
DECLARE @PhasePsychic INT = 3;
DECLARE @PhaseShooting INT = 4;
DECLARE @PhaseCharge INT = 5;
DECLARE @PhaseFight INT = 6;
DECLARE @PhaseMorale INT = 6;
DECLARE @StratagemId UNIQUEIDENTIFIER;

SELECT @StratagemId = NEWID();
INSERT INTO WH.Stratagems ([Id], [FactionId], [Name], [Description], [CommandPoints], [Created])
VALUES (@StratagemId, @EmptyGuid, 
	'Counter-Offensive', 
	'Use this Stratagem after an enemy unit has fought in this turn. Select one of your own eligible units and fight with it next.', 
	2, 
	GETUTCDATE());
INSERT INTO WH.Stratagem_Phases ([Phase], [StratagemId], [Created])
VALUES (@PhaseFight, @StratagemId, GETUTCDATE());