INCLUDE Globals.ink

{
- itemCount>=1:
->NPC_01_Second_Interaction
- NPC_01_haveTalked == 1 && itemCount <= 0:
->Hint
- NPC_01_haveTalked == 1 && itemCount >= 1:
->NPC_01_Second_Interaction
- NPC_01_haveTalked == 2:
->NPC_01_Out_Of_Dialogue
- NPC_01_haveTalked == 3:
->DontGiveItem
 -else: //if false
->NPC_01_First_Interaction //You don't have anything
}

=== NPC_01_First_Interaction
//You don't have anything interesting...   
No tienes nada interesante.
    * [Estoy buscando a mi hermano, se ha...]
      -> Uninterested
    * [¿Qué?]
     ¿Qué estás mirando? Lárgate.
     -> DONE

=== Uninterested ===
~NPC_01_haveTalked = 1
//So what?
¿Y a mí qué?
->DONE

=== Negotation ===
//I haven't, there aren't many humans that dare to enter these woods, and the ones that do, are ususally idiots.
No le he visto, no hay muchos humanos que se atrevan a adentrarse al bosque, y los que lo hacen son estúpidos.
//But wait! We can make a deal. 
¡Pero espera! No te vayas todavía.
//If you give that I promise to return the favor, who knows, maybe you'll need something from me in the future...
Si me das eso que llevas encima, prometo devolverte el favor.
Quien sabe, quizás necesites mi ayuda más adelante.
*[De acuerdo]
->GiveItem
*[No gracias]
->DontGiveItem

=== NPC_01_Second_Interaction
//Wait! You, what is that fancy thing you have there?
¡Eh tú! ¿Eso que llevas ahí es una seta luminosa?
    *[¿Quien eres?]
    Soy Drugh, tengo una pequeña y mohosa caverna aquí cerca.
    Si me das esas maravillosasa seta, estás invitada.
    **[¿Has visto a un joven por aquí?]
        -> Negotation
    **[Tengo prisa]
        -> DontGiveItem
    *[Sí]
    ->Try
  
===Try===
¿Me la das?
*[Toma]
->GiveItem
*[No]
->DontGiveItem
->DONE

=== GiveItem ===
~hasGivenItem(true)//changes NPC2 dialogue
~removeFromInventory(2)
~NPC_01_haveTalked = 2
~itemCount--
Oh muchas gracias.
Adiós.
->DONE

=== DontGiveItem ===
~NPC_01_haveTalked = 3
Te vas a arrepentir de esto.
->END

=== Hint ===
Algo brillante y sabroso...
->DONE

=== NPC_01_Out_Of_Dialogue ===
Nos veremos pronto.
->DONE

=== NPC_01_Take_Player_Home ===
Sígueme si quieres que tu hermano sobreviva
->END