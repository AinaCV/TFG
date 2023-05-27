INCLUDE Globals.ink

{
 - NPC_01_haveTalked + hasItemsinInventory:
->NPC_01_Second_Interaction //Wait!

-else:
->NPC_01_First_Interaction //You don't have anything
}

VAR haveTalkedTwice = 0
{
- haveTalkedTwice + numberOfItems <1:
-> NPC_01_Take_Player_Home
}

=== NPC_01_First_Interaction
~ NPC_01_haveTalked++
You don't have anything interesting...   
    * [I'm looking for my brother, he's...]
      -> Uninterested
    * [...]
     What are you looking at? Leave already.
     -> DONE
     
=== Uninterested ===
So what?
->DONE

=== Negotation ===
        I haven't, there aren't many humans that dare to enter these wood, and the ones that do, are ususally idiots...
        But wait! We can make a deal. 
        If you give that I promise to return the favor, who knows, maybe you'll need something from me in the future...
        *[Ok]
        ->GiveItem
        *[I'll pass]
        ->DintGiveItem->DONE

=== NPC_01_Second_Interaction
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
Something shiny...
->DONE

=== NPC_01_Take_Player_Home
Follow me if you want your brother to live.
->END