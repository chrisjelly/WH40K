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
WHERE Name = 'Necrons';

SELECT @StratagemId = NEWID();
INSERT INTO WH.Stratagems ([Id], [FactionId], [Name], [Description], [CommandPoints], [Created])
VALUES (@StratagemId, @FactionId, 
	'Techno-Oracular Targeting', 
	'Use this Stratagem in your Shooting phase, before making the wound roll for an attack made by a Necrons model from your army. Do not make a wound roll for that attack: it automatically wounds the target.', 
	1, 
	GETUTCDATE());
INSERT INTO WH.Stratagem_Phases ([Phase], [StratagemId], [Created])
VALUES (@PhaseShooting, @StratagemId, GETUTCDATE());