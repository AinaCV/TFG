INCLUDE globals.ink

{
- NPC_02_haveTalked == 2:
-> NPC_02_Out_Of_Dialogue_02 //nothing else
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
- numberOfItems: //se cumple la condición si el numero no es 0 = no ha dado la piedra
Mmm...What is that shiny thing in your poket?
Do you want to make a deal? I'll tell you what I know just for that little shiny key...
-else://si tien 0 piedras
Mmm...And why would I tell you anything? I saw what you gave to that little spiteful guy... You could give me something like that too...
}
    **[Ok...]
    ->GiveKey 
    **[No]
    ->DontGiveKey
    ->DONE
    
=== No ===
-> DONE

=== SecondChance ===
~ NPC_02_haveTalked = 2
Are you sure yoy don't want to give me something?
Jejeje...
    *[Here]
    ->GiveKey 
    ->DONE
    *[Shut up]
    ->DontGiveKey
    ->DONE

=== NPC_02_Out_Of_Dialogue_02 ===
I have nothing else to say.
->DONE

=== GiveKey ===
~ NPC_02_haveTalked = 2
It wasn't so difficult, was it? Jajajaja.
->DONE
    
=== DontGiveKey ===
~ NPC_02_haveTalked = 1
Tsk...You little rascall.
->DONE
    
->END