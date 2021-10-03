# Naming Convention for State Machine Assets


## State Machine Assets by Function
This includes __States__, __Actions__, __Conditions__

State Machine Assets are devided into __Player__, __Enemy__, __GameState__

|SM Assets | __Player__ | __Enemy__ | __GameState__ |
|---       |---         |---        |---            |
|__prefix__| p_name     | e_name    | g_name        |

<!-- SM Assets lol -->

## State Phase specific Naming

|          | __ON Enter__ | __ON Upadate__ | __On Exit__ |
|---       |---           |---             |---          |
|__suffix__| name_OnEnter | name_OnUpdate  | name_OnExit |

## State Machine Script Naming Convention

Transition Table => name_TT