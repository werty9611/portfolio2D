﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ObjectController, IBulletShooting
{
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        InitObj(GameManager.ENEMY_ROCKET_MAN);
        state = ObjectTypes.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (state == ObjectTypes.IDLE)
        {
            if (currentTime > character.AttackSpeed)
            {
                state = ObjectTypes.SHOOTING;
                animator.SetTrigger("Attack");
                currentTime = 0;
            }
        }
    }

    IEnumerator IBulletShooting.Shooting()
    {
        animator.speed = 0;
        yield return new WaitForSeconds(0.3f);
        animator.speed = 1;
        GameObject go = GameManager.Instance.LoadBullet(GameManager.BULLET_PATH);
        go.transform.position = bulletInitPos.position;
        go.GetComponent<BulletController>().SetBulletInfo(bullet, 0);
    }

    public void Idle()
    {
        state = ObjectTypes.IDLE;
        animator.SetTrigger("Idle");
    }
}
