INCLUDE globals.ink

{
- NPC_02_haveTalked == 2:
-> NPC_02_Out_Of_Dialogue02
- NPC_02_haveTalked == 1:
-> SecondChance
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
 - !hasGivenItem && itemCount <= 0:
Then go look for this
- !hasGivenItem && itemCount >= 1: 
Mmm...What is that shiny thing in your poket?
Do you want to make a deal? I'll tell you what I know just for that little shiny thing...
- hasGivenItem:
Mmm...And why would I tell you anything? I saw what you gave to that little spiteful guy... You could give me something like that too...
}
    **[Ok...]
    ->GiveItem 
    **[No]
    ->DontGiveItem
    
=== No ===
-> DONE

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

=== NPC_02_Out_Of_Dialogue02 ===
I have nothing else to say.
->DONE

=== GiveItem ===
~ NPC_02_haveTalked = 2
It wasn't so difficult, was it? Jajajaja.
->DONE
    
=== DontGiveItem ===
~ NPC_02_haveTalked = 1
Tsk...You little rascall.
->DONE
    
->END