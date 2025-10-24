# Digital Character Sheet

A digital character sheet for the [Fallout TTRPG](https://www.patreon.com/posts/fallout-ttrpg-2-121214521) made by [XP to Level 3](https://www.youtube.com/@XPtoLevel3).

![showcase](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/thumbnail.png)

NOTE: This is a stand-alone *desktop application* for **Windows**.

My friend and I started a campaign using this system but we realized there is a lot of number crunching during gameplay. That's why I decided to make a convenient tools which automates most of that process while still allowing the players complete freedom of how they customize their characters.

This application is the successor of the [first digital character sheet](https://github.com/VladimirChavdarov/Fallout_Character_Manager) I made for version 1.6 of the TTRPG. This time I decided to make it using **WPF** as it is a lot more suited for the purpose. This was both my first time working with WPF and the first time applying the MVVM pattern so a lot of the code probably looks sketchy but it works robustly and provides pleasant user experience.

This repo is open-source so if you wish to extend it, feel free to make a fork!

If you find any bugs, please create an issue on this github page and I will try to address it as soon as possible. :)

# README

### Modifier System

Most of the app is pretty straight-forward. However, something that people might miss is the modifier system that lets you modify almost any value related to your character. To activate it, just **Right-click** on a text field with some value. Here is an example:

![modiffiers](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/modifiers.gif)

That way, you can tweak the values of your character, apply temporary or permanent buffs or debuffs that could come from chems, perks, upgrades, injured limbs, environmental factors and everything in-between. However, the base value remains intact and will keep scaling if it's tied to another parameter. This makes tracing the state of your character much easier.

# Roadmap

This is a **work-in-progress** project. Here is a general list of what I plan to add before considering it finished:
 - Save/Load functionality
 - Inventory window
    - Adding/removing items from "backpack"
    - Customizing items
    - Creating brand new items
    - (optional) Crafting
 - Perks window
    - (optional) automated effect application
 - Data window - portrait, backstory, notes, other roleplay-related stuff.