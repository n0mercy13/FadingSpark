using UnityEngine;

namespace Codebase.Services.Factory
{
    public interface IFactory<TBaseObject> where TBaseObject : MonoBehaviour
    {
        TObject Create<TObject>() where TObject : TBaseObject;
    }
}