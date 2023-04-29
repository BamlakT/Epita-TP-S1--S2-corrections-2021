Ex1.cs
======

La première erreur est dans l'appel à ``Misc.IsDivisorOf`` où une constante est
utilisée au lieu de la variable ``div``.

La deuxième erreur est dans la mise à jour de la variable ``stop`` qui utilise
l'opérateur ``&=`` (renvoyant toujours faux car ``stop`` est initialisé à
``False``).  On peut utiliser un simple ``=`` ou même un ``|=``.

Ex2.cs
======

Le problème étant que l'on utilise un ``uint`` dans notre boucle, et que la
condition d'arrêt est ``i >= 0``. Par définition du type ``unsigned`` cette
condition sera toujours vérifiée donc il y a un problème de boucle infinie. En
réalité, la boucle va s'arrêter à cause d'une exception ``IndexOutOfRange``
puisque ``0 - 1`` en ``unsigned`` va underflow et revenir à un nombre plus grand
que la taille de notre tableau.

Deux solutions sont possibles :

- Itérer de 0 à ``GetLength - 1`` au lieu de l'inverse
- Utiliser un ``int`` à la place d'un ``uint``

Le problème de la deuxième solution est qu'il faut caster le retour de
``GetLength`` donc on préfèrera la première.

Ex3.cs
======

``SubFunction2`` est censée vérifier si un tableau est trié ou non mais ne
compare pas la dernière paire d'élément car la condition d'arrêt est
``Misc.GetLength - 2`` au lieu de ``Misc.GetLength - 1``.

La fonction ``SubFunction1`` est un tri par insertion fonctionnel.

La fonction ``Exo3`` ne trie pas correctement le dernier élément, on peut
simplement décaler en initialisant ``i`` à 0, et en appelant ``SubFunction1``
avec ``i`` au lieu de ``i - 1``.
