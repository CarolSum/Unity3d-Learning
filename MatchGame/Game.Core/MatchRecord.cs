using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Core
{
    /// <summary>
    /// 火柴记录
    /// </summary>
    public  class MatchRecord
    {
        /// <summary>
        /// 火柴的位置 （1—3）
        /// </summary>
        public int Position { get; set; }


        /// <summary>
        /// 当前位置的火柴数量
        /// </summary>
        public int Number { get; set; }


        /// <summary>
        /// 初始化火柴的布局
        /// </summary>
        /// <returns></returns>
        public static List<MatchRecord> GetMatchLayout()
        {
            List<MatchRecord> layout = new List<MatchRecord>();
            layout.Add(new MatchRecord()
            {
                Position = 1,
                Number = 3
            });
            layout.Add(new MatchRecord
            {
                Position = 2,
                Number = 5
            });
            layout.Add(new MatchRecord
            {
                Position = 3,
                Number = 7
            });
            return layout;
        }
    }
}
