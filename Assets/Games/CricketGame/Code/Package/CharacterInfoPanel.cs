using Games.CricketGame.Cricket_;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.Code.Package
{
    public class CharacterInfoPanel : MonoBehaviour
    {
        public Text nameText;
        public Text descText;

        public void _update(Character character)
        {//ToDO 这里其实只需要一个Enum
            nameText.text = character.name;
            descText.text = character.desc;
        }
    }
}