namespace Nico.Common
{
    /// <summary>
    /// 观察者 关注某个主题 并在OnNotify中处理消息
    /// </summary>
    public abstract class Observer
    {//ToDo 完成观察者模式的代码
        public abstract void OnNotify();
    }

}