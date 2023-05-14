INCLUDE globals.ink

{
- NPC_02_haveTalked:
-> NPC_02_Out_Of_Dialogue02
- else:
-> NPC_02_First_Interaction
}

=== NPC_02_First_Interaction ===
~ NPC_02_haveTalked++
What?
...
You're looking for some guy huh.
*[No]
->No
*[Yes]
-> Yes

 ===Yes
 { 
- numberOfStones: //se cumple la condición si el numero no es 0 = no ha dado la piedra
Mmm...What is that shiny thing in your poket?
Do you want to make a deal? I'll tell you what I know just for that little shiny key...
-else://si tien 0 piedras
Mmm...And why would I tell you anything? I saw what you gave to that little spiteful guy... You could give me something like that too...
}
    **[Ok...]
    ->GiveKey 
    **[No]
    ->DontGiveKey

    ===GiveKey
    It wasn't so difficult, was it? Jajajaja.
    ->DONE
    ===DontGiveKey
    Tsk...You little rascall.
    ->DONE

===No
-> DONE

=== NPC_02_Out_Of_Dialogue02 ===
I have nothing else to say.
->END