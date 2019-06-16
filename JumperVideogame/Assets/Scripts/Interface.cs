using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    [SerializeField] Sprite interface_v3_t2;
    [SerializeField] Sprite interface_v3_t1;
    [SerializeField] Sprite interface_v3_t0;
    [SerializeField] Sprite interface_v2_t2;
    [SerializeField] Sprite interface_v2_t1;
    [SerializeField] Sprite interface_v2_t0;
    [SerializeField] Sprite interface_v1_t2;
    [SerializeField] Sprite interface_v1_t1;
    [SerializeField] Sprite interface_v1_t0;

    [SerializeField] SpriteRenderer interfaceRenderer;


    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        interfaceRenderer.sprite = interface_v3_t2;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentHP == 3 && player.teleportsAvailable == 2) interfaceRenderer.sprite = interface_v3_t2;
        if (player.currentHP == 3 && player.teleportsAvailable == 1) interfaceRenderer.sprite = interface_v3_t1;
        if (player.currentHP == 3 && player.teleportsAvailable == 0) interfaceRenderer.sprite = interface_v3_t0;
        if (player.currentHP == 2 && player.teleportsAvailable == 2) interfaceRenderer.sprite = interface_v2_t2;
        if (player.currentHP == 2 && player.teleportsAvailable == 1) interfaceRenderer.sprite = interface_v2_t1;
        if (player.currentHP == 2 && player.teleportsAvailable == 0) interfaceRenderer.sprite = interface_v2_t0;
        if (player.currentHP == 1 && player.teleportsAvailable == 2) interfaceRenderer.sprite = interface_v1_t2;
        if (player.currentHP == 1 && player.teleportsAvailable == 1) interfaceRenderer.sprite = interface_v1_t1;
        if (player.currentHP == 1 && player.teleportsAvailable == 0) interfaceRenderer.sprite = interface_v1_t0;
    }
}
