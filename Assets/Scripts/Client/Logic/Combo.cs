using UnityEngine;
using System;
using System.Collections.Generic;
using Foundation;

public class Combo
{
    static Combo()
    {
        Combo.nextComboIdx = 0;
        Combo.combos = new Dictionary<int, int>(Combo.COMBO_LIM);
    }

    private const int COMBO_LIM = 10; // Max number of combos that can happen at any given time

    private static int nextComboIdx;
    private static Dictionary<int, int> combos;

    // Starts a combo, returns the combo index.
    public static int StartCombo()
    {
        var comboIdx = Combo.nextComboIdx;
        Combo.combos[comboIdx] = 0;
        Combo.nextComboIdx = (Combo.nextComboIdx + 1) % Combo.COMBO_LIM;

        return comboIdx;
    }

    // Chains upon an existing combo, returns chain counter.
    public static int ChainCombo(int comboIdx)
    {
        Log.Assert(Combo.combos.ContainsKey(comboIdx));

        var chainCounter = Combo.combos[comboIdx];
        chainCounter++;
        Combo.combos[comboIdx] = chainCounter;

        return chainCounter;
    }

}