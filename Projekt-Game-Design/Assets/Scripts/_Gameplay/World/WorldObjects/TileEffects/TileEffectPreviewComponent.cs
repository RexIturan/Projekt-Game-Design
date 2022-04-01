using Characters;
using GDP01.TileEffects;
using Input;
using UnityEngine;
using TMPro;

namespace GDP01
{
    public class TileEffectPreviewComponent : MonoBehaviour
    {
				[SerializeField] private TileEffectController tileEffect;
				[SerializeField] private InputCache inputCache;
				[SerializeField] private TextMeshProUGUI textMesh;
				[SerializeField] private GameObject canvas;
				private void Update() {
						if ( inputCache.cursor.abovePos.gridPos.Equals(tileEffect.GetComponent<GridTransform>().gridPosition) )
								ShowPreview();
						else
								HidePreview();
				}

				private void ShowPreview() {
						canvas.SetActive(true);

						if ( !tileEffect.GetActive() && !tileEffect.GetDestroy() ) {
								textMesh.text = tileEffect.GetTimeUntilActivation().ToString();
						}
						else
								HidePreview();
				}

				private void HidePreview() {
						canvas.SetActive(false);
				}
		}
}
