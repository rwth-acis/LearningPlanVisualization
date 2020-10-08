﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringDropdownMenu : DropdownMenu<StringData, StringListViewItem>
{
    [SerializeField] private StringDataDisplay stringSelectedItemDisplay;
    [SerializeField] private StringListView stringListViewController;

    protected override void Awake()
    {
        // workarounds because the generic types are not recognized by Unity's inspector
        selectedItemDisplay = stringSelectedItemDisplay;
        itemController = stringListViewController;
        base.Awake();
    }
}