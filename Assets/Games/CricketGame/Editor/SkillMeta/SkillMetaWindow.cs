using System.Collections.Generic;
using Games.CricketGame.Code.Pokemon.Enum;
using Games.CricketGame.Code.Pokemon.Skill;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Games.CricketGame.Editor.SkillMeta
{
    public class SkillMetaWindow : EditorWindow
    {
        private static VisualTreeAsset _visualTree;

        [MenuItem("Window/Create/SkillMeta")]
        public static void ShowExample()
        {
            SkillMetaWindow wnd = GetWindow<SkillMetaWindow>();
            wnd.titleContent = new GUIContent("Learning");
        }

        #region UI Element

        private static Button clear;
        private static Button read;
        private static Button save;
        private static Button add;

        private static EnumField skillEnum;
        private static EnumField priorityEnum;
        private static TextField skillName;
        private static FloatField hitRate;
        private static IntegerField power;
        private static ListView _listView;

        #endregion


        private static List<SkillEnum> _metas = new();

        private static void _query(VisualElement root)
        {
            clear = root.Q<Button>("clear");
            read = root.Q<Button>("read");

            save = root.Q<Button>("save");
            add = root.Q<Button>("add");

            _listView = root.Q<ListView>("ListView");

            skillEnum = root.Q<EnumField>("skillEnum");
            priorityEnum = root.Q<EnumField>("skillPriority");

            power = root.Q<IntegerField>("power");
            hitRate = root.Q<FloatField>("hitRate");
            skillName = root.Q<TextField>("skillName");

            skillEnum.Init(SkillEnum.None);
            priorityEnum.Init(PriorityEnum.正常手);
            clear.clicked += _clear;
            add.clicked += _add;
            read.clicked += _read;
            save.clicked += _save;
            _listView.fixedItemHeight = 20;
            _listView.itemsSource = _metas;
            _listView.makeItem = _makeItem;
            _listView.bindItem = _bindItem;
            _listView.onSelectionChange += _on_selected_change;
        }

        private static void _on_selected_change(IEnumerable<object> objs)
        {
            foreach (var obj in objs)
            {
                if (obj is not SkillEnum)
                {
                    Debug.Log("点击了其他地方");
                    return;
                }

                var @enum = (SkillEnum)obj;
                var meta = Code.Pokemon.Skill.SkillMeta.Find(@enum);
                skillEnum.value = meta.skillEnum;
                priorityEnum.value = meta.priority;
                power.value = meta.power;
                hitRate.value = meta.hitRate;
                skillName.value = meta.name;
            }
        }


        public void CreateGUI()
        {
            if (_visualTree == null)
            {
                _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Games/CricketGame/Editor/SkillMeta/SkillMetaWindow.uxml");
            }

            _visualTree.CloneTree(rootVisualElement);

            _query(rootVisualElement);
        }

        private static void _bindItem(VisualElement arg1, int arg2)
        {
            Label label = arg1 as Label;
            SkillEnum @enum = _metas[arg2];
            if (label != null) label.text = @enum.ToString();
        }

        private static VisualElement _makeItem()
        {
            var label = new Label();
            label.style.marginLeft = 5;
            return label;
        }


        private static void _save()
        {
            Code.Pokemon.Skill.SkillMeta.Save();
        }

        private static SkillEnum _create_meta()
        {
            string _name = skillName.value;
            SkillEnum _enum = skillEnum.value as SkillEnum? ?? SkillEnum.None;
            int _power = power.value;
            float _hitRate = hitRate.value;
            PriorityEnum _priority = priorityEnum.value as PriorityEnum? ?? PriorityEnum.后手;
            var meta = new Code.Pokemon.Skill.SkillMeta(_name, _enum, _power, _hitRate, _priority, true);
            Code.Pokemon.Skill.SkillMeta.Add(meta);
            return _enum;
        }

        private static void _add()
        {
            var _enum = _create_meta();
            if (!_metas.Contains(_enum))
            {
                _metas.Add(_enum);
            }
            else
            {
                _metas.Remove(_enum);
                _metas.Add(_enum);
            }

            _read();
        }

        private static void _clear()
        {
            Code.Pokemon.Skill.SkillMeta.Clear();
            _read();
        }

        private static void _read()
        {
            _metas = Code.Pokemon.Skill.SkillMeta.GetSkillEnumList();
            _listView.itemsSource = _metas;
        }
    }
}