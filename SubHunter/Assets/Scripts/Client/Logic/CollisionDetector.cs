using UnityEngine;
using System;
using System.Collections.Generic;

public class CollisionDetector
{
    private const int COMBO_LIM = 10; // Max number of combos that can happen at any given time

    private static CollisionDetector instance = new CollisionDetector();
    public static CollisionDetector I { get { return CollisionDetector.instance; } }

    private int nextComboIdx;
    private Dictionary<int, int> combos;

    // Starts a combo, returns the combo index.
    public int StartCombo()
    {
        var comboIdx = this.nextComboIdx;
        this.combos[comboIdx] = 1;
        this.nextComboIdx = (this.nextComboIdx + 1) % CollisionDetector.COMBO_LIM;

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

    private CollisionDetector()
    {
        this.nextComboIdx = 0;
        this.combos = new Dictionary<int, int>(CollisionDetector.COMBO_LIM);
    }
}