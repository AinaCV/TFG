INCLUDE globals.ink
->NPC02

===NPC02
What?
...
You're looking for some guy huh.
*[No]
->No
*[Yes]
-> Yes

 ===Yes
 { 
- numberOfStones:
Mmm...What is that shiny thing in your poket?
Do you want to make a deal? I'll tell you what I know just for that little shiny key...
-else:
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

->END