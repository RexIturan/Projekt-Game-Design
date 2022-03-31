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

						if( tileEffect.GetActive() ) {
								// show preview of time to live if active but not eternal
								if ( !tileEffect.GetEternal() )
										textMesh.text = tileEffect.GetTimeToLive().ToString();
								else
										HidePreview();
						}
						// else show time until activation
						else {
								textMesh.text = tileEffect.GetTimeUntilActivation().ToString();
						}
				}

				private void HidePreview() {
						canvas.SetActive(false);
				}
		}
}
