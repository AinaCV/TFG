VAR numberOfStones = 1
VAR NPC_01_haveTalked = 0
VAR NPC_02_haveTalked = 0 
VAR hasGivenStone = "true"

{ 
- numberOfStones==0: 
~hasGivenStone = "true"
- else: 
~hasGivenStone = "false"
}