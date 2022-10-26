using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Core
{
    /// <summary>
    /// 步骤记录类
    /// </summary>
    public  class StepsMemento
    {

        /// <summary>
        /// 记录第几轮
        /// </summary>
        public int Steps { get; set; }

        /// <summary>
        /// 玩家
        /// </summary>
        public GamePlayer Player { get; set; }


        /// <summary>
        /// 在哪个位置拿走物体
        /// </summary>
        public int Position { get; set; }


        /// <summary>
        /// 拿走物体个数
        /// </summary>
        public int RemoveNumber { get; set; }

    }
}
