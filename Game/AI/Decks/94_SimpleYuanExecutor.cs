using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using System.Linq;
using System;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("94-SimpleYuan", "AI_94-SimpleYuan")]
    class SimpleYuanExecutor : DefaultExecutor
    {
        /*
      卡池:
           94服卡池
       diy by 阿圆
       script by 神数不神 
       2022-10-8        
   */
        public class CardId
        {
            public const int Nibilu = 27204311;
            public const int L_Baiyinji = 81497285;
            public const int L_Ali = 1225009;
            public const int Z_Zhu = 9873001;
            public const int Z_Cangqiong = 9873015;
            public const int Z_Fengye = 9873018;
            public const int Huiliuli = 14558127;
            public const int G = 23434538;
            public const int L_Shizhong = 2511;
            public const int L_Chengfa = 189112;
            public const int Huazaikai = 4875172;
            public const int L_Huanying = 5380979;
            public const int Z_Lieyinghao = 9873008;
            public const int Z_Tanshegouzhua = 9873021;
            public const int Kuluopai = 9941060;
            public const int C_Nvshenjuhe = 9941251;
            public const int Wuxianpaoying = 10045474;
            public const int Erren = 10702100;
            public const int Luoxuanjieti = 82207032;
            public const int J_Tuji = 9871820;
            public const int Z_Yunhai = 9873009;
            public const int Z_Zhanjianchixing = 9873010;
            public const int Fangxiang = 58921041;
            public const int Kehuishou = 84404797;
            public const int Xianjiyishi = 95780008;
            public const int Z_Qinruzhuangzhi = 9873013;
            public const int Shentonggao = 40605147;
            public const int Shenxuangao = 41420027;
            public const int Qianlong = 9875211;
            public const int Huihang = 9875210;
            public const int Niesun = 9875207;

            public const int TYPE_A = 9873022;
            public const int Z_Ex_Mingdao = 9873250;
            public const int Z_Ex_Cangqiong = 9873016;
            public const int Z_Ex_Fengye = 9873019;
            public const int Z_Ex_Lieying = 9873002;
            public const int Z_Ex_Xuanzhan = 9873003;
            public const int Z_Ex_Zhangui = 9873004;
            public const int Z_Ex_Chihong = 9873006;
            public const int Z_Ex_Guanghui = 9873007;
        }
        int ExfengyeAtk = 0;
        ////OnSelectCard////
        bool S_ExChihong = false;
        bool S_Luoxuanjieti = false;
        bool S_Yunhai = false;
        bool S_Cangqiong = false;

        bool S_Exfengye_1 = false;
        bool S_Exfengye_2 = false;

        bool S_Erren_1 = false;
        bool S_Erren_2 = false;
        bool Erren_Activate = false;
        bool S_Lieyinghao_1 = false;
        bool S_Lieyinghao_2 = false;
        ////OnSelectCard////
        bool S_Kuluopai = false;
        ////OnSelectLinkMaterial///
        bool S_LinkSummon = false;
        ////OnSelectLinkMaterial///

        /////activate_rest////
        bool Qinruzhuangzhi_Activate = false;
        bool Lieyinghao_Activate = false;
        bool Zhanjianchixing_Activate = false;
        bool Yunhai_Activate = false;
        bool Tanshegouzhua_Activate = false;
        bool Niesun_Activate = false;
        bool Huihang_Activate = false;
        bool Qianlong_Activate = false;
        /////activate_rest////
        bool YunHai_Activate = false;
        /////summon/////
        bool ExCangqiong_Summon = false;
        bool ExFengye_Summon = false;
        bool ExLieying_Summon = false;
        bool ExXuanzhan_Summon = false;
        bool ExZhangui_Summon = false;
        bool ExChihong_Summon = false;
        bool ExGuanghui_Summon = false;
        /////summon/////

        //List<int> shouldSelectCards = new List<int>
        //{
        //  CardId.Z_Yunhai, CardId.Z_Qinruzhuangzhi,CardId.Z_Lieyinghao,CardId.Z_Zhanjianchixing,
        //  CardId.Z_Tanshegouzhua,CardId.Niesun, CardId.Huihang,CardId.Qianlong,
        //  /////怪兽/////
        //  CardId.Z_Fengye,CardId.Z_Zhu,CardId.Z_Cangqiong
        //};
        List<int> Impermanence_list = new List<int>();
        List<int> should_not_negate = new List<int>
        {
            81275020, 28985331
        };
        public SimpleYuanExecutor(GameAI ai, Duel duel)
     : base(ai, duel)
        {
            //无限泡影
            AddExecutor(ExecutorType.Activate, CardId.Wuxianpaoying, Impermanence_activate);
            //螺旋阶梯
            AddExecutor(ExecutorType.Activate, CardId.Luoxuanjieti, LuoxuanjietiEffect);
            //库落
            AddExecutor(ExecutorType.Activate, CardId.Kuluopai, KuluopaiEffect);
            //芳香
            AddExecutor(ExecutorType.Activate, CardId.Fangxiang);
            //G
            AddExecutor(ExecutorType.Activate, CardId.G, GEffect);
            //献祭仪式
            AddExecutor(ExecutorType.Activate, CardId.Xianjiyishi);
            //尼比鲁
            AddExecutor(ExecutorType.Activate, CardId.Nibilu, NibiluEffect);
            //灰流丽
            AddExecutor(ExecutorType.Activate, CardId.Huiliuli, HuiliuliEffect);
            //普通少女竹
            AddExecutor(ExecutorType.Activate, CardId.Z_Zhu, ZhuEffect);
            AddExecutor(ExecutorType.Summon, CardId.L_Ali);

            //拉比加斯
            AddExecutor(ExecutorType.Activate, CardId.L_Huanying);
            //白银
            AddExecutor(ExecutorType.Activate, CardId.L_Baiyinji, BaiyinjiEffect);
            //时终
            AddExecutor(ExecutorType.Activate, CardId.L_Shizhong, ShizhongEffect);

            //云海5
            AddExecutor(ExecutorType.Activate, CardId.Z_Yunhai, YunhaiActivate);
            AddExecutor(ExecutorType.Activate, CardId.Z_Yunhai, YunhaiEffect);
            //潜龙
            AddExecutor(ExecutorType.Activate, CardId.Qianlong, QianlongEffect);
            //回航
            AddExecutor(ExecutorType.Activate, CardId.Huihang, HuihangEffect);
            //枫叶
            AddExecutor(ExecutorType.SpSummon, CardId.Z_Fengye);
            //枫叶
            AddExecutor(ExecutorType.Summon, CardId.Z_Fengye);
            //苍穹雷亚 奇怪 的Bug
            AddExecutor(ExecutorType.Activate, CardId.Z_Cangqiong);
            //苍穹雷亚
            AddExecutor(ExecutorType.Summon, CardId.Z_Cangqiong);
            //可回收瓶
            AddExecutor(ExecutorType.Activate, CardId.Kehuishou, YunhaiActivate);
            AddExecutor(ExecutorType.Activate, CardId.Kehuishou, KehuishouEffect);
            //女神居合
            AddExecutor(ExecutorType.Activate, CardId.C_Nvshenjuhe, NvshenjuheEffect);
            //神之宣告
            AddExecutor(ExecutorType.Activate, CardId.Shenxuangao, ShenEffect);
            //神之通告
            AddExecutor(ExecutorType.Activate, CardId.Shenxuangao, ShenEffect);

            //阿里安娜
            AddExecutor(ExecutorType.Activate, CardId.L_Ali, AliEffect);
            //A效果 
            AddExecutor(ExecutorType.Activate, CardId.TYPE_A, AEffect);
            //二人被注释扔出错1
            AddExecutor(ExecutorType.Activate, CardId.Erren, ErrenEffect);
            //侵入装置 7
            AddExecutor(ExecutorType.Activate, CardId.Z_Qinruzhuangzhi, QinruzhuangzhiEffect);
            //明年花再开 6
            AddExecutor(ExecutorType.Activate, CardId.Huazaikai);
            //战舰赤星4
            AddExecutor(ExecutorType.Activate, CardId.Z_Zhanjianchixing, ZhanjianchixingActivate);
            AddExecutor(ExecutorType.Activate, CardId.Z_Zhanjianchixing, ZhanjianchixingEffect);
            //暗影突击者
            AddExecutor(ExecutorType.Activate, CardId.J_Tuji, TujiEffect);
            //勾爪
            AddExecutor(ExecutorType.Activate, CardId.Z_Tanshegouzhua, TanshegouzhuaEffect);
            //陷阱 欢迎
            AddExecutor(ExecutorType.Activate, CardId.L_Huanying, HuanyingEffect);

            //拉比加斯 惩罚 3
            AddExecutor(ExecutorType.Activate, CardId.L_Chengfa, ChengfaEffect);
            //陷阱 猎隼
            AddExecutor(ExecutorType.Activate, CardId.Niesun, NiesunEffect);
            //陷阱 猎鹰
            AddExecutor(ExecutorType.Activate, CardId.Z_Lieyinghao, LieyinghaoEffect);

            //竹竹 武装小圆
            AddExecutor(ExecutorType.SpSummon, CardId.Z_Ex_Mingdao, Z_LinkSummon);
            AddExecutor(ExecutorType.Activate, CardId.Z_Ex_Mingdao);

            //竹竹 EX苍穹雷亚
            AddExecutor(ExecutorType.SpSummon, CardId.Z_Ex_Cangqiong, Z_LinkSummon);
            AddExecutor(ExecutorType.Activate, CardId.Z_Ex_Fengye);

            // 竹竹 赤红
            AddExecutor(ExecutorType.SpSummon, CardId.Z_Ex_Chihong, Z_LinkSummon);
            AddExecutor(ExecutorType.Activate, CardId.Z_Ex_Chihong, ExChihongEffect);

            //竹竹 旋战鱼
            AddExecutor(ExecutorType.SpSummon, CardId.Z_Ex_Xuanzhan, Z_LinkSummon);
            AddExecutor(ExecutorType.Activate, CardId.Z_Ex_Xuanzhan, ExXuanzhanEffect);

            //竹竹 猎鹰
            AddExecutor(ExecutorType.SpSummon, CardId.Z_Ex_Lieying, Z_LinkSummon);
            AddExecutor(ExecutorType.Activate, CardId.Z_Ex_Lieying, ExLieyingEffect);

            //竹竹 光辉
            AddExecutor(ExecutorType.SpSummon, CardId.Z_Ex_Guanghui, Z_LinkSummon);
            AddExecutor(ExecutorType.Activate, CardId.Z_Ex_Guanghui, ExGuanghuiEffect);

            //竹竹 战龟
            AddExecutor(ExecutorType.SpSummon, CardId.Z_Ex_Zhangui, Z_LinkSummon);
            AddExecutor(ExecutorType.Activate, CardId.Z_Ex_Zhangui, ExZhanguiEffect);

            //竹竹 TYPEA
            AddExecutor(ExecutorType.SpSummon, CardId.TYPE_A, ASummon);

            //竹竹 EX枫叶
            AddExecutor(ExecutorType.SpSummon, CardId.Z_Ex_Fengye, Z_LinkSummon);
            AddExecutor(ExecutorType.Activate, CardId.Z_Ex_Fengye, ExFengyeEffect);

            //盖放卡片
            AddExecutor(ExecutorType.SpellSet, SpellSet);
            AddExecutor(ExecutorType.SpellSet, SpellSet_2);

            AddExecutor(ExecutorType.Repos, DefaultMonsterRepos);
        }
        #region 卡组检查
        public int CheckRemainInDeck(int id)
        {
            switch (id)
            {
                case CardId.Nibilu:
                    return Bot.GetRemainingCount(CardId.Nibilu, 1);
                case CardId.L_Baiyinji:
                    return Bot.GetRemainingCount(CardId.L_Baiyinji, 1);
                case CardId.L_Ali:
                    return Bot.GetRemainingCount(CardId.L_Ali, 3);
                case CardId.Z_Zhu:
                    return Bot.GetRemainingCount(CardId.Z_Zhu, 3);
                case CardId.Z_Cangqiong:
                    return Bot.GetRemainingCount(CardId.Z_Cangqiong, 3);
                case CardId.Z_Fengye:
                    return Bot.GetRemainingCount(CardId.Z_Fengye, 2);
                case CardId.Huiliuli:
                    return Bot.GetRemainingCount(CardId.Huiliuli, 2);
                case CardId.G:
                    return Bot.GetRemainingCount(CardId.G, 3);
                case CardId.L_Shizhong:
                    return Bot.GetRemainingCount(CardId.L_Shizhong, 2);
                case CardId.L_Chengfa:
                    return Bot.GetRemainingCount(CardId.L_Chengfa, 3);
                case CardId.Huazaikai:
                    return Bot.GetRemainingCount(CardId.Huazaikai, 1);
                case CardId.L_Huanying:
                    return Bot.GetRemainingCount(CardId.L_Huanying, 1);
                case CardId.Z_Lieyinghao:
                    return Bot.GetRemainingCount(CardId.Z_Lieyinghao, 2);
                case CardId.Z_Tanshegouzhua:
                    return Bot.GetRemainingCount(CardId.Z_Tanshegouzhua, 2);
                case CardId.Kuluopai:
                    return Bot.GetRemainingCount(CardId.Kuluopai, 1);
                case CardId.C_Nvshenjuhe:
                    return Bot.GetRemainingCount(CardId.C_Nvshenjuhe, 1);
                case CardId.Wuxianpaoying:
                    return Bot.GetRemainingCount(CardId.Wuxianpaoying, 2);
                case CardId.Erren:
                    return Bot.GetRemainingCount(CardId.Erren, 2);
                case CardId.Luoxuanjieti:
                    return Bot.GetRemainingCount(CardId.Luoxuanjieti, 1);
                case CardId.J_Tuji:
                    return Bot.GetRemainingCount(CardId.J_Tuji, 1);
                case CardId.Z_Yunhai:
                    return Bot.GetRemainingCount(CardId.Z_Yunhai, 1);
                case CardId.Z_Zhanjianchixing:
                    return Bot.GetRemainingCount(CardId.Z_Zhanjianchixing, 2);
                case CardId.Fangxiang:
                    return Bot.GetRemainingCount(CardId.Fangxiang, 1);
                case CardId.Kehuishou:
                    return Bot.GetRemainingCount(CardId.Kehuishou, 1);
                case CardId.Xianjiyishi:
                    return Bot.GetRemainingCount(CardId.Xianjiyishi, 1);
                case CardId.Z_Qinruzhuangzhi:
                    return Bot.GetRemainingCount(CardId.Z_Qinruzhuangzhi, 2);
                case CardId.Shentonggao:
                    return Bot.GetRemainingCount(CardId.Shentonggao, 1);
                case CardId.Shenxuangao:
                    return Bot.GetRemainingCount(CardId.Shenxuangao, 2);
                case CardId.Qianlong:
                    return Bot.GetRemainingCount(CardId.Qianlong, 1);
                case CardId.Huihang:
                    return Bot.GetRemainingCount(CardId.Huihang, 1);
                case CardId.Niesun:
                    return Bot.GetRemainingCount(CardId.Niesun, 2);
                default:
                    return 0;
            }
        }
        #endregion
        public override void OnNewTurn()
        {
            YunHai_Activate = false;
            Erren_Activate = false;

            Qinruzhuangzhi_Activate = false;
            Lieyinghao_Activate = false;
            Zhanjianchixing_Activate = false;
            Yunhai_Activate = false;
            Tanshegouzhua_Activate = false;
            Niesun_Activate = false;
            Huihang_Activate = false;
            Qianlong_Activate = false;

            /////summon/////
            ExCangqiong_Summon = false;
            ExFengye_Summon = false;
            ExLieying_Summon = false;
            ExXuanzhan_Summon = false;
            ExZhangui_Summon = false;
            ExChihong_Summon = false;
            ExGuanghui_Summon = false;
            /////summon/////
        }
        public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
        {
            if (cardId == 27204312) return CardPosition.FaceUpDefence;
            return base.OnSelectPosition(cardId, positions);
        }
        public override int OnSelectOption(IList<int> options)
        {
            if (options.Count == 2 && options[0] == Util.GetStringId(CardId.Qianlong, 1))
                return 1;
            if (options[0] == Util.GetStringId(CardId.Z_Zhanjianchixing, 0)
                || options[0] == Util.GetStringId(CardId.Z_Zhanjianchixing, 1))
            {
                return options[options.Count() - 1];
            }
            if (options[0] == Util.GetStringId(CardId.Niesun, 1)
                || options[0] == Util.GetStringId(CardId.Niesun, 2))
            {
                return 0;
            }
            if (options[0] == Util.GetStringId(CardId.Z_Yunhai, 0)
               || options[0] == Util.GetStringId(CardId.Z_Yunhai, 1))
            {
                if (Enemy.GetMonsterCount() + Enemy.GetSpellCount() > 0)
                {
                    return 0;
                }
                return 1;
            }

            return base.OnSelectOption(options);
        }
        public int SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false)
        {
            List<int> list = new List<int> { 0, 1, 2, 3, 4 };
            int n = list.Count;
            while (n-- > 1)
            {
                int index = Program.Rand.Next(n + 1);
                int temp = list[index];
                list[index] = list[n];
                list[n] = temp;
            }
            foreach (int seq in list)
            {
                int zone = (int)System.Math.Pow(2, seq);
                if (Bot.SpellZone[seq] == null)
                {
                    if (card != null && card.Location == CardLocation.Hand && avoid_Impermanence && Impermanence_list.Contains(seq)) continue;
                    return zone;
                };
            }
            return 0;
        }
        //无限泡影
        public bool Impermanence_activate()
        {
            // negate before effect used
            foreach (ClientCard m in Enemy.GetMonsters())
            {
                if (m.IsMonsterShouldBeDisabledBeforeItUseEffect() && !m.IsDisabled() && Duel.LastChainPlayer != 0)
                {
                    if (Card.Location == CardLocation.SpellZone)
                    {
                        for (int i = 0; i < 5; ++i)
                        {
                            if (Bot.SpellZone[i] == Card)
                            {
                                Impermanence_list.Add(i);
                                break;
                            }
                        }
                    }
                    if (Card.Location == CardLocation.Hand)
                    {
                        AI.SelectPlace(SelectSTPlace(Card, true));
                    }
                    AI.SelectCard(m);
                    return true;
                }
            }

            ClientCard LastChainCard = Util.GetLastChainCard();

            // negate spells
            if (Card.Location == CardLocation.SpellZone)
            {
                int this_seq = -1;
                int that_seq = -1;
                for (int i = 0; i < 5; ++i)
                {
                    if (Bot.SpellZone[i] == Card) this_seq = i;
                    if (LastChainCard != null
                        && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.SpellZone && Enemy.SpellZone[i] == LastChainCard) that_seq = i;
                    else if (Duel.Player == 0 && Util.GetProblematicEnemySpell() != null
                        && Enemy.SpellZone[i] != null && Enemy.SpellZone[i].IsFloodgate()) that_seq = i;
                }
                if ((this_seq * that_seq >= 0 && this_seq + that_seq == 4)
                    || (Util.IsChainTarget(Card))
                    || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(_CardId.HarpiesFeatherDuster)))
                {
                    List<ClientCard> enemy_monsters = Enemy.GetMonsters();
                    enemy_monsters.Sort(CardContainer.CompareCardAttack);
                    enemy_monsters.Reverse();
                    foreach (ClientCard card in enemy_monsters)
                    {
                        if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
                        {
                            AI.SelectCard(card);
                            Impermanence_list.Add(this_seq);
                            return true;
                        }
                    }
                }
            }
            if ((LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone
                || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget()))
                return false;
            // negate monsters
            if (is_should_not_negate() && LastChainCard.Location == CardLocation.MonsterZone) return false;
            if (Card.Location == CardLocation.SpellZone)
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (Bot.SpellZone[i] == Card)
                    {
                        Impermanence_list.Add(i);
                        break;
                    }
                }
            }
            if (Card.Location == CardLocation.Hand)
            {
                AI.SelectPlace(SelectSTPlace(Card, true));
            }
            if (LastChainCard != null) AI.SelectCard(LastChainCard);
            else
            {
                List<ClientCard> enemy_monsters = Enemy.GetMonsters();
                enemy_monsters.Sort(CardContainer.CompareCardAttack);
                enemy_monsters.Reverse();
                foreach (ClientCard card in enemy_monsters)
                {
                    if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
                    {
                        AI.SelectCard(card);
                        return true;
                    }
                }
            }
            return true;
        }
        public bool is_should_not_negate()
        {
            ClientCard last_card = Util.GetLastChainCard();
            if (last_card != null
                && last_card.Controller == 1 && last_card.IsCode(should_not_negate))
                return true;
            return false;
        }
        public override bool OnSelectYesNo(int desc)
        {
            if (desc == Util.GetStringId(CardId.Luoxuanjieti, 0))
            {
                if (Enemy.GetMonsterCount() > 0) { S_Luoxuanjieti = true; return true; }
                return false;
            }
            if (desc == Util.GetStringId(CardId.Z_Lieyinghao, 0)
                || desc == Util.GetStringId(CardId.Niesun, 0))
            {
                return Bot.Deck.Count > 1;
            }
            if (desc == Util.GetStringId(CardId.Z_Cangqiong, 0))
            {
                if (Enemy.GetSpellCount() >= 0)
                {
                    S_Cangqiong = true;
                    return true;
                }
                return false;
            }
            return base.OnSelectYesNo(desc);
        }
        private bool ShizhongEffect()
        {
            if (Card.Location == CardLocation.Hand)
            {
                return Bot.GetMonsterCount() > 0 && Bot.GetMonsters().Where(card => card != null && card.HasSetcode(0x17e) && card.IsFaceup()) != null; ;
            }
            return true;

        }
        private bool GEffect()
        {
            return Duel.Player != 0;
        }
        private bool NiesunEffect()
        {
            if (ActivateDescription == Util.GetStringId(CardId.Niesun, 1))
            {
                AI.SelectCard(CardId.TYPE_A, CardId.Z_Ex_Mingdao, CardId.Z_Ex_Fengye, CardId.Z_Ex_Lieying
                    , CardId.Z_Ex_Chihong, CardId.Z_Ex_Zhangui);
                return true;
            }
            return false;
        }
        //惩罚
        private bool ChengfaEffect()
        {
            if (Duel.Phase >= DuelPhase.Battle && Duel.Player != 0)
            {
                if (GetBestPowerPlayer(true) != Bot) return true;
                return false;
            }
            //IsEnChain() && 
            return (IsEnChain() &&  (Bot.GetMonsterCount() > 0 && Bot.GetCountCardInZone(Bot.GetMonsters(), 0x9eaf, true, false) < 1
                && Bot.GetCountCardInZone(Bot.GetHands(), 0x9eaf, true, false) < 1));
        }
        private bool BaiyinjiEffect()
        {
            if (Card.Location == CardLocation.Hand) return true;
            else
            {
                AI.SelectCard(ShouldSelectCards());
                return true;
            }
        }
        private bool HuanyingEffect()
        {
            if (Card.Location != CardLocation.Grave)
            {
                return true;
            }
            else
            {
                if (Duel.Player != 0)
                {
                    AI.SelectCard(CardId.L_Ali, CardId.L_Baiyinji);
                    return true;
                }
                return false;
            }

        }
        private bool HuiliuliEffect()
        {
            return Duel.LastChainPlayer != 0 && Util.GetLastChainCard() != null;
        }
        private bool ASummon()
        {
            if (Duel.Turn > 1 && Duel.Player == 0)
            {
                return Duel.Phase > DuelPhase.Main1;
            }
            return true;
        }
        private bool KuluopaiEffect()
        {
            ClientCard card = Util.GetLastChainCard();
            if (card != null && card.Controller != 0 && (card.Location == CardLocation.MonsterZone
                 || card.Location == CardLocation.Grave) && card.HasType(CardType.Monster)
                 && !card.IsShouldNotBeTarget())
            {
                S_Kuluopai = true;
                AI.SelectCard(card);
                return true;
            }
            return false;
        }
        private bool KehuishouEffect()
        {
            if (Card.IsFacedown()) return true;
            AI.SelectCard(new int[]{CardId.Fangxiang,CardId.L_Chengfa,CardId.Huazaikai,
                CardId.Kuluopai,CardId.C_Nvshenjuhe,CardId.Erren,CardId.J_Tuji,
                CardId.Luoxuanjieti,CardId.Shentonggao,CardId.Shenxuangao,CardId.Xianjiyishi});
            AI.SelectNextCard(ShouldSelectCards());
            return true;
        }
        private bool LieyinghaoActivate()
        {
            return Card.IsFacedown();
        }
        private bool LieyinghaoEffect()
        {
            if (Card.IsFacedown()) return true;
            Lieyinghao_Activate = true;
            if (Bot.GetCountCardInZone(Bot.GetMonsters(), 0x9eaf, true, false) < 1
                && Bot.GetCountCardInZone(Bot.GetHands(), 0x9eaf, true, false) < 1)
            {
                S_Lieyinghao_1 = true;
                //AI.SelectCard(ShouldSelectCards(false, false, true));
            }
            else
            {
                S_Lieyinghao_2 = true;
                //AI.SelectCard(ShouldSelectCards());
            }
            return true;
        }
        //普通少女竹竹
        private bool ZhuEffect()
        {
            return Util.GetLastChainCard() != null && Duel.LastChainPlayer != 0;
        }
        private bool TanshegouzhuaEffect()
        {
            return (Duel.LastChainPlayer != 0 && Util.GetLastChainCard() != null) || Duel.CurrentChain.Count <= 0;
        }
        private bool ExFengyeEffect()
        {
            if (!Enemy.GetMonsters().Any(card => card != null && card.Attack <= Card.Attack && !card.IsShouldNotBeTarget())) return false;
            S_Exfengye_1 = true;
            S_Exfengye_2 = true;
            return true;
        }
        private bool AEffect()
        {
            return Util.GetLastChainCard() != null && Duel.LastChainPlayer != 0;
        }
        private bool IsExtralMonster(ClientCard card)
        {
            return card.HasType(CardType.Link) || card.HasType(CardType.Fusion)
                || card.HasType(CardType.Xyz) || card.HasType(CardType.Synchro);
        }
        public override IList<ClientCard> OnSelectLinkMaterial(IList<ClientCard> cards, int min, int max)
        {
            if (S_LinkSummon)
            {
                S_LinkSummon = false;
                List<ClientCard> m_cards = (cards.Where(card => card != null && !IsExtralMonster(card))).ToList();
                m_cards.Sort(CardContainer.CompareCardAttack);
                //m_cards.Reverse();
                List<ClientCard> e_cards = (cards.Where(card => card != null && IsExtralMonster(card) && card.Id != CardId.TYPE_A && card.Id != CardId.Z_Ex_Fengye)).ToList();
                e_cards.Sort(CardContainer.CompareCardAttack);
                //e_cards.Reverse();
                if (cards.Any(card => card != null && card.Id == CardId.Z_Fengye) && Bot.HasInExtra(CardId.Z_Ex_Fengye)
                    && !ExFengye_Summon)
                {
                    e_cards.AddRange(m_cards);
                    return Util.CheckSelectCount(e_cards, cards, min, max);
                }
                else
                {
                    m_cards.AddRange(e_cards);
                    return Util.CheckSelectCount(m_cards, cards, min, max);
                }
            }
            return base.OnSelectLinkMaterial(cards, min, max);
        }
        //测试
        public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {
            if (S_Kuluopai) { S_Kuluopai = false; return base.OnSelectCard(cards, min, max, hint, cancelable); ; }
            else if (S_ExChihong)
            {
                S_ExChihong = false;
                List<ClientCard> _cards = cards.ToList();
                _cards.Sort(CardContainer.CompareCardAttack);
                return Util.CheckSelectCount(_cards, cards, min, max);
            }
            else if (S_Luoxuanjieti)
            {
                S_Luoxuanjieti = false;
                List<ClientCard> _cards = (cards.Where(card => card != null && card.Controller != 0)).ToList();
                if (_cards.Count <= 0) return base.OnSelectCard(cards, min, max, hint, cancelable);
                _cards.Sort(CardContainer.CompareCardAttack);
                for (int i = 0; i < _cards.Count; ++i)
                {
                    if (_cards[i].IsShouldNotBeTarget() || _cards[i].IsMonsterDangerous())
                    {
                        if (i == 0) continue;
                        var temp = _cards[0];
                        _cards[0] = _cards[i];
                        _cards[i] = temp;
                    }
                }
                return Util.CheckSelectCount(_cards, cards, min, max);
            }
            else if (S_Yunhai)
            {
                S_Yunhai = false;
                int[] res = ShouldSelectCards();
                IList<ClientCard> resCards = new List<ClientCard>();
                for (int i = 0; i < res.Length; ++i)
                {
                    foreach (var card in cards)
                    {
                        if (card.Id == res[i])
                        {
                            resCards.Add(card);
                        }
                    }
                }
                if (resCards.Count() <= 0) return base.OnSelectCard(cards, min, max, hint, cancelable);
                return Util.CheckSelectCount(resCards, cards, min, max);

            }
            else if (S_Cangqiong)
            {
                S_Cangqiong = false;
                List<ClientCard> resCards_1 = (cards.Where(card => card != null && card.Controller != 0)).ToList();
                List<ClientCard> resCards_2 = (cards.Where(card => card != null && card.Controller == 0)).ToList();
                resCards_1.AddRange(resCards_2);
                if (resCards_1.Count() <= 0) return base.OnSelectCard(cards, min, max, hint, cancelable);
                return Util.CheckSelectCount(resCards_1, cards, min, max);
            }
            else if (S_Exfengye_1 || S_Exfengye_2)
            {
                if (S_Exfengye_1) S_Exfengye_1 = false;
                else if (S_Exfengye_2) S_Exfengye_2 = false;
                if (S_Exfengye_2)
                {
                    ExfengyeAtk = cards[0].Attack;
                    return base.OnSelectCard(cards, min, max, hint, cancelable);
                }
                else
                {
                    List<ClientCard> resCards = new List<ClientCard>(cards);
                    resCards.Sort(CardContainer.CompareCardAttack);
                    return Util.CheckSelectCount(resCards, cards, min, max);
                }
            }
            else if (S_Lieyinghao_1)
            {
                S_Lieyinghao_1 = false;
                int[] res = ShouldSelectCards(false, false, true);
                IList<ClientCard> resCards = new List<ClientCard>();
                for (int i = 0; i < res.Length; ++i)
                {
                    foreach (var card in cards)
                    {
                        if (card.Id == res[i])
                        {
                            resCards.Add(card);
                        }
                    }
                }
                if (resCards.Count() <= 0) return base.OnSelectCard(cards, min, max, hint, cancelable);
                return Util.CheckSelectCount(resCards, cards, min, max);
            }
            else if (S_Lieyinghao_2)
            {
                S_Lieyinghao_2 = false;
                int[] res = ShouldSelectCards();
                IList<ClientCard> resCards = new List<ClientCard>();
                for (int i = 0; i < res.Length; ++i)
                {
                    foreach (var card in cards)
                    {
                        if (card.Id == res[i])
                        {
                            resCards.Add(card);
                        }
                    }
                }
                if (resCards.Count() <= 0) return base.OnSelectCard(cards, min, max, hint, cancelable);
                return Util.CheckSelectCount(resCards, cards, min, max);
            }
            return base.OnSelectCard(cards, min, max, hint, cancelable);
        }
        //双头战龟
        private bool ExZhanguiEffect()
        {
            if (Bot.HasInGraveyard(CardId.Z_Yunhai))
            {
                AI.SelectCard(CardId.Z_Yunhai);
            }
            else if (Bot.HasInGraveyard(CardId.Z_Qinruzhuangzhi))
            {
                AI.SelectCard(CardId.Z_Qinruzhuangzhi);
            }
            else if (Bot.HasInGraveyard(CardId.Z_Lieyinghao))
            {
                AI.SelectCard(CardId.Z_Lieyinghao);
            }
            else if (Bot.HasInGraveyard(CardId.Niesun))
            {
                AI.SelectCard(CardId.Niesun);
            }
            else if (Bot.HasInGraveyard(CardId.Z_Yunhai))
            {
                AI.SelectCard(CardId.Z_Yunhai);
            }
            else if (Bot.HasInGraveyard(CardId.Z_Zhanjianchixing))
            {
                AI.SelectCard(CardId.Z_Zhanjianchixing);
            }
            else
            {
                AI.SelectCard(ShouldSelectCards());
            }
            return true;
        }
        private bool IsEnChain()
        {
            //|| Duel.CurrentChain.Count == 0
            return (Duel.LastChainPlayer != 0 && Util.GetLastChainCard() != null) || Duel.CurrentChain.Count <= 0;
        }
        private bool YunhaiActivate()
        {
            return Card.IsFacedown();
        }
        //战舰云海
        private bool YunhaiEffect()
        {
            if (YunHai_Activate) return false;
            if (ActivateDescription == Util.GetStringId(CardId.Z_Yunhai, 0))
            {
                YunHai_Activate = true;
                ClientCard card = Util.GetBestEnemyCard();
                //&& IsEnChain()
                if (card != null && (card.HasType(CardType.Field) || card.HasType(CardType.Continuous) || card.HasType(CardType.Monster) || card.IsFacedown())) { AI.SelectCard(card); return true; }
            }
            if (ActivateDescription == Util.GetStringId(CardId.Z_Yunhai, 1))
            {
                YunHai_Activate = true;
                if (Bot.Deck.Count < 4) return false;
                if (Duel.Player == 0)
                {
                    IList<ClientCard> cards = new List<ClientCard>();
                    if (Bot.GetFields() == null) return false;
                    foreach (var card in Bot.GetFields())
                    {
                        if ((card.Id == CardId.Huihang && !Huihang_Activate)
                            || (card.Id == CardId.Qianlong && !Qianlong_Activate)
                             || (card.Id == CardId.Z_Qinruzhuangzhi && !Qinruzhuangzhi_Activate))
                        {
                            cards.Add(card);
                        }
                        else if (!card.IsExtraCard()
                            && card.HasType(CardType.Monster))
                        {
                            cards.Add(card);
                        }
                    }
                    if (cards.Count <= 0) return false;
                    AI.SelectCard(cards);
                    S_Yunhai = true;
                    return true;
                }
            }
            return false;
        }
        private bool TujiEffect()
        {
            if (Card.Location != CardLocation.MonsterZone)
            {
                return true;
            }
            else
            {
                if (IsEnChain())
                {
                    List<ClientCard> cards = Enemy.GetMonsters();
                    if (cards == null || cards.Count <= 0) return false;
                    cards.Sort(CardContainer.CompareCardAttack);
                    AI.SelectCard(cards);
                    return true;
                }
                return false;
            }
        }
        private bool QianlongEffect()
        {
            Qianlong_Activate = true;
            if (Card.Location != CardLocation.Grave)
            {
                return ExZhanguiEffect();
            }
            else
            {
                return true;
            }
        }
        private bool Z_LinkSummon()
        {
            if ((Bot.HasInMonstersZone(CardId.Z_Ex_Fengye, true, false, true)
                || Bot.HasInMonstersZone(CardId.Z_Ex_Mingdao, true, false, true))
                && Bot.GetCountCardInZone(Bot.GetMonsters(), 0x9eaf, true, false) < 2) return false;
            switch (Card.Id)
            {
                case CardId.Z_Ex_Chihong:
                    bool isShouldSummon = (Enemy.GetMonsters().Any(card => card != null && !card.IsShouldNotBeTarget()));
                    if (!isShouldSummon) return false;
                    break;
                case CardId.Z_Ex_Guanghui:
                    int[] res = ShouldSelectCards(true);
                    bool isShouldSummon_2 = false;
                    for (int i = 0; i < res.Length; i++)
                    {
                        if (res[i] <= 0) continue;
                        foreach (var card in Bot.GetHands())
                        {
                            if (card.Id == res[i])
                            {
                                isShouldSummon_2 = true;
                                break;
                            }
                        }
                    }
                    if (!isShouldSummon_2) return false;
                    break;
                case CardId.Z_Ex_Cangqiong:
                    List<ClientCard> cards = Bot.GetHands();
                    cards.AddRange(Bot.GetGraveyardMonsters());
                    if (Bot.Hand.Count <= 0 || Bot.GetSpellCountWithoutField() > 4) return false;
                    if (!((Bot.GetHands().Any(card => card != null && card.IsCode(CardId.Z_Lieyinghao)) && !Lieyinghao_Activate)
                        || Bot.GetHands().Any(card => card != null && card.IsCode(CardId.Niesun)) && !Niesun_Activate && Bot.GetMonstersInMainZone().Count < 5 && cards.Any(card => card != null && card.HasSetcode(0x5bef) && card.HasType(CardType.Monster))))
                    {
                        return false;
                    }
                    break;
                case CardId.Z_Ex_Xuanzhan:
                    if (CheckRemainInDeck(CardId.Z_Qinruzhuangzhi) < 1 && CheckRemainInDeck(CardId.Huihang) < 1
                        && !Bot.HasInSpellZone(CardId.Kehuishou, true) && Bot.GetGraveyardTraps().Count > 2) return false;
                    break;
                default:
                    break;
            }
            S_LinkSummon = true;
            switch (Card.Id)
            {
                case CardId.Z_Ex_Cangqiong:
                    ExCangqiong_Summon = true;
                    break;
                case CardId.Z_Ex_Fengye:
                    ExFengye_Summon = true;
                    break;
                case CardId.Z_Ex_Lieying:
                    ExLieying_Summon = true;
                    break;
                case CardId.Z_Ex_Xuanzhan:
                    ExXuanzhan_Summon = true;
                    break;
                case CardId.Z_Ex_Zhangui:
                    ExZhangui_Summon = true;
                    break;
                case CardId.Z_Ex_Chihong:
                    ExChihong_Summon = true;
                    break;
                case CardId.Z_Ex_Guanghui:
                    ExGuanghui_Summon = true;
                    break;
                default:
                    break;
            }
            return true;
        }
        //光辉
        private bool ExGuanghuiEffect()
        {
            if (((Duel.Player == 0 && Enemy.GetSpells().Where(card => card != null && card.IsFacedown()).Count() > 0
                && (!(Bot.HasInHand(CardId.Z_Lieyinghao) && Lieyinghao_Activate) || !Bot.HasInSpellZone(CardId.Z_Qinruzhuangzhi)))
                || Duel.Player == 1) && Bot.HasInHand(CardId.Z_Qinruzhuangzhi) && !Qinruzhuangzhi_Activate)
            {
                AI.SelectCard(CardId.Z_Qinruzhuangzhi);
            }
            else if (Duel.Player == 1 && !Zhanjianchixing_Activate && Bot.HasInHand(CardId.Z_Zhanjianchixing))
            {
                AI.SelectCard(CardId.Z_Zhanjianchixing);
            }
            else if (GetBestPowerPlayer() != Bot && Bot.HasInHand(CardId.Z_Yunhai)
                && !Yunhai_Activate)
            {
                AI.SelectCard(CardId.Z_Yunhai);
            }
            else if (!Lieyinghao_Activate && Bot.HasInHand(CardId.Z_Lieyinghao))
            {
                AI.SelectCard(CardId.Z_Lieyinghao);
            }
            return true;
        }
        //螺旋阶梯
        private bool LuoxuanjietiEffect()
        {
            if (Card.Location == CardLocation.Hand)
            {
                return Bot.GetMonsterCount() <= 0 && Enemy.GetMonsterCount() > 0;
            }
            else
            {
                if (Enemy.GetMonsterCount() <= 0) return false;
                ClientCard card = Util.GetLastChainCard();
                if (Enemy.GetMonsterCount() > 0 && card != null && card.Controller != 0
                    && card.Location == CardLocation.Onfield
                    && Duel.CurrentChain.Count > 0)
                {
                    Logger.DebugWriteLine("进入旋转楼梯01");
                    return true;
                }
                else if (Duel.Phase < DuelPhase.End)
                {
                    Logger.DebugWriteLine("进入旋转楼梯02");
                    return GetBestPowerPlayer(false) != Bot;
                }
                else
                {
                    List<ClientCard> cards = Enemy.GetMonsters();
                    cards = (cards.Where(_card => _card != null && _card.IsShouldNotBeTarget() || _card.IsMonsterDangerous())).ToList();
                    Logger.DebugWriteLine("进入旋转楼梯03");
                    return cards != null || GetBestPowerPlayer(false) != Bot;
                }
            }
        }
        //回航
        private bool HuihangEffect()
        {
            if (Card.Location == CardLocation.Grave)
            {
                Huihang_Activate = true;
                if (!Bot.HasInHand(CardId.Z_Lieyinghao))
                {
                    AI.SelectCard(CardId.Z_Lieyinghao, CardId.Niesun);
                }
                else
                {
                    AI.SelectCard(CardId.Niesun, CardId.Z_Lieyinghao);
                }
                return true;
            }
            return false;
        }
        //二人
        private bool ErrenEffect()
        {
            if (Erren_Activate) return false;
            if (Bot.GetMonsterCount() <= 0 || Enemy.GetMonsterCount() <= 0) return false;
            if (Bot.GetCountCardInZone(Bot.GetMonsters(), 0x9eaf, true, false) < 2
                && Duel.Phase < DuelPhase.Battle && Bot.GetMonsterCount() < 2) return false;
            ClientCard en_Card = Util.GetBestEnemyMonster();
            ClientCard en_Card2 = Util.GetProblematicEnemyMonster();
            if (en_Card == null) return false;
            if (en_Card2 != null) en_Card = en_Card2;
            //S_Erren_1 = true;
            // S_Erren_2 = true;
            List<ClientCard> cards = (Bot.GetMonsters().Where(card => card != null && !card.HasSetcode(0x9eaf))).ToList();
            List<ClientCard> cards_2 = (Bot.GetMonsters().Where(card => card != null && card.HasSetcode(0x9eaf))).ToList();
            cards_2.Sort(CardContainer.CompareCardAttack);
            cards.AddRange(cards_2);
            AI.SelectCard(en_Card);
            if (Bot.HasInMonstersZone(CardId.TYPE_A, true, false, true))
            {
                AI.SelectNextCard(CardId.TYPE_A);
            }
            else
            {
                AI.SelectNextCard(cards);
            }
            Erren_Activate = true;
            return true;
        }
        private bool ExChihongEffect()
        {
            S_ExChihong = true;
            return true;
        }
        private ClientField GetBestPowerPlayer(bool onlyAtk = false)
        {
            int Bot_Power = Util.GetBestPower(Bot, onlyAtk);
            int En_Power = Util.GetBestPower(Enemy, onlyAtk);
            return Bot_Power > En_Power ? Bot : Enemy;
        }
        //女神居合
        private bool NvshenjuheEffect()
        {
            if (Card.Location == CardLocation.Onfield)
            {
                int Bot_Power = Util.GetBestPower(Bot, true);
                int En_Power = Util.GetBestPower(Enemy, true);
                if (Bot_Power < En_Power || Enemy.GetMonsterCount() > Bot.GetMonsterCount())
                {
                    return true;

                }
                else
                {
                    IList<ClientCard> cards = new List<ClientCard>();
                    foreach (var card in Bot.GetMonsters())
                    {
                        if (card.HasType(CardType.Effect)
                            || card.HasType(CardType.Pendulum))
                        {
                            cards.Add(card);
                        }
                    }
                    foreach (var card in Enemy.GetMonsters())
                    {
                        if (card.HasType(CardType.Effect)
                            || card.HasType(CardType.Pendulum))
                        {
                            cards.Add(card);
                        }
                    }
                    if (cards.Count() <= 0) return false;
                    int damage = cards.Count() * 500;
                    return damage >= Enemy.LifePoints;
                }
            }
            if (Card.Location == CardLocation.Grave) return true;
            return false;
        }
        private int[] ShouldSelectCards(bool OnlyActivateCheck = false, bool Reverse = false, bool MonserReverse = false)
        {
            List<int> temp = new List<int> {   CardId.Z_Yunhai, CardId.Z_Qinruzhuangzhi,CardId.Z_Lieyinghao,CardId.Z_Zhanjianchixing,
           CardId.Z_Tanshegouzhua,CardId.Niesun, CardId.Huihang,CardId.Qianlong,
           CardId.Z_Fengye,CardId.Z_Zhu,CardId.Z_Cangqiong};
            if (Reverse) temp.Reverse();
            if (MonserReverse)
            {
                temp.Insert(0, CardId.Z_Fengye);
                temp.Insert(0, CardId.Z_Zhu);
                temp.Insert(0, CardId.Z_Cangqiong);
            }
            if (Qinruzhuangzhi_Activate && temp.Contains(CardId.Z_Qinruzhuangzhi))
            {
                temp.Remove(CardId.Z_Qinruzhuangzhi);
            }
            if (Lieyinghao_Activate && temp.Contains(CardId.Z_Lieyinghao))
            {
                temp.Remove(CardId.Z_Lieyinghao);
            }
            if (Zhanjianchixing_Activate && temp.Contains(CardId.Z_Zhanjianchixing))
            {
                temp.Remove(CardId.Z_Zhanjianchixing);
            }
            if (Yunhai_Activate && temp.Contains(CardId.Z_Yunhai))
            {
                temp.Remove(CardId.Z_Yunhai);
            }

            if (Tanshegouzhua_Activate && temp.Contains(CardId.Z_Tanshegouzhua))
            {
                temp.Remove(CardId.Z_Tanshegouzhua);
            }

            if (Niesun_Activate && temp.Contains(CardId.Niesun))
            {
                temp.Remove(CardId.Niesun);
            }
            if (Huihang_Activate && temp.Contains(CardId.Huihang))
            {
                temp.Remove(CardId.Huihang);
            }

            if (Qianlong_Activate && temp.Contains(CardId.Qianlong))
            {
                temp.Remove(CardId.Qianlong);
            }
            if (temp == null || temp.Count <= 0) return new int[] { CardId.L_Ali };
            if (Bot.Hand.Count <= 0 || OnlyActivateCheck)
            {
                int[] res_2 = new int[temp.Count];
                for (int i = 0; i < res_2.Length; i++)
                {
                    res_2[i] = temp[i];
                }
                return res_2;
            }
            if (Bot.HasInHandOrInSpellZone(CardId.Z_Qinruzhuangzhi) && temp.Contains(CardId.Z_Qinruzhuangzhi))
            {
                temp.Remove(CardId.Z_Qinruzhuangzhi);
            }
            if (Bot.HasInHandOrInSpellZone(CardId.Z_Lieyinghao) && temp.Contains(CardId.Z_Lieyinghao))
            {
                temp.Remove(CardId.Z_Lieyinghao);
            }
            if (Bot.HasInHandOrInSpellZone(CardId.Z_Zhanjianchixing) && temp.Contains(CardId.Z_Zhanjianchixing))
            {
                temp.Remove(CardId.Z_Zhanjianchixing);
            }
            if (Bot.HasInHandOrInSpellZone(CardId.Z_Yunhai) && temp.Contains(CardId.Z_Yunhai))
            {
                temp.Remove(CardId.Z_Yunhai);
            }

            if (Bot.HasInHandOrInSpellZone(CardId.Z_Tanshegouzhua) && temp.Contains(CardId.Z_Tanshegouzhua))
            {
                temp.Remove(CardId.Z_Tanshegouzhua);
            }

            if (Bot.HasInHandOrInSpellZone(CardId.Niesun) && temp.Contains(CardId.Niesun))
            {
                temp.Remove(CardId.Niesun);
            }

            if (Bot.HasInHandOrInSpellZone(CardId.Huihang) && temp.Contains(CardId.Huihang))
            {
                temp.Remove(CardId.Huihang);
            }

            if (Bot.HasInHandOrInSpellZone(CardId.Qianlong) && temp.Contains(CardId.Qianlong))
            {
                temp.Remove(CardId.Qianlong);
            }
            if (temp == null || temp.Count <= 0) return new int[] { CardId.L_Ali };
            int[] res = new int[temp.Count];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = temp[i];
            }
            return res;
        }
        private bool ExLieyingEffect()
        {
            AI.SelectCard(ShouldSelectCards());
            return true;
        }
        private bool ExXuanzhanEffect()
        {
            if (CheckRemainInDeck(CardId.Qianlong) > 0
                && CheckRemainInDeck(CardId.Huihang) > 0
                && !Qianlong_Activate
                && (CheckRemainInDeck(CardId.Niesun) > 0 || CheckRemainInDeck(CardId.Z_Lieyinghao) > 0))
            {
                AI.SelectCard(CardId.Qianlong, CardId.Huihang);
            }
            if (CheckRemainInDeck(CardId.Huihang) > 0
                && !Huihang_Activate
                && (CheckRemainInDeck(CardId.Niesun) > 0 || CheckRemainInDeck(CardId.Z_Lieyinghao) > 0))
            {
                AI.SelectCard(CardId.Huihang);
            }
            else if (CheckRemainInDeck(CardId.Z_Qinruzhuangzhi) > 0
                && !Qinruzhuangzhi_Activate && Bot.Deck.Count > 1)
            {
                AI.SelectCard(CardId.Z_Qinruzhuangzhi);
            }
            return true;
        }
        private bool NibiluEffect()
        {
            return Enemy.GetMonsterCount() > Bot.GetMonsterCount();
        }
        //神之宣告
        private bool ShenEffect()
        {
            return (Card.Id == CardId.Shentonggao ? Bot.LifePoints > 1500 : true) && ((Duel.LastChainPlayer != 0 && Util.GetLastChainCard() != null)
                || (Duel.LastSummonPlayer == 1));
        }
        //侵入装置
        private bool QinruzhuangzhiEffect()
        {
            ClientCard card = Util.GetLastChainCard();

            if ((Card.Location == CardLocation.Grave) || (card != null && !card.IsDisabled()))
            {
                Qinruzhuangzhi_Activate = true;
                return true;
            }
            return false;
        }
        //战舰赤星
        private bool ZhanjianchixingActivate()
        {
            return Card.IsFacedown() && !Bot.HasInSpellZone(Card.Id, true, true);
        }
        private bool ZhanjianchixingEffect()
        {
            //if (Card.IsFacedown() && !Bot.HasInSpellZone(Card.Id,true,true)) return true;
            if (ActivateDescription == Util.GetStringId(CardId.Z_Zhanjianchixing, 0))
            {
                if (Duel.Phase >= DuelPhase.Battle && Duel.Player != 0) return true;
                else if (Bot.GetCountCardInZone(Bot.GetMonsters(), 0x9eaf, true, false) < 1
                 && Duel.Player != 0) return true;
                else if (Bot.GetCountCardInZone(Bot.GetMonsters(), 0x9eaf, true, false) < 1
                && Bot.GetCountCardInZone(Bot.GetHands(), 0x9eaf, true, false) < 1 && Duel.Player == 0) return true;
                return false;
            }
            //效果无效
            if (ActivateDescription == Util.GetStringId(CardId.Z_Zhanjianchixing, 1))
            {
                ClientCard card = Util.GetLastChainCard();
                if (Duel.LastChainPlayer != 0 && Util.GetLastChainCard() != null &&
                    card != null && card.Location == CardLocation.MonsterZone &&
                    !card.IsShouldNotBeTarget())
                {
                    AI.SelectCard(card);
                    return true;
                }
                return false;
            }
            return false;
        }
        private bool SpellSet_2()
        {
            return (Card.HasType(CardType.Trap) || Card.HasType(CardType.QuickPlay))
                && !Card.HasSetcode(0x5bef) && !Card.IsCode(CardId.Z_Qinruzhuangzhi);
        }
        private bool SpellSet()
        {
            switch (Card.Id)
            {
                case CardId.Z_Qinruzhuangzhi:
                case CardId.Z_Lieyinghao:
                case CardId.Niesun:
                case CardId.Z_Yunhai:
                case CardId.Z_Zhanjianchixing:
                    return !Bot.HasInSpellZone(Card.Id);
                case CardId.Wuxianpaoying:
                case CardId.Shentonggao:
                case CardId.Shenxuangao:
                case CardId.Kuluopai:
                    return true;
                default:
                    break;
            }
            return Bot.GetSpellCountWithoutField() < 5 && (Card.HasType(CardType.Trap) || Card.HasType(CardType.QuickPlay))
                    && Card.HasSetcode(0x5bef) && !Bot.GetSpells().Any(card => card != null && card.Id == Card.Id && !card.IsDisabled());
        }
        private bool AliEffect()
        {
            if (ActivateDescription == Util.GetStringId(CardId.L_Ali, 0))
            {
                if (!Bot.HasInHandOrInSpellZone(CardId.L_Chengfa)
                    && CheckRemainInDeck(CardId.L_Chengfa) > 0)
                {
                    AI.SelectCard(CardId.L_Chengfa);
                }
                else
                {
                    AI.SelectCard(CardId.L_Huanying, CardId.L_Baiyinji);
                }

            }
            if (ActivateDescription == Util.GetStringId(CardId.L_Ali, 1))
            {
                return true;
            }
            return true;
        }
    }
}
