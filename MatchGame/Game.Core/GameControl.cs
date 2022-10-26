using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Game.Core
{
    public class GameControl
    {
        private readonly List<MatchRecord> _layout;

        public GameControl()
        {
            _layout = MatchRecord.GetMatchLayout();
        }


        /// <summary>
        /// 玩游戏
        /// </summary>
        public bool Play(GamePlayer player, int steps , int position, int pemoveNumber)
        {
            var matchRecord = _layout.FirstOrDefault(m => m.Position == position);
            if (matchRecord == null)
            {
                Console.WriteLine("出错了。。。。位置在1到3中取值，玩家重试。。");
                return false;
            }

            if (matchRecord.Number < pemoveNumber)
            {
                Console.WriteLine($"位置{position}只剩下{matchRecord.Number}根火柴，玩家重新开始。。");
                return false;
            }

            matchRecord.Number -= pemoveNumber;

            StepsMemento memento = new StepsMemento
            {
                Player = player,
                Position = position,
                RemoveNumber = pemoveNumber,
                Steps = steps
            };
            MementoManager.Add(memento);
            return true;
        }


        /// <summary>
        /// 撤回游戏，注意：如果没撤回到，返回的是空对象
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        public StepsMemento Undo(int steps)
        {
            var list = MementoManager.Get(steps);
            StepsMemento last = null;
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    var matchRecord = _layout.FirstOrDefault(m => m.Position == item.Position);
                    if (matchRecord != null)
                    {
                        //恢复移除的数量
                        matchRecord.Number += item.RemoveNumber; 
                    }
                    last = item;
                }
            }
            return last;
        }


        /// <summary>
        /// 游戏是否结束
        /// </summary>
        /// <returns>当前完家是否输了</returns>
        public bool IsOver()
        {
            return !_layout.Any(m => m.Number > 0);
        }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in _layout)
            {
                if (item.Number > 0)
                {
                    builder.Append($"第{item.Position}行：");
                    for (int i = 0; i < item.Number; i++)
                    {
                        builder.Append("*");
                    }
                    builder.Append("\n");
                }
            }
            return builder.ToString();
        }
    }
}
