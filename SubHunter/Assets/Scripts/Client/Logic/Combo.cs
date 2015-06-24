using UnityEngine;
using System;
using System.Collections.Generic;

public class Combo
{
    private const int COMBO_LIM = 10; // Max number of combos that can happen at any given time

    private static Combo instance = new Combo();
    public static Combo I { get { return Combo.instance; } }

    private int nextComboIdx;
    private Dictionary<int, int> combos;

    // Starts a combo, returns the combo index.
    public int StartCombo()
    {
        var comboIdx = this.nextComboIdx;
        this.combos[comboIdx] = 1;
        this.nextComboIdx = (this.nextComboIdx + 1) % Combo.COMBO_LIM;

        return comboIdx;
    }

    // Chains upon an existing combo, returns chain counter.
    public int ChainCombo(int comboIdx)
    {
        System.Diagnostics.Debug.Assert(this.combos.ContainsKey(comboIdx));

        var chainCounter = this.combos[comboIdx];
        chainCounter++;
        this.combos[comboIdx] = chainCounter;

        return chainCounter;
    }

    private Combo()
    {
        this.nextComboIdx = 0;
        this.combos = new Dictionary<int, int>(Combo.COMBO_LIM);
    }
}