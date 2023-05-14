VAR numberOfStones = 1
VAR NPC_01_haveTalked = 0
VAR NPC_02_haveTalked = 0 
VAR hasGivenStone = "true"

{ 
- numberOfStones == 1: 
~hasGivenStone = "false"
- else: 
~hasGivenStone = "true"
}