using System;
using UnityEngine;

public static class EventManager
{
    public static Action ActivateObj;
    public static Action DeactivateObj;
    public static Action<int> Interactable;
    public static Action changingRoom;

    public static Action RestartDominance;

    public static Action<SDialogue> StartDialogue;
    public static Action DeactivateThings;
    public static Action ReactivateThings;
    public static Action<GameObject> ObjAssignation;
    public static Action<LV> SwapLv;
    public static Action CircleActivator;
    public static Action CirclePuzzleStart;
    public static Action CirclePuzzleFinish;
    public static Action<int> MoveToNextRoom;
    public static Action ActivateVision;
    public static Action DeactivationVision;
    public static Action DeactivateMask;
    public static Action EndDialogueWitness;
    public static Action isThisWitness;
    public static Action DominanceEnd;
    public static Action DominanceStart;
    public static Action ChangeLayer;
    public static Action<GameObject, float> CheckRotationEnigma;
    public static Action BloodRitual;
    public static Action collectObj;
    public static Action SolveDomination;
    public static Action MaskOff;
}
