using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections.ObjectModel;

//参考URL https://qiita.com/seinosuke/items/e8c7ee2e1f98a76070e2

namespace MyLib.Tag
{
    public class TagHelper
    {
        private static TagManageAssetHolder _tagManageAssetHolder = new();

        private static HashSet<string> _tagCache = null;

        /// <summary>バリデーションチェック用文字</summary>
        private readonly static ReadOnlyCollection<string> INVALID_CHAR = new List<string>() { " ", "/" }.AsReadOnly();

        /// <summary>
        /// 指定されたタグ名をプロジェクトのタグリストに追加
        /// 不正なタグ名が渡されればUnityのコンソールにログ出力
        /// </summary>
        /// <param name="tagName">追加するタグ名</param>
        /// <returns>タグが追加された場合は[true]、既に存在する場合は[false]</returns>
        public static bool AddTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                Debug.LogError("TagHelper: タグ名が空");
                return false;
            }

            if (IsValidTagName(tagName) == false)
            {
                Debug.LogError($"TagHelper: 無効なタグ名: {tagName}");
                return false;
            }

            Object[] asset = _tagManageAssetHolder.TagManageAsset;
            if ((asset == null) || (asset.Length == 0))
            {
                Debug.LogError("TagHelper: タグマネージャーが見つかりません");
                return false;
            }

            if (IsTagExistsInCache(tagName))
            {
                return false;
            }

            SerializedObject so = new SerializedObject(asset[0]);
            SerializedProperty tags = so.FindProperty("tags");

            //// 現状このクラスでしかタグの追加は行わないので二重確認の必要なし
            //// 二重チェック（キャッシュと実際のデータ）
            //if (IsTagExistsInProperty(tags, tagName))
            //{
            //    return false;
            //}

            //タグを追加
            int index = tags.arraySize;
            tags.InsertArrayElementAtIndex(index);
            tags.GetArrayElementAtIndex(index).stringValue = tagName;
            so.ApplyModifiedProperties();
            so.Update();

            // キャッシュにも追加
            AddTagCache(tagName);

            return true;
        }

        /// <summary>
        /// 有効なタグ名か確認 現状はスペースと/があるか確認
        /// </summary>
        /// <param name="tagName">タグ名</param>
        /// <returns></returns>
        private static bool IsValidTagName(string tagName)
        {
            // タグ名のバリデーションロジック
            foreach(var c in INVALID_CHAR)
            {
                if(tagName.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// キャッシュ内にタグが存在しているか
        /// キャッシュが存在していない場合キャッシュの設定から入るので少しコストがかかる
        /// </summary>
        /// <param name="tagName">探すタグ名</param>
        /// <returns></returns>
        private static bool IsTagExistsInCache(string tagName)
        {
            if (_tagCache == null)
            {
                SetupTagCache();
            }
            return _tagCache.Contains(tagName);
        }

        /// <summary>
        /// SerializedProperty内でタグの存在を確認
        /// </summary>
        private static bool IsTagExistsInProperty(SerializedProperty tags, string tagName)
        {
            for (int i = 0; i < tags.arraySize; ++i)
            {
                if (tags.GetArrayElementAtIndex(i).stringValue == tagName)
                {
                    return true;
                }
            }
            return false;
        }

        private static void SetupTagCache()
        {
            var asset = _tagManageAssetHolder.TagManageAsset;
            var so = new SerializedObject(asset[0]);
            SerializedProperty tags = so.FindProperty("tags");

            _tagCache = new HashSet<string>();
            for (int i = 0; i < tags.arraySize; ++i)
            {
                _tagCache.Add(tags.GetArrayElementAtIndex(i).stringValue);
            }
        }

        private static void AddTagCache(string tagName)
        {
            _tagCache.Add(tagName);
        }
    }


    ///UnityのC#バージョンではまだ使えないアクセス指定　C#11らしい
    //file class TagManageAssetHolder
    //{

    //}

    public class TagManageAssetHolder
    {
        public Object[] TagManageAsset { get { return _asset ??= AssetDatabase.LoadAllAssetsAtPath(TAG_MANAGER_PATH); } }

        private const string TAG_MANAGER_PATH = "ProjectSettings/TagManager.asset";
        private Object[] _asset = null;
    }
}