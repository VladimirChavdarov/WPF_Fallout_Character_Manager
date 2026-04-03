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

## Catalogue

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

![how to create new properties and upgrades](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/new_item_attribute_tutorial.gif) 

*(P.S. This is not the most intuitive workflow and I may change it in the future so it's easier to add Properties and Upgrades for items.)*

## Inventory

On the left of the *Inventory tab* are listed all of the items that your character has. At the bottom, there is also information about the amount of *Caps* and *Carry Load*.

![inventory](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/inventory.png)

You can *right-click* any item in the inventory list or press the *"three dots" button* in the Item Details. When you do that, you will be presented with the following options:

![inventory item options](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/inventory_options.png)

- **Delete** - Removes the item from your inventory. Be careful, I didn't put a confirmation pop-up (will do so if accidental deletions happen often)!
- **Duplicate Stack** - Clones the item you have selected (including the amount). This is useful for when you want to split your stack. *Example: Your party finds a raider stockpile in the woods and you add 4 9mm SMGs (2 Decay each) to your inventory - **"(4) 9mm SMG - 2 Decay"**. Your character plans to use one and sell the other three when you reach a settlement. Eventually, the SMG they decided to use gains 2 additional levels of **Decay** and the **Ergonomic Grip** upgrade. In order to track that, we can **Duplicate** the Stack, set the original stack's value to 3 and the new stack's amount to 1. Your character ends up with the following in their inventory:*
    - *(3) 9mm SMG - 2 Decay*
    - *(1) 9mm SMG (Ergonomic Grip) - 4 Decay*
- **Lock/Unlock** - Locking an item disabled the Delete option and doesn't include it when auto-selecting junk items for scrapping. Useful when you don't want to accidentally remove something you like or for quest items you registered as junk.

## Junk Scrapping

Similar to the video games, I made a system that scraps junk items to their base components, which are then added back to your inventory.

![junk scrap](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/junk_scrap_image.png)

The panel is split into two parts.
1. **Selected Junk Stacks** - you can choose which junk items from your inventory you want to scrap, or click *"Select All"* to select the whole junk inventory. *Note that Locked items CANNOT be scrapped.*
2. **Output** - This shows what the output will be. It will display all of the items that will go to your inventory with a "+" and all items that will be removed from your inventory with a "-".

Below is a gif showing how it works:

![how to scrap junk](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/scrap_junk_tutorial.gif)

## Junk Spending

I noticed my players usually get to scrapping junk when they want to craft something using junk components. I decided to create something like a "junk calculator" in which you input the components you look for and let the program search for them in your junk items.

![junk spend](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/junk_spend_image.png)

The panel is split into three parts:
1. **Components to Spend** - Here you specify what and how much of each component you're looking for. You can treat it like your **shopping list** in a sense
2. **Selected Junk Stacks** - Here you can select which junk item stacks you want to scrap in order to get the desired components. You have manual control over it, however you can always click the **"Auto-Select"** button and it will try to choose the most optimal combination of junk item stacks. *Note that Locked items CANNOT be scrapped.*
3. **Output** - This shows what the output will be. It will display all of the items that will go to your inventory with a "+" and all items that will be removed from your inventory with a "-". You will see the **"Accept"** button above this list only if the desired components were found among the items that are marked for scrapping.

Below is a gif showing how it works:

![how to spend junk](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/spend_junk_tutorial.gif)

## Perks and Traits

Similar to items, the application has a catalogue of all official Traits and Perks from the Rulebook. Of course, it also gives you the ability to create your own.

***NOTE:** Perks and Traits are NOT automated, mostly due to how varied they are. For now, these cards are mostly so you can quickly reference information about your character, similarly to how you will do it on a paper character sheet.*

![perks and traits](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/perks_and_traits.png)

### Adding a Trait/Perk to your character

You simply have to **right-click** the Trait/Perk you want to add in the Catalogue lists on the left.

![adding traits/perks to character](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/add_trait_or_perk.gif)

### Creating new Traits/Perks

You can extend the Catalogues with your own homebrew Traits or Perks that fit your setting! Do this by clicking on the **"New Trait Template"** or **New Perk Tempate** and fill in the information.

![creating new perk/trait templates](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/add_perk_template.gif)

## Combat Items

On the first page of the Character sheet you can find a section in which you can handle your *Weapons*, *Armor* and *Power Armor*. I made this so these items' data is easily accessible during combat, alongside other values like *SP*, *HP*, etc.

![combat items](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/combat_items.png)

### Using a Weapon

Each wepon can handles the ammo that is loaded into the clip. To do that, select it from the **Ammo drop-down**.

![how to use a weapon](/WPF_Fallout_Character_Manager/Resources/ReadmeImages/weapon_tutorial.gif)

Clicking the **"Shoot"** button fires a bullet - it will uncheck one square and **reduce the Amount** of the ammo item stack by 1. Clicking the **Reload** button will fill all the bullet squares and allow you to shoot again. Technically, you can "cheat" and load the same bullets into multiple weapons but I didn't want to remove bullets in a weapon's magazine from the character's inventory as I thought this would be too micro-managy. You can also check or uncheck bullet squares yourself but this **won't fire** any bullets - the functionality is there if you want to simulate things like manually reloading bullets into a revolver for example.

The **Properties** and **Upgrades** dropdowns are there for you to quickly reference your weapon's information - clicking them won't do anything other than closing the dropdown.