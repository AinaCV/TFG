INCLUDE globals.ink

{
-NPC_02_haveTalked == 0:
->NPC_02_First_Interaction//you're looking
 -NPC_02_haveTalked == 1 && itemCount > 0: 
->NPC_02_Second_Interaction //Do you have it?
 -NPC_02_haveTalked == 2 && itemCount > 0: 
->SecondChance 
- NPC_02_haveTalked == 2:
-> NPC_02_Out_Of_Dialogue02 //Nothing else to say
- NPC_02_haveTalked == 1:
-> Wait // u sure?
- else:
-> SecondChance 
}

=== NPC_02_First_Interaction === //you're looking
What?
...
You're looking for some guy huh.
*[No]
->DONE
*[Yes]
-> Yes

 === Yes ===
Then go look for this
    *[Ok...]
    ->Wait 
    *[No]
    ->DONE
    
=== Wait ===
~NPC_02_haveTalked = 1
I'll wait here.
->DONE

=== NPC_02_Second_Interaction === //you have item?
Do you have it?
    *[Yes]
    ->Bool
   
    *[No]
    ->DontGiveItem

=== Sassy ===
Hey! Are you trying to scam me?!
->DONE

=== Bool ===
 {
 - itemCount>=1:
->GiveItem
-itemCount<=0:
->Sassy
 }
->DONE

=== SecondChance === // u sure?
~ NPC_02_haveTalked = 2
Are you sure yoy don't want to give me something?
Jejeje...
    *[Here]
    ->GiveItem 
    *[Shut up]
    ->DontGiveItem

=== NPC_02_Out_Of_Dialogue02 === //Nothing else to say
I have nothing else to say.
->DONE

=== GiveItem ===
~ NPC_02_haveTalked = 2
It wasn't so difficult, was it? Jajajaja.
->DONE
    
=== DontGiveItem === //Rascall
~ NPC_02_haveTalked = 1
Tsk...You little rascall.
->DONE
    
->END