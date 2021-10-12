DECLARE @EmptyGuid UNIQUEIDENTIFIER = 0x0;
DECLARE @PhaseCommand INT = 1;
DECLARE @PhaseMovement INT = 2;
DECLARE @PhasePsychic INT = 3;
DECLARE @PhaseShooting INT = 4;
DECLARE @PhaseCharge INT = 5;
DECLARE @PhaseFight INT = 6;
DECLARE @PhaseMorale INT = 6;
DECLARE @StratagemId UNIQUEIDENTIFIER;

DECLARE @FactionId UNIQUEIDENTIFIER;
SELECT @FactionId = Id
FROM WH.Factions
WHERE Name = 'Space Marines';

SELECT @StratagemId = NEWID();
INSERT INTO WH.Stratagems ([Id], [FactionId], [Name], [Description], [CommandPoints], [Created])
VALUES (@StratagemId, @FactionId, 
	'Death to the Traitors!', 
	'Use this Stratagem in the Fight phase, when an Adeptus Astartes unit from your army is selected to fight. ' + 
	'Until the end of the phase, each time a model in that unit makes a melee attack against a Heretic Astartes unit, you can re-roll the hit roll.', 
	1, 
	GETUTCDATE());
INSERT INTO WH.Stratagem_Phases ([Phase], [StratagemId], [Created])
VALUES (@PhaseFight, @StratagemId, GETUTCDATE());