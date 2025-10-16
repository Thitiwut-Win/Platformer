using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimReaper : Boss
{
    // public float dashSpeedX;
    // public float dashSpeedY;
    public float skillCooldown;
    public float skillTime;
    public float summonTime;
    private Vector3 spawnPosition = new Vector3(138, 38, 0);
    private bool isDashing = false;
    private bool isSkilling = false;
    private bool isSummoning = false;
    private int prevSkill = 2;
    private List<string> Skills = new List<string>()
    {
        "DecreaseVision",
        "Shoot",
        "Summon"
    };
    [SerializeField]
    private Enemy summonPrefab;
    [SerializeField]
    private Fireball fireballPrefab;
    [SerializeField]
    private AudioClip skillAudio;
    public override void Update()
    {
        base.Update();
        if (isAggro)
        {
            if (isSkilling || isSummoning) MoveToTarget(0, 0);
            else if (!isDashing) MoveToTarget(speedX, speedY);
            // else MoveToTarget(dashSpeedX, dashSpeedY);
            if (hasCooldown)
            {
                hasCooldown = false;
                int skill = Random.Range(0, 3);
                while (skill == prevSkill)
                {
                    skill = Random.Range(0, 3);
                }
                prevSkill = skill;
                PlaySkillAudio();
                Invoke(Skills[skill], 0);
                if (skill <= 1)
                {
                    isSkilling = true;
                    StartCoroutine(Skilling());
                }
                else
                {
                    isSummoning = true;
                    StartCoroutine(Summoning());
                }
                StartCoroutine(Cooldown());
            }
            // if (isAttacking)
            // {
            //     float dist = Vector3.Distance(transform.position, target.transform.position);
            //     if (dist >= 5 && !isDashing) isDashing = true;
            //     if (dist < 5 && isDashing) isDashing = false;
            // }
        }
        else
        {
            transform.position = spawnPosition;
        }
    }
    public override void Attack()
    {
        base.Attack();
    }
    private void Summon()
    {
        isSummoning = true;
        List<Vector2> direc = new List<Vector2>()
        {
            new Vector2(-2, 2),
            new Vector2(2, 2)
        };
        for (int i = 0; i < 2 - LevelManager.Instance.summonCount; i++)
        {
            Vector3 dist = new Vector3(direc[i].x, direc[i].y, 0);
            Enemy summon = Instantiate(summonPrefab, new Vector3(Mathf.Min(transform.position.x + dist.x, 140), Mathf.Max(transform.position.y + dist.y, 37), 0), Quaternion.identity);
        }
        LevelManager.Instance.summonCount = 2;
    }
    private void DecreaseVision()
    {
        FollowPlayer.Instance.SetCameraSize(3);
        StartCoroutine(Debuff());
        Teleport();
    }
    private void Teleport()
    {
        float dist = Random.Range(4, 6);
        List<int> direc = new List<int> { -1, 0, 1 };
        int direcx = Random.Range(0, 3);
        int direcy = Random.Range(0, 3);
        while (direcx == direcy)
        {
            direcy = Random.Range(0, 3);
        }
        transform.position = new Vector3(target.transform.position.x + dist * direc[direcx], target.transform.position.y + dist * direc[direcy], 0);
    }
    private void Shoot()
    {
        Fireball fireball = Instantiate(fireballPrefab, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        fireball.SetTarget(target.transform.position);
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(skillCooldown);
        hasCooldown = true;
    }
    private IEnumerator Skilling()
    {
        yield return new WaitForSeconds(skillTime);
        isSkilling = false;
    }
    private IEnumerator Summoning()
    {
        yield return new WaitForSeconds(summonTime);
        isSummoning = false;
    }
    private IEnumerator Debuff()
    {
        yield return new WaitForSeconds(6);
        FollowPlayer.Instance.SetCameraSize(5);
    }
    private void PlaySkillAudio()
    {
        audioSource.volume = LevelManager.Instance.GetVolume()/300f;
        audioSource.PlayOneShot(skillAudio);
    }
    public override void SetAnimatorParameter()
    {
        base.SetAnimatorParameter();
        animator.SetBool("IsSkilling", isSkilling);
        animator.SetBool("IsSummoning", isSummoning);
    }
}