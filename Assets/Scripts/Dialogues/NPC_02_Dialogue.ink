INCLUDE globals.ink

{
- NPC_02_haveTalked == 2:
-> NPC_02_Out_Of_Dialogue //nothing else
- NPC_02_haveTalked == 1:
-> SecondChance // u sure?
- else:
-> NPC_02_First_Interaction
}

=== NPC_02_First_Interaction ===
What?
...
You're looking for some guy huh.
*[No]
->No
*[Yes]
-> Yes

 === Yes ===
 { 
- numberOfItems: //se cumple la condición si el numero no es 0 = no ha dado ningún item
Mmm...Well, I could tell you what I know... But in exchange, I need you to get something for me
    *[Ok...]
    This kinf of mushroon grows in the wherearound of the forest, you should go there.
    ->Waiting
    *[I don't have time]
    Suit yourself.
    ->DONE

-else://si tien 0 items
Mmm...And why would I tell you anything? I saw what you gave to that little spiteful guy... You could give me something like that too...
    I've been looking for a shiny mushroom, if you bring me one I'll tell you what I know.
    *[Ok...]
    The mushroon grows in the wherearound of the forest, you should go there.
    ->GiveItem 
    *[I don't have time to play games]
    ->DontGiveItem
}
    
=== No ===
-> DONE

=== Waiting ===
I'll wait here.
->DONE

=== SecondChance ===
~ NPC_02_haveTalked = 2
Are you sure yoy don't want to give me something?
Jejeje...
    *[Here]
    ->GiveItem 
    ->DONE
    *[Shut up]
    ->DontGiveItem
    ->DONE

=== NPC_02_Out_Of_Dialogue ===
I have nothing else to say.
->DONE

=== GiveItem ===
~ NPC_02_haveTalked = 2
I saw the boy going into the deeps of the woods, if I were you I wouldn't follow him.
there is an strange and old magic enchanting these woods, he'll be lucky if he lives to see tomorrow.
->DONE
    
=== DontGiveItem ===
~ NPC_02_haveTalked = 1
Tsk...You little rascall.
->DONE
    
->END