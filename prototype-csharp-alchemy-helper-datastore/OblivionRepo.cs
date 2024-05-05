﻿namespace prototype_csharp_alchemy_helper_datastore;

public class OblivionRepo : IRepo
{
    private readonly int _level;
    private readonly Dictionary<string, string[]> _data;

    public OblivionRepo(int level)
    {
        this._level = level + 1;
        this._data = new Dictionary<string, string[]>()
        {
            { "Alkanet Flower", [ "Restore Intelligence", "Resist Poison", "Light", "Damage Fatigue" ] },
            { "Aloe Vera Leaves", [ "Restore Fatigue", "Restore Health", "Damage Magicka", "Invisibility" ] },
            { "Ambrosia", [ "Restore Health" ] },
            { "Apple", [ "Restore Fatigue", "Damage Luck", "Fortify Willpower", "Damage Health" ] },
            { "Arrowroot", [ "Restore Agility", "Damage Luck", "Fortify Strength", "Burden" ] },
            { "Beef", [ "Restore Fatigue", "Shield", "Fortify Agility", "Dispel" ] },
            { "Bergamot Seeds", [ "Resist Disease", "Dispel", "Damage Magicka", "Silence" ] },
            { "Blackberry", [ "Restore Fatigue", "Resist Shock", "Fortify Endurance", "Restore Magicka" ] },
            { "Bloodgrass", [ "Chameleon", "Resist Paralysis", "Burden", "Fortify Health" ] },
            { "Boar Meat", [ "Restore Health", "Damage Speed", "Fortify Health", "Burden" ] },
            { "Bog Beacon Asco Cap", [ "Restore Magicka", "Shield", "Damage Personality", "Damage Endurance" ] },
            { "Bonemeal", [ "Damage Fatigue", "Resist Fire", "Fortify Luck", "Night-Eye" ] },
            { "Bread Loaf", [ "Restore Fatigue", "Detect Life", "Damage Agility", "Damage Strength" ] },
            { "Cairn Bolete Cap", [ "Restore Health", "Damage Intelligence", "Resist Paralysis", "Shock Damage" ] },
            { "Carrot", [ "Restore Fatigue", "Night-Eye", "Fortify Intelligence", "Damage Endurance" ] },
            { "Cheese Wedge", [ "Restore Fatigue", "Resist Fire", "Fire Shield", "Damage Agility" ] },
            { "Cheese Wheel", [ "Restore Fatigue", "Resist Paralysis", "Damage Luck", "Fortify Willpower" ] },
            { "Cinnabar Polypore Red Cap", [ "Restore Agility", "Shield", "Damage Personality", "Damage Endurance" ] },
            { "Cinnabar Polypore Yellow Cap", [ "Restore Endurance", "Fortify Endurance", "Damage Personality", "Reflect Spell" ] },
            { "Clannfear Claws", [ "Cure Disease", "Resist Disease", "Paralyze", "Damage Health" ] },
            { "Clouded Funnel Cap", [ "Restore Intelligence", "Fortify Intelligence", "Damage Endurance", "Damage Magicka" ] },
            { "Columbine Root Pulp", [ "Restore Personality", "Resist Frost", "Fortify Magicka", "Chameleon" ] },
            { "Corn", [ "Restore Fatigue", "Restore Intelligence", "Damage Agility", "Shock Shield" ] },
            { "Crab Meat", [ "Restore Endurance", "Resist Shock", "Damage Fatigue", "Fire Shield" ] },
            { "Daedra Heart", [ "Restore Health", "Shock Shield", "Damage Magicka", "Silence" ] },
            { "Daedra Silk", [ "Burden", "Night-Eye", "Chameleon", "Damage Endurance" ] },
            { "Daedra Venin", [ "Paralyze", "Restore Fatigue", "Damage Health", "Reflect Damage" ] },
            { "Daedroth Teeth", [ "Night-Eye", "Frost Shield", "Burden", "Light" ] },
            { "Dragon's Tongue", [ "Resist Fire", "Damage Health", "Restore Health", "Fire Shield" ] },
            { "Ectoplasm", [ "Shock Damage", "Dispel", "Fortify Magicka", "Damage Health" ] },
            { "Elf Cup Cap", [ "Damage Willpower", "Cure Disease", "Fortify Strength", "Damage Intelligence" ] },
            { "Emetic Russula Cap", [ "Restore Agility", "Shield", "Damage Personality", "Damage Endurance" ] },
            { "Fennel Seeds", [ "Restore Fatigue", "Damage Intelligence", "Damage Magicka", "Paralyze" ] },
            { "Fire Salts", [ "Fire Damage", "Resist Frost", "Restore Magicka", "Fire Shield" ] },
            { "Flax Seeds", [ "Restore Magicka", "Feather", "Shield", "Damage Health" ] },
            { "Flour", [ "Restore Fatigue", "Damage Personality", "Fortify Fatigue", "Reflect Damage" ] },
            { "Fly Amanita Cap", [ "Restore Agility", "Burden", "Restore Health", "Shock Damage" ] },
            { "Foxglove Nectar", [ "Resist Poison", "Resist Paralysis", "Restore Luck", "Resist Disease" ] },
            { "Frost Salts", [ "Frost Damage", "Resist Fire", "Silence", "Frost Shield" ] },
            { "Garlic", [ "Resist Disease", "Damage Agility", "Frost Shield", "Fortify Strength" ] },
            { "Ginkgo Leaf", [ "Restore Speed", "Fortify Magicka", "Damage Luck", "Shock Damage" ] },
            { "Ginseng", [ "Damage Luck", "Cure Poison", "Burden", "Fortify Magicka" ] },
            { "Glow Dust", [ "Restore Speed", "Light", "Reflect Spell", "Damage Health" ] },
            { "Grapes", [ "Restore Fatigue", "Water Walking", "Dispel", "Damage Health" ] },
            { "Green Stain Cup Cap", [ "Restore Fatigue", "Damage Speed", "Reflect Damage", "Damage Health" ] },
            { "Green Stain Shelf Cap", [ "Restore Luck", "Fortify Luck", "Damage Fatigue", "Restore Health" ] },
            { "Ham", [ "Restore Fatigue", "Restore Health", "Damage Magicka", "Damage Luck" ] },
            { "Harrada", [ "Damage Health", "Damage Magicka", "Silence", "Paralyze" ] },
            { "Imp Gall", [ "Fortify Personality", "Cure Paralysis", "Damage Health", "Fire Damage" ] },
            { "Ironwood Nut", [ "Restore Intelligence", "Resist Fire", "Damage Fatigue", "Fortify Health" ] },
            { "Lady's Mantle Leaves", [ "Restore Health", "Damage Endurance", "Night-Eye", "Feather" ] },
            { "Lady's Smock Leaves", [ "Restore Intelligence", "Resist Fire", "Damage Fatigue", "Fortify Health" ] },
            { "Lavender Sprig", [ "Restore Personality", "Fortify Willpower", "Restore Health", "Damage Luck" ] },
            { "Leek", [ "Restore Fatigue", "Fortify Agility", "Damage Personality", "Damage Strength" ] },
            { "Lettuce", [ "Restore Fatigue", "Restore Luck", "Fire Shield", "Damage Personality" ] },
            { "Lichor", [ "Restore Magicka" ] },
            { "Mandrake Root", [ "Cure Disease", "Resist Poison", "Damage Agility", "Fortify Willpower" ] },
            { "Milk Thistle Seeds", [ "Light", "Frost Damage", "Cure Paralysis", "Paralyze" ] },
            { "Minotaur Horn", [ "Restore Willpower", "Burden", "Fortify Endurance", "Resist Paralysis" ] },
            { "Monkshood Root Pulp", [ "Restore Strength", "Damage Intelligence", "Fortify Endurance", "Burden" ] },
            { "Morning Glory Root Pulp", [ "Burden", "Damage Willpower", "Frost Shield", "Damage Magicka" ] },
            { "Mort Flesh", [ "Damage Fatigue", "Damage Luck", "Fortify Health", "Silence" ] },
            { "Motherwort Sprig", [ "Resist Poison", "Damage Fatigue", "Silence", "Invisibility" ] },
            { "Mugwort Seeds", [ "Restore Health" ] },
            { "Mutton", [ "Fortify Health", "Damage Fatigue", "Dispel", "Damage Magicka" ] },
            { "Nightshade", [ "Damage Health", "Burden", "Damage Luck", "Fortify Magicka" ] },
            { "Ogre's Teeth", [ "Damage Intelligence", "Resist Paralysis", "Shock Damage", "Fortify Strength" ] },
            { "Onion", [ "Restore Fatigue", "Water Breathing", "Detect Life", "Damage Health" ] },
            { "Orange", [ "Restore Fatigue", "Detect Life", "Burden", "Shield" ] },
            { "Pear", [ "Restore Fatigue", "Damage Speed", "Fortify Speed", "Damage Health" ] },
            { "Peony Seeds", [ "Restore Strength", "Damage Health", "Damage Speed", "Restore Fatigue" ] },
            { "Potato", [ "Restore Fatigue", "Shield", "Burden", "Frost Shield" ] },
            { "Primrose Leaves", [ "Restore Willpower", "Restore Personality", "Fortify Luck", "Damage Strength" ] },
            { "Pumpkin", [ "Restore Fatigue", "Damage Agility", "Damage Personality", "Detect Life" ] },
            { "Purgeblood Salts", [ "Restore Magicka", "Damage Health", "Fortify Magicka", "Dispel" ] },
            { "Radish", [ "Restore Fatigue", "Damage Endurance", "Chameleon", "Burden" ] },
            { "Rat Meat", [ "Damage Fatigue", "Detect Life", "Damage Magicka", "Silence" ] },
            { "Redwort Flower", [ "Resist Frost", "Cure Poison", "Damage Health", "Invisibility" ] },
            { "Rice", [ "Restore Fatigue", "Silence", "Shock Shield", "Damage Agility" ] },
            { "Root Pulp", [ "Cure Disease", "Damage Willpower", "Fortify Strength", "Damage Intelligence" ] },
            { "Sacred Lotus Seeds", [ "Resist Frost", "Damage Health", "Feather", "Dispel" ] },
            { "Scales", [ "Damage Willpower", "Water Breathing", "Damage Health", "Water Walking" ] },
            { "Scamp Skin", [ "Damage Magicka", "Resist Shock", "Reflect Damage", "Damage Health" ] },
            { "Shepherd's Pie", [ "Cure Disease", "Shield", "Fortify Agility", "Dispel" ] },
            { "S'jirra's Famous Potato Bread", [ "Detect Life", "Restore Health", "Damage Agility", "Damage Strength" ] },
            { "Somnalius Frond", [ "Restore Speed", "Damage Endurance", "Fortify Health", "Feather" ] },
            { "Spiddal Stick", [ "Damage Health", "Damage Magicka", "Fire Damage", "Restore Fatigue" ] },
            { "St. Jahn's Wort Nectar", [ "Resist Shock", "Damage Health", "Cure Poison", "Chameleon" ] },
            { "Steel-Blue Entoloma Cap", [ "Restore Magicka", "Fire Damage", "Resist Frost", "Burden" ] },
            { "Stinkhorn Cap", [ "Damage Health", "Restore Magicka", "Water Walking", "Invisibility" ] },
            { "Strawberry", [ "Restore Fatigue", "Cure Poison", "Damage Health", "Reflect Damage" ] },
            { "Summer Bolete Cap", [ "Restore Agility", "Shield", "Damage Personality", "Damage Endurance" ] },
            { "Sweetcake", [ "Restore Fatigue", "Feather", "Restore Health", "Burden" ] },
            { "Sweetroll", [ "Restore Fatigue", "Resist Disease", "Damage Personality", "Fortify Health" ] },
            { "Taproot", [ "Restore Luck", "Damage Endurance", "Resist Poison", "Shock Shield" ] },
            { "Tiger Lily Nectar", [ "Restore Endurance", "Damage Strength", "Water Walking", "Damage Willpower" ] },
            { "Tinder Polypore Cap", [ "Restore Willpower", "Resist Disease", "Invisibility", "Damage Magicka" ] },
            { "Tobacco", [ "Restore Fatigue", "Resist Paralysis", "Damage Magicka", "Dispel" ] },
            { "Tomato", [ "Restore Fatigue", "Detect Life", "Burden", "Shield" ] },
            { "Troll Fat", [ "Damage Agility", "Fortify Personality", "Damage Willpower", "Damage Health" ] },
            { "Vampire Dust", [ "Silence", "Resist Disease", "Frost Damage", "Invisibility" ] },
            { "Venison", [ "Restore Health", "Feather", "Damage Health", "Chameleon" ] },
            { "Viper's Bugloss Leaves", [ "Resist Paralysis", "Night-Eye", "Burden", "Cure Paralysis" ] },
            { "Void Salts", [ "Restore Magicka", "Damage Health", "Fortify Magicka", "Dispel" ] },
            { "Water Hyacinth Nectar", [ "Damage Luck", "Damage Fatigue", "Restore Magicka", "Fortify Magicka" ] },
            { "Watermelon", [ "Restore Fatigue", "Light", "Burden", "Damage Health" ] },
            { "Wheat Grain", [ "Restore Fatigue", "Damage Magicka", "Fortify Health", "Damage Personality" ] },
            { "White Seed Pod", [ "Restore Strength", "Water Breathing", "Silence", "Light" ] },
            { "Wisp Stalk Caps", [ "Damage Health", "Damage Willpower", "Damage Intelligence", "Fortify Speed" ] },
            { "Wormwood Leaves", [ "Fortify Fatigue", "Invisibility", "Damage Health", "Damage Magicka" ] }
        };
    }

    public Dictionary<string, string[]> GetEverything()
    {
        return this._data
            .Select(kv => new KeyValuePair<string, string[]>(kv.Key, kv.Value.Take(this._level).ToArray()))
            .ToDictionary();
    }

}
