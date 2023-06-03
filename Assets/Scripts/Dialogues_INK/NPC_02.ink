INCLUDE globals.ink

{
-NPC_02_haveTalked == 0:
->NPC_02_First_Interaction//you're looking
 -NPC_02_haveTalked == 1 && itemCount > 0: 
->NPC_02_Second_Interaction //Do you have it?
 -NPC_02_haveTalked == 2 && itemCount > 0: 
->SecondChance 
- NPC_02_haveTalked == 3:
-> NPC_02_Out_Of_Dialogue02 //Nothing else to say
- NPC_02_haveTalked == 1:
-> Wait // u sure?
- else:
-> SecondChance 
}

=== NPC_02_First_Interaction === //you're looking
//What?
¿Qué?
...
¿Buscas a alguien?//You're looking for some guy huh.
*[No]
->DONE
*[Sí]
-> Yes

 === Yes ===
//Then go look for this
Pues ahora mismo no recuerdo bien si he visto a alguien pasando por aquí, pero si me traes una seta luminosa, quizás podría acordarme.
    *[Te la traeré]
    ->Wait 
    *[No tengo tiempo para juegos]
    ->DONE
    
=== Wait ===
~NPC_02_haveTalked = 1
//I'll wait here.
Estaré esperando.
->DONE

=== NPC_02_Second_Interaction === //you have item?
//Do you have it?
¿Ya la tienes?
    *[Toma]
    ->Bool
   
    *[No]
    ->DontGiveItem

=== Sassy ===
//Hey! Are you trying to scam me?!
¡Oye, a mi no me la juega nadie!
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
//Are you sure yoy don't want to give me something?
¿Estás seguro de que no quieres darme nada?
    *[Toma]
    ->GiveItem 
    *[No tengo tiempo]
    ->DontGiveItem

=== NPC_02_Out_Of_Dialogue02 === //Nothing else to say
//I have nothing else to say.
No tengo nada más que decir.
->DONE

=== GiveItem ===
~ NPC_02_haveTalked = 3
~removeFromInventory(2)
//It wasn't so difficult, was it? 
Si contiuas por este camino encontrarás al chico. Pero debo advertirte de que un peligro acecha esa zona, quizás ya sea demasiado tarde...
->DONE
    
=== DontGiveItem === //Rascall
~ NPC_02_haveTalked = 1
//Tsk...You little rascall.
Tsk... Que maleducada.
->DONE
    
->END