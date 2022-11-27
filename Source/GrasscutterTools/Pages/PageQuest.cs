﻿/**
 *  Grasscutter Tools
 *  Copyright (C) 2022 jie65535
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License as published
 *  by the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 *
 **/

using System;
using System.Linq;
using System.Windows.Forms;

using GrasscutterTools.Game;

namespace GrasscutterTools.Pages
{
    internal partial class PageQuest : BasePage
    {
        public PageQuest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化任务列表
        /// </summary>
        public override void OnLoad()
        {
            QuestFilterChanged(null, EventArgs.Empty);
        }

        /// <summary>
        /// 任务列表过滤器文本改变时触发
        /// </summary>
        private void QuestFilterChanged(object sender, EventArgs e)
        {
            ListQuest.BeginUpdate();
            ListQuest.Items.Clear();
            ListQuest.Items.AddRange(GameData.Quests.Lines.Where(l =>
            {
                if (!ChkQuestFilterHIDDEN.Checked && l.Contains((string)ChkQuestFilterHIDDEN.Tag))
                    return false;
                if (!ChkQuestFilterUNRELEASED.Checked && l.Contains((string)ChkQuestFilterUNRELEASED.Tag))
                    return false;
                if (!ChkQuestFilterTEST.Checked && l.Contains((string)ChkQuestFilterTEST.Tag))
                    return false;
                if (!string.IsNullOrEmpty(TxtQuestFilter.Text))
                    return l.Contains(TxtQuestFilter.Text);
                return true;
            }).ToArray());
            ListQuest.EndUpdate();
        }

        /// <summary>
        /// 任务相关按钮点击时触发（Tag带子命令）
        /// </summary>
        private void QuestButsClicked(object sender, EventArgs e)
        {
            if (ListQuest.SelectedIndex == -1)
                return;
            var item = ListQuest.SelectedItem as string;
            var id = ItemMap.ToId(item);
            SetCommand("/quest", $"{(sender as Button).Tag} {id}");
        }
    }
}