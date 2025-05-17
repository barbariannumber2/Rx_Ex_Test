using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RxTest
{
    public class RxTestView : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        [SerializeField]
        private Button _button;

        public Button Button => _button;

        public void SetValueText(int value)
        {
            _text.text = value.ToString();
            Debug.Log("テキスト更新");
        }

        private void OnDestroy()
        {
            //このコンポーネントと一緒にボタンが消えるなら必要はない
            //このコンポーネントと別のオブジェクトとしてボタンがあるならこれがないとモデルのカウント関数は呼ばれるまま
            Button?.onClick?.RemoveAllListeners();
        }
    }
}