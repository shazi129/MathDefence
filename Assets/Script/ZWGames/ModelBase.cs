using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public class ObservableName : Attribute
{
    public string name = String.Empty;

    public ObservableName(string bindName)
    {
        name = bindName;
    }
}

public class ModelBase
{
    public class ModelBindingInfo
    {
        public PropertyInfo propertyInfo = null;
        public ArrayList callbacks = new ArrayList();
    }

    private Dictionary<string, ModelBase.ModelBindingInfo> _bindingTable = new Dictionary<string, ModelBase.ModelBindingInfo>();

    public ModelBase()
    {
        collectObservableProperty();
    }

    private void collectObservableProperty()
    {
        _bindingTable.Clear();

        Type type = this.GetType();
        PropertyInfo[] propertyInfos = type.GetProperties();
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            object[] obs = propertyInfos[i].GetCustomAttributes(false);
            for (int j = 0; j < obs.Length; j++)
            {
                ObservableName obName = obs[j] as ObservableName;
                if (obName != null && !_bindingTable.ContainsKey(obName.name))
                {
                    ModelBase.ModelBindingInfo bindingInfo = new ModelBase.ModelBindingInfo();
                    bindingInfo.propertyInfo = propertyInfos[i];
                    _bindingTable[obName.name] = bindingInfo;
                }
            }
        }
    }

    public void addObserverBinding<T>(string obName, Action<T> cb)
    {
        if (_bindingTable.ContainsKey(obName))
        {
            _bindingTable[obName].callbacks.Add(cb);
        }
    }

    public void removeObserverBinding<T>(string obName, Action<T> cb)
    {
        if (_bindingTable.ContainsKey(obName))
        {
            ArrayList callbacks = _bindingTable[obName].callbacks;
            callbacks.Remove(cb);
        }
    }

    public void setObservableData<T>(string obName, T data)
    {
        if (!_bindingTable.ContainsKey(obName))
        {
            return;
        }

        ModelBindingInfo bindingInfo = _bindingTable[obName];
        bindingInfo.propertyInfo.SetValue(this, data, null);

        T newData = (T)bindingInfo.propertyInfo.GetValue(this, null);

        //通知
        ArrayList callbacks = _bindingTable[obName].callbacks;
        for (int j = 0; j < callbacks.Count; j++)
        {
            Action<T> cb = (Action<T>)callbacks[j];
            cb(newData);
        }
    }
}