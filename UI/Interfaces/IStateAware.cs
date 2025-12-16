using System;
using AtanUtils.UI.Controls.Addon;
using UnityEngine;

namespace AtanUtils.UI.Interfaces
{
    public interface IStateAware
    {
        public void UpdateState(StateInfo stateInfo);
        public Action OnStateChange { get; set; }
    }
}