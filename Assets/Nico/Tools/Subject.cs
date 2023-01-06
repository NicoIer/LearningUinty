using System.Collections.Generic;

namespace Nico.Common
{
    /// <summary>
    /// 主题
    /// </summary>
    public abstract class Subject
    {
        private List<Observer> observers = new List<Observer>();

        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (Observer observer in observers)
            {
                observer.OnNotify();
            }
        }
    }

}