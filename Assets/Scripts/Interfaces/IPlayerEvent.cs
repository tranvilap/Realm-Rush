﻿using UnityEngine.EventSystems;

public interface IPlayerEvent : IEventSystemHandler
{
    void OnPlayerHurt();
}