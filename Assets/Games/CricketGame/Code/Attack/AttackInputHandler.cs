using Cysharp.Threading.Tasks;
using Games.CricketGame.Attack;
using Games.CricketGame.Cricket_;
using Games.CricketGame.Npc_;
using Games.CricketGame.UI;
using UnityEngine;

namespace Games.CricketGame.Code.Attack
{
    public class AttackInputHandler
    {
        public async UniTask<AttackInputStruct> GetNpcInput(Npc npc)
        {//ToDo fix it
            return null;
        }
        public async UniTask<AttackInputStruct> GetPlayerInput()
        {
            while (true)
            {
                if (InputStorage.player_input != null)
                {
                    var temp = InputStorage.player_input;
                    InputStorage.player_input = null;
                    Debug.Log("玩家选择技能完毕");
                    return temp;
                }
                await UniTask.WaitForFixedUpdate();
            }
        }

        public async UniTask<CricketData> GetPlayerNextCricket()
        {
            UIManager.instance.cricketPanel.OpenSelectPanel();
            while (true)
            {
                if (InputStorage.player_input != null)
                {
                    var temp = InputStorage.player_input;
                    InputStorage.player_input = null;
                    UIManager.instance.cricketPanel.CloseSelectPanel();
                    return AttackInputStruct.CastToType<CricketData>(temp);
                }
                await UniTask.WaitForFixedUpdate();
            }
        }

        public async UniTask<CricketData> GetNpcNextCricket(Npc npc)
        {
            return npc.FirstAvailableCricket();
        }

    }
}