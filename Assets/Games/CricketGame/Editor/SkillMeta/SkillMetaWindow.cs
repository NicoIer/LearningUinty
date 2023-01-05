using System.Collections.Generic;
using Games.CricketGame.Manager.Code.Manager;
using Games.CricketGame.Manager.Code.Pokemon.Skill;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Games.CricketGame.Manager.Editor.SkillMeta
{
    public class SkillMetaWindow : EditorWindow
    {
        private static VisualTreeAsset _visualTree;
        private static bool _readed = false;

        private static readonly string _template =
            "Games.CricketGame.Code.Pokemon.Skill.Effects.{0}, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

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
        private static Button reload;

        private static EnumField _skillEnum;
        private static EnumField _priorityEnum;
        private static TextField _skillName;
        private static FloatField _hitRate;
        private static IntegerField _power;
        private static ListView _listView;
        private static VisualElement _bottom;

        #endregion


        private static List<SkillEnum> _skillEnumList = new();

        private static void _query(VisualElement root)
        {
            clear = root.Q<Button>("clear");
            read = root.Q<Button>("read");
            reload = root.Q<Button>("reload");
            save = root.Q<Button>("save");
            add = root.Q<Button>("add");
            _bottom = root.Q<VisualElement>("bottom");
            _listView = root.Q<ListView>("ListView");

            _skillEnum = root.Q<EnumField>("skillEnum");
            
            _priorityEnum = root.Q<EnumField>("skillPriority");

            _power = root.Q<IntegerField>("power");
            _hitRate = root.Q<FloatField>("hitRate");
            _skillName = root.Q<TextField>("skillName");

            _skillEnum.Init(SkillEnum.None);
            _priorityEnum.Init(PriorityEnum.正常手);
            clear.clicked += _clear;
            add.clicked += _add;
            read.clicked += _read;
            save.clicked += _save;
            reload.clicked += _reload;
            _listView.fixedItemHeight = 20;
            _listView.itemsSource = _skillEnumList;
            _listView.makeItem = _makeItem;
            _listView.bindItem = _bindItem;
            _listView.onSelectionChange += _on_selected_change;
        }


        public void CreateGUI()
        {
            if (_visualTree == null)
            {
                _visualTree =
                    AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                        "Assets/Games/CricketGame/Editor/SkillMeta/SkillMetaWindow.uxml");
            }

            _visualTree.CloneTree(rootVisualElement);

            _query(rootVisualElement);
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
                _skillEnum.value = meta.skillEnum;
                _priorityEnum.value = meta.priority;
                _power.value = meta.power;
                _hitRate.value = meta.hitRate;
                _skillName.value = @enum.ToString();
            }
        }

        private static void _bindItem(VisualElement arg1, int arg2)
        {
            Label label = arg1 as Label;
            SkillEnum @enum = _skillEnumList[arg2];
            if (label != null) label.text = @enum.ToString();
        }

        private static VisualElement _makeItem()
        {
            var label = new Label();
            label.style.marginLeft = 5;
            return label;
        }


        private static SkillEnum _create_meta()
        {
            SkillEnum _enum = _skillEnum.value as SkillEnum? ?? SkillEnum.None;
            _skillName.value = _enum.ToString();
            string _name = _skillName.value;
            int powerValue = _power.value;
            float hitRateValue = _hitRate.value;
            PriorityEnum _priority = _priorityEnum.value as PriorityEnum? ?? PriorityEnum.后手;
            var meta = new Code.Pokemon.Skill.SkillMeta(_name, _enum, powerValue, hitRateValue, _priority, true);
            Code.Pokemon.Skill.SkillMeta.Add(meta);
            return _enum;
        }

        #region Button Event

        private static void _reload()
        {
            _skillEnumList = Code.Pokemon.Skill.SkillMeta.GetSkillEnumList(true);
            var map = Skill.GetEffectMap(true);
            foreach (SkillEnum @enum in _skillEnumList)
            {
                if (!map.ContainsKey(@enum))
                {
                    var type = string.Format(_template, @enum);
                    Skill.AddEffectByString(@enum, type, true);
                }
            }
            _bottom.Add(new HelpBox("重新从磁盘加载数据成功",HelpBoxMessageType.Info));
            _read();
        }
        private static void _save()
        {
            if (!_check_read())
            {
                return;
            }

            Skill.Save();
            Code.Pokemon.Skill.SkillMeta.Save();
            _bottom.Add(new HelpBox("数据保存完成",HelpBoxMessageType.Info));
        }

        private static void _add()
        {
            if (!_check_read())
            {
                return;
            }

            SkillEnum skill_enum = _create_meta();
            if (!_skillEnumList.Contains(skill_enum))
            {
                _skillEnumList.Add(skill_enum);
            }
            else
            {
                _skillEnumList.Remove(skill_enum);
                _skillEnumList.Add(skill_enum);
            }

            var type = string.Format(_template, skill_enum);
            Skill.AddEffectByString(skill_enum, type, true);
            _bottom.Add(new HelpBox("数据添加/更新完成",HelpBoxMessageType.Info));
            _read();
        }

        private static void _clear()
        {
            if (!_check_read())
            {
                return;
            }

            Code.Pokemon.Skill.SkillMeta.Clear();
            Skill.ClearEffect();
            _bottom.Add(new HelpBox("清空完成现在点击保存按钮则之前的数据都会消失,请谨慎保存!!",HelpBoxMessageType.Warning));
            _read();
        }

        private static void _read()
        {
            _skillEnumList = Code.Pokemon.Skill.SkillMeta.GetSkillEnumList();
            if (!Skill.Initilized)
            {
                var map = Skill.GetEffectMap();
                foreach (SkillEnum @enum in _skillEnumList)
                {
                    if (!map.ContainsKey(@enum))
                    {
                        var type = string.Format(_template, @enum);
                        Skill.AddEffectByString(@enum, type, true);
                    }
                }
            }

            _listView.itemsSource = _skillEnumList;
            _readed = true;
            _bottom.Add(new HelpBox("列表更新完成",HelpBoxMessageType.Info));
        }

        private static bool _check_read()
        {
            _bottom.Clear();
            if (!_readed)
            {
                _bottom.Add(new HelpBox("请先读取已有数据",HelpBoxMessageType.Warning));
                return false;
            }

            return true;
        }
        #endregion
    }
}