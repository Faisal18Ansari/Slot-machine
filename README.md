**🎰 SPIN & WIN - Unity Slot Machine Game**

Hello and welcome to SPIN & WIN! A old school slot machine, unity made, and it will provide you all the vibes of the casinos without risking your real money 😄

**🎮 Game Overview**

Did you ever wish to pull the lever of a slot machine but were afraid to break your wallet? Here is your opportunity, then! This is a fully playable slot machine type game, you have 30 coins in your possession at the beginning and you can slide or spin your way to wealth (or ruin, but hey, it is virtual money so it does not really matter).

**What makes this game fun:**

🎲 Classic 3-reel slot machine with smooth spinning animations
💰 Multiple winning combinations - match 2 or 3 symbols for different payouts
🔊 Immersive audio with jackpot celebration sounds and win effects
⏸️ Pause functionality because life happens
🎯 Reset & retry when you inevitably run out of coins (we've all been there!)

**Winning Logic:**

Match 3 identical symbols = JACKPOT! (with epic celebration)
Match 2 symbols = Nice win! (with satisfying sound effects)
Different symbols have different payout values (Seven pays the most, naturally)

**🚀 Instructions to Run WebGL Build**

Super simple - no downloads or installations needed!
Just head over to my itch.io page and play it directly in your browser:
🎮 Play SPIN & WIN on itch.io
Click the link, wait for it to load, and you're ready to spin! Works on any modern browser - Chrome, Firefox, Safari, you name it.

**⭐ Bonus Features**

Now things get interesting! I did not create only a simple slot machine - I added full betting system to make it feel like an online casino:

**🎯 Smart Betting System**

Three bet levels: 10, 50, and 100 coins
Strategic gameplay: Higher bets = higher risk, but same win multipliers
Bankroll management: The game tracks your total winnings and prevents you from betting more than you have
First spin tutorial: Your very first spin is free (manual pull) to get you comfortable with the game

**💎 Unique Prize & Payout Structure**

Instead of boring flat payouts, I implemented a dual-tier reward system:
3-Symbol Matches (JACKPOT!):

🍒 Cherry: 100 coins
🔔 Bell: 80 coins
📊 Bar: 150 coins
7️⃣ Seven: 200 coins (the big kahuna!)

2-Symbol Matches (Still pretty sweet!):

🍒 Cherry: 50 coins
🔔 Bell: 40 coins
📊 Bar: 75 coins
7️⃣ Seven: 100 coins

**🎪 Extra Polish**

Jackpot celebrations that pause the game and play epic music
Game over protection with automatic retry options when funds run low
Smooth animations that actually feel satisfying to watch
Audio management that doesn't drive you crazy

**🛠️ My Thought Process & Approach**

When building this, I wanted to create something that felt authentic rather than just a technical demo. Here's how I approached it:

Player Experience First: I started by thinking about what makes slot machines addictive and fun - the anticipation, the audio feedback, the celebration of wins.

Audio is Everything: Just spinning the wheel in anticipation of a prize in dead silence doesn't create a winning environment - that's why I added an upbeat soundtrack that gives the real adrenaline rush! Plus jackpot celebrations and win sound effects to make every victory feel special.

Progressive Complexity: Instead of building everything at once, I started with basic spinning mechanics, then added betting, then audio, then polish features.

Real Casino Logic: I researched actual slot machine behavior to make the spinning feel realistic - notice how the reels slow down gradually rather than stopping abruptly. I implemented the gradual stopping mechanics of real slot machines along with their randomness in stopping positions and symbol selection.

Fail-Safe Design: Multiple fail-safe designs had to be implemented in case there was a game-breaking rule - added extensive safety checks to prevent edge cases like betting with insufficient funds, clicking during inappropriate game states, or any other potential crashes.

Code Organization: Used Unity's event system for clean communication between scripts and organized everything with clear, descriptive variable names.

**🎯 Technical Highlights**

Clean script communication via event driven architecture
Way of animating the smooth reel spinning with coroutines
use of state management to manage a complex flow of game
Integration of the audio system to include good pause/ resume capability
Intuitive, responsive UI/UX design Contributing to the development of the UI/UX design, the interactive and clear user interface will support the accessibility of the product.
