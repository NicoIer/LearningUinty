using Games.CricketGame.Cricket_;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public abstract class AbsCricketUI : MonoBehaviour
    {
        protected CricketData _data;
        protected bool _connected;
        

        public virtual void Connect(CricketData data)
        {
            if (_connected)
            {
                DisConnect();    
            }

            _data = data;
            _data.expRateChangeAction += _exp_change;
            _data.levelUpAction += _level_change;
            _data.metaChangeAction += _meta_change;
            _data.nameChangeAction += _name_change;
            _data.healthChangeAction += _health_change;
            _connected = true;
        }

        public virtual void DisConnect()
        {
            if(!_connected)
                return;
            _data.expRateChangeAction -= _exp_change;
            _data.levelUpAction -= _level_change;
            _data.metaChangeAction -= _meta_change;
            _data.nameChangeAction -= _name_change;
            _data.healthChangeAction -= _health_change;
            _data = null;
            _connected = false;
        }

        protected abstract void _exp_change(float attained, float total);
        protected abstract void _level_change(int pre, int now);
        protected abstract void _name_change(string pre, string now);
        protected abstract void _meta_change(CricketData data);
        protected abstract void _health_change(float cur, float max);
    }
}