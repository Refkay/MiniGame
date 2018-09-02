using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MiniGame
{
    public class SelectLevelView : BaseUI
    {
        public BaseGrid baseGrid;
        public List<SelectItemMessage> messages;

        protected override void UpdateView()
        {
            base.UpdateView();
            if (messages != null)
                baseGrid.source = messages.ToArray();
        }

        public void SetMessages(List<SelectItemMessage> data)
        {
            messages = data;
            InvalidView();
        }
    }
}

