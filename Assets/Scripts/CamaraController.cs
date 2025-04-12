using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CamaraController : CinemachineExtension
{
    private float maxX;
    [SerializeField] private Transform player;

    protected override void Awake()
    {
        base.Awake();
        if (player != null)
        {
            maxX = player.position.x;
        }
    }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            float currentX = state.RawPosition.x;

            // Guardamos la posición máxima
            if (currentX > maxX)
                maxX = currentX;

            // Limitamos para que no vuelva atrás
            Vector3 pos = state.RawPosition;
            pos.x = Mathf.Max(pos.x, maxX);
            state.RawPosition = pos;
        }
    }
}
