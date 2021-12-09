# Aenderungen, die die statemachine betreffen

## GameState
- GameState fragt nicht mehr direkt, ob alle Gegner fertig sind, stattdessen wird darauf gewartet, dass der EnemyController ein EndTrun event raised
- GameState initialisiert nicht mehr die Gegner (isOnTurn, isDone-Attribute etc.)
- GameState sendet jetzt ein Event, das dem EnemyController sagt, dass er am Zug ist

## Enemy
- ist jetzt recht analog zu Player, wahrscheinlich macht es keinen Sinn, alles aufzuzaehlen, was jetzt ander ist

(damit man es trotzdem nicht vergisst)
- Enemies haben den State idle, wo nichts passiert (initial)
- sie wechseln, wenn sie vom EnemyController aufgerufen werden, in Search
- in Search wird mithilfe des AIControllers des jeweiligen Charakters ein Ziel ausgewaehlt und eine Aktion gewaehlt oder geskippt
- falls geskippt wird, wechselt der State zurueck zu idle und der naechste Charakter ist dran
- falls eine Ability ausgewaehlt wurde, wird sie ausgefuehrt im State Execute_Ability (analog zu Player)
- nach ausfuehrung kommt es zu einer weiteren auswertung der KI