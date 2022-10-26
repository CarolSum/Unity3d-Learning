using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Core
{
    public class GameClient
    {
        private readonly GamePlayer _bluePlayer;
        private readonly GamePlayer _redPlayer;
        private GameControl _gameControl;

        public GameClient()
        {
            _bluePlayer = new GamePlayer
            {
                Id = 1,
                Name = "玩家1"
            };
            _redPlayer = new GamePlayer
            {
                Id = 2,
                Name = "玩家2"
            };
            _gameControl = new GameControl();
            CurrentPlayer = _bluePlayer;
        }

        /// <summary>
        /// 当前第几轮
        /// </summary>
        public int CurrentSteps { get; private set; } = 1;


        /// <summary>
        /// 当前玩家
        /// </summary>
        public GamePlayer CurrentPlayer { get; private set; } 


        public void Start(int position, int pemoveNumber)
        {
            var result = _gameControl.Play(CurrentPlayer, CurrentSteps, position, pemoveNumber);
            if (result)
            {
                if (_gameControl.IsOver())
                {
                    this.Reset();
                    Console.WriteLine($"******************* 游戏结束，玩家【{CurrentPlayer.Name}】输了，请再接再励哦！游戏重新开始 *************");
                    return;
                }

                if (CurrentPlayer == _bluePlayer)
                    CurrentPlayer = _redPlayer;
                else
                    CurrentPlayer = _bluePlayer;

                if (CurrentPlayer == _bluePlayer)
                    CurrentSteps += 1;
            }
        }

        /// <summary>
        /// 撤回游戏到某一轮小游戏
        /// </summary>
        /// <param name="steps"></param>
        public bool Undo(int steps)
        {
            var stepsMemento = _gameControl.Undo(steps);
            if (stepsMemento != null)
            {
                this.CurrentPlayer = stepsMemento.Player;
                this.CurrentSteps = stepsMemento.Steps;
                return true;
            }
            Console.WriteLine("撤回失败，没有步数撤回！");
            return false;
           
        }


        /// <summary>
        /// 重置游戏
        /// </summary>
        public void Reset()
        {
            _gameControl = new GameControl();
            CurrentSteps = 1;
            MementoManager.Clear();
        }

        public override string ToString()
        {
            return _gameControl.ToString();
        }
    }
}
