INCLUDE Globals.ink
VAR haveTalkedTwice = 0
{
//- haveTalked >= 1 + numberOfStones == 0: //Han hablado 1 y tiene 0 piedras
    //-> NPC_02_Take_Player_Home
    
- haveTalked://han hablado 1 vez
    -> NPC_02_Out_Of_Dialogue

- else: //han hablado 0 veces
    -> NPC_02_First_Interaction
}

{
- haveTalkedTwice + numberOfStones < 1:
-> NPC_02_Take_Player_Home
}

=== NPC_02_First_Interaction
~ haveTalked = 1
Hello
Mmm... Is that...
IS THAT A MOONSTONE??!!
Give it to me, please, and I promise I will give you something as valuable as that little precious stone...    
    * [Here, I don't even want it]
      -> GiveMoonstone
    * [Who are you?]
     I am Drugh, I own a little and moitsy cave nearby.
     If you give that treasure... You are welcome in.
        **[Ok, take it.]
        -> GiveMoonstone
        **[I'm noy giving it away.]
        -> DintGiveMoonstone

=== GiveMoonstone
        Lady, you'll make me cry of happiness!! JAJAJAJA. 
        Bye.
        ~ numberOfStones = numberOfStones - 1
        ->DONE
        
=== DintGiveMoonstone
        You'll regret this...
        ->DONE

===NPC_02_Out_Of_Dialogue
What
->DONE

=== NPC_02_Take_Player_Home
Follow me if you want your brother to live.
->DONE

->END