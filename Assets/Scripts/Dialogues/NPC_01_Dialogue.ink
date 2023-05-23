INCLUDE Globals.ink
VAR haveTalkedTwice = 0
{
//- haveTalked >= 1 + numberOfItems == 0: //Han hablado 1 y tiene 0 piedras
    //-> NPC_02_Take_Player_Home
    
- NPC_01_haveTalked + numberOfItems >1://han hablado 1 vez 
                                       //y tiene Items
    -> NPC_01_Second_Interaction //Wait! 

- NPC_01_haveTalked + numberOfItems <1: //han hablado 1 vez 
                                       //y no tiene Items
    -> NPC_01_Out_Of_Dialogue //Something shiny...

- else: //han hablado 0 veces
    -> NPC_01_First_Interaction //You don't have anything interesting...   
}

{
- haveTalkedTwice + numberOfItems <1:
-> NPC_01_Take_Player_Home
}

=== NPC_01_Second_Interaction ===
Wait! You, what is that fancy thing you have there?
    **[Have you seen a young human walking by?]
        -> Negotation
    **[I'm not interested]
        -> DintGiveItem
-> DONE

=== NPC_01_First_Interaction ===
~ NPC_01_haveTalked++
You don't have anything interesting...   
    * [I'm looking for my brother, he's...]
      -> Uninterested
    * [...]
     What are you looking at? Leave already.
     -> DONE

=== Negotation ===
        I haven't, there aren't many humans that dare to enter these wood, and the ones that do, are ususally idiots...
        But wait! We can make a deal. 
        If you give that I promise to return the favor, who knows, maybe you'll need something from me in the future...
        *[Ok]
        ->GiveItem
        *[I'll pass]
        ->DintGiveItem

=== Uninterested ===
So what?
->DONE
=== GiveItem ===
  ~ numberOfItems--
        { 
        - numberOfItems == 1: 
        ~hasGivenItem = "false"
        - else: 
        ~numberOfItems = "true"
        }
->DONE

=== DintGiveItem ===
        You'll regret this...
        ->DONE

=== NPC_01_Out_Of_Dialogue ===
Something shiny...
->DONE

=== NPC_01_Take_Player_Home ===
Follow me if you want your brother to live.
->END