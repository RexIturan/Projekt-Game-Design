# Explanation for Tile Effects

For better understanding the fields in the TileEffect-component. 

### Time until activation 
This defines how many TileEffect updates it takes, for the tile effect to activate. Notice that the update that activates the tile effect WILL take actions already. If set to 1, the tile will be active on next update and take it's first actions. 

### Time To Live 
This defines how often the tile effect will have an impact after activation. Setting it to 0 will cause the effect to never take any action. 

### Creation Requirements 
When an event is raised to place new tile effects, the tiles need given attributes. CreationRequirementsTop is for the tile the effect will be placed and -Ground for the tile underneath. Fire for example may only be placed on solid ground. 