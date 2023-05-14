INCLUDE Globals.ink
-> NPC01
=== NPC01 ===
Hello
Mmm... Is that...
IS THAT A MOONSTONE??!!
Give it to me, please, and I promise I will give you something as valuable as that little precious stone...    
    * [Here, I don't even want it]
      -> GiveMoonstone
    * [Who are you?]
     I am Drugh, I own a little and moitsy cave nearby.
     If you give that treasure... You are welcome in.
        **[Ok, take it.]
        -> GiveMoonstone
        **[I'm noy giving it away.]
        -> DintGiveMoonstone

=== GiveMoonstone
        Lady, you'll make me cry of happiness!! JAJAJAJA. 
        Bye.
        ~ numberOfStones = numberOfStones - 1
        ->DONE
        
=== DintGiveMoonstone
        You'll regret this...
        ->DONE
->END