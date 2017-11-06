# WindBot

A C# bot for YGOPro, compatible with the [YGOSharp](https://github.com/IceYGO/ygosharp) and [SRVPro](https://github.com/moecube/srvpro) server.

### How to use:

* Compile `WindBot.sln` using Visual Studio or Mono.

* Put `cards.cdb` next to the compiled `WindBot.exe`.

* Run YGOPro, create a host.

* Run WindBot and observe.

### Supported commandlines

`Name`  
The nickname for the bot.

`Deck`  
The deck to be used by the bot. Available decks are listed below. Keep empty to use random deck.

`Dialog`  
The dialog texts to be used by the bot. See Dialogs folder for list.

`Host`  
The IP of the host to be connected to.

`Port`  
The port of the host to be connected to.

`HostInfo`  
The host info (password) to be used.

`Version`  
The version of YGOPro.

`Hand`  
If you are testing deck, you may want to make sure the bot go first or second. `Hand=1` will make the bot always show Scissors, 2 for Rock, 3 for Paper.

`ServerMode` and `ServerPort`  
WindBot can run as a "server", provide a http interface to create bot.

### Available decks

**Easy**:

* Burn

* Frog

* Horus

* MokeyMokey

* MokeyMokeyKing

* OldSchool

**Normal**:

* Blue-Eyes

* Dragunity

* Qliphort

* Rainbow

* Rank V

* ST1732

* Toadally Awesome (old lflist)

* Yosenju

* Zexal Weapons

* Zoodiac (old lflist, master rule 3 only)

### Unfinished decks

* Blackwing

* CyberDragon

* Evilswarm

* Gravekeeper

* Graydle

* Lightsworn

* Nekroz

### Server mode

WindBot can run as a "server", provide a http interface to create bot.

eg. `http://127.0.0.1:2399/?name=%E2%91%A8&deck=Blue-Eyes&host=127.0.0.1&port=7911&dialog=cirno.zh-CN&version=4928`

In this situation, it will be multi-threaded. This can be useful for servers, since it don't use large amount memory.

The parameters are same as commandlines, but low cased.

### Known issues

* The attack won't be canceled when battle replay happens.

* If one chain includes two activation that use `AI.SelectCard`, the second one won't select correctly.

### Changelog

#### v0x1340 (2017-11-06)

 - Update YGOPro protrol to 0x1340
 - Add support for the New Master Eule
 - Decks update
 - New commandline parameters
 - Add support for Match and TAG duel
 - Add server mode
 - Bot dialogs now customable
 - Only use normal deck when random picking decks
 - Send sorry when the AI did something wrong that make the duel can't continue (for example, selected illegal card)
 - Send info when the deck of the AI is illegal
 - Fix the issue that the bot will attack Dupe Frog with low attack monster when there is monster next to Dupe Frog
 - Fix `OnUpdateData` `OnSelectSum` (https://github.com/IceYGO/windbot/issues/7)
 - New and updated `DefaultExecutor`s
 - New and updated `AI.Utils`, `ClientCard`, `ClientField` functions
 - Add `OnNewTurn`, `AI.SelectYesNo`, `AI.SelectThirdCard`, `Duel.ChainTargets`, `Duel.LastSummonPlayer`
 - Shortcut `Bot` for `Duel.Fields[0]`, `Enemy` for `Duel.Fields[1]`
 - `CardId` is now class instead of enum so `(int)` is no longer needed
 - Update the known card enums, add `Floodgate`, `OneForXyz`, `FusionSpell`, `MonsterHasPreventActivationEffectInBattle`
 - Update `OnPreBattleBetween` to calculate the ATK of cards like NumberS39UtopiaTheLightning
 - Add desc parameter for `OnSelectEffectYn`
 - Update direct attack handling

#### v0x133D (2017-09-24)

 - Update YGOPro protrol to 0x133D
 - Use the latest YGOSharp.Network to improve performances
 - Update the namespace of `YGOSharp.OCGWrapper`
 - Fix the default trap cards not always activating

### TODO list

* More decks

* Documents for creating AI

* `AI.SelectZone`

* `AI.SelectMaterials` which select a set of cards for F/S/X/L summon

* `AI.SelectTribute`

* Select cards to pendulum summon in executor.

* Get equip of card.

* Get attack target.

* Better new master rule support

* Update the known card enums

* More default common cards executor
