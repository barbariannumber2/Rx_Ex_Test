using PracticeGame;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SelectInstaller : MonoInstaller
{
    [SerializeField]
    private IView _selectView;

    public override void InstallBindings()
    {

    }
}