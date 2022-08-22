using UnityEngine;

public static class ProjectLayers {

    public const int PlayerLayerMaskIndex = 8;
    public static string PlayerLayerMaskName { get => LayerMask.LayerToName(PlayerLayerMaskIndex); }
    public static LayerMask PlayerLayerMask { get => LayerMask.GetMask(PlayerLayerMaskName); }

    public const int EnviromentLayerMaskIndex = 9;
    public static string EnviromentLayerMaskName { get => LayerMask.LayerToName(EnviromentLayerMaskIndex); }
    public static LayerMask EnviromentLayerMask { get => LayerMask.GetMask(EnviromentLayerMaskName); }

    public const int EnemysLayerMaskIndex = 10;
    public static string EnemysLayerMaskName { get => LayerMask.LayerToName(EnemysLayerMaskIndex); }
    public static LayerMask EnemysLayerMask { get => LayerMask.GetMask(EnemysLayerMaskName); }

    public const int SoundsLayerMaskIndex = 11;
    public static string SoundsLayerMaskName { get => LayerMask.LayerToName(SoundsLayerMaskIndex); }
    public static LayerMask SoundsLayerMask { get => LayerMask.GetMask(SoundsLayerMaskName); }

    public const int InteractableLayerMaskIndex = 12;
    public static string InteractableLayerMaskName { get => LayerMask.LayerToName(InteractableLayerMaskIndex); }
    public static LayerMask InteractableLayerMask { get => LayerMask.GetMask(InteractableLayerMaskName); }

    public const int UndetectableLayerMaskIndex = 13;
    public static string UndetectableLayerMaskName { get => LayerMask.LayerToName(UndetectableLayerMaskIndex); }
    public static LayerMask UndetectableLayerMask { get => LayerMask.GetMask(UndetectableLayerMaskName); }

}
