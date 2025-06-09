using UnityEngine;

namespace PracticeGame
{
    /// <summary>
    /// 入力インターフェース種別
    /// </summary>
    public enum InputInterfaceType
    {
        /// <summary> 未定義 </summary>
        None,
        /// <summary> ニンテンドースイッチ </summary>
        NintendoSwitch,
        /// <summary> Xboxコントローラー </summary>
        XboxController,
        /// <summary> その他 </summary>
        Other,
    };
    /// <summary>
    /// キー
    /// </summary>
    public enum Key
    {
        /// <summary> 決定 </summary>
        Submit,
        /// <summary> キャンセル </summary>
        Cancel,
        /// <summary> カーソル・上 </summary>
        CursorUp,
        /// <summary> カーソル・下 </summary>
        CursorDown,
        /// <summary> カーソル・左 </summary>
        CursorLeft,
        /// <summary> カーソル・右 </summary>
        CursorRight,

    };
}