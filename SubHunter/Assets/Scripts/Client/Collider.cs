using UnityEngine;
using System;
using System.Collections.Generic;

public class Collider
{
    private const int COMBO_LIM = 10; // Max number of combos that can happen at any given time

    private static Collider instance = new Collider();
    public static Collider I { get { return Collider.instance; } }

    private int nextComboIdx;
    private Dictionary<int, int> combos;

    // Starts a combo, returns the combo index.
    public int StartCombo()
    {
        var comboIdx = Collider.nextComboIdx;
        Collider.combos[comboIdx] = 1;
        Collider.nextComboIdx = (Collider.nextComboIdx + 1) % COMBO_LIM;

        return comboIdx;
    }

    // Chains upon an existing combo, returns chain counter.
    public int ChainCombo(int comboIdx)
    {
        System.Diagnostics.Debug.Assert(Collider.combos.ContainsKey(comboIdx));

        var chainCounter = Collider.combos[comboIdx];
        chainCounter++;
        Collider.combos[comboIdx] = chainCounter;

        return chainCounter;
    }

    private Collider()
    {
        this.nextComboIdx = 0;
        this.combos = new Dictionary<int, int>(Collider.COMBO_LIM);
    }
}