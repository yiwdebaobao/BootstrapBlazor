﻿using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Modal 弹窗组件
    /// </summary>
    public abstract class ModalBase : IdComponentBase
    {
        /// <summary>
        /// 获得 后台关闭弹窗设置
        /// </summary>
        protected string? Backdrop => IsBackdrop ? null : "static";

        /// <summary>
        /// 获得 ModalDialog 集合
        /// </summary>
        protected List<ModalDialogBase> Dialogs { get; private set; } = new List<ModalDialogBase>();

        /// <summary>
        /// 获得/设置 是否后台关闭弹窗
        /// </summary>
        [Parameter] public bool IsBackdrop { get; set; }

        /// <summary>
        /// 获得/设置 子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 弹窗状态切换方法
        /// </summary>
        public void Toggle()
        {
            Dialogs.ForEach(d => d.IsShown = Dialogs.IndexOf(d) == 0);
            if (!string.IsNullOrEmpty(Id)) JSRuntime.InvokeRun(Id, "modal", "toggle");
        }

        /// <summary>
        /// 添加对话窗方法
        /// </summary>
        /// <param name="dialog"></param>
        public void AddDialogs(ModalDialogBase dialog)
        {
            if (!Dialogs.Any()) dialog.IsShown = true;
            Dialogs.Add(dialog);
        }

        /// <summary>
        /// 显示指定对话框方法
        /// </summary>
        /// <param name="dialog"></param>
        public void ShowDialog(ModalDialogBase dialog)
        {
            Dialogs.ForEach(d => d.IsShown = d == dialog);
            StateHasChanged();
        }
    }
}
