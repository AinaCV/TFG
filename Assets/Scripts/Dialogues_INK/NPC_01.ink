INCLUDE Globals.ink


{
 -itemCount > 0: //if true 
->NPC_01_Second_Interaction //You have an item
 -else: //if false
->NPC_01_First_Interaction //You don't have anything
}

{
- NPC_01_haveTalked:
->NPC_01_Out_Of_Dialogue
}

=== NPC_01_First_Interaction
You don't have anything interesting...   
    * [I'm looking for my brother, he's...]
      -> Uninterested
    * [What?]
     What are you looking at? Leave already.
     -> DONE
     
=== Uninterested ===
So what?
->DONE

=== Negotation ===
I haven't, there aren't many humans that dare to enter these woods, and the ones that do, are ususally idiots.
But wait! We can make a deal. 
If you give that I promise to return the favor, who knows, maybe you'll need something from me in the future...
*[Ok]
->GiveItem
*[I'll pass]
->DontGiveItem

=== NPC_01_Second_Interaction
Wait! You, what is that fancy thing you have there?
    *[Who are you?]
     I am Drugh, I own a little and moitsy cave nearby.
     If you give me that treasure... You are welcome in.
    **[Have you seen a young human walking by?]
        -> Negotation
    **[I'm not interested]
        -> DontGiveItem
->DONE
   
=== GiveItem
~hasGivenItem(true)//changes NPC2 dialogue
~removeFromInventory(2)
~NPC_01_haveTalked++
~itemCount--
Lady, you'll make me cry of happiness!! 
Bye.
->DONE
   
=== DontGiveItem
You'll regret this...
->DONE

===NPC_01_Out_Of_Dialogue
Something shiny...
->DONE

=== NPC_01_Take_Player_Home
Follow me if you want your brother to live.
->END