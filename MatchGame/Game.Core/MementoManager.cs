using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Game.Core
{
    /// <summary>
    /// 保存玩游戏记录的管理类
    /// </summary>
    public static class MementoManager
    {
        private readonly static Stack<StepsMemento> _mementoList;
        static MementoManager()
        {
            _mementoList = new Stack<StepsMemento>();
        }

        public static void Add(StepsMemento memento)
        {
            _mementoList.Push(memento);
        }

        public static void Clear()
        {
            _mementoList.Clear();
        }

        /// <summary>
        /// 弹出 steps 之后的步骤
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static List<StepsMemento> Get(int steps)
        {
            List<StepsMemento> list = new List<StepsMemento>();
            _Dequeue(list, steps);
            return list;
        }


        private static void _Dequeue(List<StepsMemento> list,int steps)
        {
            if (steps < 1)
                return;

            if (_mementoList.Any(m => m.Steps >= steps))
            {
                var item = _mementoList.Pop();
                list.Add(item);
                _Dequeue(list, steps);
            }
        }
    }
}
