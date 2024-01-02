# Slugrace

Slugrace is a 2D racing and betting game, created in .NET MAUI for Windows and Android. I have a series of posts on my Prospero Coder blog where I demonstrate how I use .NET MAUI (which I'm learning myself as I go along) and how I build the app from Scratch. You will find the first part of the series here: https://prosperocoder.com/maui/basics-of-net-maui-part-1-introduction-to-net-maui/

You can also watch a video on my YouTube channel where I demonstrate how to play the game. You'll find it here: https://youtu.be/bowD_q_1dFc

The game combines a typical GUI application with animated graphics. This is a game for 1-4 players who put their bets on 4 slugs. 

Settings Page
When you start the game you first see the Settings page:

![settings_windows](https://github.com/prospero-apps/slugrace-maui/assets/48125733/fa150c14-6aaf-48f5-a5fa-b1441e04682c)

Here you can set the following: 
- the number of players (1-4), 
- the names of the players (if you don’t, the generic names Player 1, Player 2, etc. will be used), 
- the initial money each player has when the game begins (the same or different for each player), 
- the ending conditions (when the game should end – there are three options).

Race Page
When you press the Ready button, you move on to the main page of the game, the Race page:

![race_bets_windows](https://github.com/prospero-apps/slugrace-maui/assets/48125733/053dc682-4295-441e-9c6a-c01a11280ccd)


This page is divided into several panels: 
- the Game Info panel where you can see the number of the race, the time of the game, etc., 
- the Slugs’ Stats panel where you can see the names of the slugs and their wins (as both absolute values and percentages), 
- the Players’ Stats panel where you can see the names of the players and their current amount of money, 
- the main game panel in the middle with the racetrack where the slugs run (you can also see the odds here, which are updated after each race), 
- the Bets panel where you can place your bets by typing them in or using a slider.

Bets and Results 
In the lower part of the Race page you can see the Bets panel. When you press the Go button and when the race is over, this panel changes to the Results panel, where you can see the results. 

![race_results_windows](https://github.com/prospero-apps/slugrace-maui/assets/48125733/f6fe9ff3-b912-4fc7-b983-8dafae367318)


Here you can see how much money each player had before the last race, how much they bet and on which slug and whether they won or lost and how much. 
When you press the Next Race button in the Results panel, it’ll change back to the Bets panel. 

In the upper right corner there are three buttons: 
- End Game – this lets you end the game at any time and takes you to the Game Over page, 
- Instructions – this takes you to the Instructions page, 
- Sound – it toggles the sound on or off. 

Game Over
When the game is over, you’ll see the reason why it’s over and the information about the winner. The game is over when the ending condition you set is met or if you end the game manually.

![gameover_windows](https://github.com/prospero-apps/slugrace-maui/assets/48125733/6548c062-f72c-4aa0-a92b-a82c8a100912)

