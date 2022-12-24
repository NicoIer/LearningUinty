using System;

namespace Script.Tools.DesignPattern
{
    /// <summary>
    /// 超出计划数量异常
    /// </summary>
    public class BeyondException : Exception
    {
        public BeyondException(string msg) : base(msg)
        {
        }
    }
}