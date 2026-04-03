# Digital Character Sheet

A digital character sheet for the [Fallout TTRPG](https://www.patreon.com/posts/fallout-ttrpg-2-121214521) made by [XP to Level 3](https://www.youtube.com/@XPtoLevel3).

![showcase](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/thumbnail.png)

NOTE: This is a stand-alone ***desktop** application* for **Windows**.

GENERAL TIP: *When in doubt, try **right-clicking**!*

# How to Install

1. Go to the *Releases* section on this page and click on the latest *Release*.
2. Scroll to the bottom of the page.
3. Download the **first** .zip file at the top. The name should be *"Fallout_Character_Manager.zip"* or something similar.
4. Extract the folder in your desired directory on your PC.
5. **(EXTRA)** If this is **NOT your first time downloading the app**, I strongly recommend going to the directory of your old version of the app and copy-pasting the **"catalogue.fct"** file into the new directory.
6. Click the **.exe** and use the app!

# Features

Below is some documentation and instructions on how to use the app.

## Modifier System

A good amount of parameters are tied to a modifier system, which gives you modular control over the value. Instead of editing the field directly, the app will open a Window which will let you modify the parameter's base value and/or add modifiers for better book-keeping of your character information. For example: *Veronica takes Med-X and gains the Anesthetic property, which gives 6 DT. Veronica's player left-clicks on the Damage Threshold field and can add a modifier with a title "Med-X", value of 6 and a note saying "This lasts for 1 hour."*

![modiffiers](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/modifier_med-x_tutorial.gif)

This feature works for almost all text-fields you see. Left-clicking a text field will either let you allow it directly or it will open the modifier window. Experiment!

## Auto-Calculations

I found out that this system is quite crunchy so I decided to streamline some stuff with auto-calculation code. Not everything is automated because it was either too finnicky to do or I didn't want to optimize the fun out of the game.

Below is a list of values that scale AUTOMATICALLY:
- **Max Stamina Points** scale with Level and Agility
- **Max Health Points** scale with Level and Endurance
- **Healing Rate** scales with Endurance
- **Armor Class** scales with equipped Armor or Power Armor
- **Damage Threshold** scales with equipped Armor or Power Armor
- **Combat Sequence** scales with Perception
- **RadDC** scales with Endurance
- **Passive Sense** scales with Perception
- **Carrying Capacity** scales with Strength
- **All Skills** scale with their respective SPECIAL stat and *Luck*
- **To Hit** and **Crit Chance** of the selected weapon scale with *Luck*
- **To Hit** and **Damage** of weapons scale with Decay
- **Armor Class** and **Damage Threshold** scale with Decay
- **Cost** of all items scales with Decay
- *Let me know if I missed something and I will add it to this list.*

<br>

Below is a list of values that DON'T scale automatically:
- **Perks** and **Traits** are not automated and need to be applied manually if the player wishes to do so.
- **Items'** effects are not applied directly but the player can choose to apply them manually.
- **Upgrades** and **Properties**. Their effects are not applied automatically and are listed for every item so the player can see them. They can apply their effects manually if they wish to do so.
- **Conditions** and **Limb Conditions**.
- **Race** bonuses.
- *Let me know if I missed something and I will add it to this list.*

## Inventory

The Inventory tab has three main parts - **Catalogue List**, **Inventory List** and **Junk Manager**.

### Catalogue

The app contains all item tables from the rulebook and generates a list of them on start. The list is sorted by categories and also comes with a search bar for some convenience. :)


![catalogue](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/catalogue.png)

The catalogue allows the following features:

### Add item to Inventory

Simply right-click the item you want to add in the Catalogue and click "Add to Inventory".

![add item from dialog](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/catalogue_add.png)

You will then be prompted with a confirmation dialog.

![add item dialog](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/add_dialog.png)

Alternatively, you can also access this option by clicking on the "three dots" button in the Item Details.

![add item from item details](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/item_details_add.png)

### Create a new Catalogue Item

As a player, you can also create homebrew items, properties and upgrades. This can either be done by clicking the **New Item Template** button at the bottom of the Catalogue, by *right-clicking* an item and choosing **Create Template from this** or by clicking the *"three dots" button* in the *Item Details* and choosing the same option. All of those will open the following window:

![new item template dialog](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/new_template_dialog.png)

Choose the type of item you want to create from the **drop-down** at the *top-left*. By default it will show you the first item in the Catalogue of that collection. You can use it as a starting point and change whatever you want on it to make your own custom template. When you're ready, click "Add to Catalogue" and the list of available items will update.

Here is a gif showing how it works:

![how to create a new item template](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/new_item_template_tutorial.gif)

### Create a new Property or Upgrade

You can create them using the same system. When you open the **"New Item Template"** window and select the type of item you want to create, click the **"New Property"** or **"New Upgrade"** and another window will appear which follows a similar convention. Add information about the upgrade, press **Add to Catalogue** and you're done. Even though you need to open the "New Item Template" window for this, you don't actually need to add this item to the Catalogue if you just want to create custom Properties or Upgrades.

Here is gif showing how it works:

<video controls>
  <source src="/WPF_Fallout_Character_Manager/Resources/ReadmeImages/new_item_attributes.mp4" type="video/mp4">
</video>

![how to create new properties and upgrades](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/new_item_attributes.mp4) 

*(P.S. This is not the most intuitive workflow and I may change it in the future so it's easier to add Properties and Upgrades for items.)*