GBA Intro Manager
by Diegoisawesome

About
--------------------------------------------------
This program can edit almost anything about the intro sequences of any 3rd generation Pokémon game, from the music played to the position where the player spawns.

NOTE: ALL VALUES ARE IN HEXADECIMAL UNLESS OTHERWISE NOTED.

Start Position
--------------------------------------------------
This section is straightforward enough; you can change the map bank, map, X, and Y positions of the player for when they leave the professor intro. If you're using RSE as a base, then you'll need to "unlock" the X and Y positions to be able to change them. This will not do anything to your ROM except unlock the position for modification, so don't be afraid to click it.

Music
--------------------------------------------------
Here, you can change the music played on the titlescreen and in the professor intro. Pretty straightforward, but you can't set them higher than 0x1FE because of reasons. Sorry!

Starting Items
--------------------------------------------------
In this section, you can change some things the player has when starting the game: the player's PC item, the amount of that item, and how much money the player has.
NOTE: The player's money is in decimal instead of hexadecimal because it makes more sense that way.

Titlescreen Pokémon Cry (FRLG)
--------------------------------------------------
This box is for the purpose of changing the Pokémon cry that plays on the titlescreen of FireRed and LeafGreen. Nothing really spectacular about it, except that you can use values above 0xFF due to a small rejuggling of the available space before it.

Truck Removal (RSE)
--------------------------------------------------
This button does one thing and one thing only: it removes the entire truck sequence from the start of the game. However, the main difference between this and other patches is that this is 100% safe and non-glitchy. This means no odd tileset messups or music fades!

Professor Intro Pokémon
--------------------------------------------------
Here, you can choose the image (and cry!) of the Pokémon that the game's professor sends out during the intro. FireRed is limited to the first 256 Pokémon available, and Ruby and Sapphire to the first 510, if you have that many. Emerald has no such limitation!

Other
--------------------------------------------------
This is the stuff that really didn't fit into any other section.

Secs on Title (FRLG): This is how many seconds the game stays on the title screen if you don't press any buttons until the game resets.

Skip Flashbacks (FRLG): Toggles the flashback system, where the game recounts some noteworthy events that happened since you last saved when you continue playing.

Skip Gender Choice: Toggles whether or not the professor asks the player their gender. If the choice is skipped, then the player is automatically male (or whatever the sprites are overwritten with).

Credits
--------------------------------------------------
Diegoisawesome - I made this! With my own two hands!
Jambo51 - Used some of his Trainer Editor source for stuff like INI reading and game text-reading.
colcolstyles - For the discovery of the "seconds on the title screen" value.
xGal - For writing the tutorial that I used to start this foray into the world of hack tool creation.
