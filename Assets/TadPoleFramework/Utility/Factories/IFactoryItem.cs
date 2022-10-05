using System;

namespace TadPoleFramework.Utility.Factories
{
    public interface IFactoryItem<TArg> where TArg : IFactoryItemArg
    {
        TArg Arg { get; set; }
        
        void ReInitialize(TArg arg);
    }
    
    public interface IFactoryItem<TArg, TEnum> : IFactoryItem<TArg> where TArg : IFactoryItemArg where TEnum : Enum
    {
        TEnum PrefabType { get; }
    }
}