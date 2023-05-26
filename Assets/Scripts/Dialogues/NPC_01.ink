INCLUDE Globals.ink
VAR haveTalkedTwice = 0
{
//- haveTalked >= 1 + numberOfStones == 0: //Han hablado 1 y tiene 0 piedras
    //-> NPC_02_Take_Player_Home
    
- NPC_01_haveTalked://han hablado 1 vez
    -> NPC_01_Out_Of_Dialogue

- else: //han hablado 0 veces
    -> NPC_01_First_Interaction
}

{
- haveTalkedTwice + numberOfItems <1:
-> NPC_01_Take_Player_Home
}

=== NPC_01_First_Interaction
{
- numberOfItems:
~ NPC_01_haveTalked++
Hello
Mmm... Is that...
IS THAT A MOONSTONE??!!
Give it to me, please, and I promise I will give you something as valuable as that little precious stone...    
    * [Here, I don't even want it]
      -> GiveItem
    * [Who are you?]
     I am Drugh, I own a little and moitsy cave nearby.
     If you give that treasure... You are welcome in.
        **[Ok, take it.]
        -> GiveItem
        **[I'm noy giving it away.]
        ->DintGiveItem
- else:
    What?
}
    ->DONE

=== GiveItem
        Lady, you'll make me cry of happiness!! JAJAJAJA. 
        Bye.
        ~hasGivenItem = "true"
        ->DONE
        
=== DintGiveItem
        You'll regret this...
       ~hasGivenItem = "false"
        ->DONE

===NPC_01_Out_Of_Dialogue
What else do you want?
->DONE

=== NPC_01_Take_Player_Home
Follow me if you want your brother to live.
->END