using UnityEngine;
using UnityEngine.AddressableAssets;
using static UnityEngine.AddressableAssets.Addressables;

namespace _Testing.Addressables {
    public class AddressableTester : MonoBehaviour {
        [SerializeField] private AssetReference assetReference;

        private void Start() {
            assetReference.LoadAssetAsync<TextAsset>().Completed += handle => {
                Debug.Log(handle.Result.text);
                Release(handle);
            };
        }
    }
}
