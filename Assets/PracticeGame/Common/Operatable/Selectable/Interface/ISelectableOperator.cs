using UnityEngine;

namespace PracticeGame
{
    /// <summary>
    /// 選択状態を変更するオブジェクトは自身が選択可能なオブジェクトとは限らないのでISelectableとは切り分け
    /// </summary>
    public interface ISelectableOperator
    {
        public void SetSelectable(bool isSelectable);
    }
}