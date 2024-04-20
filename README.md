# Org_TestGame_Prototype
A Prototype Game

Card Match Prototype


Unity Editor Version 2021.3.37f1
Visual Studio Version 2022


Project Description

Go to AssetMenu > Card Cell Settings > Create Card Cell Setting
will create a Card Grid Layout Settings Scriptable, Which can you adjusted by level designers for easy use.


CardAnimationHelper - Extension Class Responsible for Card Flip and Card Mismatch Smooth Animation.
CardCell - Main Driver class for CardCell,
CardGridController - MVC Pattern's Controller class, Handles Data Manipulation.
CardGridUIView -  MVC Pattern's View Class, Handles Data Population on User Interface.
CardMatchUtils - CardUtils contains Delay function.
GameAudioManager - Responsible for Game Audio Management.
GameplayHandler -  Responsible for Gameplay Logic Feature.
GameUIManager - Handles Game UI/UX and Enable Disable of UI Components.
GameSaveHistoryData - Main Driver class of History Save Data class.
GridCellSettings - A scriptable object, Responsible for setting up the data for CardGridLayout Settings for Level Generation.
LevelRandomizer - Randomizes Levels.
SaveLoadSystem -  Saving/Loading Each game progress by saving GameName, GameTimeStamp,Matches,Turns etc.
ScoreSystem - Handles Score Mechanism
Singleton - Generic Singleton Class
