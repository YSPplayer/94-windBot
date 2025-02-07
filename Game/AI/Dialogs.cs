﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace WindBot.Game.AI
{
    [DataContract]
    public class DialogsData
    {
        [DataMember]
        public string[] welcome { get; set; }
        [DataMember]
        public string[] deckerror { get; set; }
        [DataMember]
        public string[] duelstart { get; set; }
        [DataMember]
        public string[] newturn { get; set; }
        [DataMember]
        public string[] endturn { get; set; }
        [DataMember]
        public string[] directattack { get; set; }
        [DataMember]
        public string[] attack { get; set; }
        [DataMember]
        public string[] ondirectattack { get; set; }
        [DataMember]
        public string facedownmonstername { get; set; }
        [DataMember]
        public string[] activate { get; set; }
        [DataMember]
        public string[] summon { get; set; }
        [DataMember]
        public string[] setmonster { get; set; }
        [DataMember]
        public string[] chaining { get; set; }
    }
    public class Dialogs
    {
        private GameClient _game;

        private string[] _welcome;
        private string[] _deckerror;
        private string[] _duelstart;
        private string[] _newturn;
        private string[] _endturn;
        private string[] _directattack;
        private string[] _attack;
        private string[] _ondirectattack;
        private string _facedownmonstername;
        private string[] _activate;
        private string[] _summon;
        private string[] _setmonster;
        private string[] _chaining;

        public Dialogs(GameClient game)
        {
            _game = game;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DialogsData));
            string dialogfilename = game.Dialog;
            using (FileStream fs = Program.ReadFile("Dialogs", dialogfilename, "json"))
            {
                DialogsData data = (DialogsData)serializer.ReadObject(fs);
                _welcome = data.welcome;
                _deckerror = data.deckerror;
                _duelstart = data.duelstart;
                _newturn = data.newturn;
                _endturn = data.endturn;
                _directattack = data.directattack;
                _attack = data.attack;
                _ondirectattack = data.ondirectattack;
                _facedownmonstername = data.facedownmonstername;
                _activate = data.activate;
                _summon = data.summon;
                _setmonster = data.setmonster;
                _chaining = data.chaining;
            }
        }

        public void SendSorry()
        {
            InternalSendMessageForced(new[] { "Sorry, an error occurs." });
        }
        /////zdiy/////
        public void SendKeyMessage(string name)
        {
            switch (name)
            {
                case "94-01":
                    InternalSendMessageForced(new[] { "本人机自94服一届冠军卡组" });
                    InternalSendMessageForced(new[] { "当前为阉割版(部分卡片已移除服务器卡池)" });
                    break;
                case "94-我更帅了":
                    InternalSendMessageForced(new[] { "人机信息:" });
                    InternalSendMessageForced(new[] { "Diy By 失智" });
                    InternalSendMessageForced(new[] { "Script By 神数不神" });
                    InternalSendMessageForced(new[] { "Creative Time [2022-9-14]-[2022-9-21]" });
                    InternalSendMessageForced(new[] { "Code 002" });
                    InternalSendMessageForced(new[] { "【周赛兑换积分人机】" });
                    break;
                case "94-桐木圆":
                    InternalSendMessageForced(new[] { "人机信息:" });
                    InternalSendMessageForced(new[] { "Diy By 阿圆" });
                    InternalSendMessageForced(new[] { "Script By 神数不神" });
                    InternalSendMessageForced(new[] { "Creative Time [2022-10-8]-[2022-10-21]" });
                    InternalSendMessageForced(new[] { "Code 003" });
                    InternalSendMessageForced(new[] { "【周赛兑换积分人机】" });
                    break;
                default:
                    break;
            }

        }
        /////zdiy/////

        /////zdiy/////
        public void SendAiMessage(string message)
        {
            _game.Chat(message);
        }
        /////zdiy/////
        public void SendDeckSorry(string card)
        {
            if (card == "DECK")
                InternalSendMessageForced(new[] { "Deck illegal. Please check the database of your YGOPro and WindBot." });
            else
                InternalSendMessageForced(_deckerror, card);
        }

        public void SendWelcome()
        {
            InternalSendMessage(_welcome);
        }

        public void SendDuelStart()
        {
            InternalSendMessage(_duelstart);
        }

        public void SendNewTurn()
        {
            InternalSendMessage(_newturn);
        }

        public void SendEndTurn()
        {
            InternalSendMessage(_endturn);
        }

        public void SendDirectAttack(string attacker)
        {
            InternalSendMessage(_directattack, attacker);
        }

        public void SendAttack(string attacker, string defender)
        {
            if (defender == "monster")
            {
                defender = _facedownmonstername;
            }
            InternalSendMessage(_attack, attacker, defender);
        }

        public void SendOnDirectAttack(string attacker)
        {
            if (string.IsNullOrEmpty(attacker))
            {
                attacker = _facedownmonstername;
            }
            InternalSendMessage(_ondirectattack, attacker);
        }
        public void SendOnDirectAttack()
        {
            InternalSendMessage(_ondirectattack);
        }

        public void SendActivate(string spell)
        {
            InternalSendMessage(_activate, spell);
        }

        public void SendSummon(string monster)
        {
            InternalSendMessage(_summon, monster);
        }

        public void SendSetMonster()
        {
            InternalSendMessage(_setmonster);
        }

        public void SendChaining(string card)
        {
            InternalSendMessage(_chaining, card);
        }

        private void InternalSendMessage(IList<string> array, params object[] opts)
        {
            if (!_game._chat)
                return;
            string message = string.Format(array[Program.Rand.Next(array.Count)], opts);
            if (message != "")
                _game.Chat(message);
        }

        private void InternalSendMessageForced(IList<string> array, params object[] opts)
        {
            string message = string.Format(array[Program.Rand.Next(array.Count)], opts);
            if (message != "")
                _game.Chat(message);
        }
    }
}
