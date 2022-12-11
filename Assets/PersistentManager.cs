using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : SingletonPersistent<PersistentManager>
{

    [SerializeField] private CharacterData charSelected;                                        //Character
    public CharacterData CharSelected { get => charSelected; set => charSelected = value; }    // Selected
}
